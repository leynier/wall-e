using System;
using System.Collections.Generic;
using WallE.Hierarchy;
using WallE.Sintime.AST.Statements.Operators.Atomics;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Maps
{
    /// <summary>
    /// Class that represents a destroy.
    /// </summary>
    public class DestroyNode : MapNode
    {
        #region Properties

        /// <summary>
        /// Expression of the destroy.
        /// </summary>
        public ExpressionNode ExpressionDestroy { get; protected set; }

        public override InstructionsTypes InstructionType { get { return InstructionsTypes.Normal; } }

        public override string[] Separators { get { return null; ; } }

        public override string Keyword { get { return "destroy"; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a destroy.
        /// </summary>
        /// <param name="action">Action where is the destroy.</param>
        public DestroyNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods
        
        private bool CheckerDestroyExpression(IContext context, List<Error> errors, ExpressionNode expression)
        {
            if (expression == null)
                return IsOK = false;
            foreach (var i in expression.Operators)
            {
                if (i is VariableNode)
                {
                    if (!CheckerDestroyVariable(context, errors, i as VariableNode))
                        IsOK = false;
                }
            }
            return IsOK;
        }

        private bool CheckerDestroyVariable(IContext context, List<Error> errors, VariableNode variable)
        {
            if (!context.Checker(variable))
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, string.Format("The variable ({0}) already was indexed in another dimension.", variable.Id.Name)));
                IsOK = false;
            }
            if (variable.Index != null)
                foreach (var i in variable.Index)
                    if (!CheckerDestroyExpression(context, errors, i))
                        IsOK = false;
            return IsOK;
        }
        
        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {

            if (!CheckerType())
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (destroy) is for the maps."));
                IsOK = false;
            }
            return CheckerDestroyExpression(context, errors, ExpressionDestroy);
        }

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "destroy")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (destroy) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            if (cursor + 1 < tokens.Count)
            {
                cursor++;
                var temp = Compiler.Identify(tokens[cursor], Action);
                if (temp is OperatorNode)
                {
                    ExpressionDestroy = new ExpressionNode(Action);
                    if (!ExpressionDestroy.Parser(tokens, errors, ref cursor))
                        IsOK = false;
                }
                else
                {
                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (destroy) there should be an expression."));
                    IsOK = false;
                }
                return IsOK;
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (destroy) there should be an expression."));
            cursor++;
            return IsOK = false;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            for (int i = 0; i < Action.Program.Map.Rows; i++)
            {
                for (int j = 0; j < Action.Program.Map.Columns; j++)
                {
                    Action.Program.Object = Action.Program.Map[i, j];
                    if (Action.Program.Object != null)
                    {
                        if (IsTrue(ExpressionDestroy.Evaluate()))
                        {
                            Action.Program.Map.Remove(i, j);
                            if (Action.Program.Object is Animate)
                                for (int k = 0; k < Action.Program.ProgramRobots.Count; k++)
                                    if (Action.Program.ProgramRobots[k].Item1.Robot.Row == i && Action.Program.ProgramRobots[k].Item1.Robot.Column == j)
                                        Action.Program.ProgramRobots.RemoveAt(k);
                        }
                        else if (Action.Program.Object is Animate && (Action.Program.Object as Animate).Full > 0)
                        {
                            var robot = Action.Program.Object as Animate;
                            var list = new List<int>();
                            for (int k = 0; k < robot.Contents.Count; k++)
                            {
                                Action.Program.Object = robot.Contents[k];
                                if (Action.Program.Object != null && IsTrue(ExpressionDestroy.Evaluate()))
                                    list.Add(k);
                            }
                            int cont = 0;
                            foreach (var k in list)
                                robot.Contents.RemoveAt(k - cont--);
                        }
                    }
                }
            }
            return new Tuple<InstructionNode, bool>(this, true);
        }

        #endregion
    }
}
