using System;

namespace ConsoleAppFundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Line 1 ");
            Console.Write("still in line 1");
            Console.WriteLine(); //new line
            Console.Write("Line 2");

            Console.WriteLine();
            Console.WriteLine("Press enter to clear console and show new text");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("New Line 1");
            Console.WriteLine("New Line 2");

            Console.WriteLine("Press enter to see red text.");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Red text");

            
            Console.WriteLine("Press enter to see White background and black text.");
            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear(); //neccessary to make whole console background white
            Console.WriteLine("White background and black text");

            Console.WriteLine("Press enter to see the default background and foreground colour.");
            Console.ReadLine();
            Console.ResetColor();
            Console.Clear(); //neccessary
            Console.WriteLine("reset");

            Console.WriteLine("Press enter to end.");
            Console.ReadLine();
        }
    }
}
