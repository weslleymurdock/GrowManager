using Blazored.LocalStorage;
using GrowManager.Client.Infrastructure.Managers.Preferences;
using GrowManager.Client.Infrastructure.Settings;
using GrowManager.Shared.Constants.Storage;
using GrowManager.Shared.Settings;
using GrowManager.Shared.Wrapper;
using Microsoft.Extensions.Localization;
using MudBlazor; 

namespace Client.App.Data;

public class AppPreferenceManager : IClientPreferenceManager
{
    private readonly IRepository<ClientPreference> _localStorageService;
    private readonly IStringLocalizer<ClientPreferenceManager> _localizer;

    public AppPreferenceManager(
        IRepository<ClientPreference> localStorageService,
        IStringLocalizer<ClientPreferenceManager> localizer)
    {
        _localStorageService = localStorageService;
        _localizer = localizer;
    }

    public async Task<bool> ToggleDarkModeAsync()
    {
        var preference = await GetPreference() as ClientPreference;
        if (preference != null)
        {
            preference.IsDarkMode = !preference.IsDarkMode;
            await SetPreference(preference);
            return !preference.IsDarkMode;
        }

        return false;
    }
    public async Task<bool> ToggleLayoutDirection()
    {
        var preference = await GetPreference() as ClientPreference;
        if (preference != null)
        {
            preference.IsRTL = !preference.IsRTL;
            await SetPreference(preference);
            return preference.IsRTL;
        }
        return false;
    }

    public async Task<IResult> ChangeLanguageAsync(string languageCode)
    {
        var preference = await GetPreference() as ClientPreference;
        if (preference != null)
        {
            preference.LanguageCode = languageCode;
            await SetPreference(preference);
            return new Result
            {
                Succeeded = true,
                Messages = new List<string> { _localizer["Client Language has been changed"] }
            };
        }

        return new Result
        {
            Succeeded = false,
            Messages = new List<string> { _localizer["Failed to get client preferences"] }
        };
    }

    public async Task<MudTheme> GetCurrentThemeAsync()
    {
        var preference = await GetPreference() as ClientPreference;
        if (preference != null)
        {
            if (preference.IsDarkMode == true) return BlazorHeroTheme.DarkTheme;
        }
        return BlazorHeroTheme.DefaultTheme;
    }
    public async Task<bool> IsRTL()
    {
        var preference = await GetPreference() as ClientPreference;
        if (preference != null)
        {
            if (preference.IsDarkMode == true) return false;
        }
        return preference.IsRTL;
    }

    public async Task<IPreference> GetPreference()
    {
        return await _localStorageService.GetItemAsync<ClientPreference>(x => x.Preference == StorageConstants.Local.Preference) ?? new ClientPreference();
    }

    public async Task SetPreference(IPreference preference)
    {
        _ = await _localStorageService.SetItemAsync(preference as ClientPreference);
    }
}