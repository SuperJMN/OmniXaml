namespace OmniXaml
{
    using System.Collections.Generic;
    using System.IO;
    using Glass;
    using Parsers.ProtoParser;
    using Parsers.XamlInstructions;

    public class XamlXmlParser : IXamlParser
    {
        private readonly IObjectAssembler objectAssembler;
        private readonly IProtoParser protoParser;
        private readonly IXamlInstructionParser parser;

        public XamlXmlParser(PhaseParserKit phaseParserKit)  
        {
            Guard.ThrowIfNull(phaseParserKit, nameof(phaseParserKit));

            objectAssembler = phaseParserKit.ObjectAssembler;
            protoParser = phaseParserKit.ProtoParser;
            parser = phaseParserKit.Parser;
        }

        public object Parse(IXmlReader stream)
        {
            var xamlProtoNodes = protoParser.Parse(stream);
            var xamlNodes = parser.Parse(xamlProtoNodes);
            return Parse(xamlNodes);
        }

        private object Parse(IEnumerable<XamlInstruction> xamlNodes)
        {
            foreach (var instruction in xamlNodes) { objectAssembler.Process(instruction); }

            return objectAssembler.Result;
        }
    }

    public interface IXamlLoader
    {
        object Load(Stream stream);
        object Load(Stream stream, object instance);

        T Load<T>(Stream stream) where T : class;
        T Load<T>(Stream stream, object instance) where T : class;
    }
}