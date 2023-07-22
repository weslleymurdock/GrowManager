using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.App.Data;

public static class DatabaseBuilderExtensions
{
    public static void Add<T>(this IList<Type> _this) where T : class
    {
        _this.Add(typeof(T));
    }

    public static MauiAppBuilder UseDatabase(this MauiAppBuilder builder, Action<IDatabaseSettings> configureDelegate)
    {
        var databaseRegistration = new DatabaseRegistration(configureDelegate);
        var databaseService = new DatabaseService(databaseRegistration.CurrentSettings);
        Task.Run(async () => {
            await databaseService.Init();
        }).GetAwaiter().GetResult();
        builder.Services.AddSingleton<IDatabaseService>(databaseService);
        builder.Services.AddTransient(typeof(IRepository<>), typeof(DefaultRepository<>));
        return builder;
    }

}