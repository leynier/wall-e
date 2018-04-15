namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class WhiteNode : ConstantNode
    {
        public WhiteNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 7;
        }
        
        public override string Keyword
        {
            get
            {
                return "white";
            }
        }
        
    }
}
