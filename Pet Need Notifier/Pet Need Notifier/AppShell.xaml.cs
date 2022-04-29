using Pet_Need_Notifier.ViewModels;
using Pet_Need_Notifier.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pet_Need_Notifier
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
           
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Preferences.Set("keeplogged", false);
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
