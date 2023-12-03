namespace UdemyMicroservices.Web.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetTokenAsync();
    }
}
