namespace Cohesion.Core
{
    public interface ICommandHandler<TCommandArguments>
    {
        CommandResult Execute(TCommandArguments commandArguments);
    }
}
