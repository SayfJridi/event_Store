using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using eventStore;

namespace Testing.Mocks
{
    public class MockedService : IService
    {

        private List<Sale> saleList;

        public async Task<List<Stats>> GetStats()
        {
            var list = new List<Stats>();
            return list; 
        }
        public Task<Sale> GetSale(string id)
        {
            return Task.Run(() => this.saleList.Find((Sale sale) => new Guid(id) == sale.Id))! ;
        }

        public Task<List<Sale>> GetSales()
        {
            var list =  this.saleList;
            return Task.Run(() => list);
        }

        public void AddSale(string productName, int quantity, decimal price)
        {

        }

        public void UpdateSale(string id , string productName , int quantity, decimal price)
        {

        }

        public void DeleteSale(string id)
        {
             
        }

        public Task<string> Total()
        {
            return new Task<string>(() => " ") ;
        }

    }
}
