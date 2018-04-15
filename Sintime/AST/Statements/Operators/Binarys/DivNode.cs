namespace WallE.Sintime.AST.Statements.Operators.Binarys
{
    public class DivNode : BinaryNode
    {
        public DivNode(ActionNode action) : base(action) { }

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
                return 3;
            }
        }

        public override string Keyword
        {
            get
            {
                return "/";
            }
        }

        public override int? Operate()
        {
            if (RigthOperand.Operate() == 0) return null;
            return LeftOperand.Operate() / RigthOperand.Operate();
        }
        
    }
}
