using System.Threading.Tasks;

namespace Corwords.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}