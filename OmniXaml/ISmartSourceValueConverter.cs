namespace OmniXaml
{
    using System;

    public interface ISmartSourceValueConverter<TInput, TOutput>
    {
        (bool, object) TryConvert(TInput strValue, Type desiredTargetType);
    }
}