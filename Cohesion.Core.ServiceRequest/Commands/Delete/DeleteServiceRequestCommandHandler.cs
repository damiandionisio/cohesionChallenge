using Cohesion.Core.ServiceRequest.Exceptions;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequestById;
using System;

namespace Cohesion.Core.ServiceRequest.Commands.Delete
{
    public class DeleteServiceRequestCommandHandler
    {
        public class DeleteArguments
        {
            public Guid Id { get; set; }
        }

        public class Handler : CommandHandler<DeleteArguments>
        {
            private readonly IWriterHandler<DeleteArguments> serviceRequestWriterHandler;
            private readonly IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result> serviceRequestReaderHandler;

            public Handler(IWriterHandler<DeleteArguments> serviceAccountWriterHandler,
                IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result> serviceRequestReaderHandler)
            {
                this.serviceRequestWriterHandler = serviceAccountWriterHandler;
                this.serviceRequestReaderHandler = serviceRequestReaderHandler;
            }
            protected override CommandResult Implement(DeleteArguments commandArguments)
            {
                serviceRequestWriterHandler.Write(commandArguments);

                return CommandResult.None;
            }

            protected override ValidationResult Validate(DeleteArguments commandArguments)
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
