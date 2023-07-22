using Blazored.LocalStorage;
using Client.App.Data;
using GrowManager.Client.Infrastructure.Settings;
using GrowManager.Shared.Constants.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.App.Infrastructure.Authentication
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly IRepository<ClientPreference> localStorage;

        public AuthenticationHeaderHandler(IRepository<ClientPreference> localStorage)
            => this.localStorage = localStorage;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization?.Scheme != "Bearer")
            {
                var preferences = await this.localStorage.GetItemAsync<ClientPreference>(x => x.Preference == StorageConstants.Local.AuthToken);

                if (preferences != null && !string.IsNullOrWhiteSpace(preferences?.AuthToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", preferences.AuthToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
