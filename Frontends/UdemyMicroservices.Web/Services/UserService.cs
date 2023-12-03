using UdemyMicroservices.Web.Models;
using UdemyMicroservices.Web.Services.Interfaces;

namespace UdemyMicroservices.Web.Services.Interfaces
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserViewModel> GetUser()
        {
            return await _httpClient.GetFromJsonAsync<UserViewModel>("/api/users/getuser");
        }
    }
}
