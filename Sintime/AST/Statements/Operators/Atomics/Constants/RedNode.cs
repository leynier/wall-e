namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class RedNode : ConstantNode
    {
        public RedNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 1;
        }
        
        public override string Keyword
        {
            get
            {
                return "red";
            }
        }
        
    }
}
