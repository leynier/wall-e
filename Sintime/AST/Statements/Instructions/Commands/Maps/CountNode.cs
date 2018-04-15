using System;
using System.Collections.Generic;
using WallE.Hierarchy;
using WallE.Sintime.AST.Statements.Operators.Atomics;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Maps
{
    /// <summary>
    /// Class that represents a count.
    /// </summary>
    public class CountNode : MapNode
    {
        #region Properties

        /// <summary>
        /// Expression of the count.
        /// </summary>
        public ExpressionNode ExpressionCount { get; protected set; }

        public override InstructionsTypes InstructionType { get { return InstructionsTypes.Normal; } }

        public override string[] Separators { get { return null; ; } }

        public override string Keyword { get { return "count"; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a count.
        /// </summary>
        /// <param name="action">Action where is the count.</param>
        public CountNode(ActionNode action) : base(action) { }

        #endregion

        #region Methods

        private bool CheckerCountExpression(IContext context, List<Error> errors, ExpressionNode expression)
        {
            if (expression == null)
                return IsOK = false;
            foreach (var i in expression.Operators)
            {
                if (i is VariableNode)
                {
                    if (!CheckerCountVariable(context, errors, i as VariableNode))
                        IsOK = false;
                }
            }
            return IsOK;
        }

        private bool CheckerCountVariable(IContext context, List<Error> errors, VariableNode variable)
        {
            if (!context.Checker(variable))
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, string.Format("The variable ({0}) already was indexed in another dimension.", variable.Id.Name)));
                IsOK = false;
            }
            if (variable.Index != null)
                foreach (var i in variable.Index)
                    if (!CheckerCountExpression(context, errors, i))
                        IsOK = false;
            return IsOK;
        }
        
        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            if (!CheckerType())
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (count) is for the maps."));
                IsOK = false;
            }
            return CheckerCountExpression(context, errors, ExpressionCount);
        }

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "count")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (count) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            if (cursor + 1 < tokens.Count)
            {
                cursor++;
                var temp = Compiler.Identify(tokens[cursor], Action);
                if (temp is OperatorNode)
                {
                    ExpressionCount = new ExpressionNode(Action);
                    if (!ExpressionCount.Parser(tokens, errors, ref cursor))
                        IsOK = false;
                }
                else
                {
                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (count) there should be an expression."));
                    IsOK = false;
                }
                return IsOK;
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (count) there should be an expression."));
            cursor++;
            return IsOK = false;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            int counter = 0;
            for (int i = 0; i < Action.Program.Map.Rows; i++)
            {
                for (int j = 0; j < Action.Program.Map.Columns; j++)
                {
                    Action.Program.Object = Action.Program.Map[i, j];
                    if (Action.Program.Object != null)
                    {
                        if (IsTrue(ExpressionCount.Evaluate()))
                            counter++;
                        if (Action.Program.Object is Animate && (Action.Program.Object as Animate).Full > 0)
                        {
                            var robot = Action.Program.Object as Animate;
                            foreach (var item in robot.Contents)
                            {
                                Action.Program.Object = item;
                                if (Action.Program.Object != null && IsTrue(ExpressionCount.Evaluate()))
                                    counter++;
                            }
                        }
                    }
                }
            }
            Action.Program.Result = counter;
            return new Tuple<InstructionNode, bool>(this, true);
        }

        #endregion
    }
}
