using Cohesion.Core.ServiceRequest.Commands.Update;
using Cohesion.Core.ServiceRequest.Exceptions;
using Cohesion.Core.ServiceRequest.Models;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequestById;
using Cohesion.Core.ServiceRequest.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cohesion.Core.ServiceRequest.Test
{
    public class UpdateServiceRequestCommandHandlerTests
    {
        [Fact]
        public void UpdateServiceRequestCommandHandler_ShouldSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            var serviceRequestReaderHandler = new Mock<IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result>>();
            serviceRequestReaderHandler.Setup(x => x.Read(It.IsAny<ServiceRequestByIdQueryHandler.Arguments>())).Returns(new ServiceRequestByIdQueryHandler.Result { Id = id });

            var updateWriterHandler = new Mock<IWriterHandler<UpdateServiceRequestCommandHandler.UpdateArguments>>();
            updateWriterHandler.Setup(x => x.Write(It.IsAny<UpdateServiceRequestCommandHandler.UpdateArguments>())).Returns(CommandResult.None);

            var serviceRequestStatus = new Mock<IEnumerable<IServiceRequestStatus>>();
            var items = new List<IServiceRequestStatus>();

            serviceRequestStatus.Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

            //act
            var result = new UpdateServiceRequestCommandHandler.Handler(updateWriterHandler.Object, serviceRequestReaderHandler.Object, serviceRequestStatus.Object).Execute(new UpdateServiceRequestCommandHandler.UpdateArguments { Id = id });

            //assert
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void UpdateServiceRequestCommandHandler_CompleteRequestService_ShouldSendEmail()
        {
            //arrange
            var id = Guid.NewGuid();
            var serviceRequestReaderHandler = new Mock<IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result>>();
            serviceRequestReaderHandler.Setup(x => x.Read(It.IsAny<ServiceRequestByIdQueryHandler.Arguments>())).Returns(new ServiceRequestByIdQueryHandler.Result { Id = id });

            var updateWriterHandler = new Mock<IWriterHandler<UpdateServiceRequestCommandHandler.UpdateArguments>>();
            updateWriterHandler.Setup(x => x.Write(It.IsAny<UpdateServiceRequestCommandHandler.UpdateArguments>())).Returns(CommandResult.None);

            var emailUtility = new Mock<IEmailUtility>();
            emailUtility.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            var serviceRequestStatus = new List<IServiceRequestStatus>
            {
                new CompleteServiceRequestStatus(emailUtility.Object)
            };
            
            //act
            var result = new UpdateServiceRequestCommandHandler.Handler(updateWriterHandler.Object, serviceRequestReaderHandler.Object, serviceRequestStatus).Execute(new UpdateServiceRequestCommandHandler.UpdateArguments { Id = id, CurrentStatus = CurrentStatus.Complete });

            //assert
            Assert.Equal(id, result.Id);
            emailUtility.Verify(m => m.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void UpdateServiceRequestCommandHandler_ShouldFail()
        {
            //arrange
            var id = Guid.NewGuid();
            var serviceRequestReaderHandler = new Mock<IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result>>();
            serviceRequestReaderHandler.Setup(x => x.Read(It.IsAny<ServiceRequestByIdQueryHandler.Arguments>())).Returns((ServiceRequestByIdQueryHandler.Result)null);

            var updateWriterHandler = new Mock<IWriterHandler<UpdateServiceRequestCommandHandler.UpdateArguments>>();
            updateWriterHandler.Setup(x => x.Write(It.IsAny<UpdateServiceRequestCommandHandler.UpdateArguments>())).Returns(CommandResult.None);

            var serviceRequestStatus = new Mock<IEnumerable<IServiceRequestStatus>>();
            var items = new List<IServiceRequestStatus>();

            serviceRequestStatus.Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

            //act
            Action act = () => new UpdateServiceRequestCommandHandler.Handler(updateWriterHandler.Object, serviceRequestReaderHandler.Object, serviceRequestStatus.Object).Execute(new UpdateServiceRequestCommandHandler.UpdateArguments { Id = id });

            //assert
            Assert.Throws<ServiceRequestNotFoundException>(act);
        }
    }
}
