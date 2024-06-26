﻿using MechanicAPP_OOP2.Components.Pages;
using MechanicAPP_OOP2.Data;
using Microsoft.Extensions.Logging;
using Utility;

namespace MechanicAPP_OOP2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>();

            builder.Services.AddMauiBlazorWebView();


    #if DEBUG
            builder.Logging.AddDebug();
    #endif

            builder.Services.AddSingleton<DatabaseContext>();
            builder.Services.AddSingleton<CustomersViewModel>();
            builder.Services.AddSingleton<Customer>();

            return builder.Build();
        }
    }
}
