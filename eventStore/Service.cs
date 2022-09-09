using EventStore.Client;
using EventStore.ClientAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using Newtonsoft.Json;
using DeleteResult = EventStore.Client.DeleteResult;
using EventData = EventStore.Client.EventData;
using Position = EventStore.Client.Position;
using ResolvedEvent = EventStore.Client.ResolvedEvent;
using StreamPosition = EventStore.Client.StreamPosition;

namespace eventStore
{
    public class Service : IService
    {

        private readonly IRepository<Sale> _saleRepository;

       public Service(IRepository<Sale> saleRepository)
        {
            this._saleRepository = saleRepository;
        }
        public async void AddSale(string productName, int quantity, decimal price)
        {

            
        }


        public  async void UpdateSale(string id,string productName , int quantity,decimal price)
        {
            var sale = await this._saleRepository.GetById(id); 
            sale.ProductName = productName;
            sale.Quantity = quantity;
            sale.Price = price;
            this._saleRepository.Update(id, sale); 




        }

        public async Task<Sale> GetSale(string id)
        {
            var sale = await  this._saleRepository.GetById(id);
                return sale;
        }


        public async Task<List<Sale>> GetSales()
        {

            var sales = await this._saleRepository.Get(); 

            return sales;

        }

        public async Task<List<Stats>> GetStats()
        {

            var sales = await this.GetSales();

            var stats = sales.GroupBy(s => s.ProductName);

            foreach (var s in stats)
            {
                Console.WriteLine(s.Key + "  " + s.Sum(s => s.Quantity));
            }
            return new List<Stats>();
        }

        public async Task<string> Total()
        {
            List<Sale> sales = await this.GetSales();

            var a = sales.Sum((Sale s) => (s.Price * s.Quantity));
            Console.WriteLine(a);
            return $"Your Total Sale =  + {a}"; 
        }

        public async void DeleteSale(string id)
        {

            var sale = await this._saleRepository.DeleteById(id);
         
           

        }
         

    }
}
