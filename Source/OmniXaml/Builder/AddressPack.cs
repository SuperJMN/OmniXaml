namespace OmniXaml.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class AddressPack : Collection<ConfiguredAssemblyWithNamespaces>
    {
        public AddressPack()
        {
        }

        public AddressPack(IEnumerable<ConfiguredAssemblyWithNamespaces> assemblyAndClrs) : base(assemblyAndClrs.ToList())
        {           
        }

        public Type Get(string name)
        {
            return (from configuredAssemblyWithNamespaces in Items
                let g = configuredAssemblyWithNamespaces.Get(name)
                where g != null
                select g).FirstOrDefault();
        }
    }
}