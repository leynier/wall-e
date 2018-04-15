namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class ColumnNode : AttributeNode
    {
        public ColumnNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "column";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map)
                return Action.Program.Object.Column;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.Column;
            return 0;
        }
    }
}
