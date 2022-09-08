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
        public static EventStoreClientSettings settings = EventStoreClientSettings.Create("esdb://127.0.0.1:2113?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000");
        EventStoreClient client = new EventStoreClient(settings);


        public async void AddSale(string productName, int quantity, decimal price)
        {
            var sale = new Sale(){Id= Guid.NewGuid(), ProductName = productName, Quantity = quantity ,Price = price };
            var evt = new SaleCreated() { CreationFields = sale}; 
            var eventData = new EventData(
                Uuid.NewUuid(),
                evt.Type,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evt))
            );
            await client.AppendToStreamAsync(
                $"sales-{sale.Id}",
                StreamState.Any,
                new List<EventData> {
                    eventData
                });
        }


        public  async void UpdateSale(string id,string productName , int quantity,decimal price)
        {
            var sale = await GetSale(id);
            if (sale.Deleted == true)
            {
                Console.WriteLine("Not Found Sale");
            }

            sale.ProductName = productName; 
            sale.Quantity = quantity;
            sale.Price = price;
            var evt = new SaleUpdated() { UpdatedFields = sale };
            var eventData = new EventData(
                Uuid.NewUuid(),
                evt.Type,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evt))
            );
            await client.AppendToStreamAsync(
                $"sales-{sale.Id}",
                StreamState.Any,
                new List<EventData> {
                    eventData
                });
          


        }

        public async Task<Sale> GetSale(string id)
        {
           
                var result = client.ReadStreamAsync(
                    Direction.Forwards,
                    $"sales-{id}",
                    StreamPosition.Start);
                var sale = new Sale();
                if (await result.ReadState == ReadState.StreamNotFound)
                {
                    throw new FileNotFoundException($"Sale With Id {id}"); 
                } 

                var events = await result.ToListAsync(); 
                sale = Sale.ApplyEvents(events);
                return sale; 
                


            
           

        }


        public async Task<List<Sale>> GetSales()
        {
            var results = client.ReadAllAsync(
                Direction.Forwards, Position.Start);
             
            var events = await results.ToListAsync();
            List<Sale> sales = new List<Sale>();

            events = events.FindAll(e => e.Event.EventStreamId.StartsWith("sales-"));

            var eventsGrouped = events.GroupBy(evt => evt.Event.EventStreamId);

            foreach (var Events in eventsGrouped)
            {
                var sale = Sale.ApplyEvents(Events.ToList()); 
                if(sale.Deleted == false)
                  sales.Add(sale); 
            }

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

            var sale = await this.GetSale(id);
         
           
            if (sale.Deleted == true ||     sale == new Sale())
            {
                return;
            }
            var evt = new SaleDeleted();
            var eventData = new EventData(
                Uuid.NewUuid(),
                evt.Type,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evt))
            );
            await client.AppendToStreamAsync(
                $"sales-{sale.Id}",
                StreamState.Any,
                new List<EventData> {
                    eventData
                });

        }
         

    }
}
