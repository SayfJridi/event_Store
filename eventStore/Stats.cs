using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventStore
{
    public class Stats
    {
        public String ProductName { get; set; }
        public int QuanitiesSold { get; set; }

        public Stats(string productName, int quanitiesSold)
        {
            ProductName=productName;
            QuanitiesSold=quanitiesSold;
        }

        public override string ToString()
        {
            return $"Product Name : {ProductName} + Quantity: {QuanitiesSold}";
        }


    }
}
