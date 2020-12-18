namespace OmniXaml.TypeLocation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TypeDirectory : ITypeDirectory
    {
        private readonly ISet<XamlNamespace> xamlNamespaces;

        public TypeDirectory(IEnumerable<XamlNamespace> xamlNamespaces)
        {
            this.xamlNamespaces =  new HashSet<XamlNamespace>(xamlNamespaces);
        }

        public Type GetTypeByFullAddress(Address address)
        {
            if (address.Namespace == string.Empty)
            {
                throw new TypeLocationException($@"Cannot find the type {address.TypeName} because no default namespace has been specified. Please, specify it with xmlns attribute (xmlns=""…"")");
            }

            var ns = GetNamespace(address.Namespace);

            if (ns == null)
            {
                throw new TypeLocationException($"Cannot find the address '{address}'");                
            }

            return ns.Get(address.TypeName);
        }

        private Namespace GetNamespace(string name)
        {
            if (IsClrNamespace(name))
            {
                return TypeLocation.ClrNamespace.ExtractNamespace(name);
            }

            return xamlNamespaces.FirstOrDefault(xamlNamespace => xamlNamespace.Name == name);
        }

        private static bool IsClrNamespace(string ns)
        {
            var namespaceDeclarations = new List<string>{ "using:", "clr-namespace:" };

            return namespaceDeclarations.Exists(decl => ns.StartsWith(decl));
        }
    }
}
