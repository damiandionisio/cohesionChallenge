using Cohesion.Core.ServiceRequest.Exceptions;
using Cohesion.Core.ServiceRequest.Models;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequestById;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cohesion.Core.ServiceRequest.Commands.Update
{
    public static class UpdateServiceRequestCommandHandler
    {
        public class UpdateArguments
        {
            public Guid Id { get; set; }
            public string BuildingCode { get; set; }
            public string Description { get; set; }
            public CurrentStatus CurrentStatus { get; set; }
            public string ModifiedBy { get; set; }
        }

        public class Handler : CommandHandler<UpdateArguments>
        {
            private readonly IWriterHandler<UpdateArguments> serviceRequestWriterHandler;
            private readonly IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result> serviceRequestReaderHandler;
            private readonly IEnumerable<IServiceRequestStatus> serviceRequestStatus;

            public Handler(IWriterHandler<UpdateArguments> serviceAccountWriterHandler,
                IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result> serviceRequestReaderHandler,
                IEnumerable<IServiceRequestStatus> serviceRequestStatus)
            {
                this.serviceRequestWriterHandler = serviceAccountWriterHandler;
                this.serviceRequestReaderHandler = serviceRequestReaderHandler;
                this.serviceRequestStatus = serviceRequestStatus;
            }
            protected override CommandResult Implement(UpdateArguments commandArguments)
            {
                serviceRequestWriterHandler.Write(commandArguments);

                var step = serviceRequestStatus.FirstOrDefault(x => x.Status == commandArguments.CurrentStatus);
                step?.Handle();

                return new CommandResult { Id = commandArguments.Id };
            }

            protected override ValidationResult Validate(UpdateArguments commandArguments)
            {
                var result = serviceRequestReaderHandler.Read(new ServiceRequestByIdQueryHandler.Arguments { Id = commandArguments.Id });
                if (result == null)
                {
                    throw new ServiceRequestNotFoundException();
                }

                return ValidationResult.NoErrors;
            }
        }
    }
}
