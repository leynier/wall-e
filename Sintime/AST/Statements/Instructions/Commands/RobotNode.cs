namespace WallE.Sintime.AST.Statements.Instructions.Commands
{
    /// <summary>
    /// Abstract class that represents the commands of the robots.
    /// </summary>
    public abstract class RobotNode : CommandNode
    {
        #region Properties

        public override NodeTypes NodeType { get { return NodeTypes.Robot; } }

        public override InstructionsTypes InstructionType { get { return InstructionsTypes.Special; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a command of robot.
        /// </summary>
        /// <param name="action">Action where is the command of robot.</param>
        public RobotNode(ActionNode action) : base(action) { }

        #endregion
    }
}
