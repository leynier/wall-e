using System;

namespace WallE.Sintime.AST.Statements
{
    /// <summary>
    /// Abstract class that represents a instruction.
    /// </summary>
    public abstract class InstructionNode : StatementNode
    {
        #region Properties

        /// <summary>
        /// Type of the instruction.
        /// </summary>
        public abstract InstructionsTypes InstructionType { get; }

        public override string VisualColor
        {
            get
            {
                return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.DarkBlue);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a instruction.
        /// </summary>
        /// <param name="action">Action where is the instruction.</param>
        public InstructionNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        public abstract Tuple<InstructionNode, bool> Execute(CallStack<InstructionNode> stacks);

        #endregion
    }

    /// <summary>
    /// Enum that represents the types of instructions.
    /// </summary>
    public enum InstructionsTypes
    {
        Normal,
        Special
    }
}
