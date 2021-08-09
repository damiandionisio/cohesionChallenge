using Cohesion.Core;
using Cohesion.Core.ServiceRequest.Commands.Create;
using Cohesion.Infrastructure.Db;

namespace Cohesion.Infrastructure.ServiceRequest.Writers
{
    public class CreateServiceRequestWriter : WriterHandler<CreateServiceRequestCommandHandler.Arguments>
    {
        protected override CommandResult Implement(CreateServiceRequestCommandHandler.Arguments commandArguments)
        {
            var serviceRequest = new Database.ServiceRequest()
            {
                CreatedBy = commandArguments.CreatedBy,
                LastModifiedBy = commandArguments.CreatedBy,
                BuildingCode = commandArguments.BuildingCode,
                CurrentStatus = Core.ServiceRequest.Models.CurrentStatus.Created,
                Description = commandArguments.Description,
            };

            Database.ServiceRequestList.Add(serviceRequest);

            return new CommandResult { Id = serviceRequest.Id };
        }
    }
}
