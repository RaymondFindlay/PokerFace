using System;
using System.Collections.Generic;
using System.Text;

namespace PokerFace
{
    public static class ConsoleHelper
    {
        public static string ReadInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
    }
}
