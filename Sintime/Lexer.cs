using System;
using System.Collections.Generic;
using System.IO;

namespace WallE.Sintime
{
    /// <summary>
    /// Class that represents the lexical analysis.
    /// </summary>
    public class Lexer
    {
        private class LexicalAnalysis : IDisposable
        {
            #region Properties

            private TextReader reader;
            private int line;
            private string file;

            #endregion

            #region Constructors

            /// <summary>
            /// Initialize a lexical analysis.
            /// </summary>
            /// <param name="file">The file path.</param>
            /// <param name="reader">TextReader of the file.</param>
            public LexicalAnalysis(string file, TextReader reader)
            {
                this.reader = reader;
                this.file = file;
                line = 1;
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Execute the lexical analysis.
            /// </summary>
            /// <param name="errors">List to save errors.</param>
            /// <returns>List of tokens.</returns>
            public List<Token> Analyze(List<Error> errors)
            {
                var tokens = new List<Token>();
                while (!EOF)
                {
                    if (char.IsLetter(Peek()) || Peek() == '_' || Peek() == '@')
                    {   // try to read keyword or identifier
                        string nextWord = NextIdentifier();
                        tokens.Add(Create(nextWord));
                        continue;
                    }
                    else if (char.IsDigit(Peek()))
                    {   // try to read a number
                        string number = NextNumber();
                        tokens.Add(Create(number));
                    }
                    else if (Peek() == '\"')
                    {
                        string text = NextText();
                        if (Peek() == '\"')
                            tokens.Add(Create(text + NextChar().ToString()));
                        else
                            errors.Add(new Error(file, line, ErrorTypes.Expected, "\""));
                    }
                    else
                    {
                        char nextChar = NextChar();
                        switch (nextChar)
                        {
                            case '\n':
                            case ',':
                            case ';':
                            case '+':
                            case '-':
                            case '*':
                            case '%':
                            case '^':
                            case '&':
                            case '~':
                            case '|':
                            case '(':
                            case ')':
                            case '[':
                            case ']':
                                if (nextChar == '\n')
                                {
                                    if (tokens.Count != 0 && tokens[tokens.Count - 1].Text != "\n")
                                        tokens.Add(Create(nextChar.ToString()));
                                }
                                else
                                    tokens.Add(Create(nextChar.ToString()));
                                break;
                            case '/':   // comment or division
                                if (TryMatch('/'))
                                    SkipLine();
                                else if (TryMatch('*'))
                                {
                                    if (!SkipComment())
                                        errors.Add(new Error(file, line, ErrorTypes.Expected, "*/"));
                                }
                                else
                                    tokens.Add(Create(nextChar.ToString()));
                                break;
                            case '=':
                            case '!':
                            case '<':
                            case '>':
                                if (TryMatch('='))
                                    tokens.Add(Create(nextChar.ToString() + "="));
                                else
                                    tokens.Add(Create(nextChar.ToString()));
                                break;
                            default:
                                if (!char.IsWhiteSpace(nextChar))
                                    tokens.Add(Create(nextChar.ToString()));
                                break;
                        }
                    }
                }
                if (tokens.Count != 0 && tokens[tokens.Count - 1].Text == "\n")
                    tokens.RemoveAt(tokens.Count - 1);
                return tokens;
            }

            public void Dispose()
            {
                reader.Close();
            }

            #endregion

            #region Private Methods

            private Token Create(string text)
            {
                if (text == "\n")
                    return new Token(text, file, line - 1);
                return new Token(text, file, line);
            }

            private bool EOF
            {
                get { return reader.Peek() == -1; }
            }

            private char Peek()
            {
                return (char)reader.Peek();
            }

            private string NextIdentifier()
            {
                string word = "";
                while (!EOF && (char.IsLetterOrDigit(Peek()) || Peek() == '_' || (word.Length == 0 && Peek() == '@')))
                    word += NextChar();
                return word;
            }

            private string NextNumber()
            {
                string number = "";
                while (!EOF && char.IsDigit(Peek()))
                    number += NextChar();
                return number;
            }

            private bool EOL
            {
                get { return EOF || Peek() == '\n'; }
            }

            private string NextText()
            {
                string text = NextChar() + "";
                while (!EOL && Peek() != '\"')
                    text += NextChar();
                return text;
            }

            private char NextChar()
            {
                var c = (char)reader.Read();
                if (c == '\n') line++;
                return c;
            }

            private bool TryMatch(char nextChar)
            {
                if (!EOL && Peek() == nextChar)
                {
                    NextChar();
                    return true;
                }
                return false;
            }

            private void SkipLine()
            {
                while (!EOL)
                    NextChar();
            }

            private bool SkipComment()
            {
                while (!EOF)
                {
                    if (NextChar() == '*' && Peek() == '/')
                    {
                        NextChar();
                        return true;
                    }
                }
                return false;
            }

            #endregion
        }

        /// <summary>
        /// Execute the lexical analysis of a file.
        /// </summary>
        /// <param name="file">The file path.</param>
        /// <param name="errors">List to save errors.</param>
        /// <returns>List of tokens.</returns>
        public static List<Token> Analyze(string file, List<Error> errors)
        {
            using (LexicalAnalysis analyzer = new LexicalAnalysis(file, new StreamReader(file)))
            {
                return analyzer.Analyze(errors);
            }
        }
    }
}
