namespace Cohesion.Core
{
    public abstract class ReaderHandler<TReaderArguments, TReaderResult> : IReaderHandler<TReaderArguments, TReaderResult>
    {
        public TReaderResult Read(TReaderArguments readerArguments)
        {
            //TODO: Add DB connection logic
            return Implement(readerArguments);
        }

        protected abstract TReaderResult Implement(TReaderArguments readerArguments);
    }
}
