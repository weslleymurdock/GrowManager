using GrowManager.Shared.Settings;
using GrowManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace GrowManager.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}