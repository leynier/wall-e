namespace WallE.Sintime.AST.Statements.Operators.Binarys
{
    public class LogicOrNode : BinaryNode
    {
        public LogicOrNode(ActionNode action) : base(action) { }

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
                return 9;
            }
        }

        public override string Keyword
        {
            get
            {
                return "|";
            }
        }

        public override int? Operate()
        {
            return LeftOperand.Operate() | RigthOperand.Operate();
        }
        
    }
}
