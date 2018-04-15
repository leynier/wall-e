namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class NorthNode : ConstantNode
    {
        public NorthNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 0;
        }
        
        public override string Keyword
        {
            get
            {
                return "north";
            }
        }
        
    }
}
