namespace WallE.Sintime.AST.Statements.Operators.Binarys
{
    public class AndNode : BinaryNode
    {
        public AndNode(ActionNode action) : base(action) { }

        public override Associativitys Associativity
        {
            get
            {
                return Associativitys.LeftToRight;
            }
        }

        public override double Precedence
        {
            get
            {
                return 10;
            }
        }

        public override string Keyword
        {
            get
            {
                return "and";
            }
        }

        public override int? Operate()
        {
            return IsTrue(LeftOperand.Operate()) && IsTrue(RigthOperand.Operate()) ? 1 : 0;
        }
        
    }
}
