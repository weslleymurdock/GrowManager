using GrowManager.Shared.Constants.Localization;
using GrowManager.Shared.Settings;
using System.Linq;

namespace GrowManager.Client.Infrastructure.Settings
{
    public record ClientPreference : IPreference
    {
        public string UserImageURL { get; set; }
        public string Preference { get; set; }
        public bool IsDarkMode { get; set; }
        public bool IsRTL { get; set; }
        public bool IsDrawerOpen { get; set; }
        public string PrimaryColor { get; set; }
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
    }
}