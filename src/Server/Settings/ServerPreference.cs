using GrowManager.Shared.Constants.Localization;
using GrowManager.Shared.Settings;
using System.Linq;

namespace GrowManager.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}