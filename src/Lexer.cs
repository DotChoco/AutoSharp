
using System.Text;

namespace AutoSharp.Lexer
{
    public class Lexer
    {
        List<Tuple<string, TokEnum>> Lexems = new();
        int NumLine = default;
        public Lexer(string Data, bool IsPath) 
        {
            string data = default;
            if (IsPath){
                StreamReader sr = new(Data);
                data = sr.ReadToEnd();
                sr.Close();
            }
            else
                data = Data;

            Analizer(data.Split('\n'));
        }

        void Analizer(string[] data)
        {
            int ASCII = default;
            string word = string.Empty;
            char letter = char.MinValue;

            foreach (string line in data) {
                //limpio la variable
                word = string.Empty;

                for (int index = 0; index < line.Length; index++)
                {
                    letter = line[index];
                    if(word == "Lexer")
                        Console.WriteLine("");
                    // le doy el valor ASCII de cada caracter recorrido
                    ASCII = letter;

                    if (ASCII != Cons.CR && ASCII != Cons.SPACE)
                    {
                        //filtra todo lo que no sea strings y chars
                        if (ASCII != Cons.STRING && ASCII != Cons.CHAR
                            && (ASCII != Cons.SLASH && line[index+1] != Cons.SLASH))
                        {
                            if (IsBracketOrSCN(ASCII))
                            {
                                if (word != null && word != string.Empty)
                                {
                                    FillLexems(word);
                                    word = string.Empty;
                                }
                                FillLexems(letter.ToString());
                                continue;
                            }

                            if (ASCII != Cons.DOT)
                                word += letter;
                            else
                            {
                                if (index - 1 > 0 && index + 1 < line.Length)
                                {
                                    if (IsNumeric($"{line[index - 1]}"))
                                    {
                                        word += letter;
                                        continue;
                                    }
                                }
                                FillLexems(word);
                                word = string.Empty;
                            }
                            if ((ASCII == Cons.SPACE || letter == line.Last())
                            && (word != null && word != string.Empty))
                            {
                                FillLexems(word);
                                word = string.Empty;
                            }
                        }
                        else
                        {
                            if (word != null && word != string.Empty)
                            {
                                FillLexems(word);
                                word = string.Empty;
                            }
                            index += ChainLexemes(line, index);
                        }
                    }
                    else
                    {
                        if ((ASCII == Cons.SPACE || letter == line.Last())
                            && (word != null && word != string.Empty))
                        {
                            FillLexems(word);
                            word = string.Empty;
                        }
                    }
                }

                // voy aumentando cada que hay una nueva linea
                NumLine++;
            }
        }


        /// <summary>
        /// Fill the Lexems List
        /// </summary>
        /// <param name="lexem"></param>
        void FillLexems(string lexem, TokEnum token = default)
        {
            if(token != default)
                Lexems.Add(new(lexem, token));
            else
                Lexems.Add(new(lexem, PrimitiveFilter(lexem)));
        }

        /// <summary>
        /// Separate the Strings or Chars of 
        /// the other text
        /// </summary>
        /// <param name="line"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        int ChainLexemes(string line, int index)
        {
            int conter = default;
            string word = string.Empty;
            string lineCopy = line;
            for (int i = index; i < line.Length; i++)
            {
                if (line[i] != Cons.SPACE)
                    word += line[i];
                if (i != index && line[i] == line[index])
                {
                    if (line[index] == Cons.SLASH)
                    {
                        // Buscar la posición de los dobles slash
                        int indexLine = lineCopy.IndexOf("//");

                        // Si se encuentra, eliminar todo lo que esté después de ellos
                        if (indexLine >= 0)
                        {
                            lineCopy = lineCopy.Substring(indexLine);
                        }

                        // Eliminar espacios en blanco al final, si es necesario
                        FillLexems(lineCopy.TrimEnd(), TokEnum.SCMT);
                        conter = line.Length;
                    }
                    else if(line[index] == Cons.STRING)
                        FillLexems(word, TokEnum.STRING);
                    else if(line[index] == Cons.CHAR)
                        FillLexems(word, TokEnum.CHAR);
                    break;
                }
                else conter++;
            }

            return conter;
        }


        /// <summary>
        /// Is Bracket Or SemiColon
        /// </summary>
        /// <param name="ASCII"></param>
        /// <returns></returns>
        bool IsBracketOrSCN(int ASCII)
        {
            if (ASCII == Cons.ORB || ASCII == Cons.CRB || ASCII == Cons.OSB 
            || ASCII == Cons.CSB || ASCII == Cons.OB || ASCII == Cons.CB
            || ASCII == Cons.SCN)
            {
                return true;
            }
            return false;
        }

        TokEnum PrimitiveFilter(string lexem)
        {
            if(lexem == ((char)Cons.SCN).ToString())
                return TokEnum.SEMICOLON;
            if(lexem == ((char)Cons.CB).ToString())
                return TokEnum.CB;
            else if (lexem == ((char)Cons.OB).ToString())
                return TokEnum.OB;
            else if (lexem == ((char)Cons.CSB).ToString())
                return TokEnum.CSB;
            else if (lexem == ((char)Cons.OSB).ToString())
                return TokEnum.OSB;
            else if (lexem == ((char)Cons.CRB).ToString())
                return TokEnum.CRB;
            else if (lexem == ((char)Cons.ORB).ToString())
                return TokEnum.ORB;

            if (IsFloat(lexem))
                return TokEnum.FLOAT;
            else if (IsInteger(lexem))
                return TokEnum.INTEGER;

            if(lexem == Cons.USING)
                return TokEnum.USING;
            else if (lexem == Cons.PRIVATE)
                return TokEnum.PRIVATE;
            else if (lexem == Cons.PUBLIC)
                return TokEnum.PUBLIC;
            else if (lexem == Cons.STATIC)
                return TokEnum.STATIC;
            else if (lexem == Cons.VOID_WORD)
                return TokEnum.VOID;
            else if (lexem == Cons.INTERNAL)
                return TokEnum.INTERNAL;
            else if (lexem == Cons.NAMESPACE)
                return TokEnum.NAMESPACE;
            else if (lexem == Cons.CLASS)
                return TokEnum.CLASS;

            if (lexem == Cons.STRING_WORD || lexem == Cons.CHAR_WORD
                || lexem == Cons.INT_WORD || lexem == Cons.FLOAT_WORD
                || lexem == Cons.BOOL_WORD)
                return TokEnum.Type;

            return TokEnum.IDENTIFIER;
        }


        bool IsFloat(string lexem)
        {
            if (double.TryParse(lexem, out double floatValue)
                && lexem.Contains(".") && floatValue % 1 != 0)
            {
                return true;
            }
            return false;
        }
        bool IsInteger(string lexem)
        {
            if (int.TryParse(lexem, out int floatValue)
                && lexem.Contains(".") && floatValue % 1 != 0)
            {
                return true;
            }
            return false;
        }



        bool IsNumeric(string lexem) => double.TryParse(lexem, out double result);
        
    }
}
