namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class BlackNode : ConstantNode
    {
        public BlackNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 6;
        }
        
        public override string Keyword
        {
            get
            {
                return "black";
            }
        }
        
    }
}
