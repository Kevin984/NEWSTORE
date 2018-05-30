using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NewOnlineStore.Models
{
    [DataContract]

    public class Stock
    {
        public Stock(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }
        public Stock(int id ,Product product, int amount)
        {
            ID = id;
            Product = product;
            Amount = amount;
        }


        public int ID { get; set; }

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }

        public int Amount { get; set; }
    }
}
