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

            Random random = new Random(11);
            string accountNo = random.Next().ToString();
            Console.WriteLine(accountNo);
        }
    }
}
