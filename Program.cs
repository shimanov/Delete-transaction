using System;
using Transaction.Classes;

namespace Transaction
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Введите номер зависшей транзакции (полностью): \t");
            string posting = Console.ReadLine();

            Sql s = new Sql();
            s.SearchPosting(posting);

            Console.WriteLine("Удалить зависшую тразкакцию? (y/n?)");
            string result = Console.ReadLine();

            if (result == "y")
            {
                s.DeletePosting(posting);
                Console.WriteLine("Транзакция удалена");
            }
            else
            {
                Environment.Exit(0);
            }

            Console.ReadKey();
        }
    }
}
