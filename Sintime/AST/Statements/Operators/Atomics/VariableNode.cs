using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallE.Sintime.AST.Statements.Operators.Atomics
{
    /// <summary>
    /// Class that represents a variable.
    /// </summary>
    public class VariableNode : AtomicNode
    {
        #region Properties

        /// <summary>
        /// List of indexes of the variable.
        /// </summary>
        public List<ExpressionNode> Index { get; protected set; }

        /// <summary>
        /// Identifier of the variable
        /// </summary>
        public IdNode Id { get; protected set; }

        public override string Keyword { get { return null; } }

        public override string[] Separators
        {
            get { return new string[] { "[", "]", ";" }; }
        }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a variable.
        /// </summary>
        /// <param name="action">Action where is the variable.</param>
        public VariableNode(ActionNode action) : base(action)
        {
            Index = new List<ExpressionNode>();
        }

        #endregion

        #region Methods

        public override int? Operate()
        {
            return Action.Program.Memory.Get(Id.Name, Index.Select(a => a.Evaluate()).ToList());
        }
        
        public override bool Parser(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            // Check if the current token is not an identifier.
            if (tokens[cursor].Type != TokenTypes.Identifier)
            {
                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "An (identifier) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            Id = new IdNode();
            Id.Parser(tokens, errors, ref cursor);
            // If the identifier begins (@) his name is changed.
            if (Id.Name[0] == '@')
            {
                StringBuilder s = new StringBuilder(Action.Id.Name);
                s.Append("_");
                s.Append(Id.Name, 1, Id.Name.Length - 1);
                Id = new IdNode(s.ToString());
            }
            // If it is not the end of file and it is an ([).
            if (cursor < tokens.Count && tokens[cursor].Text == "[")
            {
                // If the next token is not the end of file.
                if (cursor + 1 < tokens.Count)
                {
                    cursor++;
                    var temp = Compiler.Identify(tokens[cursor], Action);
                    if (temp is OperatorNode)
                    {
                        var tempExpression = new ExpressionNode(Action);
                        if (!tempExpression.Parser(tokens, errors, ref cursor))
                            IsOK = false;
                        Index.Add(tempExpression);
                    }
                    else
                    {
                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After ([) there should be an expression."));
                        IsOK = false;
                    }
                    while (cursor < tokens.Count)
                    {
                        if (tokens[cursor].Text == "]")
                        {
                            cursor++;
                            return IsOK;
                        }
                        else if (tokens[cursor].Text == ";")
                        {
                            if (cursor + 1 < tokens.Count)
                            {
                                cursor++;
                                temp = Compiler.Identify(tokens[cursor], Action);
                                if (temp is OperatorNode)
                                {
                                    var tempExpression = new ExpressionNode(Action);
                                    if (!tempExpression.Parser(tokens, errors, ref cursor))
                                        IsOK = false;
                                    Index.Add(tempExpression);
                                }
                                else
                                {
                                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (;) there should be an expression."));
                                    IsOK = false;
                                }
                            }
                            else
                            {
                                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (;) there should be an expression."));
                                cursor++;
                                return IsOK = false;
                            }
                        }
                        else
                        {
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After them value of indexation of the variables should be a (]) or a (;)."));
                            return IsOK = false;
                        }
                    }
                    errors.Add(new Error(tokens[cursor - 1].File, tokens[cursor - 1].Line, ErrorTypes.Expected, "In an indexed variable it should finish with a (])."));
                    return IsOK = false;
                }
                else
                {
                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After ([) there should be an expression."));
                    cursor++;
                    return IsOK = false;
                }
            }
            return IsOK;
        }
        
        public override bool Checker(IContext context, List<Error> errors)
        {
            if (!context.Checker(this))
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, string.Format("The variable ({0}) already was indexed in another dimension.", Id.Name)));
                IsOK = false;
            }
            foreach(var i in Index)
                if(!i.Checker(context,errors))
                    IsOK = false;
            return IsOK;
        }

        #endregion
    }
}
