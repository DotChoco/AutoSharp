
namespace AutoSharp.Lexer
{
    public static class Cons
    {
        /// <summary>
        /// ASCII de punto y coma(semicolon)
        /// </summary>
        public const int SCN = 59;

        /// <summary>
        /// ASCII de espacio
        /// </summary>
        public const int SPACE = 32;
        
        /// <summary>
        /// ASCII de retorno de carrete
        /// </summary>
        public const int CR = 13;

        /// <summary>
        /// ASCII para el punto
        /// </summary>
        public const int DOT = 46;

        /// <summary>
        /// ASCII para las comillas dobles
        /// </summary>
        public const int STRING = 34;

        /// <summary>
        /// ASCII para las comillas simples
        /// </summary>
        public const int CHAR = 39;

        /// <summary>
        /// ASCII para parentesis abierto
        /// </summary>
        public const int ORB = 40;

        /// <summary>
        /// ASCII para parentesis cerrado
        /// </summary>
        public const int CRB = 41;

        /// <summary>
        /// ASCII para corchete abierto
        /// </summary>
        public const int OSB = 91; 

        /// <summary>
        /// ASCII para corchete cerrado
        /// </summary>
        public const int CSB = 93;

        /// <summary>
        /// ASCII para corchete abierto
        /// </summary>
        public const int OB = 123;

        /// <summary>
        /// ASCII para corchete cerrado
        /// </summary>
        public const int CB = 125;

        /// <summary>
        /// ASCII para el slash
        /// </summary>
        public const int SLASH = 47;

    }
    public enum OpEnum
    {
        SCN = 0, //semicolon
        PLUS, //Plus 
        MINUS,
        MULT,
        DIVIDE,
        SQR,
        POW
    }


}
