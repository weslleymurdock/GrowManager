using GrowManager.Application.Interfaces.Serialization.Options;
using System.Text.Json;

namespace GrowManager.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}