using System.Collections.Generic;

namespace WallE.Sintime.AST.Statements.Operators.Atomics
{
    public class NumberNode : AtomicNode
    {
        public override string Keyword
        {
            get
            {
                return null;
            }
        }

        public override string[] Separators
        {
            get
            {
                return null;
            }
        }

        public override int Line
        {
            get
            {
                return line;
            }
        }

        private int line;

        private int value;

        public NumberNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return value;
        }
        
        public override bool Parser(List<Token> tokens, List<Error> errors, ref int cursor)
        {
            if (!int.TryParse(tokens[cursor].Text, out value))
            {
                errors.Add(new Error(tokens[cursor].File, tokens[cursor].Line, ErrorTypes.Expected, "Se esperaba un numero."));
                return IsOK = false;
            }
            line = tokens[cursor].Line;
            cursor++;
            return IsOK;
        }

    }
}
