using Cohesion.Core.ServiceRequest.Exceptions;
using Cohesion.Core.ServiceRequest.Models;
using System;

namespace Cohesion.Core.ServiceRequest.Queries.GetServiceRequestById
{
    public static class ServiceRequestByIdQueryHandler
    {
        public class Arguments
        {
            public Guid Id { get; set; }
        }

        public class Result : Auditable
        {
            public string BuildingCode { get; set; }
            public string Description { get; set; }
            public CurrentStatus CurrentStatus { get; set; }
        }

        public class Handler : QueryHandler<Arguments, Result>
        {
            private readonly IReaderHandler<Arguments, Result> reader;
            public Handler(IReaderHandler<Arguments, Result> reader)
            {
                this.reader = reader;
            }

            protected override Result Implement(Arguments queryArguments)
            {
                var result = reader.Read(queryArguments);

                if (result == null)
                {
                    throw new ServiceRequestNotFoundException();
                }

                return result;
            }

            protected override ValidationResult Validate(Arguments queryArguments)
            {
                return ValidationResult.NoErrors;
            }
        }
    }
}
