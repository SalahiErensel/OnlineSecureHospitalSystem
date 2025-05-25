using Blazored.LocalStorage;
using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Authorization
{
    public class AuthorizationService
    {
        private readonly ILocalStorageService _localStorage;

        public AuthorizationService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public AuthenticatedUser? CurrentUser { get; set; }

        public async Task SetUserAsync(AuthenticatedUser user)
        {
            CurrentUser = user;
            await _localStorage.SetItemAsync("authUser", user);
        }

        public async Task LoadUserAsync()
        {
            var user = await _localStorage.GetItemAsync<AuthenticatedUser>("authUser");
            CurrentUser = user;
        }

        public async Task ClearUserAsync()
        {
            CurrentUser = null;
            await _localStorage.RemoveItemAsync("authUser");
        }

        public bool IsInRole(string roleName)
        {
            return CurrentUser?.RoleName == roleName;
        }
    }
}