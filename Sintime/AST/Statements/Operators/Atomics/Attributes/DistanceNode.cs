using WallE.Hierarchy;

namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class DistanceNode : AttributeNode
    {
        public DistanceNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "distance";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map && Action.Program.Object is Animate)
                return (Action.Program.Object as Animate).Distance;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.Distance;
            return 0;
        }
    }
}
