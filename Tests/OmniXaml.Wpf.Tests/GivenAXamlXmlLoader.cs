namespace OmniXaml.Wpf.Tests
{
    using Wpf;

    public class GivenAXamlXmlLoader
    {
        protected object LoadXaml(string xamlContent)
        {
            var p = new WpfLoader();
            return LoadMixin.FromString(p, xamlContent);
        }

        protected object LoadXaml(string xamlContent, object rootInstance)
        {
            var p = new WpfLoader();
            return LoadMixin.FromString(p, xamlContent, rootInstance);
        }
    }
}