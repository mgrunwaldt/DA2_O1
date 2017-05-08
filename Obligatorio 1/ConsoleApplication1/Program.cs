using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Product p = new Product();
            p.Name = "Matias";
            ProductsRepository pr = new ProductsRepository();
            pr.Add(p);
            Console.Write(p.Name + p.Id);
        }
    }
}
