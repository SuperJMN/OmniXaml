namespace OmniXaml
{
    using System;
    using System.IO;
    using Parsers.ProtoParser;

    public class XamlXmlLoader : IXamlLoader
    {
        private readonly IXamlParserFactory xamlParserFactory;
        private IXmlReader xmlReader;

        public XamlXmlLoader(IXamlParserFactory xamlParserFactory)
        {
            this.xamlParserFactory = xamlParserFactory;
        }

        public object Load(Stream stream)
        {
            return Load(stream, xamlParserFactory.CreateForReadingFree());
        }

        public object Load(Stream stream, object instance)
        {
            return Load(stream, xamlParserFactory.CreateForReadingSpecificInstance(instance));
        }

        public T Load<T>(Stream stream) where T : class
        {
            return Load(stream, xamlParserFactory.CreateForReadingFree()) as T;
        }

        public T Load<T>(Stream stream, object instance) where T : class
        {
            return Load(stream, xamlParserFactory.CreateForReadingSpecificInstance(instance)) as T;
        }


        private object Load(Stream stream, IXamlParser parser)
        {
            try
            {
                xmlReader = new XmlCompatibilityReader(stream);
                return parser.Parse(xmlReader);
            }
            catch (Exception e)
            {
                throw new XamlLoadException($"Error loading XAML: {e}", xmlReader.LineNumber, xmlReader.LinePosition, e);
            }
        }
    }
}