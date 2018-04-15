using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions
{
    /// <summary>
    /// Abstract class that represents a command.
    /// </summary>
    public abstract class CommandNode : InstructionNode
    {
        #region Properties

        /// <summary>
        /// Expression of the command.
        /// </summary>
        public ExpressionNode ExpressionIf { get; protected set; }

        /// <summary>
        /// Command extra of the command.
        /// </summary>
        public CommandNode CommandElse { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a command.
        /// </summary>
        /// <param name="action">Action where is the command.</param>
        public CommandNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        public override sealed bool Parser(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (!ParserCommand(tokens, errors, ref cursor))
                IsOK = false;
            // Check if the token is an (if) to execute the Parser.
            if (cursor < tokens.Count && tokens[cursor].Text == "if")
            {
                // Check if it is not the end of file.
                if (cursor + 1 < tokens.Count)
                {
                    cursor++;
                    ExpressionIf = new ExpressionNode(Action);
                    var temp = Compiler.Identify(tokens[cursor], Action);
                    if (temp is OperatorNode)
                    {
                        if (!ExpressionIf.Parser(tokens, errors, ref cursor))
                            IsOK = false;
                    }
                    else
                    {
                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (if) there should be an (expression)."));
                        IsOK = false;
                    }
                    // Check if it is not the end of file.
                    if (cursor < tokens.Count)
                    {
                        // Check if the current token is a end of line.
                        if (tokens[cursor].Text == "\n")
                        {
                            cursor++;
                            return IsOK;
                        }
                        // If not, check that the token be an (else).
                        else if (tokens[cursor].Text == "else")
                        {
                            // Check if it is not the end of file.
                            if (cursor + 1 < tokens.Count)
                            {
                                cursor++;
                                // Search the instance specifies of the (statement).
                                var tempStatement = Compiler.Identify(tokens[cursor], Action);
                                // Check that the (statement) be an (command).
                                if (tempStatement is CommandNode)
                                {
                                    if (!tempStatement.Parser(tokens, errors, ref cursor))
                                        IsOK = false;
                                    CommandElse = tempStatement as CommandNode;
                                    return IsOK;
                                }
                                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (else) there should be a (command.)"));
                                // Set the cursor in the next line.
                                while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                                    cursor++;
                                cursor++;
                                return IsOK = false;
                            }
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (else) there should be a (command.)"));
                            // Set the cursor in the next line.
                            while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                                cursor++;
                            cursor++;
                            return IsOK = false;
                        }
                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "A (else) or an (end of line) was expected."));
                        // Set the cursor in the next line.
                        while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                            cursor++;
                        cursor++;
                        return IsOK = false;
                    }
                    return IsOK;
                }
                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (if) there should be an (expression)."));
                cursor++;
                return IsOK = false;
            }
            // If not, check that the token be an end of line or the end of file.
            else if ((cursor < tokens.Count && tokens[cursor].Text == "\n") || (cursor == tokens.Count))
            {
                cursor++;
                return IsOK;
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "A (if) or an (end of line) was expected."));
            // Set the cursor in the next line.
            while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                cursor++;
            cursor++;
            return IsOK = false;
        }

        public override sealed bool Checker(IContext context, List<Error> errors)
        {
            if (!CheckerCommand(context, errors))
                IsOK = false;
            if (ExpressionIf != null && !ExpressionIf.Checker(context, errors))
                IsOK = false;
            if (CommandElse != null && !CommandElse.Checker(context, errors))
                IsOK = false;
            return IsOK;
        }

        public override sealed Tuple<InstructionNode, bool> Execute(CallStack<InstructionNode> stacks)
        {
            if (ExpressionIf == null || IsTrue(ExpressionIf.Evaluate()))
                return ExecuteCommand(stacks); 
            else if (CommandElse != null)
                return CommandElse.Execute(stacks);
            return new Tuple<InstructionNode, bool>(this, false);
        }

        protected abstract bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor);

        protected abstract bool CheckerCommand(IContext context, List<Error> errors);

        protected abstract Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks);

        #endregion
    }

}
