using Cohesion.Core.ServiceRequest.Exceptions;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequestById;
using Moq;
using System;
using Xunit;

namespace Cohesion.Core.ServiceRequest.Test
{
    public class ServiceRequestByIdQueryHandlerTests
    {
        [Fact]
        public void ServiceRequestByIdQueryHandlerTests_ShouldSuccess()
        {
            //arrange
            var id = Guid.NewGuid();

            var serviceRequestReaderHandler = new Mock<IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result>>();
            serviceRequestReaderHandler.Setup(x => x.Read(It.IsAny<ServiceRequestByIdQueryHandler.Arguments>())).Returns(new ServiceRequestByIdQueryHandler.Result { Id = id });

            //act
            var result = new ServiceRequestByIdQueryHandler.Handler(serviceRequestReaderHandler.Object).Query(new ServiceRequestByIdQueryHandler.Arguments());

            //assert
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void ServiceRequestByIdQueryHandlerTests_ShouldFail()
        {
            //arrange
            var id = Guid.NewGuid();

            var serviceRequestReaderHandler = new Mock<IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result>>();
            serviceRequestReaderHandler.Setup(x => x.Read(It.IsAny<ServiceRequestByIdQueryHandler.Arguments>())).Returns((ServiceRequestByIdQueryHandler.Result) null);

            //act
            Action act = () => new ServiceRequestByIdQueryHandler.Handler(serviceRequestReaderHandler.Object).Query(new ServiceRequestByIdQueryHandler.Arguments());

            //assert
            Assert.Throws<ServiceRequestNotFoundException>(act);
        }
    }
}
