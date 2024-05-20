using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Helpers
{
    public interface IMailHelper
    {
        ActionResponse<string> SendMail(string toName, string toEmail, string subject, string body);
    }
}
