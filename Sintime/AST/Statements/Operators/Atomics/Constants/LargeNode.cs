namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class LargeNode : ConstantNode
    {
        public LargeNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 3;
        }
        
        public override string Keyword
        {
            get
            {
                return "large";
            }
        }
        
    }
}
