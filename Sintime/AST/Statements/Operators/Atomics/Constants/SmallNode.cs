namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class SmallNode : ConstantNode
    {
        public SmallNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 1;
        }
        
        public override string Keyword
        {
            get
            {
                return "small";
            }
        }
        
    }
}
