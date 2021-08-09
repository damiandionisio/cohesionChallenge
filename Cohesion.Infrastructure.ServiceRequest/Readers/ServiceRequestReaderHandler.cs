using Cohesion.Core;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequests;
using Cohesion.Infrastructure.Db;
using System.Linq;

namespace Cohesion.Infrastructure.ServiceRequest.Readers
{
    public class ServiceRequestReaderHandler : ReaderHandler<ServiceRequestQueryHandler.Arguments, ServiceRequestQueryHandler.Result>
    {
        protected override ServiceRequestQueryHandler.Result Implement(ServiceRequestQueryHandler.Arguments readerArguments)
        {
            return new ServiceRequestQueryHandler.Result
            {
                ServiceResults = Database.ServiceRequestList.Select(x => new ServiceRequestQueryHandler.Result.ServiceResult
                {
                    Id = x.Id,
                    BuildingCode = x.BuildingCode,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    CurrentStatus = x.CurrentStatus,
                    Description = x.Description,
                    LastModifiedBy = x.LastModifiedBy,
                    LastModifiedDate = x.LastModifiedDate
                })
            };
        }
    }
}
