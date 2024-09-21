
namespace AutoSharp.Lexer
{
    public class Lexer
    {
        List<Tuple<string, TokEnum>> Lexems = new();
        int NumLine = default;
        public Lexer(string FilePath) 
        {
            StreamReader sr = new(FilePath);
            string data = sr.ReadToEnd();
            sr.Close();

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
            Lexems.Add(new(lexem, token));
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



            return TokEnum.IDENTIFIER;
        }
    
        
        
        
    }
}
