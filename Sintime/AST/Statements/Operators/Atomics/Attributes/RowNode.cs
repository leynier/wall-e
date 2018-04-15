namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class RowNode : AttributeNode
    {
        public RowNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "row";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map)
                return Action.Program.Object.Row;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.Row;
            return 0;
        }
    }
}
