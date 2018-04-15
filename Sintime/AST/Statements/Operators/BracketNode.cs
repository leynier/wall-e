using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Operators
{
    /// <summary>
    /// Abstract class that represents a bracket
    /// </summary>
    public abstract class BracketNode : OperatorNode
    {
        #region Properties

        public override int Arity { get { return 0; } }

        public override Associativitys Associativity { get { return Associativitys.LeftToRight; } }

        public override double Precedence { get { return 0; } }

        public override string VisualColor
        {
            get { return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.DarkGray); }
        }

        public override string[] Separators { get { return null; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a bracket.
        /// </summary>
        /// <param name="action">Action where is the bracket.</param>
        public BracketNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        public override int? Operate()
        {
            return null;
        }

        public override bool Fill(Stack<OperatorNode> stack, List<Error> errors, string file, int line)
        {
            errors.Add(new Error(file, line, ErrorTypes.Unknown, "The bracket this bad positioned in the expression."));
            return IsOK = false;
        }

        #endregion
    }

}
