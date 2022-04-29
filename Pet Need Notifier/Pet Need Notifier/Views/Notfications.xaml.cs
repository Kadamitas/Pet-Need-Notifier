using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pet_Need_Notifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View1 : ContentView
    {
        public View1()
        {
            InitializeComponent();
        }
        /*async void Hold_Notification(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Are you sure you want to delete this Notification", "Cancel", "Delete");

            if (action == "Delete")
            {
                Notifications.Remove((Notification)((MenuItem)sender).CommandParameter);
            }
        }
        */
    }
}