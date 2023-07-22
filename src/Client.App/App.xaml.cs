﻿namespace Client.App
{
    public partial class App : Application
    {
        public App(IServiceProvider services)
        {
            InitializeComponent();
            
            MainPage = services.GetService<MainPage>();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Title = "Client.App";
            return window;
        }
    }
}
