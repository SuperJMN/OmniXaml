namespace OmniXaml.ObjectAssembler.Commands
{
    using Typing;

    public class ValueCommand : Command
    {
        private readonly string value;

        public ValueCommand(StateCommuter stateCommuter, ITypeRepository typeSource, ITopDownValueContext topDownValueContext, string value) : base(stateCommuter)
        {
            this.value = value;
            ValuePipeLine = new ValuePipeline(typeSource, topDownValueContext);
        }

        private ValuePipeline ValuePipeLine { get; }

        public override void Execute()
        {
            switch (StateCommuter.ValueProcessingMode)
            {
                case ValueProcessingMode.InitializationValue:
                    StateCommuter.Current.Instance = ValuePipeLine.ConvertValueIfNecessary(value, StateCommuter.Current.XamlType);
                    StateCommuter.ValueProcessingMode = ValueProcessingMode.AssignToMember;
                    break;

                case ValueProcessingMode.Key:
                    StateCommuter.SetKey(value);
                    StateCommuter.ValueProcessingMode = ValueProcessingMode.AssignToMember;
                    break;

                case ValueProcessingMode.ConstructionParameter:
                    StateCommuter.AddCtorArgument(value);
                    break;

                case ValueProcessingMode.AssignToMember:
                    StateCommuter.RaiseLevel();
                    StateCommuter.Current.Instance = value;
                    StateCommuter.AssociateCurrentInstanceToParent();
                    StateCommuter.DecreaseLevel();
                    break;

                case ValueProcessingMode.Name:
                    StateCommuter.SetNameForCurrentInstance(value);
                    StateCommuter.ValueProcessingMode = ValueProcessingMode.AssignToMember;
                    break;

                default:
                    throw new ParseException(
                        "Unexpected mode was set trying to process a Value XAML instruction. " +
                        $"We found \"{StateCommuter.ValueProcessingMode}\") and it cannot be handled.");
            }                                    
        }

        public override string ToString()
        {
            return $"Value: {value}";
        }
    }
}