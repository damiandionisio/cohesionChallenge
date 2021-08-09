using Cohesion.Core.ServiceRequest.Commands.Create;
using Moq;
using System;
using Xunit;

namespace Cohesion.Core.ServiceRequest.Test
{
    public class CreateServiceRequestCommandHandlerTests
    {
        [Fact]
        public void UpdateServiceRequestCommandHandler_ShouldSuccess()
        {
            //arrange
            var createWriterHandler = new Mock<IWriterHandler<CreateServiceRequestCommandHandler.Arguments>>();
            createWriterHandler.Setup(x => x.Write(It.IsAny<CreateServiceRequestCommandHandler.Arguments>())).Returns(CommandResult.None);

            //act
            var result = new CreateServiceRequestCommandHandler.Handler(createWriterHandler.Object).Execute(new CreateServiceRequestCommandHandler.Arguments());

            //assert
            Assert.Equal(Guid.Empty, result.Id);
        }
    }
}
