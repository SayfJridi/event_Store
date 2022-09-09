using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace eventStore
{
    internal class DependancyInjection
    {

        public static ServiceCollection Services => new ServiceCollection();


        public DependancyInjection()
        {
            Services.AddSingleton<IEventStore, EventStoreService>().AddSingleton<IRepository<Sale>, SaleRepository>();
        }



    }
}
