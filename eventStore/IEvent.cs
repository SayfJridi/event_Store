using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventStore
{
    public interface IEvent
    {
        public string Type { get; } 
    }
}
