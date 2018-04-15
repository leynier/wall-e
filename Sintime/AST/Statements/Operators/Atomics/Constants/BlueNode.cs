namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class BlueNode : ConstantNode
    {
        public BlueNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 4;
        }
        
        public override string Keyword
        {
            get
            {
                return "blue";
            }
        }
        
    }
}
