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
        }

        private async void ProductsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductsPage());
        }
    }
}
