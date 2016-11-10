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
    public partial class ServicesPage : ContentPage
    {
        private List<Product> products;

        public ServicesPage()
        {
            InitializeComponent();

            LoadProducts();


            quantityStepper.ValueChanged += QuantityStepper_ValueChanged;
            addButton.Clicked += AddButton_Clicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var da = new DataAccess())
            {
                serviceListView.ItemsSource = da.GetList<Service>(true)
                                                    .Where(s => s.DateRegistered.Year == DateTime.Today.Year &&
                                                           s.DateRegistered.Month == DateTime.Today.Month &&
                                                           s.DateRegistered.Day == DateTime.Today.Day)
                                                    .OrderByDescending(s => s.DateService).ToList();
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (productPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "You must select a products.", "Acept");
                productPicker.Focus();
                return;
            }


            var service = new  Service
            {

                DateRegistered = DateTime.Now,
                DateService = dateDatePicker.Date,
                ProductId = products[productPicker.SelectedIndex].ProductId,
                Price = products[productPicker.SelectedIndex].Price,
                Quantity = quantityStepper.Value


            };

            using (var da = new DataAccess())
            {
                da.Insert(service);
                serviceListView.ItemsSource = da.GetList<Service>(true)
                                              .Where(s => s.DateRegistered.Year == DateTime.Today.Year &&
                                                     s.DateRegistered.Month     == DateTime.Today.Month &&
                                                     s.DateRegistered.Day       == DateTime.Today.Day)
                                              .OrderByDescending(s =>s.DateService).ToList();


            }


            //limpiar campos:
            productPicker.SelectedIndex = -1;
            dateDatePicker.Date = DateTime.Now;
            quantityEntry.Text = "1";
            quantityStepper.Value = 1;

        }

        private void LoadProducts()
        {

            using (var da = new DataAccess())
            {
                products = da.GetList<Product>(false).OrderBy(p=>p.Description).ToList();

                foreach (var product in products)
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
