namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class TrueNode : ConstantNode
    {
        public TrueNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 1;
        }
        
        public override string Keyword
        {
            get
            {
                return "true";
            }
        }

    }
}
