using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Robots
{
    /// <summary>
    /// Class that represents the action of turning of the robot.
    /// </summary>
    public class TurnNode : RobotNode
    {
        #region Properties

        /// <summary>
        /// Type of the turn.
        /// </summary>
        public TurnTypes TurnType { get; protected set; }

        public override string[] Separators { get { return new string[] { "left", "right" }; } }

        public override string Keyword { get { return "turn"; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a turn.
        /// </summary>
        /// <param name="action">Action where is the turn.</param>
        public TurnNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "turn")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (turn) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            // Check that the next token is a (left) or a (right)
            if (cursor + 1 < tokens.Count && (tokens[cursor + 1].Text == "left" || tokens[cursor + 1].Text == "right"))
            {
                cursor++;
                TurnType = tokens[cursor++].Text == "left" ? TurnTypes.Left : TurnTypes.Right;
            }
            else
            {
                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (turn) there should be a (left) or a (right)."));
                cursor++;
                IsOK = false;
            }
            return IsOK;
        }

        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            if (!CheckerType())
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (turn) is for the robots."));
                IsOK = false;
            }
            return IsOK;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            switch (TurnType)
            {
                case TurnTypes.Left:
                    Action.Program.Robot.TurnLeft();
                    break;
                case TurnTypes.Right:
                    Action.Program.Robot.TurnRight();
                    break;
            }
            return new Tuple<InstructionNode, bool>(this, true);
        }

        #endregion
    }

    /// <summary>
    /// Enum that represents the types of turn.
    /// </summary>
    public enum TurnTypes
    {
        Left,
        Right
    }
}
