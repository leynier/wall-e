using System;
using System.Collections.Generic;
using WallE.Hierarchy;
using WallE.Sintime.AST.Statements;
using WallE.Sintime.AST.Statements.Instructions.Commands.Maps;
using WallE.Sintime.AST.Statements.Instructions.Commands.Robots;

namespace WallE.Sintime.AST
{
    /// <summary>
    /// Sealed class that represents a program.
    /// </summary>
    public sealed class ProgramNode : Node
    {
        #region Properties

        /// <summary>
        /// Main action of the program.
        /// </summary>
        public ActionNode MainAction { get; private set; }

        /// <summary>
        /// List of actions of the program.
        /// </summary>
        public List<ActionNode> Actions { get; private set; }

        /// <summary>
        /// List of robots of the program.
        /// </summary>
        public List<Tuple<ProgramNode, IEnumerator<Tuple<InstructionNode, bool>>>> ProgramRobots { get; set; }

        /// <summary>
        /// Memory of the program.
        /// </summary>
        public Memory Memory { get; private set; }

        /// <summary>
        /// Map of the program.
        /// </summary>
        public Map Map { get; set; }

        /// <summary>
        /// Robot of the program.
        /// </summary>
        public Robot Robot { get; private set; }

        /// <summary>
        /// Object of the program.
        /// </summary>
        public Hierarchy.Object Object { get; set; }

        /// <summary>
        /// Result of the program.
        /// </summary>
        public int? Result { get; set; }

        /// <summary>
        /// Random of the program.
        /// </summary>
        public Random Random { get; }

        public override string Keyword { get { return null; } }

        public override string[] Separators { get { return null; } }

        public override string VisualColor
        {
            get
            {
                return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.DarkBlue);
            }
        }

        private NodeTypes type;

        public override NodeTypes NodeType
        {
            get
            {
                return type;
            }
        }

        private string file;

        public override string File
        {
            get
            {
                return file;
            }
        }

        public override int Line
        {
            get
            {
                return 0;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a program.
        /// </summary>
        /// <param name="file">Path of the file of the program.</param>
        private ProgramNode(string file)
        {
            this.file = file;
            ProgramRobots = new List<Tuple<ProgramNode, IEnumerator<Tuple<InstructionNode, bool>>>>();
            MainAction = new ActionNode(this);
            Actions = new List<ActionNode>();
            Memory = new Memory();
            Random = new Random(Environment.TickCount);
            IsOK = true;
        }

        /// <summary>
        /// Initialize a program if the type is Map.
        /// </summary>
        /// <param name="file">Path of the file of the program.</param>
        /// <param name="map">Map where is the program.</param>
        public ProgramNode(string file, Map map):this(file)
        {
            Map = map;
            type = NodeTypes.Map;
        }

        /// <summary>
        /// Initialize a program if the type is Robot.
        /// </summary>
        /// <param name="file">Path of the file of the program.</param>
        /// <param name="robot">Map where is the program.</param>
        public ProgramNode(string file, Robot robot) : this(file)
        {
            Robot = robot;
            type = NodeTypes.Robot;
        }

        #endregion

        #region Methods

        public override bool Parser(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            // List of instructions to form the main action.
            var tempMain = new List<InstructionNode>();
            // Run by the list of tokens to the end.
            while (cursor < tokens.Count)
            {
                // Check if the token is of action.
                if (tokens[cursor].Text == "action")
                {
                    // ActionNode that matches to parsear for next adding it to the list.
                    var tempAction = new ActionNode(this);
                    // Parsing the action and if itself parsing wrong I mark the program as no OK.
                    if (!tempAction.Parser(tokens, errors, ref cursor))
                        IsOK = false;
                    // Add the action to the list of actions.
                    Actions.Add(tempAction);
                }
                // If the token is not of action.
                else
                {
                    // Search the instance specifies of statement.
                    var tempStatement = Compiler.Identify(tokens[cursor], MainAction);
                    // Check that the statement be an instruction.
                    if (!(tempStatement is InstructionNode))
                    {
                        errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Unknown, string.Format("The instruction ({0}) is not recognized.", tokens[cursor].Text)));
                        // Set on the next line the cursor.
                        while (cursor < tokens.Count && tokens[cursor].Text != "\n")
                            cursor++;
                        cursor++;
                        IsOK = false;
                    }
                    else
                    {
                        // Execute the instruction's Parser I specify.
                        if (!tempStatement.Parser(tokens, errors, ref cursor))
                            IsOK = false;
                        // Add it to temporary the list of instructions.
                        tempMain.Add(tempStatement as InstructionNode);
                    }
                }
            }
            // Create the main action with the list of instructions temporary.
            MainAction.Instructions = tempMain;
            return IsOK;
        }
        
        public override bool Checker(IContext context, List<Error> errors)
        {
            foreach (var i in Actions)
                if (!i.Checker(context,errors))
                    IsOK = false;
            if (!MainAction.Checker(context, errors))
                IsOK = false;
            return IsOK;
        }

        public IEnumerator<Tuple<InstructionNode,bool>> Execute()
        {
            CallStack<InstructionNode> stack = new CallStack<InstructionNode>(MainAction.Instructions);
            var e = stack.GetEnumerator();
            while (true)
            {
                if (!e.MoveNext())
                {
                    stack.Clear();
                    if (NodeType == NodeTypes.Map && ProgramRobots.Count != 0)
                    {
                        var list = new List<InstructionNode>();
                        list.Add(new AdvanceNode(MainAction));
                        stack = new CallStack<InstructionNode>(list);
                        e = stack.GetEnumerator();
                        e.MoveNext();
                    }
                    else if (NodeType == NodeTypes.Robot)
                    {
                        var list = new List<InstructionNode>(MainAction.Instructions);
                        if (e.Current.InstructionType == InstructionsTypes.Normal)
                            list.Insert(0, new WaitNode(MainAction));
                        stack = new CallStack<InstructionNode>(list);
                        e = stack.GetEnumerator();
                        e.MoveNext();
                    }
                    else yield break;
                }
                if (e.Current == null) yield break;
                yield return e.Current.Execute(stack);
                if (e.Current is AdvanceNode)
                {
                    var mask = new bool[ProgramRobots.Count];
                    var count = ProgramRobots.Count;
                    var random = new Random(DateTime.Now.Second);
                    var counter = ProgramRobots.Count;
                    while (counter > 0)
                    {
                        var tempRandom = random.Next(count);
                        if (!mask[tempRandom])
                        {
                            mask[tempRandom] = true;
                            while (ProgramRobots[tempRandom].Item2.MoveNext())
                            {
                                yield return ProgramRobots[tempRandom].Item2.Current;
                                if (ProgramRobots[tempRandom].Item2.Current.Item1.InstructionType == InstructionsTypes.Special)
                                    break;
                            }
                            counter--;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
