using Cohesion.Core;
using Cohesion.Core.ServiceRequest.Commands.Update;
using Cohesion.Infrastructure.Db;
using System.Linq;

namespace Cohesion.Infrastructure.ServiceRequest.Writers
{
    public class UpdateServiceRequestWriter : WriterHandler<UpdateServiceRequestCommandHandler.UpdateArguments>
    {
        protected override CommandResult Implement(UpdateServiceRequestCommandHandler.UpdateArguments commandArguments)
        {
            var result = Database.ServiceRequestList.FirstOrDefault(x => x.Id == commandArguments.Id);

            result.BuildingCode = commandArguments.BuildingCode;
            result.CurrentStatus = commandArguments.CurrentStatus;
            result.Description = commandArguments.Description;
            result.LastModifiedDate = System.DateTime.Now;
            result.LastModifiedBy = commandArguments.ModifiedBy;

            return new CommandResult();
        }
    }
}
