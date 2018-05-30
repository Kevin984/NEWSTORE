using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NewOnlineStore.Models
{
    [DataContract]

    public class Product
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public Product(int id, string name, double price)
        {
            ID = id;
            Name = name;
            Price = price;
        }
        public int ID { get; set; }

     //   public int StockID { get; set; }

      //  public virtual Stock Stock { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

    }
}
