using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EventStore.Client;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using EventData = EventStore.Client.EventData;
using Position = EventStore.Client.Position;
using ResolvedEvent = EventStore.Client.ResolvedEvent;
using StreamPosition = EventStore.Client.StreamPosition;

namespace eventStore
{
    public class EventStoreService : IEventStore
    {

        private static EventStoreClientSettings settings = EventStoreClientSettings.Create("esdb://127.0.0.1:2113?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000");
        private EventStoreClient client = new EventStoreClient(settings);
    
        public async Task<List<ResolvedEvent>> GetEvents(string streamId)
        {
            var result = client.ReadStreamAsync(Direction.Forwards, streamId, StreamPosition.Start);
             var List =  await result.ToListAsync();
             return List; 

        }


        public async void AddEvent(string streamId,IEvent @event)
        {
            var eventData = new EventData(
                Uuid.NewUuid(),
                @event.Type,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event))
            );
            await client.AppendToStreamAsync(
                streamId,
                StreamState.Any,
                new List<EventData> {
                    eventData
                });
        }

        public async Task<List<ResolvedEvent>> GetAllEvents()
        {
            var result = client.ReadAllAsync(Direction.Forwards, Position.Start);
            var List = await result.ToListAsync();
            return List;
        }
    }
}
