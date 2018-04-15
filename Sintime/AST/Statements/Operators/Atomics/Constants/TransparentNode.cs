using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class TransparentNode : ConstantNode
    {
        public TransparentNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 0;
        }
        
        public override string Keyword
        {
            get
            {
                return "transparent";
            }
        }
        
    }
}
