using Pet_Need_Notifier.Services;
using Pet_Need_Notifier.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pet_Need_Notifier
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
