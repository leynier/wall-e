using System.Collections.Generic;
using System.Drawing;
using WallE.Sintime.AST.Statements;

namespace WallE.Sintime.AST
{
    /// <summary>
    /// Class that represents an action.
    /// </summary>
    public sealed class ActionNode : Node
    {
        #region Properties

        /// <summary>
        /// Identifier of the action.
        /// </summary>
        public IdNode Id { get; private set; }

        /// <summary>
        /// Program where is the action.
        /// </summary>
        public ProgramNode Program { get; private set; }

        /// <summary>
        /// List of contained instructions.
        /// </summary>
        public List<InstructionNode> Instructions { get; set; }

        public override string Keyword { get { return "action"; } }

        public override string[] Separators { get { return new string[] { "end" }; } }

        public override string VisualColor { get { return ColorTranslator.ToHtml(Color.DarkBlue); } }

        public override NodeTypes NodeType { get { return Program.NodeType; } }

        public override string File { get { return Program.File; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an action.
        /// </summary>
        /// <param name="program">Program where is the action.</param>
        public ActionNode(ProgramNode program)
        {
            Program = program;
            Instructions = new List<InstructionNode>();
            IsOK = true;
        }

        #endregion

        #region Methods

        public override bool Parser(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            // Check that the current token be an (action)
            if (cursor >= tokens.Count || tokens[cursor].Text != "action")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "An (action) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            // Check that the that follow to (action) be an (identifier).
            if (cursor + 1 < tokens.Count && tokens[cursor + 1].Type == TokenTypes.Identifier)
            {
                cursor++;
                Id = new IdNode();
                Id.Parser(tokens, errors, ref cursor);
                // Check if the identifier begins with (@).
                if (Id.Name[0] == '@')
                {
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (identifier) of (action) cannot begin with (@)."));
                    IsOK = false;
                    Id = new IdNode(Id.Name.Substring(1));
                }
            }
            else
            {
                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (action) an (identifier) should be."));
                // Set on the next line the cursor.
                while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                    cursor++;
                IsOK = false;
            }
            // Check if the code finished unfinished the (action).
            if (cursor == tokens.Count)
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (end) of the (action) was not found."));
                return IsOK = false;
            }
            // Check that after the identifier of the action he finds a end of line.
            else if (tokens[cursor].Text != "\n")
            {
                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After the (identifier) of the (action) a end of line should be."));
                // Set the cursor in the next line.
                while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                    cursor++;
                IsOK = false;
            }
            cursor++;
            while (cursor < tokens.Count)
            {
                if (tokens[cursor].Text == "end")
                {
                    // Check that after the (end) I finish the code or find a end of line.
                    if (cursor + 1 < tokens.Count && tokens[cursor + 1].Text == "\n" || cursor + 1 == tokens.Count)
                    {
                        cursor += 2;
                        return IsOK;
                    }
                    else
                    {
                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After the (end) a end of line should be."));
                        // Set the cursor in the next line.
                        while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                            cursor++;
                        cursor++;
                        return IsOK = false;
                    }
                }
                // Check that there not be actions intrad of other actions.
                else if (tokens[cursor].Text == "action")
                {
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (end) of the (action) was not found."));
                    return IsOK = false;
                }
                // Search the instance specifies of the (statement).
                var tempStatement = Compiler.Identify(tokens[cursor], this);
                // Check that the (statement) be an (instruction).
                if (!(tempStatement is InstructionNode))
                {
                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Unknown, string.Format("Instruction ({0}) is not recognized.", tokens[cursor].Text)));
                    // Set on the next line the cursor.
                    while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                        cursor++;
                    cursor++;
                    IsOK = false;
                }
                else
                {
                    // Execute the parser of the specific (instruction).
                    if (!tempStatement.Parser(tokens, errors, ref cursor))
                        IsOK = false;
                    Instructions.Add(tempStatement as InstructionNode);
                }
            }
            errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (end) of the (action) was not found."));
            return IsOK = false;
        }

        public override bool Checker(IContext context, List<Error> errors)
        {
            if (!context.Checker(this))
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (identifier) of the (action) already was used previously."));
                IsOK = false;
            }
            var contextChild = context.CreateChildContext();
            foreach (var i in Instructions)
                if (!i.Checker(contextChild, errors))
                    IsOK = false;
            return IsOK;
        }

        #endregion
    }

}
