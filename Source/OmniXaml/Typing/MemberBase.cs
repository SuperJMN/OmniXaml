namespace OmniXaml.Typing
{
    public abstract class MemberBase
    {
        public string Name { get; }
        public XamlType XamlType { get; protected set; }
        public abstract bool IsDirective { get; }
        public abstract bool IsAttachable { get; }
        public abstract bool IsEvent { get; }

        protected MemberBase(string name)
        {
            this.Name = name;
        }

        protected bool Equals(MemberBase other)
        {
            return string.Equals(Name, other.Name) && XamlType.Equals(other.XamlType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((MemberBase)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode() * 397) ^ XamlType.GetHashCode();
            }
        }
    }
}