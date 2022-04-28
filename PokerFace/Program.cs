using PokerFace.Evaluator;
using PokerFace.Exceptions;
using System;
using System.IO;

namespace PokerFace
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var evaluator = new HandEvaluator();
                var path = ConsoleHelper.ReadInput("Please enter file path: ");

                if (!File.Exists(path))
                {
                    throw new NullFileException();
                }

                if (Path.GetExtension(path) != ".txt")
                {
                    throw new InvalidFileTypeException();
                }
                
                using (var reader = new StreamReader(path))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = reader.ReadLine().Trim();
                        Console.WriteLine(evaluator.Evaluate(line));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
