using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Maps
{
    /// <summary>
    /// Class that represents a advance.
    /// </summary>
    public class AdvanceNode : MapNode
    {
        #region Properties

        public override InstructionsTypes InstructionType { get { return InstructionsTypes.Special; } }

        public override string[] Separators { get { return null; } }

        public override string Keyword { get { return "advance"; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a advance.
        /// </summary>
        /// <param name="action">Action where is the advance.</param>
        public AdvanceNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            if (!CheckerType())
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (advance) is for the maps."));
                IsOK = false;
            }
            return IsOK;
        }

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "advance")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (advance) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            cursor++;
            return IsOK;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            return new Tuple<InstructionNode, bool>(this, true);
        }

        #endregion
    }
}
