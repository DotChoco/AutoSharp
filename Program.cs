using AutoSharp.Lexer;
using AutoSharp.Properties;

namespace AutoSharp
{

    internal class Program
    {
        static void Main(string[] args)
        {
            int num = default;
            int res = num + 1;
            
            Console.WriteLine("Hello, World!");
            Lexer.Lexer lexer = new(Resources.test, false);
        }
    }
}
