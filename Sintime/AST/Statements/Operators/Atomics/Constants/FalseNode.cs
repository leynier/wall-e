namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class FalseNode : ConstantNode
    {
        public FalseNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 0;
        }
        
        public override string Keyword
        {
            get
            {
                return "false";
            }
        }
        
    }
}
