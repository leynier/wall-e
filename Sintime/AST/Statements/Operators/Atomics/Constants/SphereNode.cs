﻿namespace WallE.Sintime.AST.Statements.Operators.Atomics.Constants
{
    public class SphereNode : ConstantNode
    {
        public SphereNode(ActionNode action) : base(action) { }
        
        public override int? Operate()
        {
            return 1;
        }
        
        public override string Keyword
        {
            get
            {
                return "sphere";
            }
        }

    }
}
