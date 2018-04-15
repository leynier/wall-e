using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Operators
{
    /// <summary>
    /// Abstract class that represents an unary operator.
    /// </summary>
    public abstract class UnaryNode : OperatorNode
    {
        #region Properties

        /// <summary>
        /// Operand of the operator.
        /// </summary>
        public OperatorNode Operand { get; protected set; }

        public override int Arity { get { return 1; } }

        public override string[] Separators { get { return null; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an unary operator.
        /// </summary>
        /// <param name="action">Action where is the operator.</param>
        public UnaryNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        public override bool Fill(Stack<OperatorNode> stack, List<Error> errors, string file, int line)
        {
            if (stack.Count > 0)
            {
                Operand = stack.Pop();
                stack.Push(this);
                return IsOK;
            }
            errors.Add(new Error(file, line, ErrorTypes.Unknown, "Lack operands in the expression."));
            return IsOK = false;
        }

        #endregion
    }
}
