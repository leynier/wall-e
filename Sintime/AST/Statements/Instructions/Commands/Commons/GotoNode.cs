using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Commons
{
    /// <summary>
    /// Class that represents a goto.
    /// </summary>
    public class GotoNode : CommonNode
    {
        #region Properties

        /// <summary>
        /// Identifier of the goto.
        /// </summary>
        public IdNode Id { get; protected set; }

        /// <summary>
        /// Label of the goto.
        /// </summary>
        public LabelNode Label { get; protected set; }

        public override InstructionsTypes InstructionType { get { return InstructionsTypes.Normal; } }

        public override string Keyword { get { return "goto"; } }

        public override string[] Separators { get { return null; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a goto.
        /// </summary>
        /// <param name="action">Action where is the goto.</param>
        public GotoNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "goto")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (goto) was expected."));
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
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (identifier) of (goto) cannot begin with (@)."));
                    IsOK = false;
                    Id = new IdNode(Id.Name.Substring(1));
                }
            }
            else
            {
                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (goto) there should be an (identifier)."));
                cursor++;
                IsOK = false;
            }
            return IsOK;
        }

        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            if (Id == null)
                return IsOK = false;
            foreach (var i in Action.Instructions)
            {
                if (i is LabelNode && (i as LabelNode).Id != null && (i as LabelNode).Id.Name == Id.Name)
                {
                    Label = i as LabelNode;
                    return IsOK;
                }
            }
            errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (label) of the (goto) was not found."));
            return IsOK = false;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            stacks.Pop();
            var e = Action.Instructions.GetEnumerator();
            do { e.MoveNext(); } while (!(e.Current is LabelNode) || (e.Current as LabelNode).Id.Name != Label.Id.Name);
            stacks.Push(e);
            return new Tuple<InstructionNode, bool>(this, true);
        }

        #endregion
    }
}
