using System.Collections.Generic;
using WallE.Sintime.AST.Statements.Operators;
using WallE.Sintime.AST.Statements.Operators.Brackets;

namespace WallE.Sintime.AST.Statements
{
    /// <summary>
    /// Class that represent an expression.
    /// </summary>
    public class ExpressionNode : StatementNode
    {
        #region  Properties

        /// <summary>
        /// List of operators of the expression.
        /// </summary>
        public List<OperatorNode> Operators { get; protected set; }

        /// <summary>
        /// Root operator of the expression.
        /// </summary>
        protected OperatorNode value;

        public override NodeTypes NodeType { get { return NodeTypes.Common; } }

        public override string VisualColor
        {
            get
            {
                return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.SteelBlue);
            }
        }

        public override string Keyword { get { return null; } }

        public override string[] Separators { get { return null; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an expression.
        /// </summary>
        /// <param name="action">Action where is the expression.</param>
        public ExpressionNode(ActionNode action) : base(action)
        {
            Operators = new List<OperatorNode>();
        }

        #endregion

        #region Methods

        public virtual int? Evaluate()
        {
            return value.Operate();
        }

        private bool ShuntingYard(List<Error> errors, string file, int line)
        {
            OperatorNode last = null;
            var operators = new Stack<OperatorNode>();
            var output = new Stack<OperatorNode>();
            foreach (var now in this.Operators)
            {
                if (now is AtomicNode)
                {
                    if (last != null && !(last is OpenBracketNode) && !(last is UnaryNode) && !(last is BinaryNode))
                    {
                        errors.Add(new Error(file, line, ErrorTypes.Expected, "Invalid expression."));
                        return IsOK = false;
                    }
                    output.Push(now);
                }
                else if (now is BracketNode)
                {
                    if (now is OpenBracketNode)
                    {
                        if (last != null && !(last is OpenBracketNode) && !(last is UnaryNode) && !(last is BinaryNode))
                        {
                            errors.Add(new Error(file, line, ErrorTypes.Expected, "Invalid expression."));
                            return IsOK = false;
                        }
                        operators.Push(now);
                    }
                    else
                    {
                        if (!(last is ClosedBracketNode) && !(last is AtomicNode))
                        {
                            errors.Add(new Error(file, line, ErrorTypes.Expected, "Invalid expression."));
                            return IsOK = false;
                        }
                        if (operators.Count == 0)
                        {
                            errors.Add(new Error(file, line, ErrorTypes.Expected, "An (open bracket) was expected."));
                            return IsOK = false;
                        }
                        while (!(operators.Peek() is OpenBracketNode))
                        {
                            operators.Pop().Fill(output, errors, file, line);
                            if (operators.Count == 0)
                            {
                                errors.Add(new Error(file, line, ErrorTypes.Expected, "An (open bracket) was expected."));
                                return IsOK = false;
                            }
                        }
                        operators.Pop();
                    }
                }
                else
                {
                    if (now is UnaryNode)
                    {
                        if (last != null && !(last is OpenBracketNode) && !(last is BinaryNode))
                        {
                            errors.Add(new Error(file, line, ErrorTypes.Expected, "Invalid expression."));
                            return IsOK = false;
                        }
                    }
                    else if (now is BinaryNode)
                    {
                        if (!(last is ClosedBracketNode) && !(last is AtomicNode))
                        {
                            errors.Add(new Error(file, line, ErrorTypes.Expected, "Invalid expression."));
                            return IsOK = false;
                        }
                    }
                    while (operators.Count > 0 && !(operators.Peek() is BracketNode)
                        && (now.Precedence > operators.Peek().Precedence
                        || (now.Precedence == operators.Peek().Precedence && now.Associativity == Associativitys.LeftToRight)))
                    {
                        if (!operators.Pop().Fill(output, errors, file, line))
                            return IsOK = false;
                    }
                    operators.Push(now);
                }
                last = now;
            }
            while (operators.Count != 0)
            {
                if (operators.Peek() is OpenBracketNode)
                {
                    errors.Add(new Error(file, line, ErrorTypes.Expected, "An (closed bracket) was expected."));
                    return IsOK = false;
                }
                if (!operators.Pop().Fill(output, errors, file, line))
                    return IsOK = false;
            }
            if (Operators.Count == 0 || output.Count == 0)
            {
                errors.Add(new Error(file, line, ErrorTypes.Expected, "An (expression) was expected."));
                return IsOK = false;
            }
            if (output.Count > 1)
            {
                errors.Add(new Error(file, line, ErrorTypes.Expected, "Lack operators in the expression."));
                return IsOK = false;
            }
            value = output.Pop();
            this.line = value.Line;
            return IsOK = true;
        }

        public override bool Parser(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            while (cursor < tokens.Count)
            {
                var tempStatement = Compiler.Identify(tokens[cursor], Action);
                if (tempStatement is OperatorNode)
                {
                    if (!tempStatement.Parser(tokens, errors, ref cursor))
                        IsOK = false;
                    Operators.Add(tempStatement as OperatorNode);
                }
                else break;
            }
            return ShuntingYard(errors, tokens[cursor - 1].File, tokens[cursor - 1].Line);
        }

        public override bool Checker(IContext context, List<Error> errors)
        {
            foreach (var i in Operators)
                if (!i.Checker(context, errors))
                    IsOK = false;
            return IsOK;
        }

        #endregion
    }
}
