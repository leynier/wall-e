namespace WallE.Sintime.AST.Statements.Operators.Atomics
{
    /// <summary>
    /// Class that represents a random.
    /// </summary>
    public class RandomNode : AtomicNode
    {
        #region Properties
        
        public override string VisualColor
        {
            get { return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.Gray); }
        }
        
        public override string Keyword { get { return "random"; } }

        public override string[] Separators { get { return null; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a random.
        /// </summary>
        /// <param name="action">Action where is the random.</param>
        public RandomNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods
        
        public override int? Operate()
        {
            return Action.Program.Random.Next();
        }

        #endregion
    }
}
