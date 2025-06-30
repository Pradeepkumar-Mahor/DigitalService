using System.Threading.Tasks;

namespace DigitalService.UI.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}