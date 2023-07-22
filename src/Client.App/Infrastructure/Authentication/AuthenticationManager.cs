using Blazored.LocalStorage;
using Client.App.Data;
using GrowManager.Application.Requests.Identity;
using GrowManager.Application.Responses.Identity;
using GrowManager.Client.Infrastructure.Authentication;
using GrowManager.Client.Infrastructure.Managers.Identity.Authentication;
using GrowManager.Client.Infrastructure.Routes;
using GrowManager.Client.Infrastructure.Settings;
using GrowManager.Shared.Constants.Storage;
using GrowManager.Shared.Wrapper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client.App.Infrastructure.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly HttpClient _httpClient;
        private readonly IRepository<ClientPreference> _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IStringLocalizer<AuthenticationManager> _localizer;

        public AuthenticationManager(
            HttpClient httpClient,
            IRepository<ClientPreference> localStorage,
            AuthenticationStateProvider authenticationStateProvider,
            IStringLocalizer<AuthenticationManager> localizer)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
            _localizer = localizer;
        }

        public async Task<ClaimsPrincipal> CurrentUser()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return state.User;
        }

        public async Task<IResult> Login(TokenRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.Get, model);
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            if (response.IsSuccessStatusCode)
            {
                var token = result?.Token;
                var refreshToken = result?.RefreshToken;
                var userImageURL = result?.UserImageURL;
                var preference = new ClientPreference() { Preference = StorageConstants.Local.AuthToken, AuthToken = token, RefreshToken = refreshToken, UserImageURL = userImageURL };
                await _localStorage.SetItemAsync(preference);
                preference.Preference = StorageConstants.Local.RefreshToken;
                await _localStorage.SetItemAsync(preference);
                if (!string.IsNullOrEmpty(userImageURL))
                {
                    preference.Preference = StorageConstants.Local.UserImageURL;
                    await _localStorage.SetItemAsync(preference);
                }
                ((GrowManagerAppStateProvider)this._authenticationStateProvider).MarkUserAsAuthenticated(model.Email);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await Result.SuccessAsync();
            }
            else
            {
                return await Result.FailAsync(await response?.Content.ReadAsStringAsync()!); ;
            }
        }

        public async Task<IResult> Logout()
        {
            await _localStorage.RemoveItemAsync(x => x.Preference == StorageConstants.Local.AuthToken);
            await _localStorage.RemoveItemAsync(x => x.Preference == StorageConstants.Local.RefreshToken);
            await _localStorage.RemoveItemAsync(x => x.Preference == StorageConstants.Local.UserImageURL);
            ((GrowManagerStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            return await Result.SuccessAsync();
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<ClientPreference>(x => x.Preference == StorageConstants.Local.AuthToken);
            var refreshToken = await _localStorage.GetItemAsync<ClientPreference>(x => x.Preference == StorageConstants.Local.RefreshToken);

            var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.Refresh, new RefreshTokenRequest { Token = token.AuthToken, RefreshToken = refreshToken.RefreshToken });

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(_localizer["Something went wrong during the refresh token action"]);
            }

            token.AuthToken = result?.Token;
            token.Preference = StorageConstants.Local.AuthToken;
            refreshToken.RefreshToken = result?.RefreshToken;
            refreshToken.Preference = StorageConstants.Local.RefreshToken;
            await _localStorage.SetItemAsync(token);
            await _localStorage.SetItemAsync(refreshToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AuthToken);
            return token!.AuthToken;
        }

        public async Task<string> TryRefreshToken()
        {
            //check if token exists
            var availableToken = await _localStorage.GetItemAsync<ClientPreference>(x => x.Preference == StorageConstants.Local.RefreshToken);
            if (string.IsNullOrEmpty(availableToken.AuthToken)) return string.Empty;
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var exp = user.FindFirst(c => c.Type.Equals("exp"))?.Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
            var timeUTC = DateTime.UtcNow;
            var diff = expTime - timeUTC;
            if (diff.TotalMinutes <= 1)
                return await RefreshToken();
            return string.Empty;
        }

        public async Task<string> TryForceRefreshToken()
        {
            return await RefreshToken();
        }
    }
}
