using System;

namespace CLHBankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(DateTime.Now.Date);
            Console.WriteLine(DateTime.Now.Year);
            Console.WriteLine(DateTime.Now.Month);
            Console.WriteLine(DateTime.Now.DayOfWeek);


            Random r = new Random();
            Console.WriteLine($"00{r.Next(1000, 9999)}{r.Next(1000, 9999)}");
            Console.WriteLine($"00{r.Next(1000, 9999)}{r.Next(1000, 9999)}");
            //0026381312

            int n = int.Parse("123");
            int value;
            var n2 = int.TryParse("jjj", out value);
            var n3 = int.TryParse("44.4", out value);
            int n1 = int.Parse("www");

        }
    }
}
