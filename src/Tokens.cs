
namespace AutoSharp.Lexer
{
    public enum TokEnum
    {
        IDENTIFIER,
        Type,
        ORB, //parentesis abierto
        CRB, //parentesis cerrado
        OSB, //corchete abierto
        CSB, //corchete cerrado
        OB, //llave abierta
        CB, //llave cerrada
        ASSIGNATION, 
        SCMT, //comentario simple
        STRING,
        SEMICOLON, //PyC
        COLON, //dos puntos
        INTEGER, //int
        FLOAT, //float
        BOOL, //bool
        CHAR, //char
        ADD, //suma
        SUBS, //resta
        PLUS, //mas 
        MINUS, //menis
        MULT, //multiplicacion
        DIVIDE, //division
        SQR, //raiz
        POW, //exponente
        USING,
        NAMESPACE,
        CLASS,
        ENCAPSULATION,
        VOID
    }

    public static class Tokens
    {
        public static bool IsFloat(string lexeme)
        {
            return double.TryParse(lexeme, out double floatValue)
                && lexeme.Contains((char)Cons.DOT);
        }


        public static bool IsInteger(string lexeme) => int.TryParse(lexeme, out int result);


        public static bool IsPrimitive(string lexeme)
        {
            return lexeme == Cons.STRING_WORD || lexeme == Cons.CHAR_WORD
                || lexeme == Cons.INT_WORD || lexeme == Cons.FLOAT_WORD
                || lexeme == Cons.BOOL_WORD;
        }


        public static bool IsOperator(string lexeme, out TokEnum token)
        {
            var operators = new Dictionary<string, TokEnum>
            {
                { ((char)Cons.ADD).ToString(), TokEnum.ADD },
                { ((char)Cons.MINUS).ToString(), TokEnum.MINUS },
                { ((char)Cons.SLASH).ToString(), TokEnum.DIVIDE },
                { ((char)Cons.POW).ToString(), TokEnum.POW },
                { ((char)Cons.MULT).ToString(), TokEnum.MULT },
                { ((char)Cons.EQUALS).ToString(), TokEnum.ASSIGNATION }
            };

            if (operators.TryGetValue(lexeme, out token))
                return true;

            token = TokEnum.IDENTIFIER; // Valor por defecto si no es un operador
            return false;
        }


        public static bool IsResevedWord(string lexeme, out TokEnum token)
        {
            var words = new Dictionary<string, TokEnum>
            {
                { Cons.USING, TokEnum.USING },
                { Cons.VOID_WORD, TokEnum.VOID },
                { Cons.NAMESPACE, TokEnum.NAMESPACE },
                { Cons.CLASS, TokEnum.CLASS },
            };
            if (words.TryGetValue(lexeme, out token))
                return true;

            token = TokEnum.IDENTIFIER;
            return false;
        }


        public static bool IsEncapsulation(string lexeme)
        {
            return lexeme == Cons.PRIVATE || lexeme == Cons.PUBLIC
                || lexeme == Cons.STATIC || lexeme == Cons.INTERNAL;
        }


        public static bool IsBracketOrSCN(string lexeme, out TokEnum token)
        {
            var operators = new Dictionary<string, TokEnum>
            {
                { ((char)Cons.SCN).ToString(), TokEnum.SEMICOLON },
                { ((char)Cons.CB).ToString(), TokEnum.CB },
                { ((char)Cons.OB).ToString(), TokEnum.OB },
                { ((char)Cons.CSB).ToString(), TokEnum.CSB },
                { ((char)Cons.OSB).ToString(), TokEnum.OSB },
                { ((char)Cons.CRB).ToString(), TokEnum.CRB },
                { ((char)Cons.ORB).ToString(), TokEnum.ORB }
            };

            if (operators.TryGetValue(lexeme, out token))
                return true;

            token = TokEnum.IDENTIFIER; // Valor por defecto si no es un operador
            return false;
        }


    }

}
