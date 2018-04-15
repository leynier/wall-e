using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Commons
{
    /// <summary>
    /// Class that represents an execution.
    /// </summary>
    public class ExecuteNode : CommonNode
    {
        #region Properties

        /// <summary>
        /// Identifier of the execute.
        /// </summary>
        public IdNode Id { get; protected set; }

        /// <summary>
        /// Action of the execute.
        /// </summary>
        public ActionNode ActionExecute { get; protected set; }

        public override string Keyword { get { return "execute"; } }

        public override string[] Separators { get { return null; } }

        public override InstructionsTypes InstructionType { get { return InstructionsTypes.Normal; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an execution.
        /// </summary>
        /// <param name="action">Action where is the execution.</param>
        public ExecuteNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "execute")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "An (execute) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            // Check that the next token is an identifier.
            if (cursor + 1 < tokens.Count && tokens[cursor + 1].Type == TokenTypes.Identifier)
            {
                cursor++;
                Id = new IdNode();
                Id.Parser(tokens, errors, ref cursor);
                //If the identifier begins with (@).
                if (Id.Name[0] == '@')
                {
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (identifier) of (execute) cannot begin with (@)."));
                    IsOK = false;
                    Id = new IdNode(Id.Name.Substring(1));
                }
            }
            else
            {
                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (execute) there should be an (identifier)."));
                cursor++;
                IsOK = false;
            }
            return IsOK;
        }

        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            if (Id == null)
                return IsOK = false;
            foreach (var i in Action.Program.Actions)
            {
                if (i.Id != null && i.Id.Name == Id.Name)
                {
                    ActionExecute = i;
                    return IsOK;
                }
            }
            errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (action) of the (execute) was not found."));
            return IsOK = false;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            stacks.Push(ActionExecute.Instructions);
            return new Tuple<InstructionNode, bool>(this, true);
        }

        #endregion
    }
}
