namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class ShapeNode : AttributeNode
    {
        public ShapeNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "shape";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map)
                return Action.Program.Object.Shape;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.ShapeForward;
            return 0;
        }
    }
}
