namespace WallE.Sintime.AST.Statements.Operators.Binarys
{
    public class AddNode : BinaryNode
    {
        public AddNode(ActionNode action) : base(action) { }

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
                return 4;
            }
        }

        public override string Keyword
        {
            get
            {
                return "+";
            }
        }

        public override int? Operate()
        {
            return LeftOperand.Operate() + RigthOperand.Operate();
        }
        
    }
}
