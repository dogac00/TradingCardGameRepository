using System;

namespace TCGCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter first player name: ");
            var name1 = Console.ReadLine();
            Console.WriteLine("Enter second player name: ");
            var name2 = Console.ReadLine();
            var io = new ConsoleImpl();

            var game = new Game(name1, name2, io);

            game.Start();
        }
    }
}
