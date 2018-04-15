namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class StoredNode : AttributeNode
    {
        public StoredNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "stored";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map)
                return Action.Program.Object.Stored;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.Stored;
            return 0;
        }
        
    }
}
