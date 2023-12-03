using IdentityModel.Client;
using UdemyMicroservices.Shared.Dtos;
using UdemyMicroservices.Web.Models;

namespace UdemyMicroservices.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
