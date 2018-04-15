using System;
using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Commons
{
    /// <summary>
    /// Class that represents a move.
    /// </summary>
    public class MoveNode : CommonNode
    {
        #region Properties

        /// <summary>
        /// Type of the move.
        /// </summary>
        public MoveTypes MoveType { get; protected set; }

        /// <summary>
        /// Initial row of the move.
        /// </summary>
        public ExpressionNode FromRow { get; protected set; }

        /// <summary>
        /// Initial column of the move.
        /// </summary>
        public ExpressionNode FromColumn { get; protected set; }

        /// <summary>
        /// Final row of the move.
        /// </summary>
        public ExpressionNode ToRow { get; protected set; }

        /// <summary>
        /// Final column of the move.
        /// </summary>
        public ExpressionNode ToColumn { get; protected set; }

        public override InstructionsTypes InstructionType
        {
            get { return MoveType == MoveTypes.Map ? InstructionsTypes.Normal : InstructionsTypes.Special; }
        }

        public override string[] Separators
        {
            get { return new string[] { "from", "row", "=", ",", "column", "to", "forward", "backward" }; }
        }

        public override string Keyword { get { return "move"; } }

        public override NodeTypes NodeType { get { return type; } }

        public override int Line { get { return line; } }

        protected NodeTypes type;

        private Dictionary<string, bool> dictionary;

        private bool from, to;

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a move.
        /// </summary>
        /// <param name="action">Action where is the move.</param>
        public MoveNode(ActionNode action) : base(action)
        {
            dictionary = new Dictionary<string, bool>();
            dictionary.Add("row", false);
            dictionary.Add("column", false);
            from = false;
            to = false;
        }

        #endregion

        #region Methods

        private bool ParserFrom(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            from = true;
            dictionary["row"] = false;
            dictionary["column"] = false;
            if (cursor >= tokens.Count || tokens[cursor].Text != "from")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (from) was expected."));
                return IsOK = false;
            }
            if (cursor + 1 < tokens.Count)
            {
                cursor++;
                if (tokens[cursor].Text == ",")
                {
                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After a (from) there should be an (assign) of (attribute)."));
                    IsOK = false;
                }
                while (cursor < tokens.Count)
                {
                    if (dictionary.ContainsKey(tokens[cursor].Text))
                    {
                        string atributo = tokens[cursor].Text;
                        if (cursor + 1 < tokens.Count)
                        {
                            cursor++;
                            if (tokens[cursor].Text != "=")
                            {
                                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After the (attribute) there should be an (=)."));
                                IsOK = false;
                            }
                            else cursor++;
                            if (cursor < tokens.Count)
                            {
                                var tempExpression = new ExpressionNode(Action);
                                var temp = Compiler.Identify(tokens[cursor], Action);
                                if (temp is OperatorNode)
                                {
                                    if (!tempExpression.Parser(tokens, errors, ref cursor))
                                        IsOK = false;
                                    if (dictionary[atributo])
                                    {
                                        errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : tokens.Count - 1].File, tokens[cursor < tokens.Count ? cursor : tokens.Count - 1].Line, ErrorTypes.Expected, "The attribute (" + atributo + ") already was assigned."));
                                        IsOK = false;
                                    }
                                    else
                                    {
                                        dictionary[atributo] = true;
                                        switch (atributo)
                                        {
                                            case "row":
                                                FromRow = tempExpression;
                                                break;
                                            case "column":
                                                FromColumn = tempExpression;
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (=) there should be an (expression)."));
                                    IsOK = false;
                                }
                            }
                            else
                            {
                                errors.Add(new Error(tokens[cursor - 1].File, tokens[cursor - 1].Line, ErrorTypes.Expected, "After (=) there should be an (expression)."));
                                return IsOK = false;
                            }
                        }
                        else
                        {
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After the (attribute) there should be an (=)."));
                            cursor++;
                            return IsOK = false;
                        }
                    }
                    if (cursor >= tokens.Count)
                        return IsOK;
                    else if (tokens[cursor].Text == ",")
                    {
                        if (cursor + 1 < tokens.Count)
                        {
                            cursor++;
                            if (dictionary.ContainsKey(tokens[cursor].Text))
                                continue;
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After a (,) there should be an (assign) of (attribute)."));
                            IsOK = false;
                        }
                        else
                        {
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After a (,) there should be an (assign) of (attribute)."));
                            cursor++;
                            IsOK = false;
                            break;
                        }
                    }
                    else if (tokens[cursor].Text == "to" && !to)
                    {
                        return ParserTo(tokens, errors, ref cursor);
                    }
                    else if (tokens[cursor].Text != "if" && tokens[cursor].Text != "else" && tokens[cursor].Text != "\n")
                    {
                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, string.Format("The attribute ({0}) is not recognized.", tokens[cursor].Text)));
                        cursor++;
                        IsOK = false;
                        break;
                    }
                    else break;
                }
                return IsOK;
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After a (from) there should be an (assign) of (attribute)."));
            cursor++;
            return IsOK = false;
        }

        private bool ParserTo(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            to = true;
            dictionary["row"] = false;
            dictionary["column"] = false;
            if (cursor >= tokens.Count || tokens[cursor].Text != "to")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (to) was expected."));
                return IsOK = false;
            }
            if (cursor + 1 < tokens.Count)
            {
                cursor++;
                if (tokens[cursor].Text == ",")
                {
                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After a (to) there should be an (assign) of (attribute)."));
                    IsOK = false;
                }
                while (cursor < tokens.Count)
                {
                    if (dictionary.ContainsKey(tokens[cursor].Text))
                    {
                        string atributo = tokens[cursor].Text;
                        if (cursor + 1 < tokens.Count)
                        {
                            cursor++;
                            if (tokens[cursor].Text != "=")
                            {
                                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After the (attribute) there should be an (=)."));
                                IsOK = false;
                            }
                            else cursor++;
                            if (cursor < tokens.Count)
                            {
                                var tempExpression = new ExpressionNode(Action);
                                var temp = Compiler.Identify(tokens[cursor], Action);
                                if (temp is OperatorNode)
                                {
                                    if (!tempExpression.Parser(tokens, errors, ref cursor))
                                        IsOK = false;
                                    if (dictionary[atributo])
                                    {
                                        errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : tokens.Count - 1].File, tokens[cursor < tokens.Count ? cursor : tokens.Count - 1].Line, ErrorTypes.Expected, "The attribute (" + atributo + ") already was assigned."));
                                        IsOK = false;
                                    }
                                    else
                                    {
                                        dictionary[atributo] = true;
                                        switch (atributo)
                                        {
                                            case "row":
                                                ToRow = tempExpression;
                                                break;
                                            case "column":
                                                ToColumn = tempExpression;
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (=) there should be an (expression)."));
                                    IsOK = false;
                                }
                            }
                            else
                            {
                                errors.Add(new Error(tokens[cursor - 1].File, tokens[cursor - 1].Line, ErrorTypes.Expected, "After (=) there should be an (expression)."));
                                return IsOK = false;
                            }
                        }
                        else
                        {
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After the (attribute) there should be an (=)."));
                            cursor++;
                            return IsOK = false;
                        }
                    }
                    if (cursor >= tokens.Count)
                        return IsOK;
                    else if (tokens[cursor].Text == ",")
                    {
                        if (cursor + 1 < tokens.Count)
                        {
                            cursor++;
                            if (dictionary.ContainsKey(tokens[cursor].Text))
                                continue;
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After a (,) there should be an (assign) of (attribute)."));
                            IsOK = false;
                        }
                        else
                        {
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After a (,) there should be an (assign) of (attribute)."));
                            cursor++;
                            IsOK = false;
                            break;
                        }
                    }
                    else if (tokens[cursor].Text == "from" && !from)
                    {
                        return ParserFrom(tokens, errors, ref cursor);
                    }
                    else if (tokens[cursor].Text != "if" && tokens[cursor].Text != "else" && tokens[cursor].Text != "\n")
                    {
                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, string.Format("The attribute ({0}) is not recognized.", tokens[cursor].Text)));
                        cursor++;
                        IsOK = false;
                        break;
                    }
                    else break;
                }
                return IsOK;
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After a (to) there should be an (assign) of (attribute)."));
            cursor++;
            return IsOK = false;
        }

        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            if (!CheckerType())
            {
                if (MoveType == MoveTypes.Map)
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (move) used is for the maps."));
                else
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (move) used is for the robots."));
                IsOK = false;
            }
            if (MoveType == MoveTypes.Map)
            {
                if (FromRow == null)
                {
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "Lack the initial row of move."));
                    IsOK = false;
                }
                if (FromColumn == null)
                {
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "Lack the initial columnn of move."));
                    IsOK = false;
                }
                if (ToRow == null)
                {
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "Lack the final row of move."));
                    IsOK = false;
                }
                if (ToColumn == null)
                {
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "Lack the final column of move."));
                    IsOK = false;
                }
                if (FromRow != null && !FromRow.Checker(context, errors))
                    IsOK = false;
                if (FromColumn != null && !FromColumn.Checker(context, errors))
                    IsOK = false;
                if (ToRow != null && !ToRow.Checker(context, errors))
                    IsOK = false;
                if (ToColumn != null && !ToColumn.Checker(context, errors))
                    IsOK = false;
            }
            return IsOK;
        }

        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "move")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (move) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            if (cursor + 1 < tokens.Count)
            {
                cursor++;
                switch (tokens[cursor].Text)
                {
                    case "forward":
                        MoveType = MoveTypes.Forward;
                        type = NodeTypes.Robot;
                        cursor++;
                        return IsOK;
                    case "backward":
                        MoveType = MoveTypes.Backward;
                        type = NodeTypes.Robot;
                        cursor++;
                        return IsOK;
                    case "from":
                        MoveType = MoveTypes.Map;
                        type = NodeTypes.Map;
                        return ParserFrom(tokens, errors, ref cursor);
                    case "to":
                        MoveType = MoveTypes.Map;
                        type = NodeTypes.Map;
                        return ParserTo(tokens, errors, ref cursor);
                    default:
                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (move) there should be a (forward), a (backward), a (from) or a (to)."));
                        return IsOK = false;
                }
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (move) there should be a (forward), a (backward), a (from) or a (to)."));
            cursor++;
            return IsOK = false;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            var result = true;
            switch (MoveType)
            {
                case MoveTypes.Map:
                    int? fromRow = FromRow.Evaluate();
                    int? fromColumn = FromColumn.Evaluate();
                    int? toRow = ToRow.Evaluate();
                    int? toColumn = ToColumn.Evaluate();
                    if (fromRow != null && fromColumn != null && toRow != null && toColumn != null)
                    {
                        if (!Action.Program.Map.Move((int)fromRow, (int)fromColumn, (int)toRow, (int)toColumn))
                            result = false;
                    }
                    else result = false;
                    break;
                case MoveTypes.Forward:
                    if (!Action.Program.Robot.MoveForward())
                        result = false;
                    break;
                case MoveTypes.Backward:
                    if (!Action.Program.Robot.MoveBackward())
                        result = false;
                    break;
            }
            return new Tuple<InstructionNode, bool>(this, result);
        }

        #endregion
    }

    /// <summary>
    /// Enum that represents the types of movements of the robot.
    /// </summary>
    public enum MoveTypes
    {
        Map,
        Forward,
        Backward
    }
}