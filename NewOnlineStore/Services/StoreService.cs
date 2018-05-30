using NewOnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NewOnlineStore.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StoreService" in both code and config file together.
    public class StoreService : IStoreService
    {
        private DbContext db = new DbContext();
        public void BuyProduct(Product product , string username, string password)
        {
            Customer customer = CheckUserCredentials(username, password);
            if (customer != null)
            {
                double price = product.Price;
                //         Stock stock = db.Stocks.First(i => i.Product == product);
                Stock stock = db.FindStockByProductId(product.ID);
                bool productIsInStock = (stock != null && stock.Amount >= 1);
                bool customerHasSufficientBalance = customer.Balance >= price;
                if (productIsInStock && customerHasSufficientBalance)
                {
                    stock.Amount -= 1;
                    db.UpdateStock(stock);
                    customer.Balance -= price;
                    db.UpdateCustomer(customer);
                    Transaction transaction = new Transaction(customer, product);
                    db.InsertTransaction(transaction);
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public List<Product> GetAllProducts(string username, string password)
        {
            Customer customer = CheckUserCredentials(username, password);
            if (customer == null)
            {
               List<int> productIDs = db.GetAllStocks().Where(s => s.Amount > 0).Select(s => s.Product.ID).ToList();
                List<Product> products = db.GetAllProducts();
     
               foreach (Product product in products.ToList())
                {

                if (!productIDs.Contains(product.ID))
                    {
                        products.Remove(product);
                    }
               }
                return products;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public double GetCustomerBalance(string username, string password)
        {
            Customer customer = CheckUserCredentials(username, password);
            if (customer != null)
            {
                return customer.Balance;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public List<Transaction> GetCustomerBuyHistory(string username, string password)
        {
            Customer customer = CheckUserCredentials(username, password);
            if (customer != null)
            {
                //           return db.Transactions.Where(t => t.CustomerID == customer.ID).ToList();
                return db.GetTransactionsByCustomer(customer); 
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public bool Login(string username, string password)
        {
            Customer customer = CheckUserCredentials(username, password);
            if (customer != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Register(string username)
        {
                 Customer customer = db.FindCustomerByUsername(username);
                 if (customer == null)
                 {
                     char[] usernameArray = username.ToCharArray();
                     Array.Reverse(usernameArray);
                     string password = new string(usernameArray);
                     Customer newCustomer = new Customer(username, password, 5.00);
                     db.InsertCustomer(newCustomer);
                     return password;
                 }
                 else
                 {
                     return null;
                 }
        }

        public Customer CheckUserCredentials(string username, string password)
        {
            //  return db.Customers.First(i => i.Username == username && i.Password == password);
            return db.FindCustomerByCredentials(username, password);
        }
    }
}
