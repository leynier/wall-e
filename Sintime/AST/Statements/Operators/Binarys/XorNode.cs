namespace WallE.Sintime.AST.Statements.Operators.Binarys
{
    public class XorNode : BinaryNode
    {
        public XorNode(ActionNode action) : base(action) { }

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
                return 12;
            }
        }

        public override string Keyword
        {
            get
            {
                return "xor";
            }
        }

        public override int? Operate()
        {
            return (IsTrue(LeftOperand.Operate()) && !IsTrue(RigthOperand.Operate())) || (!IsTrue(LeftOperand.Operate()) && IsTrue(RigthOperand.Operate())) ? 1 : 0;
        }
        
    }
}
