namespace WallE.Sintime.AST.Statements.Operators.Brackets
{
    public class ClosedBracketNode : BracketNode
    {
        #region Properties

        public override string Keyword { get { return ")"; } }

        #endregion

        #region Constructors

        public ClosedBracketNode(ActionNode action) : base(action) { }

        #endregion
    }
}
