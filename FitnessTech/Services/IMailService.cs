namespace FitnessTech.Services
{
    public interface IMailService
    {
        void SendMessage(string name, string email, string description);
    }
}