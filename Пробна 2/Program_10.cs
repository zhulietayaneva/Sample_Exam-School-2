using System;
using System.Collections.Generic;
using System.Linq;

namespace Couples
{
    class Program
    {
        static void Main(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            List<int> encountered = new List<int>();
            List<int> couples = new List<int>();


            while (n!=0)
            {
                if (!encountered.Contains(n))
                {
                    encountered.Add(n);
                }
                else
                {
                    couples.Add(n);
                }


                n = int.Parse(Console.ReadLine());
            }

            
            Console.WriteLine(encountered.Except(couples).ToList()[0]);



        }
    }
}
