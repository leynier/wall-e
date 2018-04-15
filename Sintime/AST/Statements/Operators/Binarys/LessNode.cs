namespace WallE.Sintime.AST.Statements.Operators.Binarys
{
    public class LessNode : BinaryNode
    {
        public LessNode(ActionNode action) : base(action) { }

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
                return 5;
            }
        }

        public override string Keyword
        {
            get
            {
                return "<";
            }
        }

        public override int? Operate()
        {
            return (LeftOperand.Operate() < RigthOperand.Operate()) ? 1 : 0;
        }
        
    }
}
