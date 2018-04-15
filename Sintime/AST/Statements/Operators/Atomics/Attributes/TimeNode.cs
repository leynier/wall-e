using WallE.Hierarchy;

namespace WallE.Sintime.AST.Statements.Operators.Atomics.Attributes
{
    public class TimeNode : AttributeNode
    {
        public TimeNode(ActionNode action) : base(action) { }

        public override string Keyword
        {
            get
            {
                return "time";
            }
        }

        public override int? Operate()
        {
            if (Action.NodeType == NodeTypes.Map && Action.Program.Object is Animate)
                return (Action.Program.Object as Animate).Time;
            if (Action.NodeType == NodeTypes.Robot)
                return Action.Program.Robot.Time;
            return 0;
        }
    }
}
