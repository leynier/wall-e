using System;
using System.Collections.Generic;
using WallE.Hierarchy;

namespace WallE.Sintime.AST.Statements.Instructions.Commands.Maps
{
    /// <summary>
    /// Class that represents a create.
    /// </summary>
    public class CreateNode : MapNode
    {
        #region Properties

        /// <summary>
        /// Type of the create.
        /// </summary>
        public CreateTypes CreateType { get; protected set; }

        public ExpressionNode Rows { get; protected set; }

        public ExpressionNode Columns { get; protected set; }

        public ExpressionNode Row { get; protected set; }

        public ExpressionNode Column { get; protected set; }

        public ExpressionNode Shape { get; protected set; }

        public ExpressionNode Size { get; protected set; }

        public ExpressionNode Color { get; protected set; }

        public ExpressionNode Number { get; protected set; }

        public ExpressionNode Direction { get; protected set; }

        public string Program { get; protected set; }

        public override InstructionsTypes InstructionType { get {  return InstructionsTypes.Normal; } }

        private Dictionary<string, bool> paramsMap;

        private Dictionary<string, bool> paramsObject;

        private Dictionary<string, bool> paramsRobot;

        public override string[] Separators
        {
            get
            {
                return new string[] { "map", "object", "robot", "=", ",", "rows", "columns", "row", "column", "shape", "color", "size", "number", "program", "direction" };
            }
        }

        public override string Keyword { get { return "create"; } }

        public override int Line { get { return line; } }

        private int line;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a create.
        /// </summary>
        /// <param name="action">Action where is the create.</param>
        public CreateNode(ActionNode action) : base(action)
        {
            paramsMap = new Dictionary<string, bool>();
            paramsObject = new Dictionary<string, bool>();
            paramsRobot = new Dictionary<string, bool>();
            paramsMap.Add("rows", false);
            paramsMap.Add("columns", false);
            paramsObject.Add("row", false);
            paramsObject.Add("column", false);
            paramsObject.Add("shape", false);
            paramsObject.Add("color", false);
            paramsObject.Add("size", false);
            paramsObject.Add("number", false);
            paramsRobot.Add("row", false);
            paramsRobot.Add("column", false);
            paramsRobot.Add("color", false);
            paramsRobot.Add("number", false);
            paramsRobot.Add("program", false);
            paramsRobot.Add("direction", false);
        }

        #endregion

        #region Methods

