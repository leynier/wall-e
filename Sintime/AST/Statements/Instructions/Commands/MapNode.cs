namespace WallE.Sintime.AST.Statements.Instructions.Commands
{
    /// <summary>
    /// Abstract class that represents the commands of the maps.
    /// </summary>
    public abstract class MapNode : CommandNode
    {
        #region Properties

        public override NodeTypes NodeType { get { return NodeTypes.Map; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a command of map.
        /// </summary>
        /// <param name="action">Action where is the command of map.</param>
        public MapNode(ActionNode action) : base(action) { }

        #endregion
    }
}
