namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class BotNode : ConstantNode
    {
        public BotNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 4;
        }
        
        public override string Keyword
        {
            get
            {
                return "bot";
            }
        }
        
    }
}
