namespace WallE.Sintime.AST.Statements.Operators.Binarys
{
    public class LogicXorNode : BinaryNode
    {
        public LogicXorNode(ActionNode action) : base(action) { }

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
                return 8;
            }
        }

        public override string Keyword
        {
            get
            {
                return "^";
            }
        }

        public override int? Operate()
        {
            if (LeftOperand.Operate() == null || RigthOperand.Operate() == null)
                return null;
            return LeftOperand.Operate() ^ RigthOperand.Operate();
        }
        
    }
}
