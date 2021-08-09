using Cohesion.Core.ServiceRequest.Commands.Delete;
using Cohesion.Core.ServiceRequest.Exceptions;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequestById;
using Moq;
using System;
using Xunit;

namespace Cohesion.Core.ServiceRequest.Test
{
    public class DeleteServiceRequestCommandHandlerTests
    {
        [Fact]
        public void DeleteServiceRequestCommandHandler_ShouldSuccess()
        {
            //arrange
            var id = Guid.NewGuid();
            var serviceRequestReaderHandler = new Mock<IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result>>();
            serviceRequestReaderHandler.Setup(x => x.Read(It.IsAny<ServiceRequestByIdQueryHandler.Arguments>())).Returns(new ServiceRequestByIdQueryHandler.Result { Id = id });

            var deleteWriterHandler = new Mock<IWriterHandler<DeleteServiceRequestCommandHandler.DeleteArguments>>();
            deleteWriterHandler.Setup(x => x.Write(It.IsAny<DeleteServiceRequestCommandHandler.DeleteArguments>())).Returns(CommandResult.None);

            //act
            var result = new DeleteServiceRequestCommandHandler.Handler(deleteWriterHandler.Object, serviceRequestReaderHandler.Object).Execute(new DeleteServiceRequestCommandHandler.DeleteArguments { Id = id });

            //assert
            Assert.Equal(CommandResult.None, result);

        }

        [Fact]
        public void DeleteServiceRequestCommandHandler_ShouldFail()
        {
            //arrange
            var id = Guid.NewGuid();
            var serviceRequestReaderHandler = new Mock<IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result>>();
            serviceRequestReaderHandler.Setup(x => x.Read(It.IsAny<ServiceRequestByIdQueryHandler.Arguments>())).Returns((ServiceRequestByIdQueryHandler.Result)null);

            var deleteWriterHandler = new Mock<IWriterHandler<DeleteServiceRequestCommandHandler.DeleteArguments>>();
            deleteWriterHandler.Setup(x => x.Write(It.IsAny<DeleteServiceRequestCommandHandler.DeleteArguments>())).Returns(CommandResult.None);

            //act
            Action act = () => new DeleteServiceRequestCommandHandler.Handler(deleteWriterHandler.Object, serviceRequestReaderHandler.Object).Execute(new DeleteServiceRequestCommandHandler.DeleteArguments { Id = id });

            //assert
            Assert.Throws<ServiceRequestNotFoundException>(act);
        }
    }
}
