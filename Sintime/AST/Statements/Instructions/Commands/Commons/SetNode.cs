using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Commons
{
    /// <summary>
    /// Class that represents a group of assigns.
    /// </summary>
    public class SetNode : CommonNode
    {
        #region Properties

        /// <summary>
        /// List of assigns.
        /// </summary>
        public List<AssignNode> Assigns { get; protected set; }

        public override InstructionsTypes InstructionType { get { return InstructionsTypes.Normal;  } }

        public override string Keyword { get { return "set"; } }

        public override string[] Separators { get { return new string[] { "," }; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a set.
        /// </summary>
        /// <param name="action">Action where is the set.</param>
        public SetNode(ActionNode action) : base(action)
        {
            Assigns = new List<AssignNode>();
        }

        #endregion

        #region Methods

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "set")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (set) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            if (cursor + 1 < tokens.Count)
            {
                cursor++;
                if (tokens[cursor].Type != TokenTypes.Identifier)
                {
                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (set) there should be an (assign)."));
                    IsOK = false;
                }
                while (cursor < tokens.Count)
                {
                    if (tokens[cursor].Type == TokenTypes.Identifier)
                    {
                        var tempAssign = new AssignNode(Action);
                        if (!tempAssign.Parser(tokens, errors, ref cursor))
                            IsOK = false;
                        Assigns.Add(tempAssign);
                    }
                    if (cursor >= tokens.Count)
                        return IsOK;
                    else if (tokens[cursor].Text == ",")
                    {
                        if (cursor + 1 < tokens.Count)
                        {
                            cursor++;
                            if (tokens[cursor].Type == TokenTypes.Identifier)
                                continue;
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (,) there should be an (assign)."));
                            IsOK = false;
                        }
                        else
                        {
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (,) there should be an (assign)."));
                            cursor++;
                            IsOK = false;
                            break;
                        }
                    }
                    else break;
                }
                return IsOK;
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (set) there should be an (assign)."));
            cursor++;
            return IsOK = false;
        }

        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            foreach (var i in Assigns)
                if (!i.Checker(context, errors))
                    IsOK = false;
            return IsOK;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            foreach (var i in Assigns)
                i.Assignate();
            return new Tuple<InstructionNode, bool>(this, true);
        }

        #endregion
    }
}
