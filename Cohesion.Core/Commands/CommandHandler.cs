namespace Cohesion.Core
{
    public abstract class CommandHandler<TCommandArguments> : ICommandHandler<TCommandArguments>
    {
        public CommandResult Execute(TCommandArguments commandArguments)
        {
            var validationResult = Validate(commandArguments);

            if(!validationResult.HasErrors)
                return this.Implement(commandArguments);

            return CommandResult.None;
        }

        protected abstract CommandResult Implement(TCommandArguments commandArguments);
        
        protected abstract ValidationResult Validate(TCommandArguments commandArguments);
    }
}
