using System.Collections.Generic;

namespace WallE.Sintime
{
    /// <summary>
    /// Class that represents the memory.
    /// </summary>
    public class Memory
    {
        #region Properties

        private Dictionary<string, int> dimentions;
        private Dictionary<string, int?> variables;
        private Dictionary<string, bool> used;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a memory.
        /// </summary>
        public Memory()
        {
            dimentions = new Dictionary<string, int>();
            variables = new Dictionary<string, int?>();
            used = new Dictionary<string, bool>();
        }

        #endregion

        #region Public Methods

        public int? Get(string name, List<int?> index)
        {
            if (!GetUsed(name))
            {
                SetUsed(name);
                SetDimention(name, index.Count);
            }
            if (GetDimetion(name) != index.Count)
                return 0;
            return GetVariable(BuildString(name, index));
        }

        public bool Set(string name, int? value, List<int?> index)
        {
            if (!GetUsed(name))
            {
                SetUsed(name);
                SetDimention(name, index.Count);
            }
            if (GetDimetion(name) != index.Count)
                return false;
            SetVariable(BuildString(name, index), value);
            return true;
        }

        #endregion

        #region Private Methods

        private int GetDimetion(string name)
        {
            int result;
            return dimentions.TryGetValue(name, out result) ? result : 0;
        }

        private void SetDimention(string name, int dimention)
        {
            dimentions.Add(name, dimention);
        }

        private int? GetVariable(string name)
        {
            int? result;
            return variables.TryGetValue(name, out result) ? result : 0;
        }

        private void SetVariable(string name, int? value)
        {
            if (variables.ContainsKey(name))
                variables[name] = value;
            else
                variables.Add(name, value);
        }

        private bool GetUsed(string name)
        {
            bool result;
            return used.TryGetValue(name, out result) ? true : false;
        }

        private void SetUsed(string name)
        {
            used.Add(name, true);
        }

        private string BuildString(string name, List<int?> index)
        {
            foreach (var i in index)
                if (i == null) name += " nan";
                else name += " " + i.ToString();
            return name;
        }

        #endregion
    }

}
