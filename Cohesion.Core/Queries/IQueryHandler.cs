namespace Cohesion.Core
{
    public interface IQueryHandler<TQuery, TResult>
    {
        TResult Query(TQuery queryArguments);
    }
}
