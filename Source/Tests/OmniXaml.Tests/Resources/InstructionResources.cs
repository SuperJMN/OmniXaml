namespace OmniXaml.Tests.Resources
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Builder;
    using Classes;
    using Classes.Another;
    using Classes.WpfLikeModel;
    using Common;
    using Typing;

    public class InstructionResources
    {
        public InstructionResources(GivenARuntimeTypeSourceWithNodeBuilders source)
        {
            RootNs = source.RootNs;
            AnotherNs = source.AnotherNs;
            SpecialNs = source.SpecialNs;
            X = source.X;
        }

        public NamespaceDeclaration SpecialNs { get; set; }

        private XamlInstructionBuilder X { get; }

        private NamespaceDeclaration RootNs { get; }

        private NamespaceDeclaration AnotherNs { get; }

        public IEnumerable<Instruction> ExtensionWithTwoArguments
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.SampleProperty),
                    X.StartObject(typeof (DummyExtension)),
                    X.MarkupExtensionArguments(),
                    X.Value("One"),
                    X.Value("Second"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ExtensionWithNonStringArgument
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Number),
                    X.StartObject(typeof (IntExtension)),
                    X.MarkupExtensionArguments(),
                    X.Value("123"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> OneObject
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> ObjectWithMember
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.SampleProperty),
                    X.Value("Property!"),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> ObjectWithEnumMember
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.EnumProperty),
                    X.Value("One"),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> ObjectWithNullableEnumProperty
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.NullableEnumProperty),
                    X.Value("Two"),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> ObjectWithTwoMembers
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.SampleProperty),
                    X.Value("Property!"),
                    X.EndMember(),
                    X.StartMember<DummyClass>(d => d.AnotherProperty),
                    X.Value("Another!"),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> CollectionWithInnerCollection
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),

                    // Inner collection
                    X.StartMember<Item>(d => d.Children),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> WithCollectionAndInnerAttribute
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(d => d.Title),
                    X.Value("SomeText"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> MemberWithIncompatibleTypes
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Number),
                    X.Value("12"),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> ExtensionWithArgument
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.SampleProperty),
                    X.StartObject(typeof (DummyExtension)),
                    X.MarkupExtensionArguments(),
                    X.Value("Option"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> ExtensionThatReturnsNull
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.SampleProperty),
                    X.StartObject(typeof (ExtensionThatReturnsNull)),                    
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> GetString(NamespaceDeclaration sysNs)
        {
            return new Collection<Instruction>
            {
                X.NamespacePrefixDeclaration(sysNs),
                X.StartObject<string>(),
                X.Initialization(),
                X.Value("Text"),
                X.EndMember(),
                X.EndObject()
            };
        }

        public IEnumerable<Instruction> GetSingleObject()
        {
            return new List<Instruction>
            {
                X.NamespacePrefixDeclaration(RootNs),
                X.StartObject<DummyClass>(),
                X.EndObject(),
            };
        }

        public IEnumerable<Instruction> NestedChild
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(c => c.Child),
                    X.StartObject<ChildClass>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> InstanceWithChild
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Child),
                    X.StartObject(typeof (ChildClass)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ObjectWithChild
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Child),
                    X.StartObject<ChildClass>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> ComplexNesting
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(c => c.SampleProperty),
                    X.Value("Sample"),
                    X.EndMember(),
                    X.StartMember<DummyClass>(c => c.Child),
                    X.StartObject<ChildClass>(),
                    X.StartMember<ChildClass>(c => c.Content),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(d => d.Text),
                    X.Value("Value!"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> CollectionWithMoreThanOneItem
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> NestedChildWithContentProperty
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<ChildClass>(),
                    X.StartMember<ChildClass>(c => c.Content),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> TwoNestedPropertiesUsingContentProperty
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject(typeof (DummyClass)),
                    X.StartMember<DummyClass>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(i => i.Title),
                    X.Value("Main1"),
                    X.EndMember(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(i => i.Title),
                    X.Value("Main2"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.StartMember<DummyClass>(d => d.Child),
                    X.StartObject(typeof (ChildClass)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> MixedPropertiesWithContentPropertyAfter
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject(typeof (Grid)),
                    X.StartMember<Grid>(d => d.RowDefinitions),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject(typeof (RowDefinition)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.StartMember<Grid>(d => d.Children),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject(typeof (TextBlock)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> CollectionWithMixedEmptyAndNotEmptyNestedElements
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject(typeof (Grid)),
                    X.StartMember<Grid>(d => d.Children),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject(typeof (TextBlock)),
                    X.EndObject(),
                    X.StartObject(typeof (TextBlock)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> MixedPropertiesWithContentPropertyBefore
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject(typeof (Grid)),
                    X.StartMember<Grid>(d => d.Children),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject(typeof (TextBlock)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.StartMember<Grid>(d => d.RowDefinitions),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject(typeof (RowDefinition)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> TwoNestedPropertiesOneOfThemUsesContentPropertyWithSingleItem
        {
            get
            {
                var expectedInstructions = new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject(typeof (ChildClass)),
                    X.StartMember<ChildClass>(d => d.Content),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(item => item.Children),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(i => i.Title),
                    X.Value("Item1"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.StartMember<DummyClass>(d => d.Child),
                    X.StartObject(typeof (ChildClass)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
                return expectedInstructions;
            }
        }

        public IEnumerable<Instruction> TwoNestedProperties
        {
            get
            {
                var expectedInstructions = new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(i => i.Title),
                    X.Value("Main1"),
                    X.EndMember(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(i => i.Title),
                    X.Value("Main2"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.StartMember<DummyClass>(d => d.Child),
                    X.StartObject(typeof (ChildClass)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
                return expectedInstructions;
            }
        }

        public IEnumerable<Instruction> ContentPropertyNesting
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(i => i.Title),
                    X.Value("Main1"),
                    X.EndMember(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(i => i.Title),
                    X.Value("Main2"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.StartMember<DummyClass>(d => d.Child),
                    X.StartObject(typeof (ChildClass)),
                    X.StartMember<ChildClass>(d => d.Content),
                    X.StartObject<Item>(),
                    // Collection of Items
                    X.StartMember<Item>(i => i.Children),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(i => i.Title),
                    X.Value("Item1"),
                    X.EndMember(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(i => i.Title),
                    X.Value("Item2"),
                    X.EndMember(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(i => i.Title),
                    X.Value("Item3"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    // End of collection of items

                    X.EndObject(),
                    X.EndMember(),
                    X.StartMember<ChildClass>(c => c.Child),
                    X.StartObject(typeof (ChildClass)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> CreateExpectedNodesForImplicitContentPropertyWithImplicityCollection()
        {
            var expectedInstructions = new List<Instruction>
            {
                X.NamespacePrefixDeclaration(RootNs),
                X.StartObject(typeof (ChildClass)),
                X.StartMember<ChildClass>(d => d.Content),
                X.StartObject<Item>(),
                X.StartMember<Item>(item => item.Children),
                X.GetObject(),
                X.Items(),
                X.StartObject<Item>(),
                X.StartMember<Item>(i => i.Title),
                X.Value("Item1"),
                X.EndMember(),
                X.EndObject(),
                X.EndMember(),
                X.EndObject(),
                X.EndMember(),
                X.EndObject(),
                X.EndMember(),
                X.EndObject(),
            };
            return expectedInstructions;
        }

        public IEnumerable<Instruction> ElementWithTwoDeclarations
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.NamespacePrefixDeclaration(AnotherNs),
                    X.StartObject<DummyClass>(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> KeyDirective
        {
            get
            {
                return new Collection<Instruction>
                {
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Resources),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<ChildClass>(),
                    X.Key(),
                    X.Value("SomeKey"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> KeyDirective2
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.NamespacePrefixDeclaration(CoreTypes.SpecialNamespace, "x"),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Resources),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject(typeof (ChildClass)),
                    X.Key(),
                    X.Value("SomeKey"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> DifferentNamespacesAndMoreThanOneProperty
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration("root", string.Empty),
                    X.NamespacePrefixDeclaration("another", "x"),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.SampleProperty),
                    X.Value("One"),
                    X.EndMember(),
                    X.StartMember<DummyClass>(d => d.AnotherProperty),
                    X.Value("Two"),
                    X.EndMember(),
                    X.StartMember<DummyClass>(d => d.ChildFromAnotherNamespace),
                    X.StartObject(typeof (Foreigner)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ContentPropertyForSingleProperty
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject(typeof (ChildClass)),
                    X.StartMember<ChildClass>(d => d.Content),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ContentPropertyForCollectionMoreThanOneElement
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> CollapsedTagWithProperty
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.SampleProperty),
                    X.Value("SomeText"),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> SingleInstance
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> DifferentNamespaces
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration("root", string.Empty),
                    X.NamespacePrefixDeclaration("another", "x"),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.ChildFromAnotherNamespace),
                    X.StartObject(typeof (Foreigner)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> CollectionWithOneItem
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> CollectionWithOneItemAndAMember
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.StartMember<Item>(d => d.Title),
                    X.Value("SomeText"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> ExpandedStringProperty
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(c => c.SampleProperty),
                    X.Value("Property!"),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> TestReverseMembersReverted
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartMember<Setter>(c => c.Property),
                    X.StartObject(typeof (DummyClass)),
                    X.StartMember<ChildClass>(c => c.Content),
                    X.Value("Tío"),
                    X.EndMember(),
                    X.StartMember<ChildClass>(c => c.Name),
                    X.Value("Hola"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.StartMember<Setter>(c => c.Value),
                    X.StartObject(typeof (DummyClass)),
                    X.StartMember<ChildClass>(c => c.Content),
                    X.Value("Tío"),
                    X.EndMember(),
                    X.StartMember<ChildClass>(c => c.Name),
                    X.Value("Hola"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                };
            }
        }

        public IEnumerable<Instruction> TestReverseMembers
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartMember<Setter>(c => c.Value),
                    X.StartObject(typeof (DummyClass)),
                    X.StartMember<ChildClass>(c => c.Name),
                    X.Value("Hola"),
                    X.EndMember(),
                    X.StartMember<ChildClass>(c => c.Content),
                    X.Value("Tío"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.StartMember<Setter>(c => c.Property),
                    X.StartObject(typeof (DummyClass)),
                    X.StartMember<ChildClass>(c => c.Name),
                    X.Value("Hola"),
                    X.EndMember(),
                    X.StartMember<ChildClass>(c => c.Content),
                    X.Value("Tío"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                };
            }
        }

        public IEnumerable<Instruction> TwoMembersReversed
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartMember<Setter>(c => c.Property),
                    X.Value("Property"),
                    X.EndMember(),
                    X.StartMember<Setter>(c => c.Value),
                    X.Value("Value"),
                    X.EndMember(),
                };
            }
        }

        public IEnumerable<Instruction> TwoMembers
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartMember<Setter>(c => c.Value),
                    X.Value("Value"),
                    X.EndMember(),
                    X.StartMember<Setter>(c => c.Property),
                    X.Value("Property"),
                    X.EndMember(),
                };
            }
        }

        public List<Instruction> SimpleExtensionWithOneAssignment
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.SampleProperty),
                    X.StartObject(typeof (DummyExtension)),
                    X.StartMember<DummyExtension>(d => d.Property),
                    X.Value("SomeValue"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public List<Instruction> SimpleExtension
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.SampleProperty),
                    X.StartObject(typeof (DummyExtension)),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public List<Instruction> ContentPropertyForCollectionOneElement
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> TextBlockWithText
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<TextBlock>(),
                    X.StartMember<TextBlock>(c => c.Text),
                    X.Value("Hi all!!"),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ChildInNameScope
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.NamespacePrefixDeclaration(SpecialNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Child),
                    X.StartObject<ChildClass>(),
                    X.Name(),
                    X.Value("MyObject"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ChildInDeeperNameScope
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<Window>(),
                    X.StartMember<Window>(d => d.Content),
                    X.StartObject<Grid>(),

                    X.StartMember<Grid>(g => g.Children),
                    X.GetObject(),

                    X.StartObject<TextBlock>(),
                    X.Name(),
                    X.Value("MyTextBlock"),
                    X.EndMember(),
                    X.EndObject(),

                    X.StartObject<TextBlock>(),
                    X.Name(),
                    X.Value("MyOtherTextBlock"),
                    X.EndMember(),
                    X.EndObject(),

                    X.StartObject<TextBlock>(),
                    X.EndObject(),

                    X.EndObject(),
                    X.EndMember(),

                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> NameWithNoNamescopesToRegisterTo
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),

                    X.StartObject<Grid>(),

                    X.StartMember<Grid>(g => g.Children),
                    X.GetObject(),

                    X.StartObject<TextBlock>(),
                    X.Name(),
                    X.Value("MyTextBlock"),
                    X.EndMember(),
                    X.EndObject(),

                    X.StartObject<TextBlock>(),
                    X.EndObject(),

                    X.EndObject(),
                    X.EndMember(),

                    X.EndObject(),
                };
            }
        }

        public List<Instruction> ComboBoxCollectionOnly
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartMember<ComboBox>(c => c.Items),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<DummyClass>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                };
            }
        }

        public List<Instruction> StyleSorted
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartObject<Style>(),
                    X.StartMember<Style>(c => c.Setter),
                    X.StartObject<Setter>(),
                    X.StartMember<Setter>(c => c.Property),
                    X.Value("Property"),
                    X.EndMember(),
                    X.StartMember<Setter>(c => c.Value),
                    X.Value("Value"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public List<Instruction> StyleUnsorted
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartObject<Style>(),
                    X.StartMember<Style>(c => c.Setter),
                    X.StartObject<Setter>(),
                    X.StartMember<Setter>(c => c.Value),
                    X.Value("Value"),
                    X.EndMember(),
                    X.StartMember<Setter>(c => c.Property),
                    X.Value("Property"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public List<Instruction> SetterUnsorted
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartObject<Setter>(),
                    X.StartMember<Setter>(c => c.Value),
                    X.Value("Value"),
                    X.EndMember(),
                    X.StartMember<Setter>(c => c.Property),
                    X.Value("Property"),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public List<Instruction> SetterSorted
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartObject<Setter>(),
                    X.StartMember<Setter>(c => c.Property),
                    X.Value("Property"),
                    X.EndMember(),
                    X.StartMember<Setter>(c => c.Value),
                    X.Value("Value"),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public List<Instruction> ComboBoxUnsorted
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartObject<ComboBox>(),
                        X.StartMember<ComboBox>(c => c.SelectedIndex),
                            X.Value("1"),
                        X.EndMember(),
                        X.StartMember<ComboBox>(d => d.Items),
                            X.GetObject(),
                                X.Items(),
                                    X.StartObject<Item>(),
                                    X.EndObject(),
                                X.EndMember(),
                            X.EndObject(),
                        X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public List<Instruction> ComboBoxSorted
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartObject<ComboBox>(),
                        X.StartMember<ComboBox>(d => d.Items),
                            X.GetObject(),
                                X.Items(),
                                    X.StartObject<Item>(),
                                    X.EndObject(),
                                X.EndMember(),
                            X.EndObject(),
                        X.EndMember(),
                        X.StartMember<ComboBox>(c => c.SelectedIndex),
                            X.Value("1"),
                        X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public List<Instruction> TwoComboBoxesUnsorted
        {
            get
            {
                var result = new List<Instruction>(); 
                result.Add(X.StartObject<Grid>());
                result.Add(X.StartMember<Grid>(g => g.Children));
                result.Add(X.GetObject());
                result.Add(X.Items());
                result.AddRange(ComboBoxUnsorted);
                result.AddRange(ComboBoxUnsorted);
                result.Add(X.EndMember());
                result.Add(X.EndObject());
                result.Add(X.EndMember());
                result.Add(X.EndObject());

                return result;
            }
        }

        public List<Instruction> TwoComboBoxesSorted
        {
            get
            {
                var result = new List<Instruction>();
                result.Add(X.StartObject<Grid>());
                result.Add(X.StartMember<Grid>(g => g.Children));
                result.Add(X.GetObject());
                result.Add(X.Items());
                result.AddRange(ComboBoxSorted);
                result.AddRange(ComboBoxSorted);
                result.Add(X.EndMember());
                result.Add(X.EndObject());
                result.Add(X.EndMember());
                result.Add(X.EndObject());

                return result;
            }
        }

        public List<Instruction> ListBoxSortedWithExtension
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartObject<ListBox>(),
                        X.StartMember<ListBox>(d => d.Items),
                            X.GetObject(),
                                X.Items(),
                                    X.StartObject<ListBoxItem>(),
                                        X.StartMember<ListBoxItem>(lbi => lbi.Content),
                                            X.StartObject<TextBlock>(),
                                                X.StartMember<TextBlock>(tb => tb.Text),
                                                    X.StartObject<BindingExtension>(),
                                                        X.MarkupExtensionArguments(),
                                                            X.Value("DoubleValue"),
                                                        X.EndMember(),
                                                        X.StartMember<BindingExtension>(b => b.Mode),
                                                            X.Value("TwoWay"),
                                                        X.EndMember(),
                                                    X.EndObject(),
                                                X.EndMember(),
                                            X.EndObject(),
                                        X.EndMember(),
                                    X.EndObject(),
                                X.EndMember(),
                            X.EndObject(),
                        X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> AttemptToAssignItemsToNonCollectionMember
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.StartMember<DummyClass>(d => d.Item),
                    X.GetObject(),
                    X.Items(),
                    X.StartObject<Item>(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> TwoRoots
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.EndObject(),
                    X.StartObject<DummyClass>(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ParentShouldReceiveInitializedChild
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<SpyingParent>(),
                    X.StartMember<SpyingParent>(d => d.Child),
                    X.StartObject<ChildClass>(),
                    X.StartMember<ChildClass>(d => d.Name),
                    X.Value("SomeName"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> MixedCollection
        {
            get
            {
                var colections = new NamespaceDeclaration("clr-namespace:System.Collections;assembly=mscorlib", "sysCol");
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(colections),
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<ArrayList>(),

                    X.Items(),
                    
                    X.StartObject<DummyClass>(),
                    X.EndObject(),

                    X.StartObject<DummyClass>(),
                    X.EndObject(),

                    X.StartObject<DummyClass>(),
                    X.EndObject(),

                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ListBoxWithItemAndTextBlockWithNames
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.NamespacePrefixDeclaration(SpecialNs),
                    X.StartObject<Window>(),
                        X.StartMember<Window>(w => w.Content),
                            X.StartObject<ListBox>(),
                                X.Name(),
                                    X.Value("MyListBox"),
                                X.EndMember(),
                                X.StartMember<ListBox>(l => l.Items),
                                    X.GetObject(),
                                        X.Items(),
                                            X.StartObject<ListBoxItem>(),
                                                X.Name(),
                                                    X.Value("MyListBoxItem"),
                                                X.EndMember(),
                                                X.StartMember<ListBoxItem>(lbi => lbi.Content),
                                                    X.StartObject<TextBlock>(),
                                                        X.Name(),
                                                            X.Value("MyTextBlock"),
                                                        X.EndMember(),
                                                    X.EndObject(),
                                                X.EndMember(),
                                            X.EndObject(),
                                        X.EndMember(),
                                    X.EndObject(),
                                X.EndMember(),
                            X.EndObject(),
                        X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ListBoxWithItemAndTextBlockNoNames
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.NamespacePrefixDeclaration(SpecialNs),
                    X.StartObject<Window>(),
                        X.StartMember<Window>(w => w.Content),
                            X.StartObject<ListBox>(),                                
                                X.StartMember<ListBox>(l => l.Items),
                                    X.GetObject(),
                                        X.Items(),
                                            X.StartObject<ListBoxItem>(),                                                
                                                X.StartMember<ListBoxItem>(lbi => lbi.Content),
                                                    X.StartObject<TextBlock>(),                                                        
                                                    X.EndObject(),
                                                X.EndMember(),
                                            X.EndObject(),
                                        X.EndMember(),
                                    X.EndObject(),
                                X.EndMember(),
                            X.EndObject(),
                        X.EndMember(),
                    X.EndObject(),
                };
            }
        }


        public IEnumerable<Instruction> NamedObject
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.NamespacePrefixDeclaration(SpecialNs),
                    X.StartObject<TextBlock>(),
                    X.Name(),
                    X.Value("MyTextBlock"),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> TwoNestedNamedObjects
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.NamespacePrefixDeclaration(SpecialNs),
                    X.StartObject<ListBoxItem>(),
                    X.Name(),
                    X.Value("MyListBoxItem"),
                    X.EndMember(),
                    X.StartMember<ListBoxItem>(lbi => lbi.Content),
                    X.StartObject<TextBlock>(),
                    X.Name(),
                    X.Value("MyTextBlock"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        

        public IEnumerable<Instruction> RootInstanceWithAttachableMember

        {
            get
            {
                // Equivalent to <DummyClass xmlns="root" Container.Property="Value"></DummyClass>

                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                    X.AttachableProperty<Container>("Property"),
                    X.Value("Value"),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> DirectContentForOneToMany
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration("root", ""),
                    X.StartObject(typeof (ItemsControl)),
                    X.StartMember<ItemsControl>(d => d.Items),
                    X.GetObject(),
                    X.Items(),
                    X.Value("Hello"),
                    X.EndMember(),
                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ExpandedAttachablePropertyAndItemBelow
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<DummyClass>(),
                        X.StartMember<DummyClass>(d => d.Items),
                            X.GetObject(),
                                X.Items(),
                                    X.StartObject<Item>(),
                                        X.AttachableProperty<Container>("Property"),
                                            X.Value("Value"),
                                        X.EndMember(),
                                    X.EndObject(),
                                    X.StartObject<Item>(),                      
                                    X.EndObject(),
                                X.EndMember(),
                            X.EndObject(),
                        X.EndMember(),
                    X.EndObject()
                };
            }
        }

        public IEnumerable<Instruction> CustomCollection
        {
            get
            {
                return new List<Instruction>
                {
                    X.StartObject<CustomCollection>(),
                    X.Items(),

                    X.StartObject<int>(),
                    X.Initialization(),
                    X.Value("1"),
                    X.EndMember(),
                    X.EndObject(),

                    //X.StartObject<int>(),
                    //X.Initialization(),
                    //X.Value("2"),
                    //X.EndMember(),
                    //X.EndObject(),

                    //X.StartObject<int>(),
                    //X.Initialization(),
                    //X.Value("3"),
                    //X.EndMember(),
                    //X.EndObject(),

                    X.EndMember(),

                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> AttachableMemberThatIsCollection
        {
            get
            {
                var system = new NamespaceDeclaration("clr-namespace:System;assembly=mscorlib", "sys");

                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.NamespacePrefixDeclaration(system),
                    X.StartObject<DummyClass>(),
                    X.AttachableProperty<Container>("Collection"),
                    X.StartObject<CustomCollection>(),
                    X.Items(),

                    X.StartObject<int>(),
                    X.Initialization(),
                    X.Value("1"),
                    X.EndMember(),
                    X.EndObject(),

                    X.StartObject<int>(),
                    X.Initialization(),
                    X.Value("2"),
                    X.EndMember(),
                    X.EndObject(),

                    X.StartObject<int>(),
                    X.Initialization(),
                    X.Value("3"),
                    X.EndMember(),
                    X.EndObject(),

                    X.EndMember(),

                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }            
        }

        public IEnumerable<Instruction> AttachableMemberThatIsCollectionImplicit
        {
            get
            {
                var system = new NamespaceDeclaration("clr-namespace:System;assembly=mscorlib", "sys");

                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.NamespacePrefixDeclaration(system),
                    X.StartObject<DummyClass>(),
                    X.AttachableProperty<Container>("Collection"),
                    X.GetObject(),
                    X.Items(),

                    X.StartObject<int>(),
                    X.Initialization(),
                    X.Value("1"),
                    X.EndMember(),
                    X.EndObject(),

                    X.StartObject<int>(),
                    X.Initialization(),
                    X.Value("2"),
                    X.EndMember(),
                    X.EndObject(),

                    X.StartObject<int>(),
                    X.Initialization(),
                    X.Value("3"),
                    X.EndMember(),
                    X.EndObject(),

                    X.EndMember(),

                    X.EndObject(),
                    X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> PureCollection
        {
            get
            {
                var colections = new NamespaceDeclaration("clr-namespace:System.Collections;assembly=mscorlib", "sysCol");
                var system = new NamespaceDeclaration("clr-namespace:System;assembly=mscorlib", "sys");

                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(colections),
                    X.NamespacePrefixDeclaration(system),
                    X.StartObject<ArrayList>(),

                        X.Items(),

                        X.StartObject<int>(),
                            X.Initialization(),
                            X.Value("1"),
                            X.EndMember(),
                        X.EndObject(),

                        X.StartObject<int>(),
                            X.Initialization(),
                            X.Value("2"),
                            X.EndMember(),
                        X.EndObject(),

                        X.StartObject<int>(),
                            X.Initialization(),
                            X.Value("3"),
                            X.EndMember(),
                        X.EndObject(),

                        X.EndMember(),

                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ExplicitCollection
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<RootObject>(),
                        X.StartMember<RootObject>(o => o.Collection),
                            X.StartObject<CustomCollection>(),
                                X.Items(),
                                    X.StartObject<DummyClass>(),
                                    X.EndObject(),
                                    X.StartObject<DummyClass>(),
                                    X.EndObject(),
                                    X.StartObject<DummyClass>(),
                                    X.EndObject(),
                                X.EndMember(),
                            X.EndObject(),
                        X.EndMember(),
                    X.EndObject(),
                };
            }
        }

        public IEnumerable<Instruction> ImplicitCollection
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<RootObject>(),
                        X.StartMember<RootObject>(o => o.Collection),
                            X.GetObject(),
                                X.Items(),
                                    X.StartObject<DummyClass>(),
                                    X.EndObject(),
                                    X.StartObject<DummyClass>(),
                                    X.EndObject(),
                                    X.StartObject<DummyClass>(),
                                    X.EndObject(),
                                X.EndMember(),
                            X.EndObject(),
                        X.EndMember(),
                    X.EndObject(),
                };
            }            
        }

        public IEnumerable<Instruction> MemberAfterInitalizationValue
        {
            get
            {
                return new List<Instruction>
                {
                    X.NamespacePrefixDeclaration(RootNs),
                    X.StartObject<RootObject>(),
                        X.StartMember<RootObject>(o => o.Collection),
                            X.GetObject(),
                                X.Items(),
                                    X.StartObject<string>(),
                                    X.Initialization(),
                                    X.Value("foo"),
                                    X.EndMember(),
                                    X.EndObject(),
                                    X.StartObject<DummyClass>(),
                                    X.StartMember<DummyClass>(x => x.Number),
                                    X.Value("123"),
                                    X.EndMember(),
                                    X.EndObject(),
                                X.EndMember(),
                            X.EndObject(),
                        X.EndMember(),
                    X.EndObject(),
                };
            }
        }
    }
}