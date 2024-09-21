using AutoSharp.Properties;

namespace AutoSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = default;
            int res = num + 1;

            Lexer.Lexer lexer = new(Resources.test);
            lexer.LexemeList.ForEach(x =>
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{x.Item1}, ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{x.Item2.Item1}, ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{x.Item2.Item2}\n");
            });
        }
    }
}
