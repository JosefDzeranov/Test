using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp2
{
    class Program
    {
        private struct Sale
        {
            public Guid salesid;
            public string productid;
            public DateTime datetime;
            public string customerid;
        }
        static void Main(string[] args)
        {
            List<Sale> list = getSales();
            Stopwatch s = Stopwatch.StartNew();
            var productsRates = list.GroupBy(sale => sale.customerid, sale => sale, (key, value) => new { customerid = key, productid = value.OrderBy(sale => sale.datetime).First().productid })
                                .GroupBy(x => x.productid, x => x.customerid, (key, value) => new { productid = key, rate = value.Count()});
            s.Stop();
            foreach (var productRate in productsRates)
                Console.WriteLine(productRate.productid + " " + productRate.rate);
            Console.WriteLine("Elapsed Time: {0} ms", s.ElapsedMilliseconds);
            Console.ReadLine();
        }
        private static List<Sale> getSales()
        {
            Random rand = new Random();
            List<string> products = new List<string>() { "Хлеб", "Молоко", "Мясо"};
            List<string> customers = new List<string>() { "Петров", "Иванов", "Сидоров", "Соколов", "Мельников", "Стрельников", "Степанов" };
            List<Sale> list = new List<Sale>();
            DateTime start = new DateTime(2016, 1, 1);
            int range = (DateTime.Today - start).Days;
            while (list.Count < 100)
            {
                list.Add(new Sale
                {
                    salesid = Guid.NewGuid(),
                    productid = products[rand.Next(products.Count)],
                    datetime = start.AddDays(rand.Next(range)),
                    customerid = customers[rand.Next(customers.Count)]
                });
                Console.WriteLine("{0} {1} {2} {3}", list.Last().salesid, list.Last().productid, list.Last().datetime.ToString("d"), list.Last().customerid);
            }
            return list;
        }
    }
}
