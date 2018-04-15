namespace WallE.Sintime
{
    /// <summary>
    /// Class that represents a token.
    /// </summary>
    public class Token
    {
        #region Properties

        /// <summary>
        /// Text of the token.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Type of the token.
        /// </summary>
        public TokenTypes Type { get; private set; }

        /// <summary>
        /// Path of the file of the token.
        /// </summary>
        public string File { get; private set; }

        /// <summary>
        /// Line of the token on file.
        /// </summary>
        public int Line { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a token.
        /// </summary>
        /// <param name="text">Text of the token.</param>
        /// <param name="file">Path of the file of the token.</param>
        /// <param name="line">Line of the token on file.</param>
        public Token(string text, string file, int line)
        {
            Text = text;
            File = file;
            Line = line;
            Type = GetTokenType();
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            if (Text == "\n")
                return "\\n ( " + Type + " )";
            return Text + " ( " + Type + " )";
        }

        private TokenTypes GetTokenType()
        {
            if (Compiler.Statements.Contains(Text))
                return TokenTypes.Statement;
            if (Compiler.Separators.Contains(Text))
                return TokenTypes.Separator;
            if (char.IsDigit(Text[0]))
                return TokenTypes.NumberLiteral;
            if (Text[0] == '\"')
                return TokenTypes.StringLiteral;
            if (char.IsLetter(Text[0]) || Text[0] == '_' || Text[0] == '@')
                return TokenTypes.Identifier;
            if (Text == "\n")
                return TokenTypes.Separator;
            return TokenTypes.Unknwon;
        }

        #endregion
    }

    /// <summary>
    /// Enum that represents the types of token.
    /// </summary>
    public enum TokenTypes
    {
        /// <summary>
        /// The token is not recognized.
        /// </summary>
        Unknwon,
        /// <summary>
        /// The token represents a number.
        /// </summary>
        NumberLiteral,
        /// <summary>
        /// The token represents a string.
        /// </summary>
        StringLiteral,
        /// <summary>
        /// The token represents a sentence.
        /// </summary>
        Statement,
        /// <summary>
        /// The token represents an identifier.
        /// </summary>
        Identifier,
        /// <summary>
        /// The token represents a separator.
        /// </summary>
        Separator
    }
    
}
