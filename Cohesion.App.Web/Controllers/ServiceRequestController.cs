using Cohesion.App.Web.Filters;
using Cohesion.Core;
using Cohesion.Core.ServiceRequest.Commands.Create;
using Cohesion.Core.ServiceRequest.Commands.Delete;
using Cohesion.Core.ServiceRequest.Commands.Update;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequestById;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cohesion.App.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CohesionExceptionFilter))]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IQueryHandler<ServiceRequestQueryHandler.Arguments, ServiceRequestQueryHandler.Result> serviceRequestHandler;
        private readonly IQueryHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result> serviceRequestByIdHandler;
        private readonly ICommandHandler<CreateServiceRequestCommandHandler.Arguments> createServiceRequestCommandHandler;
        private readonly ICommandHandler<UpdateServiceRequestCommandHandler.UpdateArguments> updateServiceRequestCommandHandler;
        private readonly ICommandHandler<DeleteServiceRequestCommandHandler.DeleteArguments> deleteServiceRequestCommandHandler;

        public ServiceRequestController(IQueryHandler<ServiceRequestQueryHandler.Arguments, ServiceRequestQueryHandler.Result> serviceRequestHandler,
            ICommandHandler<CreateServiceRequestCommandHandler.Arguments> createServiceRequestCommandHandler,
            IQueryHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result> serviceRequestByIdHandler,
            ICommandHandler<UpdateServiceRequestCommandHandler.UpdateArguments> updateServiceRequestCommandHandler,
            ICommandHandler<DeleteServiceRequestCommandHandler.DeleteArguments> deleteServiceRequestCommandHandler
            )
        {
            this.serviceRequestHandler = serviceRequestHandler;
            this.createServiceRequestCommandHandler = createServiceRequestCommandHandler;
            this.serviceRequestByIdHandler = serviceRequestByIdHandler;
            this.updateServiceRequestCommandHandler = updateServiceRequestCommandHandler;
            this.deleteServiceRequestCommandHandler = deleteServiceRequestCommandHandler;
        }

        [HttpGet]
        public IActionResult GetServiceRequest()
        {
            return Ok(this.serviceRequestHandler.Query(new ServiceRequestQueryHandler.Arguments()));
        }

        [HttpGet("{id}")]
        public IActionResult GetServiceRequestById(Guid id)
        {
            return Ok(this.serviceRequestByIdHandler.Query(new ServiceRequestByIdQueryHandler.Arguments { Id = id }));
        }

        [HttpPost]
        public IActionResult CreateServiceRequest(CreateServiceRequestCommandHandler.Arguments args)
        {
            return Created(string.Empty,this.createServiceRequestCommandHandler.Execute(args));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateServiceRequest(Guid id, UpdateServiceRequestCommandHandler.UpdateArguments args)
        {
            args.Id = id;

            return Ok(this.updateServiceRequestCommandHandler.Execute(args));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteServiceRequest(Guid id, DeleteServiceRequestCommandHandler.DeleteArguments args)
        {
            args.Id = id;

            return Created(string.Empty, this.deleteServiceRequestCommandHandler.Execute(args));
        }
    }
}
