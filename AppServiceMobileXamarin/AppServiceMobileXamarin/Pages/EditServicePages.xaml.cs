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
