using WallE.Hierarchy;

namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class FullNode : AttributeNode
    {
        public FullNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "full";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map && Action.Program.Object is Animate)
                return (Action.Program.Object as Animate).Full;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.Full;
            return 0;
        }
    }
}
