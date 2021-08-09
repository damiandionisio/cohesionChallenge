namespace Cohesion.Core
{
    public interface IReaderHandler<TReaderArguments, TReaderResult>
    {
        TReaderResult Read(TReaderArguments readerArguments);
    }
}
