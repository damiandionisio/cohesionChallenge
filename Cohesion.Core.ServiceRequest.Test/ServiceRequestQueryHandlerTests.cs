using Cohesion.Core.ServiceRequest.Queries.GetServiceRequests;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cohesion.Core.ServiceRequest.Test
{
    public class ServiceRequestQueryHandlerTests
    {
        [Fact]
        public void ServiceRequestQueryHandlerTests_ShouldSuccess()
        {
            //arrange
            var id = Guid.NewGuid();

            var serviceRequestReaderHandler = new Mock<IReaderHandler<ServiceRequestQueryHandler.Arguments, ServiceRequestQueryHandler.Result>>();
            serviceRequestReaderHandler.Setup(x => x.Read(It.IsAny<ServiceRequestQueryHandler.Arguments>())).Returns(new ServiceRequestQueryHandler.Result
            {
                ServiceResults = new List<ServiceRequestQueryHandler.Result.ServiceResult>
                { 
                    new ServiceRequestQueryHandler.Result.ServiceResult
                    {
                        Id = id
                    }
                }
            });

            //act
            var result = new ServiceRequestQueryHandler.Handler(serviceRequestReaderHandler.Object).Query(new ServiceRequestQueryHandler.Arguments());

            //assert
            Assert.Single(result.ServiceResults);
        }

        [Fact]
        public void ServiceRequestQueryHandlerTests_ShouldFail()
        {
            //arrange
            var id = Guid.NewGuid();

            var serviceRequestReaderHandler = new Mock<IReaderHandler<ServiceRequestQueryHandler.Arguments, ServiceRequestQueryHandler.Result>>();
            serviceRequestReaderHandler.Setup(x => x.Read(It.IsAny<ServiceRequestQueryHandler.Arguments>())).Returns(new ServiceRequestQueryHandler.Result
            {
                ServiceResults = new List<ServiceRequestQueryHandler.Result.ServiceResult>()
            });

            //act
            Action act = () => new ServiceRequestQueryHandler.Handler(serviceRequestReaderHandler.Object).Query(new ServiceRequestQueryHandler.Arguments());

            //assert
            Assert.Throws<KeyNotFoundException>(act);
        }
    }
}
