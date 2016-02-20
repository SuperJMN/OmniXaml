namespace OmniXaml.ObjectAssembler
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Commands;
    using Glass;
    using TypeConversion;
    using Typing;

    public class ValuePipeline
    {
        private readonly ITypeRepository typeRepository;
        private readonly ITopDownValueContext topDownValueContext;

        public ValuePipeline(ITypeRepository typeRepository, ITopDownValueContext topDownValueContext)
        {
            this.typeRepository = typeRepository;
            this.topDownValueContext = topDownValueContext;
        }

        public ITypeRepository TypeRepository => typeRepository;

        public object ConvertValueIfNecessary(object value, XamlType targetType)
        {
            if (IsAlreadyCompatible(value, targetType.UnderlyingType))
            {
                return value;
            }

            object converted;
            if (TrySpecialConversion(value, targetType.UnderlyingType, out converted))
            {
                return converted;
            }

 
            if (TryConverterIfAny(value, targetType, out converted))
            {
                return converted;
            }

            throw new ParseException($"Could not convert '{value}' (of type {value.GetType()}) to {targetType.UnderlyingType}");
        }

        private bool TryConverterIfAny(object value, XamlType targetType, out object result)
        {
            var typeConverter = targetType.TypeConverter;
            if (typeConverter != null)
            {
                var context = new TypeConverterContext(typeRepository, topDownValueContext);
                if (typeConverter.CanConvertFrom(context, value.GetType()))
                {
                    {
                        result = typeConverter.ConvertFrom(context, CultureInfo.InvariantCulture, value);
                        return true;
                    }
                }
            }

            result = null;
            return false;
        }

        private static bool IsAlreadyCompatible(object value, Type targetType)
        {
            if (value == null)
            {
                return true;
            }


            return targetType.GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo());
        }

        private static bool TrySpecialConversion(object value, Type targetType, out object result)
        {
            var type = value.GetType();

            if (type == typeof(string) && targetType == typeof(string))
            {
                result = value;
                return true;
            }

            if (type == typeof(string) && targetType == typeof(object))
            {
                result = value.ToString();
                return true;
            }


            if (TryEnumConversion(value, targetType, out result))
            {
                return true;
            }

            result = null;
            return false;
        }

        private static bool TryEnumConversion(object value, Type targetType, out object converted)
        {
            object result;
            if (targetType.IsNullable())
            {
                var typeInfo = Nullable.GetUnderlyingType(targetType);
                if (typeInfo != null && typeInfo.GetTypeInfo().IsEnum)
                {
                    if (EnumExtensions.TryParse(typeInfo, value.ToString(), out result))
                    {
                        converted = result;
                        return true;
                    }
                }
            }

            if (targetType.GetTypeInfo().IsEnum)
            {
                if (EnumExtensions.TryParse(targetType, value.ToString(), out result))
                {
                    converted = result;
                    return true;
                }
            }

            converted = null;
            return false;
        }
    }
}