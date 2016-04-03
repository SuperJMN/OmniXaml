namespace OmniXaml.Typing
{
    using System;
    using System.Reflection;

    public class AttachableMember : MutableMember
    {
        private readonly MethodInfo setter;
        private readonly MethodInfo getter;

        public AttachableMember(string name,
            MethodInfo getter,
            MethodInfo setter,
            ITypeRepository typeRepository,
            ITypeFeatureProvider featureProvider) : base(name, typeRepository.GetByType(getter.DeclaringType), typeRepository, featureProvider)
        {
            this.getter = getter;
            this.setter = setter;
            XamlType = LookupType();
        }

        public override bool IsAttachable => true;
        public override bool IsDirective => false;

        public override bool IsEvent => false;

        public override MethodInfo Getter => getter;

        public override MethodInfo Setter => setter;

        private XamlType LookupType()
        {
            return TypeRepository.GetByType(getter.ReturnType);
        }

        public override string ToString()
        {
            return $"Attachable {DeclaringType}.{Name}";
        }
    }
}