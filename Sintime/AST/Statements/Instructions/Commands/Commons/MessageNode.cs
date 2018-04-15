using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Commons
{
    /// <summary>
    /// Class that represents a message.
    /// </summary>
    public class MessageNode : CommonNode
    {
        #region Properties

        /// <summary>
        /// String of the message.
        /// </summary>
        public string Message { get; protected set; }
        
        /// <summary>
        /// Expression of the message.
        /// </summary>
        public ExpressionNode MessageExpression { get; protected set; }

        /// <summary>
        /// Type of the message.
        /// </summary>
        public MessageTypes MessageType { get; protected set; }

        public override string Keyword { get { return "message"; } }

        public override string[] Separators { get { return new string[] { "if", "else" }; } }

        public override InstructionsTypes InstructionType { get { return InstructionsTypes.Normal; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a message.
        /// </summary>
        /// <param name="action">Action where is the message.</param>
        public MessageNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "message")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (message) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            // If the next token is not the end of file.
            if (cursor + 1 < tokens.Count)
            {
                cursor++;
                MessageExpression = new ExpressionNode(Action);
                var temp = Compiler.Identify(tokens[cursor], Action);
                // Check that the next token is a string or a number.
                if (tokens[cursor].Type == TokenTypes.StringLiteral)
                {
                    Message = tokens[cursor].Text.Substring(1, tokens[cursor++].Text.Length - 2);
                    MessageType = MessageTypes.String;
                }
                // Check that the next token is a identifier.
                else if (temp is OperatorNode)
                {
                    MessageType = MessageTypes.Expression;
                    if (!MessageExpression.Parser(tokens, errors, ref cursor))
                        IsOK = false;
                }
                else
                {
                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (message) there should be a (string) or an (expression)."));
                    IsOK = false;
                }
                return IsOK;
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (message) there should be a (string) or an (expression)."));
            cursor++;
            return IsOK = false;
        }

        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            if (MessageType == MessageTypes.Expression)
                if (MessageExpression == null || !MessageExpression.Checker(context, errors))
                    IsOK = false;
            return IsOK;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            return new Tuple<InstructionNode, bool>(this, true);
        }

        #endregion
    }

    /// <summary>
    /// Enum that represents the types of messages.
    /// </summary>
    public enum MessageTypes
    {
        String,
        Expression
    }
}
