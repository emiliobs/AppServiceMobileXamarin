using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace AppServiceMobileXamarin.Classes
{
   public class Service
    {
        [PrimaryKey, AutoIncrement]
        public int ServiceId { get; set; }

        public DateTime DateService { get; set; }

        public DateTime DateRegistered { get; set; }

        public int  ProductId { get; set; }

        [ManyToOne]
        public Product Product { get; set; }

        public decimal Price { get; set; }

        public double Quantity { get; set; }

        public decimal Value
        {
            get
            {
                return  Price * (decimal) Quantity;
            }
        }


        public override int GetHashCode()
        {
            return ServiceId;
        }

        public override string ToString()
        {
            return $"{ServiceId} {DateService:d} {Value:C2}";
        }
    }
}
