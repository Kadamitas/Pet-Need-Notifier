using Pet_Need_Notifier.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Pet_Need_Notifier.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}