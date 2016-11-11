﻿namespace OmniXaml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class SourceValueConverter : ISourceValueConverter
    {
        readonly Dictionary<Type, Func<ConverterValueContext, object>> converters = new Dictionary<Type, Func<ConverterValueContext, object>>();

        public object GetCompatibleValue(ConverterValueContext valueContext)
        {
            var targetType = valueContext.TargetType;
            var sourceValue = (string)valueContext.Value;

            if (targetType == typeof(int))
            {
                return int.Parse(sourceValue);
            }

            if (targetType == typeof(double))
            {
                return int.Parse(sourceValue);
            }

            if (typeof(Delegate).GetTypeInfo().IsAssignableFrom(targetType.GetTypeInfo()))
            {
                var rootInstance = valueContext.BuildContext.AmbientRegistrator.Instances.First();
                var callbackMethodInfo = rootInstance.GetType()
                    .GetRuntimeMethods().First(method => method.Name.Equals(sourceValue));
                return callbackMethodInfo.CreateDelegate(valueContext.TargetType, rootInstance);
            }

            Func<ConverterValueContext, object> converter;
            if (converters.TryGetValue(targetType, out converter))
            {
                return converter(valueContext);
            }

            if (targetType.GetTypeInfo().IsEnum)
            {
                return Enum.Parse(targetType, sourceValue);
            }

            return sourceValue;
        }

       
        public void Add(Type type, Func<ConverterValueContext, object> func)
        {
            converters.Add(type, func);
        }
    }
}