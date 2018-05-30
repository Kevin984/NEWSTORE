using NewOnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NewOnlineStore.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStoreService" in both code and config file together.
    [ServiceContract]
    public interface IStoreService
    {
        [OperationContract]
        Customer CheckUserCredentials(string username, string password);

        [OperationContract]
        List<Product> GetAllProducts(string username, string password);

        [OperationContract]
        void BuyProduct(Product product, string username, string password);

        [OperationContract]
        double GetCustomerBalance(string username, string password);

        [OperationContract]
        List<Transaction> GetCustomerBuyHistory(string username, string password);

        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        string Register(string username);
  

    }
}
