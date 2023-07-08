
using GrowManager.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace GrowManager.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}