namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class SizeNode : AttributeNode
    {
        public SizeNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "size";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map)
                return Action.Program.Object.Size;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.SizeForward;
            return 0;
        }
    }
}
