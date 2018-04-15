using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements
{
    /// <summary>
    /// Abstract class that represents a operator.
    /// </summary>
    public abstract class OperatorNode : StatementNode
    {
        #region Properties

        /// <summary>
        /// Arity of the operator.
        /// </summary>
        public abstract int Arity { get; }

        /// <summary>
        /// Associativity of the operator.
        /// </summary>
        public abstract Associativitys Associativity { get; }

        /// <summary>
        /// Precedence of the operator.
        /// </summary>
        public abstract double Precedence { get; }

        public override NodeTypes NodeType { get { return NodeTypes.Common; } }

        public override string VisualColor
        {
            get
            {
                return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.SteelBlue);
            }
        }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a operator.
        /// </summary>
        /// <param name="action">Action where is the operator.</param>
        public OperatorNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        /// <summary>
        /// Method for execute the operation of the operator.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        public abstract int? Operate();

        /// <summary>
        /// Method that fill the operands of the operators.
        /// </summary>
        public abstract bool Fill(Stack<OperatorNode> stack, List<Error> errors, string file, int line);

        public override bool Parser(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor < tokens.Count && tokens[cursor].Text != Keyword)
            {
                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, string.Format("An ({0}) was expected.", Keyword)));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            cursor++;
            return IsOK;
        }

        public override bool Checker(IContext context, List<Error> errors)
        {
            return IsOK;
        }

        #endregion
    }

    /// <summary>
    /// Enum that represents the types of associativitys.
    /// </summary>
    public enum Associativitys { LeftToRight, RightToLeft }
}
