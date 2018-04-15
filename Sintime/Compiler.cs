using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WallE.Hierarchy;
using WallE.Sintime.AST;
using WallE.Sintime.AST.Statements;
using WallE.Sintime.AST.Statements.Operators.Atomics;

namespace WallE.Sintime
{
    /// <summary>
    /// Static class that represents the compiler.
    /// </summary>
    public static class Compiler
    {
        #region Properties

        private static bool inicialized = false;

        private static string path;

        private static HashSet<string> statements;

        private static HashSet<string> separators;

        private static Dictionary<string, Type> statementNodes;

        public static string Path
        {
            get
            {
                CheckInicialized();
                return path;
            }
        }

        public static HashSet<string> Statements
        {
            get
            {
                CheckInicialized();
                return statements;
            }
        }

        public static HashSet<string> Separators
        {
            get
            {
                CheckInicialized();
                return separators;
            }
        }

        public static Dictionary<string, Type> StatementNodes
        {
            get
            {
                CheckInicialized();
                return statementNodes;
            }
        }

        #endregion

        #region Public Methods

        public static void Initialize()
        {
            Initialize(Environment.CurrentDirectory);
        }

        public static void Initialize(string file)
        {
            inicialized = true;
            path = file;
            statements = new HashSet<string>();
            statements.Add("action");
            separators = new HashSet<string>();
            separators.Add("end");
            statementNodes = new Dictionary<string, Type>();
            foreach (var assembly in SearchAssemblys(path))
                foreach (var node in SearchTypes(assembly))
                    FillStatementAndSeparators(node);
        }

        public static IEnumerator<Tuple<InstructionNode, bool>> Execute(string fileName, List<Error> errors, Map map)
        {
            map.Restart();
            var result = new ProgramNode(fileName, map);
            if (Compile(fileName,errors,result))
                return result.Execute();
            return null;
        }

        public static bool CompileMap(string fileName, List<Error> errors)
        {
            var mapa = new Map();
            var result = new ProgramNode(fileName, mapa);
            return Compile(fileName, errors, result);
        }

        public static bool CompileRobot(string fileName, List<Error> errors)
        {
            var mapa = new Map();
            var robot = new Robot(0, 0, 0, 0, 0, "", mapa);
            var result = new ProgramNode(fileName, robot);
            return Compile(fileName, errors, result);
        }

        public static StatementNode Identify(Token token, ActionNode action)
        {
            CheckInicialized();
            if (token.Type == TokenTypes.NumberLiteral)
                return new NumberNode(action);
            if (token.Type == TokenTypes.Identifier)
                return new VariableNode(action);
            Type temp;
            if (statementNodes.TryGetValue(token.Text, out temp))
                return (StatementNode)Activator.CreateInstance(temp, action);
            return null;
        }

        #endregion

        #region Private Methods

        private static bool Compile(string fileName, List<Error> errors, ProgramNode result)
        {
            CheckInicialized();
            int cursor = 0;
            var tokens = Lexer.Analyze(fileName, errors);
            var ok = true;
            if (!result.Parser(tokens, errors, ref cursor))
                ok = false;
            if (!result.Checker(new Context(), errors))
                ok = false;
            return ok;
        }

        private static IEnumerable<Assembly> SearchAssemblys(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
                if (file.EndsWith(".dll") || file.EndsWith(".exe"))
                    yield return Assembly.LoadFile(file);
        }

        private static IEnumerable<StatementNode> SearchTypes(Assembly assembly)
        {
            ActionNode action = new ActionNode(null);
            foreach (var type in assembly.GetTypes())
                if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(StatementNode)))
                    yield return (StatementNode)Activator.CreateInstance(type, action);
        }

        private static void FillStatementAndSeparators(StatementNode node)
        {
            if (node.Keyword != null)
            {
                if (statements.Contains(node.Keyword))
                {
                    if (node.GetType().IsSubclassOf(statementNodes[node.Keyword]))
                        statementNodes[node.Keyword] = node.GetType();
                    else if (!statementNodes[node.Keyword].IsSubclassOf(node.GetType()))
                        throw new DuplicateWaitObjectException("Collision of the passwords of the instructions.");
                }
                else
                {
                    statements.Add(node.Keyword);
                    statementNodes.Add(node.Keyword, node.GetType());
                }
            }
            if (node.Separators != null)
            {
                foreach (var text in node.Separators)
                    if (text != null && !separators.Contains(text))
                        separators.Add(text);
            }
        }

        private static void CheckInicialized()
        {
            if (!inicialized) Initialize();
        }

        #endregion
    }

}
