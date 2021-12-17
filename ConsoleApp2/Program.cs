using ConsoleApp2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace ConsoleApp4
{
    class Program
    {

        static void Main(string[] args)
        {
            List<int> sequence = new List<int>()
            {
                 1, 523, 34, 55, 231, 144, 233, 45, 377
            };

            var n = 2;

            Console.WriteLine(maxProfit(sequence, n));

        }

        static int maxProfit(List<int> t, int n)
        {
            ProfitCalculator pc = new ProfitCalculator(t);
            return pc.GetMaxProfit(n);
        }

    }

 

}

