using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions
{
    /// <summary>
    /// Class that represents a label.
    /// </summary>
    public class LabelNode : InstructionNode
    {
        #region Properties

        /// <summary>
        /// Identifier of the label.
        /// </summary>
        public IdNode Id { get; protected set; }

        public override NodeTypes NodeType { get { return NodeTypes.Common; } }

        public override InstructionsTypes InstructionType { get { return InstructionsTypes.Normal; } }

        public override string Keyword { get { return "label"; } }

        public override string[] Separators { get { return null; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a label.
        /// </summary>
        /// <param name="action">Action where is the label.</param>
        public LabelNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        public override bool Parser(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "label")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (label) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            // Check that the next token is an identifier.
            if (cursor + 1 < tokens.Count && tokens[cursor + 1].Type == TokenTypes.Identifier)
            {
                cursor++;
                Id = new IdNode();
                Id.Parser(tokens, errors, ref cursor);
                // If the identifier begins with (@).
                if (Id.Name[0] == '@')
                {
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (identifier) of the (label) cannot start with (@)."));
                    IsOK = false;
                    Id = new IdNode(Id.Name.Substring(1));
                }
                // Check that the next token is a end of line or the end of file.
                if (cursor < tokens.Count && tokens[cursor].Text != "\n")
                {
                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After the (identifier) of the (label) there should be a (end of line)."));
                    // Set the cursor in the next line.
                    while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                        cursor++;
                    cursor++;
                    return IsOK = false;
                }
                cursor++;
                return IsOK;
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After the (label) there should be an (identifier)."));
            // Set the cursor in the next line.
            while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                cursor++;
            cursor++;
            return IsOK = false;
        }
        
        public override bool Checker(IContext context, List<Error> errors)
        {
            if (!context.Checker(this))
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (identifier) of the (label) already was used previously."));
                IsOK = false;
            }
            return IsOK;
        }

        public override Tuple<InstructionNode, bool> Execute(CallStack<InstructionNode> stacks) { return new Tuple<InstructionNode, bool>(this, true); }

        #endregion
    }
}