        private bool ParserAux(List<Token> tokens, List<Error> errors, ref int cursor, Dictionary<string, bool> dictionary, string subcommand)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != subcommand)
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (" + subcommand + ") was expected."));
                return IsOK = false;
            }
            if (cursor + 1 < tokens.Count)
            {
                cursor++;
                if (tokens[cursor].Text == ",")
                {
                    errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After a (" + subcommand+ ") there should be an assignment of attribute."));
                    IsOK = false;
                }
                while (cursor < tokens.Count)
                {
                    if (dictionary.ContainsKey(tokens[cursor].Text))
                    {
                        string atributo = tokens[cursor].Text;
                        if (atributo == "program") Program = "";
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
                                if (atributo == "program")
                                {
                                    if (tokens[cursor].Type == TokenTypes.StringLiteral)
                                        Program = string.Format("{0}\\{1}", Environment.CurrentDirectory, tokens[cursor].Text.Substring(1, tokens[cursor++].Text.Length - 2));
                                    else
                                    {
                                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "Lack the attribute (program) of the robot."));
                                        IsOK = false;
                                    }
                                }
                                else
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
                                                case "rows":
                                                    Rows = tempExpression;
                                                    break;
                                                case "columns":
                                                    Columns = tempExpression;
                                                    break;
                                                case "row":
                                                    Row = tempExpression;
                                                    break;
                                                case "column":
                                                    Column = tempExpression;
                                                    break;
                                                case "shape":
                                                    Shape = tempExpression;
                                                    break;
                                                case "color":
                                                    Color = tempExpression;
                                                    break;
                                                case "size":
                                                    Size = tempExpression;
                                                    break;
                                                case "number":
                                                    Number = tempExpression;
                                                    break;
                                                case "direction":
                                                    Direction = tempExpression;
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (=) there should be an expression."));
                                        IsOK = false;
                                    }
                                }
                            }
                            else
                            {
                                errors.Add(new Error(tokens[cursor - 1].File, tokens[cursor - 1].Line, ErrorTypes.Expected, "After (=) there should be an expression."));
                                return IsOK = false;
                            }
                        }
                        else
                        {
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After the attribute there should be an (equal)."));
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
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (,) there should be an assignment of attribute."));
                            IsOK = false;
                        }
                        else
                        {
                            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (,) there should be an assignment of attribute."));
                            cursor++;
                            IsOK = false;
                            break;
                        }
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
            cursor++;
            return IsOK;
        }
        
        protected override bool ParserCommand(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (cursor >= tokens.Count || tokens[cursor].Text != "create")
            {
                errors.Add(new Error(tokens[cursor < tokens.Count ? cursor : cursor - 1].File, tokens[cursor < tokens.Count ? cursor : cursor - 1].Line, ErrorTypes.Expected, "A (create) was expected."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            if (cursor + 1 < tokens.Count)
            {
                cursor++;
                switch(tokens[cursor].Text)
                {
                    case "map":
                        CreateType = CreateTypes.Map;
                        return ParserAux(tokens, errors, ref cursor, paramsMap, "map");
                    case "object":
                        CreateType = CreateTypes.Object;
                        return ParserAux(tokens, errors, ref cursor, paramsObject, "object");
                    case "robot":
                        CreateType = CreateTypes.Robot;
                        return ParserAux(tokens, errors, ref cursor, paramsRobot, "robot");
                    default:
                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (create) there should be a (map), an (object) or a (robot)."));
                        return IsOK = false;
                }
            }
            errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "After (create) there should be a (map), an (object) or a (robot)."));
            cursor++;
            return IsOK = false;
        }

        protected override bool CheckerCommand(IContext context, List<Error> errors)
        {
            if (!CheckerType())
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (create) is for the maps."));
                IsOK = false;
            }
            if (CreateType == CreateTypes.Robot && Program == null)
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The attribute (program) is obligatory in the robots."));
                IsOK = false;
            }
            if (Rows != null && !Rows.Checker(context, errors))
                IsOK = false;
            if (Columns != null && !Columns.Checker(context, errors))
                IsOK = false;
            if (Row != null && !Row.Checker(context, errors))
                IsOK = false;
            if (Column != null && !Column.Checker(context, errors))
                IsOK = false;
            if (Shape != null && !Shape.Checker(context, errors))
                IsOK = false;
            if (Color != null && !Color.Checker(context, errors))
                IsOK = false;
            if (Direction != null && !Direction.Checker(context, errors))
                IsOK = false;
            if (Number != null && !Number.Checker(context, errors))
                IsOK = false;
            if (Size != null && !Size.Checker(context, errors))
                IsOK = false;
            if (CreateType == CreateTypes.Robot)
            {
                if (System.IO.File.Exists(Program))
                {
                    int row = Row == null || Row.Evaluate() == null ? 0 : (int)Row.Evaluate();
                    int column = Column == null || Column.Evaluate() == null ? 0 : (int)Column.Evaluate();
                    int color = Color == null || Color.Evaluate() == null ? 5 : (int)Color.Evaluate();
                    int direction = Direction == null || Direction.Evaluate() == null ? 0 : (int)Direction.Evaluate();
                    int number = Number == null || Number.Evaluate() == null ? 0 : (int)Number.Evaluate();
                    var robot = new Robot(row, column, number, color, direction, Program, Action.Program.Map);
                    var result = new ProgramNode(Program, robot);
                    int cursor = 0;
                    var tokens = Lexer.Analyze(Program, errors);
                    if (!result.Parser(tokens, errors, ref cursor))
                        IsOK = false;
                    if (!result.Checker(new Context(), errors))
                        IsOK = false;
                }
                else
                {
                    errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (program) of the Robot was not found."));
                    IsOK = false;
                }
            }
            return IsOK;
        }

        protected override Tuple<InstructionNode, bool> ExecuteCommand(CallStack<InstructionNode> stacks)
        {
            var ok = true;
            if (CreateType == CreateTypes.Robot)
            {
                int row = Row == null || !IsTrue(Row.Evaluate()) ? 0 : (int)Row.Evaluate();
                int column = Column == null || !IsTrue(Column.Evaluate()) ? 0 : (int)Column.Evaluate();
                int color = Color == null || !IsTrue(Color.Evaluate()) ? 5 : (int)Color.Evaluate();
                int direction = Direction == null || !IsTrue(Direction.Evaluate()) ? 0 : (int)Direction.Evaluate();
                int number = Number == null || !IsTrue(Number.Evaluate()) ? 0 : (int)Number.Evaluate();
                var robot = Action.Program.Map.CreateRobot(row, column, number, color, direction, Program);
                if (robot != null)
                {
                    var result = new ProgramNode(Program, robot);
                    int cursor = 0;
                    var errors = new List<Error>();
                    var tokens = Lexer.Analyze(Program, errors);
                    result.Parser(tokens, errors, ref cursor);
                    result.Checker(new Context(), errors);
                    Action.Program.ProgramRobots.Add(new Tuple<ProgramNode, IEnumerator<Tuple<InstructionNode, bool>>>(result, result.Execute()));
                }
                else ok = false;
            }
            else if (CreateType == CreateTypes.Object)
            {
                int row = Row == null || !IsTrue(Row.Evaluate()) ? 0 : (int)Row.Evaluate();
                int column = Column == null || !IsTrue(Column.Evaluate()) ? 0 : (int)Column.Evaluate();
                int color = Color == null || !IsTrue(Color.Evaluate()) ? 4 : (int)Color.Evaluate();
                int shape = Shape == null || !IsTrue(Shape.Evaluate()) ? 1 : (int)Shape.Evaluate();
                int number = Number == null || !IsTrue(Number.Evaluate()) ? 0 : (int)Number.Evaluate();
                int size = Size == null || !IsTrue(Size.Evaluate()) ? 2 : (int)Size.Evaluate();
                var objeto = Action.Program.Map.CreateObject(row, column, number, size, shape, color);
                if (objeto == null) ok = false;
            }
            else
            {
                int rows = Rows == null || !IsTrue(Rows.Evaluate()) ? 10 : (int)Rows.Evaluate();
                int columns = Columns == null || !IsTrue(Columns.Evaluate()) ? 20 : (int)Columns.Evaluate();
                if (!Action.Program.Map.Restart(rows, columns))
                    ok = false;
                Action.Program.ProgramRobots = new List<Tuple<ProgramNode, IEnumerator<Tuple<InstructionNode, bool>>>>();
            }
            return new Tuple<InstructionNode, bool>(this, ok);
        }

        #endregion
    }

    /// <summary>
    /// Enum that represents the types of creations.
    /// </summary>
    public enum CreateTypes
    {
        Map,
        Object,
        Robot
    }
}
