namespace OmniXaml.Wpf
{
    using System.IO;

    public class WpfXamlLoader : IXamlLoader
    {
        private readonly XamlXmlLoader innerLoader;

        public WpfXamlLoader()
        {
            innerLoader = new XamlXmlLoader(new WpfParserFactory(new WpfXamlLoaderTypeFactory()));
        }

        public object Load(Stream stream)
        {
            return innerLoader.Load(stream);
        }

        public object Load(Stream stream, object rootInstance)
        {
            return innerLoader.Load(stream, rootInstance);
        }

        public T Load<T>(Stream stream) where T : class
        {
            return innerLoader.Load<T>(stream);
        }

        public T Load<T>(Stream stream, object instance) where T : class
        {
            return innerLoader.Load<T>(stream, instance);
        }
    }
}