using Cohesion.Core.ServiceRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cohesion.Core.ServiceRequest.Queries.GetServiceRequests
{
    public static class ServiceRequestQueryHandler
    {
        public class Arguments
        {
        }

        public class Result 
        {
            public IEnumerable<ServiceResult> ServiceResults { get; set; }

            public class ServiceResult: Auditable
            {
                public string BuildingCode { get; set; }
                public string Description { get; set; }
                public CurrentStatus CurrentStatus { get; set; }
            }
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

                if (result.ServiceResults.Count() == 0)
                {
                    throw new KeyNotFoundException();
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
