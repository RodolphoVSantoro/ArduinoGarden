using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 1000;
            int b ;
            for (a = 1000; a < 1100; a += 10)
            {
                b = a << 5;
                Console.WriteLine(a);
                Console.WriteLine(b);
            }
            Console.ReadLine();
        }
    }
}
