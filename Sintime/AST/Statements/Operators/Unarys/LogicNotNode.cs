namespace WallE.Sintime.AST.Statements.Operators.Unarys
{
    public class LogicNotNode : UnaryNode
    {
        #region Properties

        public override Associativitys Associativity
        {
            get { return Associativitys.RightToLeft; }
        }

        public override double Precedence { get { return 1; } }

        public override string Keyword { get { return "~"; } }

        #endregion

        #region Constructors

        public LogicNotNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        public override int? Operate()
        {
            return ~Operand.Operate();
        }

        #endregion
    }
}
