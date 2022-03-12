using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Pet_Need_Notifier.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            test_label.Text = Preferences.Get("test_text", string.Empty);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("test_text", test_label.Text);
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Preferences.Clear();
        }
    }
}