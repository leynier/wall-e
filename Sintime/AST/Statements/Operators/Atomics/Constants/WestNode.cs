namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class WestNode : ConstantNode
    {
        public WestNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 3;
        }
        
        public override string Keyword
        {
            get
            {
                return "west";
            }
        }

    }
}
