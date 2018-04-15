using System.Collections.Generic;
using WallE.Sintime.AST;
using WallE.Sintime.AST.Statements.Instructions;
using WallE.Sintime.AST.Statements.Operators.Atomics;

namespace WallE.Sintime
{
    /// <summary>
    /// Class that represents a context.
    /// </summary>
    public class Context : IContext
    {
        private Context parent;

        private Dictionary<string, LabelNode> labels;

        private Dictionary<string, ActionNode> actions;
        
        private Dictionary<string, int> variables;

        public Context(Context context = null)
        {
            parent = context;
            labels = new Dictionary<string, LabelNode>();
            actions = new Dictionary<string, ActionNode>();
            variables = new Dictionary<string, int>();
        }
        
        public bool Checker(LabelNode label)
        {
            if (label.Id == null)
                return true;
            if (labels.ContainsKey(label.Id.Name))
                return false;
            labels.Add(label.Id.Name, label);
            return true;
        }

        public bool Checker(ActionNode action)
        {
            var temp = this;
            while (temp.parent != null)
                temp = parent;
            if (action.Id == null)
                return true;
            if (temp.actions.ContainsKey(action.Id.Name))
                return false;
            temp.actions.Add(action.Id.Name, action);
            return true;
        }

        public bool Checker(VariableNode variable)
        {
            var temp = this;
            while (temp.parent != null)
                temp = parent;
            if (variable.Id == null)
                return true;
            if (temp.variables.ContainsKey(variable.Id.Name))
            {
                if (temp.variables[variable.Id.Name] != variable.Index.Count)
                    return false;
            }
            else temp.variables.Add(variable.Id.Name, variable.Index.Count);
            return true;
        }
        
        IContext IContext.CreateChildContext()
        {
            return new Context(this);
        }
    }

    /// <summary>
    /// Interface that represents a context.
    /// </summary>
    public interface IContext
    {
        bool Checker(LabelNode label);
        bool Checker(ActionNode action);
        bool Checker(VariableNode variable);
        IContext CreateChildContext();
    }
}
