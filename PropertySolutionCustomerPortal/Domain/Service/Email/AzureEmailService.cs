using Azure;
using Azure.Communication.Email;
using PropertySolutionCustomerPortal.Domain.Service.Email.Model;

namespace PropertySolutionCustomerPortal.Domain.Service.Email
{
    public interface IAzureEmailService
    {
        Task<AzureEmailResponse> SendEmail(EmailRequest emailRequest);
    }

    public class AzureEmailService : IAzureEmailService
    {
        public async Task<AzureEmailResponse> SendEmail(EmailRequest emailRequest)
        {
            try
            {
                var ConnectionString = "endpoint=https://testeventdemo.asiapacific.communication.azure.com/;accesskey=wDtBDh6rqc/73sBdOWQ9DC7U38AS+gbW8MWqtkufPraYnDVpPasL2Z2mzRA+hix/H9VRds4D//tQZsj2su7RuA==";
                EmailClient emailClient = new EmailClient(ConnectionString);

                var subject = emailRequest.Subject;
                var htmlContent = emailRequest.Body;
                var sender = "DoNotReply@783ce854-3cf2-479f-bad3-3d70b7320010.azurecomm.net";
                var recipient = emailRequest.PrimaryRecieverAddress;

                EmailSendOperation emailSendOperation = emailClient.Send(WaitUntil.Completed, sender, recipient, subject, htmlContent);

                return new AzureEmailResponse
                {
                    OperationId = emailSendOperation.Id,
                    Status = emailSendOperation.Value.Status
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
