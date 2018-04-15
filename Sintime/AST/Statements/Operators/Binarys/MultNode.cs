namespace WallE.Sintime.AST.Statements.Operators.Binarys
{
    public class MultNode : BinaryNode
    {
        public MultNode(ActionNode action) : base(action) { }

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
                return "*";
            }
        }

        public override int? Operate()
        {
            return LeftOperand.Operate() * RigthOperand.Operate();
        }
        
    }
}
