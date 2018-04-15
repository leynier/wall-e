namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class MediumNode : ConstantNode
    {
        public MediumNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 2;
        }
        
        public override string Keyword
        {
            get
            {
                return "medium";
            }
        }
        
    }
}
