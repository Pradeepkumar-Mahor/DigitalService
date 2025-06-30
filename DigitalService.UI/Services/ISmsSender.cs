using System.Threading.Tasks;

namespace DigitalService.UI.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
