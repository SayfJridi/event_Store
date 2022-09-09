using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.Client;

namespace eventStore
{
    public  interface IEventStore
    {
        public void AddEvent(string streamId , IEvent @event); 
        public Task<List<ResolvedEvent>> GetEvents(string streamId);

        public Task<List<ResolvedEvent>> GetAllEvents();
    }
}
