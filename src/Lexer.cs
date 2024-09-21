
namespace AutoSharp.Lexer
{
    public class Lexer
    {

        public List<Tuple<string, Tuple<TokEnum, int>>> LexemeList { get => Lexemes;}
        
        List<Tuple<string, Tuple<TokEnum, int>>> Lexemes = new();
        int NumLine = default;
        
        
        public Lexer(string Data, bool IsPath = false) 
        {
            string data = string.Empty;
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
                    //Debug
                    if(word == "1")
                        Console.WriteLine("");

                    letter = line[index];
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
                                    FillLexems(word, NumLine+1);
                                    word = string.Empty;
                                }
                                FillLexems(letter.ToString(), NumLine+1);
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
                                FillLexems(word, NumLine + 1);
                                word = string.Empty;
                            }
                            if ((ASCII == Cons.SPACE || letter == line.Last())
                            && (word != null && word != string.Empty))
                            {
                                FillLexems(word, NumLine + 1);
                                word = string.Empty;
                            }
                        }
                        else
                        {
                            if (word != null && word != string.Empty)
                            {
                                FillLexems(word, NumLine + 1);
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
                            FillLexems(word, NumLine + 1);
                            word = string.Empty;
                        }
                    }
                }

                // voy aumentando cada que hay una nueva linea
                NumLine++;
            }
        }


        /// <summary>
        /// Fill the Lexemes List
        /// </summary>
        /// <param name="lexeme"></param>
        void FillLexems(string lexeme, int line, TokEnum token = default)
        {
            if(token != default)
                Lexemes.Add(new(lexeme, new(token, line)));
            else
                Lexemes.Add(new(lexeme, new(LexemFilter(lexeme), line)));
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
                        FillLexems(lineCopy.TrimEnd(), NumLine + 1 ,TokEnum.SCMT);
                        conter = line.Length;
                    }
                    else if(line[index] == Cons.STRING)
                        FillLexems(word, NumLine + 1, TokEnum.STRING);
                    else if(line[index] == Cons.CHAR)
                        FillLexems(word, NumLine + 1, TokEnum.CHAR);
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
            return ASCII == Cons.ORB || ASCII == Cons.CRB || ASCII == Cons.OSB
            || ASCII == Cons.CSB || ASCII == Cons.OB || ASCII == Cons.CB
            || ASCII == Cons.SCN;
        }

        
        TokEnum LexemFilter(string lexeme)
        {
            //Brackes y PyC
            if(Tokens.IsBracketOrSCN(lexeme, out TokEnum bracketToken))
                return bracketToken;

            //Datos Numericos
            if (Tokens.IsFloat(lexeme))
                return TokEnum.FLOAT;
            else if (Tokens.IsInteger(lexeme))
                return TokEnum.INTEGER;

            //Encapsulamientos
            if (Tokens.IsEncapsulation(lexeme))
                return TokEnum.ENCAPSULATION;

            //Palabras reservadas
            if (Tokens.IsResevedWord(lexeme, out TokEnum reservedWordToken))
                return reservedWordToken;

            //Datos Primitivos
            if (Tokens.IsPrimitive(lexeme))
                return TokEnum.Type;

            //Operadores
            if(Tokens.IsOperator(lexeme, out TokEnum operatorToken))
                return operatorToken;

            return TokEnum.IDENTIFIER;
        }


        bool IsNumeric(string lexeme) => double.TryParse(lexeme, out double result);
        
    }
}
