using System.Collections.Generic;
using System.Linq;
using WallE.Sintime.AST.Statements.Operators.Atomics;

namespace WallE.Sintime.AST.Statements
{
    /// <summary>
    /// Class that represents a assign.
    /// </summary>
    public class AssignNode : StatementNode
    {
        #region Properties

        /// <summary>
        /// Expression of the assign.
        /// </summary>
        public ExpressionNode ExpressionAssign { get; protected set; }

        /// <summary>
        /// Variable of the assign.
        /// </summary>
        public VariableNode VariableAssign { get; protected set; }

        public string VariableName { get; protected set; }

        public int? VariableValue { get; protected set; }

        public List<int?> VariableIndex { get; protected set; }

        public override string VisualColor
        {
            get { return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.SteelBlue); }
        }

        public override string Keyword { get { return null; } }

        public override string[] Separators { get { return new string[] { "=" }; } }

        public override NodeTypes NodeType { get { return NodeTypes.Common; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an assign.
        /// </summary>
        /// <param name="action">Action where is the instruction.</param>
        public AssignNode(ActionNode action) : base(action)
        {
            VariableAssign = new VariableNode(Action);
            ExpressionAssign = new ExpressionNode(Action);
        }

        #endregion

        #region Methods

        public override bool Parser(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            // If the file is not finished.
            if (cursor < tokens.Count)
            {
                line = tokens[cursor].Line;
                VariableAssign = new VariableNode(Action);
                // If the Parser of the (variable) is not executed correctly.
                if (!VariableAssign.Parser(tokens, errors, ref cursor))
                    IsOK = false;
                // If the file is not finished.
                if (cursor < tokens.Count)
                {
                    // If the current token is not (=)
                    if (tokens[cursor].Text != "=")
                    {
                        errors.Add(new Error(tokens[cursor - 1].File, tokens[cursor - 1].Line, ErrorTypes.Expected, "After the (variable) of the (assign) there should be an (=)."));
                        IsOK = false;
                    }
                    else cursor++;
                    // If the file is not finished.
                    if (cursor < tokens.Count)
                    {
                        ExpressionAssign = new ExpressionNode(Action);
                        // Return the result of the Parser of the expression.
                        var temp = Compiler.Identify(tokens[cursor], Action);
                        if (temp is OperatorNode)
                        {
                            if (!ExpressionAssign.Parser(tokens, errors, ref cursor))
                                IsOK = false;
                        }
                        else
                        {
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After the (=) sign there should be an (expression)."));
                            IsOK = false;
                        }
                        return IsOK;
                    }
                    errors.Add(new Error(tokens[cursor - 1].File, tokens[cursor - 1].Line, ErrorTypes.Expected, "After the (=) sign there should be an (expression)."));
                    return IsOK = false;

                }
                errors.Add(new Error(tokens[cursor - 1].File, tokens[cursor - 1].Line, ErrorTypes.Expected, "After the (variable) of the (assign) there should be an (=)."));
                return IsOK = false;
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "An (assign) was expected."));
            return IsOK = false;
        }
        
        public override bool Checker(IContext context, List<Error> errors)
        {
            if (!VariableAssign.Checker(context, errors))
                IsOK = false;
            if (!ExpressionAssign.Checker(context, errors))
                IsOK = false;
            return IsOK;
        }

        public virtual bool Assignate()
        {
            VariableName = VariableAssign.Id.Name;
            VariableValue = ExpressionAssign.Evaluate();
            VariableIndex = VariableAssign.Index.ConvertAll(a => a.Evaluate());
            return Action.Program.Memory.Set(VariableName, VariableValue, VariableIndex);
        }

        #endregion
    }
}
