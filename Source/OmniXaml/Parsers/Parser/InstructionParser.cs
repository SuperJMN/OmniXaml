namespace OmniXaml.Parsers.Parser
{
    using System.Collections.Generic;
    using MarkupExtensions;
    using ProtoParser;
    using Sprache;
    using Typing;
    using ParseException = OmniXaml.ParseException;

    public class InstructionParser : IInstructionParser
    {
        private readonly IRuntimeTypeSource typeSource;
        private IEnumerator<ProtoInstruction> instructionStream;

        public InstructionParser(IRuntimeTypeSource typeSource)
        {
            this.typeSource = typeSource;
        }

        private bool EndOfStream { get; set; }

        private bool CurrentNodeIsElement => CurrentNodeType == NodeType.Element || CurrentNodeType == NodeType.EmptyElement;

        private NodeType CurrentNodeType => instructionStream.Current?.NodeType ?? NodeType.None;

        private bool IsNestedPropertyImplicit => CurrentNodeType != NodeType.PropertyElement && CurrentNodeType != NodeType.EmptyPropertyElement &&
                                                 CurrentNodeType != NodeType.EndTag;

        private bool CurrentNodeIsText => CurrentNodeType == NodeType.Text;

        private ProtoInstruction Current => instructionStream.Current;
        private string CurrentText => instructionStream.Current.Text;
        private string CurrentPropertyText => Current.PropertyAttributeText;
        private MemberBase CurrentMember => Current.PropertyAttribute;

        public IEnumerable<Instruction> Parse(IEnumerable<ProtoInstruction> protoNodes)
        {
            instructionStream = protoNodes.GetEnumerator();
            SetNextInstruction();

            foreach (var prefix in ParsePrefixDefinitions())
            {
                yield return prefix;
            }
            foreach (var element in ParseElements())
            {
                yield return element;
            }
        }

        private IEnumerable<Instruction> ParseElements()
        {
            if (CurrentNodeIsText)
            {
                yield return Inject.Value(CurrentText);
            }

            while (CurrentNodeIsElement && !EndOfStream)
            {
                switch (instructionStream.Current.NodeType)
                {
                    case NodeType.Element:
                        foreach (var instruction in ParseNonEmptyElement()) { yield return instruction; }
                        break;
                    case NodeType.EmptyElement:
                        foreach (var instruction in ParseEmptyElement()) { yield return instruction; }
                        break;
                }

                // There may be text nodes after each element. Skip all of them.
                SkipTextNodes();
            }
        }

        private void SkipTextNodes()
        {
            while (CurrentNodeType == NodeType.Text)
            {
                SetNextInstruction();
            }
        }

        private IEnumerable<Instruction> ParseEmptyElement()
        {
            yield return Inject.StartOfObject(instructionStream.Current.XamlType);

            SetNextInstruction();

            foreach (var member in ParseMembersOfObject())
            {
                yield return member;
            }

            yield return Inject.EndOfObject();

            if (CurrentNodeType == NodeType.Text)
            {
                SetNextInstruction();
            }
        }

        private IEnumerable<Instruction> ParseNonEmptyElement()
        {
            yield return Inject.StartOfObject(instructionStream.Current.XamlType);
            var parentType = instructionStream.Current.XamlType;

            if (parentType.NeedsConstructionParameters)
            {
                foreach (var instruction in InjectNodesForTypeThatRequiresInitialization()) { yield return instruction; }
            }
            else
            {
                SetNextInstruction();

                foreach (var instruction in ParseMembersOfObject()) { yield return instruction; }

                if (parentType.IsCollection)
                {
                    foreach (var instruction in ParseItemsOfCollection()) { yield return instruction; }
                }
                else
                {
                    foreach (var instruction in ParseContentPropertyIfAny(parentType)) { yield return instruction; }
                }

                SkipTextNodes();

                foreach (var instruction in ParseNestedProperties(parentType)) { yield return instruction; }
            }

            yield return Inject.EndOfObject();
            ReadEndTag();
        }

        private IEnumerable<Instruction> ParseItemsOfCollection()
        {
            yield return Inject.Items();
            foreach (var xamlInstruction in ParseElements()) { yield return xamlInstruction; }
            yield return Inject.EndOfMember();
        }

        private IEnumerable<Instruction> InjectNodesForTypeThatRequiresInitialization()
        {
            SetNextInstruction();

            foreach (var member in ParseMembersOfObject())
            {
                yield return member;
            }

            yield return Inject.Initialization();
            yield return Inject.Value(CurrentText);
            yield return Inject.EndOfMember();
        }

        private void ReadEndTag()
        {
            SkipTextNodes();

            if (CurrentNodeType != NodeType.EndTag)
            {
                throw new ParseException("Expected End Tag");
            }

            SetNextInstruction();
        }

        private IEnumerable<Instruction> ParseNestedProperties(XamlType parentType)
        {
            while (CurrentNodeType == NodeType.PropertyElement || CurrentNodeType == NodeType.EmptyPropertyElement)
            {
                var member = instructionStream.Current.PropertyElement;
                if (member.XamlType.IsCollection)
                {
                    SetNextInstruction();
                    foreach (var instruction in ParseCollectionInsideThisProperty(member)) { yield return instruction; }
                }
                else
                {
                    foreach (var instruction in ParseNestedProperty(member)) { yield return instruction; }
                }

                // After and EndTag, there could be a ContentProperty! so we consider parsing it.
                if (CurrentNodeType == NodeType.EndTag)
                {
                    SetNextInstruction();
                    foreach (var instruction in ParseContentPropertyIfAny(parentType)) { yield return instruction; }
                }
            }
        }

        private IEnumerable<Instruction> ParseContentPropertyIfAny(XamlType parentType)
        {
            if (IsNestedPropertyImplicit)
            {
                var contentProperty = parentType.ContentProperty;

                if (contentProperty == null)
                {
                    // There is no content property, so try to use a TypeConverter to supply the value.
                    yield return Inject.Initialization();
                    yield return Inject.Value(CurrentText);
                    yield return Inject.EndOfMember();
                }
                else if (contentProperty.XamlType.IsCollection)
                {
                    foreach (var instruction in ParseCollectionInsideThisProperty(contentProperty)) { yield return instruction; }
                }
                else
                {
                    yield return Inject.StartOfMember(contentProperty);
                    foreach (var instruction in ParseElements()) { yield return instruction; }
                    yield return Inject.EndOfMember();
                }
            }
        }

        private void SetNextInstruction()
        {
            if (EndOfStream)
            {
                throw new ParseException("The end of the stream has already been reached!");
            }

            EndOfStream = !instructionStream.MoveNext();
        }

        private IEnumerable<Instruction> ParseCollectionInsideThisProperty(MemberBase member)
        {
            yield return Inject.StartOfMember(member);

            if (IsBeginingOfImplicitCollection)
            {
                yield return Inject.GetObject();
                yield return Inject.Items();

                foreach (var instruction in ParseElements())
                {
                    yield return instruction;
                }

                yield return Inject.EndOfMember();
                yield return Inject.EndOfObject();

            }
            else
            {
                foreach (var xamlInstruction in ParseNonEmptyElement()) { yield return xamlInstruction; }
            }

            yield return Inject.EndOfMember();
        }

        private bool IsBeginingOfImplicitCollection => Current.XamlType == null || !Current.XamlType.IsCollection;

        private IEnumerable<Instruction> ParseNestedProperty(MemberBase member)
        {
            yield return Inject.StartOfMember(member);

            SetNextInstruction();

            foreach (var instruction in ParseInnerContentOfNestedProperty())
            {
                yield return instruction;
            }

            SkipTextNodes();
            yield return Inject.EndOfMember();
        }

        private IEnumerable<Instruction> ParseInnerContentOfNestedProperty()
        {
            if (CurrentNodeType == NodeType.Text)
            {
                yield return Inject.Value(instructionStream.Current.Text);
            }
            else
            {
                foreach (var instruction in ParseElements())
                {
                    yield return instruction;
                }
            }
        }

        private IEnumerable<Instruction> ParseMembersOfObject()
        {
            while (CurrentNodeType == NodeType.Attribute && !EndOfStream)
            {
                var valueOfMember = CurrentPropertyText;

                yield return Inject.StartOfMember(CurrentMember);

                if (IsMarkupExtension(valueOfMember))
                {
                    foreach (var instruction in ParseMarkupExtension(valueOfMember))
                    {
                        yield return instruction;
                    }
                }
                else
                {
                    yield return Inject.Value(valueOfMember);
                }

                yield return Inject.EndOfMember();

                SetNextInstruction();
            }
        }

        private IEnumerable<Instruction> ParseMarkupExtension(string valueOfMember)
        {
            var tree = MarkupExtensionParser.MarkupExtension.Parse(valueOfMember);
            var markupExtensionConverter = new MarkupExtensionNodeToXamlNodesConverter(typeSource);
            return markupExtensionConverter.ParseMarkupExtensionNode(tree);
        }

        private IEnumerable<Instruction> ParsePrefixDefinitions()
        {
            while (CurrentNodeType == NodeType.PrefixDefinition)
            {
                var protoXamlNode = instructionStream.Current;
                yield return Inject.PrefixDefinitionOfNamespace(protoXamlNode);
                SetNextInstruction();
            }
        }

        private static bool IsMarkupExtension(string text)
        {
            return text.Length > 3 && text.StartsWith("{") && text.EndsWith("}");
        }
    }
}