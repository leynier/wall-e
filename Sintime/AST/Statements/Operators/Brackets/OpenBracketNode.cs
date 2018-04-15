namespace WallE.Sintime.AST.Statements.Operators.Brackets
{
    public class OpenBracketNode : BracketNode
    {
        #region Properties

        public override string Keyword { get { return "("; } }

        #endregion

        #region Constructors

        public OpenBracketNode(ActionNode action) : base(action) { }

        #endregion
    }
}
