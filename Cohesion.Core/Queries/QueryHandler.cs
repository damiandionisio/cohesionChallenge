namespace Cohesion.Core
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TResult: new()
    {
        public TResult Query(TQuery queryArguments)
        {
            var validationResult = Validate(queryArguments);
            if (!validationResult.HasErrors)
            {
                return Implement(queryArguments);
            }

            throw new UIValidationException(validationResult);
        }
        protected abstract TResult Implement(TQuery queryArguments);
        protected abstract ValidationResult Validate(TQuery queryArguments);
    }
}
