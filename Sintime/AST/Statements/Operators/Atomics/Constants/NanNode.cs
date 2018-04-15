namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class NanNode : ConstantNode
    {
        public NanNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return null;
        }
        
        public override string Keyword
        {
            get
            {
                return "nan";
            }
        }
        
    }
}
