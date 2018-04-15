using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Operators
{
    /// <summary>
    /// Abstract class that represents an actomic operator.
    /// </summary>
    public abstract class AtomicNode : OperatorNode
    {
        #region Properties

        public override int Arity { get { return 0; } }

        public override Associativitys Associativity { get { return Associativitys.LeftToRight; } }

        public override double Precedence { get { return 0; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an atomic operator.
        /// </summary>
        /// <param name="action">Action where is the operator.</param>
        public AtomicNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        public override bool Fill(Stack<OperatorNode> stack, List<Error> errors, string file, int line)
        {
            stack.Push(this);
            return IsOK;
        }

        #endregion
    }
}
