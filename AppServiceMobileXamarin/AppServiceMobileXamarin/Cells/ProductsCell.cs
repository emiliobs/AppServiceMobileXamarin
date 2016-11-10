using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppServiceMobileXamarin.Cells
{
    public class ProductsCell : ViewCell
    {
        public ProductsCell()
        {
            var descriptionLabel = new Label
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes =  FontAttributes.Bold,
                TextColor =  Color.Red
            };

            descriptionLabel.SetBinding(Label.TextProperty, new Binding("Description"));

            var priceLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions =  LayoutOptions.Center,
                TextColor = Color.Red
            };

            priceLabel.SetBinding(Label.TextProperty, new Binding("Price", stringFormat: "{0:C2}"));

            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                 descriptionLabel, priceLabel
                }
            };
        }
    }
}
