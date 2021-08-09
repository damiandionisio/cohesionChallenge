namespace Cohesion.Core
{
    public abstract class WriterHandler<TWriterArguments> : IWriterHandler<TWriterArguments>
    {
        public CommandResult Write(TWriterArguments writerArguments)
        {
            return Implement(writerArguments);
        }

        protected abstract CommandResult Implement(TWriterArguments readerArguments);
    }
}
