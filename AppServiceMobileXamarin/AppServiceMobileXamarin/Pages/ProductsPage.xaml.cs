using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AppServiceMobileXamarin.Cells;
using AppServiceMobileXamarin.Classes;
using AppServiceMobileXamarin.Data;
using Xamarin.Forms;

namespace AppServiceMobileXamarin.Pages
{
    public partial class ProductsPage : ContentPage
    {
        public ProductsPage()
        {
            InitializeComponent();

            productsListView.ItemTemplate = new DataTemplate(typeof(ProductsCell));

            addButton.Clicked += AddButton_Clicked;
            productsListView.ItemSelected += ProductsListView_ItemSelected;
        }

        private async void ProductsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditProductsPages((Product) e.SelectedItem));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var db = new DataAccess())
            {
                productsListView.ItemsSource = db.GetList<Product>(false).OrderBy(p => p.Description);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(descritionEntry.Text))
            {
                await DisplayAlert("Error", "You must enter a description", "Acept");
                descritionEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(priceEntry.Text))
            {
                await DisplayAlert("Error", "You must enter a price", "Acept");
                priceEntry.Focus();
                return;
            }

            var price = decimal.Parse(priceEntry.Text);

            if (price < 0)
            {
                await DisplayAlert("Error", "the price must be a value greather or equals to zero.", "Acept");
                return;
            }

            var product = new Product
            {
                Description =  descritionEntry.Text,
                Price =  price
            };

            using (var db = new DataAccess())
            {
                db.Insert(product);

                productsListView.ItemsSource = db.GetList<Product>(false).OrderBy(p => p.Description);
            }

            descritionEntry.Text= String.Empty;
            priceEntry.Text  = String.Empty;

            await DisplayAlert("Message", "The product was added successfully", "Acept");
        }
    }
}
