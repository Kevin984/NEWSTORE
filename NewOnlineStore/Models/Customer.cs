using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NewOnlineStore.Models
{
    [DataContract]

    public class Customer
    {
        public Customer(string username, string password, double balance)
        {
            Username = username;
            Password = password;
            Balance = balance;
        }

        public Customer(int id, string username, string password, double balance)
        {
            ID = id;
            Username = username;
            Password = password;
            Balance = balance;
        }

        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public double Balance { get; set; }
    }
}
