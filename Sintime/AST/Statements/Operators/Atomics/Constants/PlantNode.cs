namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class PlantNode : ConstantNode
    {
        public PlantNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 3;
        }
        
        public override string Keyword
        {
            get
            {
                return "plant";
            }
        }
        
    }
}
