using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Robots
{
    /// <summary>
    /// Class that represents the action of waiting of the robot.
    /// </summary>
    public class WaitNode : RobotNode
    {
        #region Properties
        
        public override string[] Separators { get { return null; } }

        public override string Keyword { get { return "wait"; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a wait.
        /// </summary>
        /// <param name="action">Action where is the wait.</param>
        public WaitNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "wait")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (wait) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            cursor++;
            return IsOK;
        }

        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            if (!CheckerType())
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (wait) is for the robots."));
                IsOK = false;
            }
            return IsOK;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            Action.Program.Robot.Wait();
            return new Tuple<InstructionNode, bool>(this, true);
        }

        #endregion
    }
}
