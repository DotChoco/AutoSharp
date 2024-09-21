
using System.Xml.Serialization;

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

        /// <summary>
        /// ASCII para el igual
        /// </summary>
        public const int EQUALS = 61;

        /// <summary>
        /// ASCII para el not
        /// </summary>
        public const int NOT = 33;

        // <summary>
        /// ASCII para la raiz
        /// </summary>
        public const int MULT = 42;

        // <summary>
        /// ASCII para el exponente
        /// </summary>
        public const int POW = 94;

        // <summary>
        /// ASCII para la resta
        /// </summary>
        public const int MINUS = 45;

        // <summary>
        /// ASCII para la suma
        /// </summary>
        public const int ADD = 43;


        public const string INTERNAL = "internal";
        public const string PUBLIC = "public";
        public const string PRIVATE = "private";
        public const string NAMESPACE = "namespace";
        public const string USING = "using";
        public const string CLASS = "class";
        public const string STATIC = "static";

        public const string VOID_WORD = "void";
        public const string STRING_WORD = "string";
        public const string CHAR_WORD = "char";
        public const string INT_WORD = "int";
        public const string FLOAT_WORD = "float";
        public const string BOOL_WORD = "bool";

    }
}
