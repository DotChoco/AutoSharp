using AutoSharp.Lexer;
namespace AutoSharp
{

    internal class Program
    {
        static void Main(string[] args)
        {
            int num = default;
            int res = num + 1;

            Console.WriteLine("Hello, World!");
            Lexer.Lexer lexer = new(@"C:\Users\carlo\Downloads\test.cs");
        }
    }
}
