using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServiceMobileXamarin.Cells;
using AppServiceMobileXamarin.Classes;
using AppServiceMobileXamarin.Data;
using Xamarin.Forms;

namespace AppServiceMobileXamarin.Pages
{
    public partial class QueriesPage : ContentPage
    {
        public QueriesPage()
        {
            InitializeComponent();

            serviceListView.ItemTemplate = new DataTemplate(typeof(ServiceCell));
            serviceListView.RowHeight = 70;

            LoadServices();

            dateDatePicker.DateSelected += DateDatePicker_DateSelected;
        }

        private void DateDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
           LoadServices();
        }

        private async void LoadServices()
        {
            using (var da = new DataAccess())
            {
                //var list = da.GetList<Service>(true).Where(s => s.DateService.Date == dateDatePicker.Date).ToList();

                var list = da.GetList<Service>(true)
                    .Where(s => s.DateService.Year == dateDatePicker.Date.Year &&
                                s.DateService.Month == dateDatePicker.Date.Month &&
                                s.DateService.Day == dateDatePicker.Date.Day).ToList();


                var total = list.Sum(l => l.Value);
                serviceListView.ItemsSource = list;

                totalEntry.Text = $"{total:C2}";
            }
        }
    }
}
