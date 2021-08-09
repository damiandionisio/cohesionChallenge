namespace Cohesion.Core.ServiceRequest.Commands.Create
{
    public static class CreateServiceRequestCommandHandler
    {
        public class Arguments
        {
            public string CreatedBy { get; set; }
            public string BuildingCode { get; set; }
            public string Description { get; set; }
        }

        public class Handler : CommandHandler<Arguments>
        {
            private readonly IWriterHandler<Arguments> serviceAccountWriterHandler;
            public Handler(IWriterHandler<Arguments> serviceAccountWriterHandler)
            {
                this.serviceAccountWriterHandler = serviceAccountWriterHandler;
            }
            protected override CommandResult Implement(Arguments commandArguments)
            {
                return serviceAccountWriterHandler.Write(commandArguments);
            }

            protected override ValidationResult Validate(Arguments commandArguments)
            {
                return ValidationResult.NoErrors;
            }
        }
    }
}
