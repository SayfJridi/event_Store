using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EventStore.Client;

namespace eventStore
{
    public class SaleUpdated
    {
        public Sale UpdatedFields { get; set; }

        public String Type
        {
            get;

        } = "Update"; 
    }
    public class SaleDeleted
    {
        DateTime DeletedDate = DateTime.Now; 

        public String Type
        {
            get;
        } = "Delete"; 
    }
    public class SaleCreated
    {
        public Sale CreationFields { get; set; }

        public String Type
        {
            get;

        } = "Create"; 
    }

}
