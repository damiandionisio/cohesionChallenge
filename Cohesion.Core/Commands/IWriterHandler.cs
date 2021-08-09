namespace Cohesion.Core
{
    public interface IWriterHandler<TWriterArguments>
    {
        CommandResult Write(TWriterArguments args);
    }
}
