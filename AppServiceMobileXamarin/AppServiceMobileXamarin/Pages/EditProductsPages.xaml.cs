using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServiceMobileXamarin.Classes;
using AppServiceMobileXamarin.Data;
using Xamarin.Forms;

namespace AppServiceMobileXamarin.Pages
{
    public partial class EditProductsPages : ContentPage
    {
        private Product product;
        public EditProductsPages(Product product)
        {
            InitializeComponent();

            this.product = product;

            descritionEntry.Text = product.Description;
            priceEntry.Text = product.Price.ToString();

            UpdateButton.Clicked += UpdateButton_Clicked;
            deleteButton.Clicked += DeleteButton_Clicked;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var rta = await DisplayAlert("Confirm", "Are you sure to delete the record?", "Yes", "No");

            if (!rta)
            {
                return;
            }

            using (var da = new DataAccess())
            {
                //PREGUNTO SI EL PRODUCTO NO ESTA RELACIONADO:
                var services = da.GetList<Service>(false).Where(s => s.ProductId == product.ProductId).FirstOrDefault();
                if (services != null)
                {
                    await DisplayAlert("Message", "The record can't be delete because it has related records", "Acept");
                    return;
                }

                da.Delete(product);
            }

            await DisplayAlert("Message", "The record was deleted", "Acept");
            await Navigation.PopAsync();
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(descritionEntry.Text))
            {
                await DisplayAlert("Error", "You must enter a description", "Acept");
                descritionEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(priceEntry.Text))
            {
                await DisplayAlert("Error", "You must enter a description", "Acept");
                priceEntry.Focus();
                return;
            }

            var price = decimal.Parse(priceEntry.Text);
            if (price < 0)
            {
                await DisplayAlert("Error", "The price must be a value greather or equals to zero.", "Acept");
                return;
            }

            product.Description = descritionEntry.Text;
            product.Price = price;

            using (var da = new DataAccess())
            {
                da.Update(product);
            }

            await DisplayAlert("Message", "The record was updated.", "Acept");
            await Navigation.PopAsync();
        }
    }
}
