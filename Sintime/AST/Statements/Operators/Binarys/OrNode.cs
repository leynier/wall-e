namespace WallE.Sintime.AST.Statements.Operators.Binarys
{
    public class OrNode : BinaryNode
    { 
        public OrNode(ActionNode action) : base(action) { }

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
                return 11;
            }
        }

        public override string Keyword
        {
            get
            {
                return "or";
            }
        }

        public override int? Operate()
        {
            return IsTrue(LeftOperand.Operate()) || IsTrue(RigthOperand.Operate()) ? 1 : 0;
        }
        
    }
}
