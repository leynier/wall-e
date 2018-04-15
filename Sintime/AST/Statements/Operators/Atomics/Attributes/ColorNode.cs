namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class ColorNode : AttributeNode
    {
        public ColorNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "color";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map)
                return Action.Program.Object.Color;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.ColorForward;
            return 0;
        }
    }
}
