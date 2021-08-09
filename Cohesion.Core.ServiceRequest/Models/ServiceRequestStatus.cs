using Cohesion.Core.ServiceRequest.Utilities;

namespace Cohesion.Core.ServiceRequest.Models
{
    public interface IServiceRequestStatus
    {
        CurrentStatus Status { get; }

        void Handle();
    }

    public class CompleteServiceRequestStatus : IServiceRequestStatus
    {
        private readonly IEmailUtility emailUtility;
        public CurrentStatus Status => CurrentStatus.Complete;

        public CompleteServiceRequestStatus(IEmailUtility emailUtility)
        {
            this.emailUtility = emailUtility;
        }

        public void Handle()
        {
            emailUtility.SendEmail(string.Empty, "Service request completed", "test@test.com");
        }
    }
}
