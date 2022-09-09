using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventStore
{
    public class SaleRepository : IRepository<Sale>
    {
        private readonly EventStoreService  _eventStore; 
        public SaleRepository(EventStoreService eventStore)
        {
            this._eventStore = eventStore;
        }

        public async Task<Sale> GetById(string id)
        {
            var events = await this._eventStore.GetEvents($"sales-{id}");
            var sale = Sale.ApplyEvents(events);
            return sale; 
        }

        public async Task<Sale> Update(string id, Sale updatedFields)
        {
            var @event = new SaleUpdated() { UpdatedFields = updatedFields };
            _eventStore.AddEvent($"sales-{id}", @event);
            return await this.GetById(id);
        }

        public async Task<Sale> DeleteById(string id)
        {
            var @event = new SaleDeleted() {  };
            _eventStore.AddEvent($"sales-{id}", @event);
            return await this.GetById(id);
        }

        public async Task<List<Sale>> Get()
        {
            var events = await this._eventStore.GetAllEvents();

            List<Sale> sales = new List<Sale>();

            events = events.FindAll(e => e.Event.EventStreamId.StartsWith("sales-"));

            var eventsGrouped = events.GroupBy(evt => evt.Event.EventStreamId);

            foreach (var Events in eventsGrouped)
            {
                var sale = Sale.ApplyEvents(Events.ToList());
                if (sale.Deleted == false)
                    sales.Add(sale);
            }

            return sales;
        }

        public async Task<Sale> Create(Sale sale)
        {
            var @event  = new SaleCreated() { CreationFields = sale };
             this._eventStore.AddEvent($"sales-{sale.Id}",@event);
             return await  this.GetById(sale.Id.ToString()); 
        }

       
    }
}
