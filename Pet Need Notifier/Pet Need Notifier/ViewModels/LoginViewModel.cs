using Pet_Need_Notifier.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pet_Need_Notifier.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            
            if (Preferences.Get("keeplogged", false) == true )
            {

                await Shell.Current.GoToAsync($"//{nameof(Adder)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(Login_Info)}");
            }
        }
    }
}
