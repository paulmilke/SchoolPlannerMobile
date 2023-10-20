using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace MobileApp_C971_LAP2_PaulMilke
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}