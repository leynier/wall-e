namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class YellowNode : ConstantNode
    {
        public YellowNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 2;
        }

        public override string Keyword
        {
            get
            {
                return "yellow";
            }
        }
        
    }
}
