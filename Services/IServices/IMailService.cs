namespace CityInfo.API.Services.IServices
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}