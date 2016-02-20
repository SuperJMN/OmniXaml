namespace OmniXaml.Typing
{
    using System;
    using System.Reflection;
    using Glass;

    public class TypeRepository : ITypeRepository
    {
        private readonly INamespaceRegistry namespaceRegistry;
        private readonly ITypeFactory typeTypeFactory;
        private readonly ITypeFeatureProvider featureProvider;

        public TypeRepository(INamespaceRegistry namespaceRegistry, ITypeFactory typeTypeFactory, ITypeFeatureProvider featureProvider)
        {
            Guard.ThrowIfNull(namespaceRegistry, nameof(namespaceRegistry));
            Guard.ThrowIfNull(typeTypeFactory, nameof(typeTypeFactory));
            Guard.ThrowIfNull(featureProvider, nameof(featureProvider));

            this.namespaceRegistry = namespaceRegistry;
            this.typeTypeFactory = typeTypeFactory;
            this.featureProvider = featureProvider;
        }

        public ITypeFeatureProvider FeatureProvider => featureProvider;

        public ITypeFactory TypeFactory => typeTypeFactory;

        public virtual XamlType GetByType(Type type)
        {
            Guard.ThrowIfNull(type, nameof(type));

            return XamlType.Create(type, this, TypeFactory, featureProvider);
        }

        public XamlType GetByQualifiedName(string qualifiedName)
        {
            var tuple = qualifiedName.Dicotomize(':');

            var prefix = tuple.Item2 == null ? string.Empty : tuple.Item1;
            var typeName = tuple.Item2 ?? tuple.Item1;

            return GetByPrefix(prefix, typeName);
        }

        public XamlType GetByPrefix(string prefix, string typeName)
        {
            var ns = namespaceRegistry.GetNamespaceByPrefix(prefix);

            if (ns == null)
            {
                throw new ParseException($"Cannot find a namespace with the prefix \"{prefix}\"");
            }

            var type = ns.Get(typeName);

            if (type == null)
            {
                throw new ParseException($"The type \"{{{prefix}:{typeName}}} cannot be found\"");
            }

            return GetByType(type);
        }

        public XamlType GetByFullAddress(XamlTypeName xamlTypeName)
        {
            var ns = namespaceRegistry.GetNamespace(xamlTypeName.Namespace);

            if (ns == null)
            {
                throw new TypeNotFoundException($"Error trying to resolve a XamlType: Cannot find the namespace '{xamlTypeName.Namespace}'");
            }

            var correspondingType = ns.Get(xamlTypeName.Name);

            if (correspondingType != null)
            {
                return GetByType(correspondingType);
            }

            throw new TypeNotFoundException($"Error trying to resolve a XamlType: The type {xamlTypeName.Name} has not been found into the namespace '{xamlTypeName.Namespace}'");
        }

        public Member GetMember(PropertyInfo propertyInfo)
        {
            var owner = GetByType(propertyInfo.DeclaringType);
            return new Member(propertyInfo.Name, owner, this, featureProvider);
        }

        public AttachableMember GetAttachableMember(string name, MethodInfo getter, MethodInfo setter)
        {
            return new AttachableMember(name, getter, setter, this, featureProvider);
        }       
    }
}