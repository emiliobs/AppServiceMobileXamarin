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
    public partial class EditServicePages : ContentPage
    {
        private Service service;
        private List<Product> Products;
        public EditServicePages(Service service)
        {
            InitializeComponent();
            this.service = service;

            LoadProducts();

            //llenos los campos del formulario:
            ShowProduct();

            quantityStepper.ValueChanged += QuantityStepper_ValueChanged;
            updateButton.Clicked += UpdateButton_Clicked;
            deleteButton.Clicked += DeleteButton_Clicked;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var rta = await DisplayAlert("Confirm", "Are you sure to delete the record.", "Yes", "No");
            if (!rta)
            {
                return;
            }

            using (var da = new DataAccess())
            {
                da.Delete(service);
            }

            await DisplayAlert("Message","The record was deleted.","Acept");
            await Navigation.PopAsync();
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            service.DateService = dateDatePicker.Date;
            service.Price = Products[productPicker.SelectedIndex].Price;
            service.ProductId = Products[productPicker.SelectedIndex].ProductId;
            service.Quantity = quantityStepper.Value;

            using (var da = new DataAccess())
            {
                da.Update(service);
            }

            await DisplayAlert("Message", "The record was updated.", "Acept");
            await Navigation.PopAsync();
        }

        private void ShowProduct()
        {
            //llenos los campos del formulario:
            dateDatePicker.Date = service.DateService;
            quantityEntry.Text = service.Quantity.ToString();
            quantityStepper.Value = service.Quantity;

            //saco el i para que quede de forma global:
            int i = 0;
            for (; i < Products.Count; i++)
            {
                if (Products[i].ProductId == service.ProductId)
                {
                    break;
                }
            }

            productPicker.SelectedIndex = i;

        }



        private void LoadProducts()
        {
            using (var da = new DataAccess())
            {
                Products = da.GetList<Product>(false).OrderBy(p => p.Description).ToList();

                foreach (var product in Products)
                {
                    productPicker.Items.Add(product.Description);
                }
            }
        }

        private void QuantityStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            quantityEntry.Text = quantityStepper.Value.ToString();
        }
    }
}
