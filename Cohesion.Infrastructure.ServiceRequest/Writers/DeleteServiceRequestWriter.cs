using Cohesion.Core;
using Cohesion.Core.ServiceRequest.Commands.Delete;
using Cohesion.Infrastructure.Db;
using System.Linq;

namespace Cohesion.Infrastructure.ServiceRequest.Writers
{
    public class DeleteServiceRequestWriter : WriterHandler<DeleteServiceRequestCommandHandler.DeleteArguments>
    {
        protected override CommandResult Implement(DeleteServiceRequestCommandHandler.DeleteArguments commandArguments)
        {
            var result = Database.ServiceRequestList.FirstOrDefault(x => x.Id == commandArguments.Id);

            Database.ServiceRequestList.Remove(result);

            return new CommandResult();
        }
    }
}
