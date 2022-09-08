using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventStore
{
    public  interface IService
    {
        public  void AddSale(string productName, int quantity, decimal price);
        public  void UpdateSale(string id, string productName, int quantity, decimal price);
        public  Task<Sale> GetSale(string id);
        public  Task<List<Sale>> GetSales();
        public  Task<List<Stats>> GetStats();
        public  Task<string> Total();
        public  void DeleteSale(string id);
    }
}
