using Client.App.Data;
using GrowManager.Client.Infrastructure.Authentication;
using GrowManager.Client.Infrastructure.Managers.ExtendedAttribute;
using GrowManager.Client.Infrastructure.Managers.Preferences;
using GrowManager.Client.Infrastructure.Managers;
using GrowManager.Domain.Entities.ExtendedAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Reflection;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using MudBlazor.Services;
using MudBlazor;
using GrowManager.Shared.Constants.Permission;
using GrowManager.Domain.Entities.Misc;
using GrowManager.Client.Infrastructure.Settings;

namespace Client.App
{
    public static partial class MauiProgram
    {
        private const string ClientName = "GrowManager.API";
        public const string BaseAddress = "https://growmanagerappservice.azurewebsites.net/";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                })
                .UseDatabase(settings =>
                {
                    settings.FileName = "GrowManager.db3";
                    settings.Tables.Add<ClientPreference>();
                });

#if DEBUG
            // Caution: Recommended to enable Developer Tools only for development!!!
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services
                .AddSingleton<MainViewModel>()
                .AddSingleton<MainPage>()
                .AddSingleton(AppInfo.Current)
                .AddSingleton<AppPreferenceManager>()
                .AddLocalization(options =>
                {
                    options.ResourcesPath = "Resources";
                })
                .AddAuthorizationCore(options =>
                {
                    RegisterPermissionClaims(options);
                })
                .AddMudServices(configuration =>
                {
                    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                    configuration.SnackbarConfiguration.HideTransitionDuration = 100;
                    configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
                    configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
                    configuration.SnackbarConfiguration.ShowCloseIcon = true;
                })
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<Infrastructure.Authentication.GrowManagerAppStateProvider>()
                .AddScoped<AuthenticationStateProvider, Infrastructure.Authentication.GrowManagerAppStateProvider>()
                .AddManagers()
                .AddExtendedAttributeManagers()
                .AddTransient<Infrastructure.Authentication.AuthenticationHeaderHandler>()
                .AddTransient<GrowManager.Client.Infrastructure.Managers.Identity.Authentication.IAuthenticationManager, Infrastructure.Authentication.AuthenticationManager>()
                .AddTransient<AppPreferenceManager>()
                .AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(ClientName).EnableIntercept(sp))
                .AddHttpClient(ClientName, client =>
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
                    client.BaseAddress = new Uri(BaseAddress);
                })
                .AddHttpMessageHandler<Infrastructure.Authentication.AuthenticationHeaderHandler>();
            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddMauiBlazorWebView();
            return builder.Build();
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            var managers = typeof(IManager);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service) && type.Service.Name != "IAuthenticationManager")
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }

        public static IServiceCollection AddExtendedAttributeManagers(this IServiceCollection services)
        {
            //TODO - add managers with reflection!

            return services
                .AddTransient(typeof(IExtendedAttributeManager<int, int, Document, DocumentExtendedAttribute>), typeof(ExtendedAttributeManager<int, int, Document, DocumentExtendedAttribute>));
        }

        private static void RegisterPermissionClaims(AuthorizationOptions options)
        {
            foreach (var prop in typeof(GrowManager.Shared.Constants.Permission.Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    options.AddPolicy(propertyValue.ToString()!, policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue.ToString()!));
                }
            }
        }
    }
}
