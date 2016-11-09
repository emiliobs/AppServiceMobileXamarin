using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace AppServiceMobileXamarin.Classes
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int  ProductId { get; set; }

        [Unique]
        public String Description { get; set; }

        public decimal Price  { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Service> Services { get; set; }


        public override int GetHashCode()
        {
            return ProductId;
        }

        public override string ToString()
        {
            return $"{ProductId} {Description} {Price:C2}";
        }
    }
}
