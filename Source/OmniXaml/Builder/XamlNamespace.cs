namespace OmniXaml.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Typing;

    public class XamlNamespace : Namespace
    {
        private readonly AddressPack addressPack;

        public XamlNamespace(string name)
        {
            this.Name = name;
            this.addressPack = new AddressPack();
        }

        public XamlNamespace(string name, AddressPack addressPack)
        {
            this.Name = name;
            this.addressPack = addressPack;
        }

        public AddressPack Addresses => addressPack;

        public static IEnumerable<XamlNamespace> DefinedInAssemblies(IEnumerable<Assembly> assemblies)
        {
            var namespaces = from a in assemblies
                let attributes = a.GetCustomAttributes<XmlnsDefinitionAttribute>()
                from byNamespace in attributes.GroupBy(arg => arg.XmlNamespace)
                let name = byNamespace.Key
                let clrNamespaces = byNamespace.Select(arg => arg.ClrNamespace)
                select Map(name)
                    .With(
                        new[]
                        {
                            Route.Assembly(a)
                                .WithNamespaces(clrNamespaces)
                        });

            return namespaces;
        }

        public static AssemblyNameConfig Map(string root)
        {            
            return new AssemblyNameConfig(root);
        }

        public override Type Get(string typeName)
        {
            return Addresses.Get(typeName);
        }
    }
}