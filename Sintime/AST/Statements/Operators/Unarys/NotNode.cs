namespace WallE.Sintime.AST.Statements.Operators.Unarys
{
    public class NotNode : UnaryNode
    {
        #region Properties

        public override Associativitys Associativity
        {
            get { return Associativitys.RightToLeft; }
        }

        public override double Precedence { get { return 1; } }

        public override string Keyword { get { return "not"; } }

        #endregion

        #region Constructors
        
        public NotNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        public override int? Operate()
        {
            return (Operand.Operate() == 0) ? 1 : 0;
        }

        #endregion
    }
}
