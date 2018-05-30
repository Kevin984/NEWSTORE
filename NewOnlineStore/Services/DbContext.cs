using MySql.Data.MySqlClient;
using NewOnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewOnlineStore.Services
{
    public class DbContext
    {
        private static string cs = @"server=localhost;userid=root;password=test;database=onlinestore";
        private MySqlConnection connection = new MySqlConnection(cs);

        public List<Product> GetAllProducts()
        {
            connection.Open();
            string SQL = "SELECT * FROM onlinestore.product";

            MySqlCommand cmd = new MySqlCommand(SQL,connection);

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Product> plist = new List<Product>();

            while (reader.Read())
            {
               int id = (int)reader["idProduct"];
               string name = reader["Name"].ToString();
               double price = (double)reader["Price"];
             //   int stockId = (int)reader["StockID"];
                Product product = new Product(id, name, price);
                plist.Add(product);
            }
            connection.Close();
            return plist;

        }


        public List<Stock> GetAllStocks()
        {
           MySqlConnection connection2 = new MySqlConnection(cs);

        connection2.Open();
            string SQL = "SELECT * FROM onlinestore.stock";

            MySqlCommand cmd = new MySqlCommand(SQL, connection2);

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Stock> slist = new List<Stock>();
            

            while (reader.Read())
            {
                int id = (int)reader["idstock"];
                int amount = (int)reader["amount"];
                int productId = (int)reader["product_ID"];
            //    connection.Close();
                Product product = FindProductById(productId);
                Stock stock = new Stock(id, product, amount);
                stock.Product = product;
                slist.Add(stock);
            }
            connection2.Close();
            return slist;

        }

        public Product FindProductById(int id)
        {
            MySqlConnection connection3 = new MySqlConnection(cs);

            connection3.Open();
            string SQL = "SELECT * FROM onlinestore.product where idProduct = " + id;

            MySqlCommand cmd = new MySqlCommand(SQL, connection3);

            MySqlDataReader reader = cmd.ExecuteReader();

            Product product = null;
            while (reader.Read())
            {
                int pid = (int)reader["idProduct"];
                string name = reader["Name"].ToString();
                double price = (double )reader["Price"];
                Product pProduct = new Product(id, name, price);
                product = pProduct;
            }
            connection3.Close();

            return product;
        }

        public Stock FindStockByProductId(int id)
        {
            connection.Open();
            string SQL = "SELECT * FROM onlinestore.stock where product_ID = " + id;

            MySqlCommand cmd = new MySqlCommand(SQL, connection);

            MySqlDataReader reader = cmd.ExecuteReader();

            Stock stock = null;
            while (reader.Read())
            {
                int id2 = (int)reader["idstock"];
                int amount = (int)reader["amount"];
                int productId = (int)reader["product_ID"];
                //    connection.Close();
                Product product2 = FindProductById(productId);
                stock = new Stock(id2, product2, amount);
                stock.Product = product2;
            }
             connection.Close();

            return stock;
        }


        public Customer FindCustomerByCredentials(string username, string password)
        {
            connection.Open();
            string SQL = "SELECT * FROM onlinestore.customer where username = '" + username+"' AND password = '"+password+"'";
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            Customer customer = null;
            while (reader.Read())
            {
                int id = (int)reader["idcustomer"];
                string username2 = reader["username"].ToString();
                string password2 = reader["password"].ToString();
                double balance = (double)reader["balance"];
                customer = new Customer(id, username2, password2, balance);
            }

            connection.Close();
            return customer;
        }

        public Customer FindCustomerByUsername(string username)
        {
            connection.Open();
            string SQL = "SELECT * FROM onlinestore.customer where username = '" + username + "'";
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            Customer customer = null;
            while (reader.Read())
            {
                int id = (int)reader["idcustomer"];
                string username2 = reader["username"].ToString();
                string password2 = reader["password"].ToString();
                double balance = (double)reader["balance"];
                customer = new Customer(id, username2, password2, balance);
            }

            connection.Close();
            return customer;
        }

        public void InsertCustomer(Customer customer)
        {
            connection.Open();
            string SQL = "INSERT INTO onlinestore.customer (username,password,balance) VALUES('"+customer.Username+"','"+customer.Password+"',"+customer.Balance+")";
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void InsertTransaction(Transaction transaction)
        {
            connection.Open();
            string SQL = "INSERT INTO onlinestore.transaction (Product_ID, Customer_ID) VALUES(" + transaction.Product.ID + "," + transaction.Customer.ID + ")";
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public List<Transaction> GetTransactionsByCustomer(Customer customer)
        {
            connection.Open();
            string SQL = "SELECT * FROM onlinestore.transaction where Customer_ID = "+ customer.ID;
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Transaction> transactions = new List<Transaction>();
            while (reader.Read())
            {
                int id = (int)reader["idtransaction"];
                double amount = (double)reader["amount"];
                int pid = (int)reader["Product_ID"];
                Product product = FindProductById(pid);
                Transaction t = new Transaction(id, customer, product, amount);
                transactions.Add(t);
            }

            connection.Close();
            return transactions;
        }

        public void UpdateStock(Stock stock)
        {
            connection.Open();
            string SQL = "UPDATE onlinestore.stock SET amount = " + stock.Amount;
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateCustomer(Customer customer)
        {
            connection.Open();
            string SQL = "UPDATE onlinestore.customer SET username = '" + customer.Username + "', password ='" + customer.Password + "', balance = '" + customer.Balance + "'";
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}
