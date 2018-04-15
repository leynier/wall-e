using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Operators.Atomics
{
    public abstract class AttributeNode : AtomicNode
    {
        public override NodeTypes NodeType
        {
            get
            {
                return NodeTypes.Robot;
            }
        }

        public override string VisualColor
        {
            get
            {
                return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.Gray);
            }
        }

        public override string[] Separators
        {
            get
            {
                return null;
            }
        }

        public AttributeNode(ActionNode action) : base(action) { }
        
        public override bool Checker(IContext context, List<Error> errors)
        {
            if (!CheckerType())
            {
                errors.Add(new Error(File, Line, ErrorTypes.Expected, "The (attributes) is for the robots."));
                IsOK = false;
            }
            return IsOK;
        }
        
    }
}
