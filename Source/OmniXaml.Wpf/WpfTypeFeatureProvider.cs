namespace OmniXaml.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Markup;
    using Builder;
    using TypeConversion;
    using Typing;
    using System.Reflection;
    internal class WpfTypeFeatureProvider : ITypeFeatureProvider
    {
        private readonly ITypeFeatureProvider inner;

        public WpfTypeFeatureProvider(ITypeConverterProvider converterProvider) 
        {
            inner = new TypeFeatureProvider(converterProvider);
        }

        public ITypeConverter GetTypeConverter(Type type)
        {
            return inner.GetTypeConverter(type);
        }

        public string GetContentPropertyName(Type type)
        {
            return type.GetTypeInfo().GetCustomAttribute<ContentPropertyAttribute>()?.Name;
        }

        public IEnumerable<TypeConverterRegistration> TypeConverters => inner.TypeConverters;
        
        public void RegisterMetadata(Type type, Metadata metadata)
        {
            inner.RegisterMetadata(type, metadata);
        }

        public Metadata GetMetadata(Type type)
        {
            return inner.GetMetadata(type);
        }
    }
}