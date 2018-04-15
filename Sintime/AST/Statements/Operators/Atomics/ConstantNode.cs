namespace WallE.Sintime.AST.Statements.Operators.Atomics
{
    public abstract class ConstantNode : AtomicNode
    {
        public override string[] Separators
        {
            get
            {
                return null;
            }
        }

        public override string VisualColor
        {
            get
            {
                return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.DimGray);
            }
        }
        
        public ConstantNode(ActionNode action) : base(action) { }
        
    }
}
