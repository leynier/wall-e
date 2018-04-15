using System.Collections.Generic;

namespace WallE.Sintime.AST
{
    /// <summary>
    /// Abstract class that represents a statement.
    /// </summary>
    public abstract class StatementNode : Node
    {
        #region Properties

        /// <summary>
        /// Action where is the statement.
        /// </summary>
        public ActionNode Action { get; protected set; }

        public override string File { get { return Action.File; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a statement.
        /// </summary>
        /// <param name="action">Action where is the statement.</param>
        public StatementNode(ActionNode action)
        {
            Action = action;
            IsOK = true;
        }

        #endregion

        #region Methods

        protected bool CheckerType()
        {
            if (NodeType != NodeTypes.Common && NodeType != Action.NodeType)
            {
                return IsOK = false;
            }
            return IsOK;
        }

        protected bool IsTrue(int? valor)
        {
            return valor != 0 && valor != null;
        }

        #endregion
    }
}
