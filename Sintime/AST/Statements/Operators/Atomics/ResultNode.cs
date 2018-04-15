using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Operators.Atomics
{
    public class ResultNode : AtomicNode
    {
        public override NodeTypes NodeType
        {
            get
            {
                return NodeTypes.Map;
            }
        }

        public ResultNode(ActionNode action) : base(action) { }

        public override string[] Separators
        {
            get
            {
                return null;
            }
        }

        public override string VisualColor
        {
            get
            {
                return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.Gray);
            }
        }

        public override string Keyword
        {
            get
            {
                return "result";
            }
        }

        public override int? Operate()
        {
            return Action.Program.Result;
        }

        public override bool Checker(IContext context, List<Error> errors)
        {
            if (!CheckerType())
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (result) is for the maps."));
                IsOK = false;
            }
            return IsOK;
        }

    }
}
