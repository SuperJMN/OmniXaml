using System;

namespace OmniXaml.Typing
{
    public class Directive : MemberBase
    {
        public Directive(string name) : base(name)
        {
            XamlType = XamlType.CreateForBuiltInType(typeof(object));
        }

        public Directive(string name, XamlType xamlType) : base(name)
        {
            XamlType = xamlType;
        }

        public override bool IsDirective => true;
        public override bool IsAttachable => false;
        public override bool IsEvent => false;

        public override string ToString()
        {
            return $"·{Name}· Directive";
        }
    }
}