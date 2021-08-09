namespace Cohesion.Core.ServiceRequest.Utilities
{
    public interface IEmailUtility
    {
        void SendEmail(string subject, string body, string email);
    }
}
