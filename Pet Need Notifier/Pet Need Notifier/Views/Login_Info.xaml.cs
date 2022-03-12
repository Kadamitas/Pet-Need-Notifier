using Pet_Need_Notifier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pet_Need_Notifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login_Info : ContentPage
    {
        public Login_Info()
        {
            InitializeComponent();


        }

        async void Log_In_Button(object sender, EventArgs e)
        {
            if (
            Preferences.Get("username", string.Empty).Equals(user_label.Text) &&
            Preferences.Get("password", string.Empty).Equals(pass_label.Text)
            )
            { // Change later to Query for username
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                await DisplayAlert("Invalid Login", "Username or Password Invalid", "OK");
            }

        }
        async void Sign_Up_Button(object sender, EventArgs e)
        {
            if (!Preferences.Get("username", string.Empty).Equals(user_label.Text)){ // Change later to Query for username
                Preferences.Set("username", user_label.Text);
                Preferences.Set("password", pass_label.Text);
            }
            else
            {
                await DisplayAlert("Username Exists", "Try logging in", "OK");
            }
        }
    }
}