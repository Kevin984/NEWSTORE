using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NewOnlineStore.Models
{
    [DataContract]

    public class Transaction
    {
        public Transaction(int id, Customer customer, Product product, double amount)
        {
            ID = id;
            Customer = customer;
            Product = product;
            Amount = amount;
        }

        public Transaction( Customer customer, Product product)
        {
            Customer = customer;
            Product = product;
            
        }

        public int ID { get; set; }

        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }

        public double Amount { get; set; }
    }
}
