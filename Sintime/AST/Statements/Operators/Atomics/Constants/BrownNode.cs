namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class BrownNode : ConstantNode
    {
        public BrownNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 5;
        }
        
        public override string Keyword
        {
            get
            {
                return "brown";
            }
        }
        
    }
}
