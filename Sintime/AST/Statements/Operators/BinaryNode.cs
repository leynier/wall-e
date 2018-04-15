using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Operators
{
    /// <summary>
    /// Abstract class that represents a binary operator.
    /// </summary>
    public abstract class BinaryNode : OperatorNode
    {
        #region Properties

        /// <summary>
        /// Left operand of the operator.
        /// </summary>
        public OperatorNode LeftOperand { get; protected set; }

        /// <summary>
        /// Right operand of the operator.
        /// </summary>
        public OperatorNode RigthOperand { get; protected set; }

        public override string[] Separators { get { return null; } }

        public override int Arity {get { return 2; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a binary operator.
        /// </summary>
        /// <param name="action">Action where is the operator.</param>
        public BinaryNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        public override bool Fill(Stack<OperatorNode> stack, List<Error> errors, string file, int line)
        {
            if (stack.Count > 1)
            {
                RigthOperand = stack.Pop();
                LeftOperand = stack.Pop();
                stack.Push(this);
                return IsOK;
            }
            errors.Add(new Error(file, line, ErrorTypes.Unknown, "Lack operands in the expression."));
            return IsOK = false;
        }

        #endregion
    }
}
