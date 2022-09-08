using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventStore
{

  
    public  class Store
    {

     
        private readonly Service _service ;

        public Store(Service service)
        {

            _service = service;
        }
        public  void Run(){
        while (true)
            {
                try
                {
                    Console.WriteLine("Who Are u ? ");
                    Console.WriteLine("1 - Add A Sale that happened");
                    Console.WriteLine("2 - See The Items Sold");
                    Console.WriteLine("3 - Total Sale");
                    Console.WriteLine("4 - Update Sale");
                    Console.WriteLine("5 - Find a  Sale");
                    Console.WriteLine("6 - Delete a  Sale");
                    Console.WriteLine("7 - Fetch the Stats");

                    var key = Console.ReadLine();

                    if (key.Equals("1"))
                    {
                        Console.Clear();
                        Console.WriteLine("Now You Can Add a new Sale");
                        Console.WriteLine("Can You Give Us The Product Name Please ? ");

                        var _productName = Console.ReadLine();
                        Console.WriteLine("Now , What is The Quantity Sold ? ");
                        var _quantiy = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Can we Finish This Off with the price . ");
                        var _price = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Thank You The Data is now being Inserted :) :) ");
                        _service.AddSale(_productName, _quantiy, _price);
                    }

                    if (key.Equals("2"))
                    {
                        var list = _service.GetSales();

                        list.Result.ForEach(s => Console.WriteLine(s.ToString()));
                    }

                    if (key.Equals("3"))
                    {
                        Console.WriteLine(_service.Total().Result);

                    }

                    if (key.Equals("4"))
                    {
                        Console.Clear();
                        Console.WriteLine("Now You Can Add a new Sale");
                        Console.WriteLine("Can You Give Us the id please ? ");
                        var id = Console.ReadLine();
                        var sale = _service.GetSale(id).Result;
                        Console.WriteLine("Can You Give Us The new Product Name Please ? ");

                        var _productName = Console.ReadLine();
                        Console.WriteLine("Now , What is The Real Quantity Sold ? ");
                        var _quantiy = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Can we Finish This Off with the corrected price . ");
                        var _price = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Thank You The Data is now being Inserted :) :) ");
                        _service.UpdateSale(sale.Id.ToString(), _productName, _quantiy, _price);
                    }

                    if (key.Equals("5"))
                    {
                        Console.Clear();

                        Console.WriteLine("Can You Give Us the id please ? ");
                        var id = Console.ReadLine();
                        var sale = _service.GetSale(id).Result;
                        Console.WriteLine(sale.ToString());
                    }

                    if (key.Equals("6"))
                    {
                        Console.Clear();

                        Console.WriteLine("Can You Give Us the id please ? ");
                        var id = Console.ReadLine();
                        _service.DeleteSale(id);

                    }

                    if (key.Equals("7"))
                    {
                        Console.Clear();


                        var stats = _service.GetStats().Result;
                        foreach (var stat in stats)
                        {
                            Console.WriteLine(stat.ToString());
                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }







                Console.ReadKey();
                Console.Clear(); 



            }
        }

    }
}
