
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
            if (lexeme == ((char)Cons.ADD).ToString())
            {
                token = TokEnum.ADD;
                return true;
            }
            else if (lexeme == ((char)Cons.MINUS).ToString())
            {
                token = TokEnum.MINUS;
                return true;
            }
            else if (lexeme == ((char)Cons.SLASH).ToString())
            {
                token = TokEnum.DIVIDE;
                return true;
            }
            else if (lexeme == ((char)Cons.POW).ToString())
            {
                token = TokEnum.POW;
                return true;
            }
            else if (lexeme == ((char)Cons.MULT).ToString())
            {
                token = TokEnum.MULT;
                return true;
            }
            else if (lexeme == ((char)Cons.EQUALS).ToString())
            {
                token = TokEnum.ASSIGNATION;
                return true;
            }
             token = TokEnum.IDENTIFIER;
            return false;
        }


        public static bool IsResevedWord(string lexeme, out TokEnum token)
        {
            if (lexeme == Cons.USING)
            {
                token = TokEnum.USING;
                return true;
            }
            else if (lexeme == Cons.VOID_WORD)
            {
                token = TokEnum.VOID;
                return true;
            }
            else if (lexeme == Cons.NAMESPACE)
            {
                token = TokEnum.NAMESPACE;
                return true;
            }
            else if (lexeme == Cons.CLASS)
            {
                token = TokEnum.CLASS;
                return true;
            }

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
            if (lexeme == ((char)Cons.SCN).ToString()){
                token = TokEnum.SEMICOLON;
                return true;
            }
            if (lexeme == ((char)Cons.CB).ToString())
            {
                token = TokEnum.CB;
                return true;
            }
            else if (lexeme == ((char)Cons.OB).ToString())
            {
                token = TokEnum.OB;
                return true;
            }
            else if (lexeme == ((char)Cons.CSB).ToString())
            {
                token = TokEnum.CSB;
                return true;
            }
            else if (lexeme == ((char)Cons.OSB).ToString())
            {
                token = TokEnum.OSB;
                return true;
            }
            else if (lexeme == ((char)Cons.CRB).ToString())
            {
                token = TokEnum.CRB;
                return true;
            }
            else if (lexeme == ((char)Cons.ORB).ToString())
            {
                token = TokEnum.ORB;
                return true;
            }

            token = TokEnum.IDENTIFIER;
            return false;
        }


    }

}
