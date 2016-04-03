namespace OmniXaml.Typing
{
    using System;
    using System.Reflection;
    using Glass;

    public class Member : MutableMember
    {
        public Member(string name, XamlType declaringType, ITypeRepository typeRepository, ITypeFeatureProvider featureProvider)
            : base(name, declaringType, typeRepository, featureProvider)
        {
            IsEvent = CheckIfEvent();
            XamlType = LookupType();
        }

        private bool CheckIfEvent()
        {
            var eventProperty = DeclaringType.UnderlyingType.GetRuntimeEvent(Name);
            return eventProperty != null;
        }

        public override bool IsEvent { get; }

        public override bool IsAttachable => false;

        public override bool IsDirective => false;

        public override MethodInfo Getter
        {
            get
            {
                if (IsEvent)
                {
                    return DeclaringType.UnderlyingType.GetRuntimeEvent(Name).RaiseMethod;
                }
                else
                {
                    return DeclaringType.UnderlyingType.GetRuntimeProperty(Name).GetMethod;
                }
            }
        }

        public override MethodInfo Setter
        {
            get
            {
                if (IsEvent)
                {
                    return DeclaringType.UnderlyingType.GetRuntimeEvent(Name).AddMethod;
                }
                else
                {
                    return DeclaringType.UnderlyingType.GetRuntimeProperty(Name).SetMethod;
                }
            }
        }

        private XamlType LookupType()
        {
            var underlyingType = DeclaringType.UnderlyingType;
            if (!IsEvent)
            {
                var property = underlyingType.GetRuntimeProperty(Name);
                property.ThrowIfNull(() => new ParseException($"Cannot find a property or event named \"{Name}\" in the type {underlyingType}"));
                return TypeRepository.GetByType(property.PropertyType);
            }

            return TypeRepository.GetByType(DeclaringType.UnderlyingType.GetRuntimeEvent(Name).EventHandlerType);
        }
    }
}