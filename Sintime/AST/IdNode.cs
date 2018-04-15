using System.Collections.Generic;

namespace WallE.Sintime.AST
{
    /// <summary>
    /// Class that represents an identifier.
    /// </summary>
    public class IdNode : Node
    {
        #region Properties

        /// <summary>
        /// Name of the identifier.
        /// </summary>
        public string Name { get; protected set; }

        public override NodeTypes NodeType { get { return NodeTypes.Common; } }

        private string file;

        public override string File { get { return file; } }

        private int line;

        public override int Line { get { return line; } }

        public override string[] Separators { get { return null; } }

        public override string Keyword { get { return null; } }

        public override string VisualColor
        {
            get
            {
                return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.Blue);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an identifier.
        /// </summary>
        public IdNode()
        {
            IsOK = true;
        }

        /// <summary>
        /// Initialize an identifier.
        /// </summary>
        /// <param name="name">Name of the identifier.</param>
        public IdNode(string name)
        {
            Name = name;
            IsOK = true;
        }

        #endregion

        #region Methods

        public override bool Parser(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor < tokens.Count && tokens[cursor].Type == TokenTypes.Identifier)
            {
                file = tokens[cursor].File;
                line = tokens[cursor].Line;
                Name = tokens[cursor++].Text;
                return IsOK;
            }
            errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "An identifier was expected."));
            return IsOK = false;
        }
        
        public override bool Checker(IContext context, List<Error> errors)
        {
            return IsOK;
        }

        #endregion
    }
}
