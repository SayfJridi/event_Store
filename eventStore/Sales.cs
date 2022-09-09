
using EventStore.Client;

namespace eventStore
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EventStore.Client;
    using Newtonsoft.Json;

    public class Sale
    {

        public Guid Id { get; set; }
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public bool Deleted { get; set; } = false; 

        public override string ToString()
        {
            return
                $"------------------------------------------------------------------------------------------\x0A Id:{Id} | ProductName:{ProductName} |  Quantity:{Quantity}  | Price:{Price} |  Deleted: {Deleted}";
        }

        public static Sale ApplyEvents(List<ResolvedEvent> eventList)
        {
            Sale variable = new Sale(); 
        
       eventList.ForEach((ResolvedEvent evt) =>
       {
           if (evt.Event.EventType == "Create")
           {
               var @event = JsonConvert.DeserializeObject<SaleCreated>(Encoding.UTF8.GetString(evt.Event.Data.ToArray()));

               variable = Sale.ApplyEvent(@event); 
           }

           if (evt.Event.EventType == "Update")
           {
               var @event = JsonConvert.DeserializeObject<SaleUpdated>(Encoding.UTF8.GetString(evt.Event.Data.ToArray()));

               variable = Sale.ApplyEvent(@event,variable);
               
           }

           if (evt.Event.EventType == "Delete")
           {
               var @event = JsonConvert.DeserializeObject<SaleDeleted>(Encoding.UTF8.GetString(evt.Event.Data.ToArray()));
               Sale.ApplyEvent(@event,variable); 
           }
       });

       return variable; 
        }


        public static Sale ApplyEvent(SaleCreated @event)
        {
            return new Sale()
            {
                Id = @event.CreationFields.Id,
                Price = @event.CreationFields.Price,
                Quantity = @event.CreationFields.Quantity,
                ProductName = @event.CreationFields.ProductName
            };
        }
        public static Sale ApplyEvent(SaleUpdated @event, Sale sale)
        {
            sale.Price = @event.UpdatedFields.Price;
            sale.Quantity = @event.UpdatedFields.Quantity;
            sale.ProductName = @event.UpdatedFields.ProductName;
            return sale; 

        }
        public static Sale ApplyEvent(SaleDeleted @event ,  Sale sale)
        {
            
            sale.Deleted = true;
            return sale;
        }
    }


}
