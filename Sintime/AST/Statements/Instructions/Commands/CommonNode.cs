namespace WallE.Sintime.AST.Statements.Instructions.Commands
{
    /// <summary>
    /// Abstract class that represents the common commands.
    /// </summary>
    public abstract class CommonNode : CommandNode
    {
        #region Properties

        public override NodeTypes NodeType { get { return NodeTypes.Common; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a common command.
        /// </summary>
        /// <param name="action">Action where is the command.</param>
        public CommonNode(ActionNode action) : base(action) { }

        #endregion
    }
}
