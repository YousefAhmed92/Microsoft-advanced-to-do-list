namespace MyTasks01.Interfaces
{
    public interface IEmailInterface
    {
        Task SendEmailService(string toEmail, string subject, string body);
    }
}
