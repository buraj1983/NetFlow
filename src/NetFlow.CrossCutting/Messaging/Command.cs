namespace NetFlow.Common.Messaging
{
    public abstract class Command : Message, ICommand
    {
        public string Name { get; }

        protected Command(string name)
        {
            Name = name;
        }
    }
}