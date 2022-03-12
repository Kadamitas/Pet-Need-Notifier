using Pet_Need_Notifier.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Pet_Need_Notifier.ViewModels
{
    public class Login_Info_ViewModel : BaseViewModel
    {
        public Command About_Jump { get; }

        public Login_Info_ViewModel()
        {
            About_Jump = new Command(AboutJumpClicked);
        }

        private async void AboutJumpClicked(object obj)
        {
            if (true){
                //Later add query for data to be autofu
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            } else{

            }

        }
    }
}
