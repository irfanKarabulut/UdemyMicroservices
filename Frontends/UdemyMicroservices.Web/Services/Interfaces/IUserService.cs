using UdemyMicroservices.Web.Models;

namespace UdemyMicroservices.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}