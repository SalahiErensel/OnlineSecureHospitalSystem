using Microsoft.AspNetCore.Components;
using MudBlazor;
using OnlineSecureHospitalSystem.Shared;

namespace OnlineSecureHospitalSystem.Services.Authorization
{
    public class AccessControlService
    {
        private readonly AuthorizationService _authService;
        private readonly NavigationManager _navigation;
        private readonly IDialogService _dialogService;

        public AccessControlService(AuthorizationService authService, NavigationManager navigation, IDialogService dialogService)
        {
            _authService = authService;
            _navigation = navigation;
            _dialogService = dialogService;
        }

        public async Task CheckAccessAsync(bool requireDefaultPasswordUpdated, string[] allowedRoles)
        {
            await _authService.LoadUserAsync();

            var user = _authService.CurrentUser;

            if (user == null)
            {
                if (!_navigation.Uri.EndsWith("/unauthorized"))
                {
                    _navigation.NavigateTo("/unauthorized", forceLoad: true);
                }
                return;
            }

            if (allowedRoles != null && !allowedRoles.Contains(user.RoleName))
            {
                if (!_navigation.Uri.EndsWith("/unauthorized"))
                {
                    _navigation.NavigateTo("/unauthorized");
                }
                return;
            }

            if (requireDefaultPasswordUpdated && user.IsDefaultPassword == 1)
            {
                await ShowPasswordUpdateDialogAsync();
                return;
            }
        }


        public async Task ShowPasswordUpdateDialogAsync()
        {
            var parameters = new DialogParameters
    {
        { "ContentText", "You must update your default password before using the system." },
        { "ButtonText", "Update Now" },
        { "Color", Color.Warning }
    };

            var dialog = await _dialogService.ShowAsync<DefaultPasswordDialog>(
                "Password Update Required", parameters);

            var result = await dialog.Result;

            if (!result!.Canceled)
            {
                if (!_navigation.Uri.EndsWith("/default-password-update"))
                {
                    _navigation.NavigateTo("/default-password-update");
                }
            }
            else if (!_navigation.Uri.EndsWith("/unauthorized") && !_navigation.Uri.EndsWith("/"))
            {
                _navigation.NavigateTo("/");
            }
        }
    }
}