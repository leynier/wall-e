using WallE.Hierarchy;

namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class DirectionNode : AttributeNode
    {
        public DirectionNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "direction";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map && Action.Program.Object is Animate)
                return (Action.Program.Object as Animate).Direction;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.Direction;
            return 0;
        }
    }
}
