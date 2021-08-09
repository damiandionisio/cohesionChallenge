using Cohesion.Core;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequestById;
using Cohesion.Infrastructure.Db;
using System.Linq;

namespace Cohesion.Infrastructure.ServiceRequest.Readers
{
    public class ServiceRequestByIdReaderHandler : ReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result>
    {
        protected override ServiceRequestByIdQueryHandler.Result Implement(ServiceRequestByIdQueryHandler.Arguments readerArguments)
        {
            return Database.ServiceRequestList.Where(x => x.Id == readerArguments.Id).Select(x => new ServiceRequestByIdQueryHandler.Result
            {
                Id = x.Id,
                BuildingCode = x.BuildingCode,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                CurrentStatus = x.CurrentStatus,
                Description = x.Description,
                LastModifiedBy = x.LastModifiedBy,
                LastModifiedDate = x.LastModifiedDate
            }).FirstOrDefault();
        }
    }
}
