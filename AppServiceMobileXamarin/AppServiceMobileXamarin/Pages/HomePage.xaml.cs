using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppServiceMobileXamarin.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            productsButton.Clicked += ProductsButton_Clicked;
            serviciesButton.Clicked += ServiciesButton_Clicked;
            queriesButton.Clicked += QueriesButton_Clicked;
        }

        private async void QueriesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QueriesPage());
        }

        private async void ServiciesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ServicesPage());
        }

        private async void ProductsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductsPage());
        }
    }
}
