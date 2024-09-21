
namespace AutoSharp.Lexer
{
    public class Tokens
    {

    }

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
        USING,
        NAMESPACE,
        CLASS,
        INTERNAL,
        STATIC,
        PUBLIC,
        PRIVATE,
        VOID
    }

}
