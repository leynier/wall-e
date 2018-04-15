namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class NumberNode : AttributeNode
    {
        public NumberNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "number";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map)
                return Action.Program.Object.Number;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.NumberForward;
            return 0;
        }
    }
}
