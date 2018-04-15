using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Commons
{
    /// <summary>
    /// Class that represents the action of exiting of the robot.
    /// </summary>
    public class ExitNode : CommonNode
    {
        #region Properties

        public override InstructionsTypes InstructionType { get { return InstructionsTypes.Special;} }

        public override string[] Separators { get { return null; } }

        public override string Keyword { get { return "exit"; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an exit.
        /// </summary>
        /// <param name="action">Action where is the exit.</param>
        public ExitNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "exit")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (exit) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            cursor++;
            return IsOK;
        }

        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            return IsOK;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            if (Action.NodeType == NodeTypes.Robot)
            {
                Action.Program.Robot.Wait();
                stacks.Push(Action.Program.MainAction.Instructions);
            }
            else
            {
                stacks.Clear();
                Action.Program.ProgramRobots.Clear();
            }
            return new Tuple<InstructionNode, bool>(this, true);
        }

        #endregion
    }
}
