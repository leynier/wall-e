using System.Collections.Generic;

namespace WallE.Sintime.AST
{
    /// <summary>
    /// Abstract class that represents the root of the hierarchy of the AST.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Type of the node. (Common, Map, Robot)
        /// </summary>
        public abstract NodeTypes NodeType { get; }

        /// <summary>
        /// Path of the file of the node.
        /// </summary>
        public abstract string File { get; }

        /// <summary>
        /// Line of the token on file.
        /// </summary>
        public abstract int Line { get; }

        /// <summary>
        /// Keyword that represents the node.
        /// </summary>
        public abstract string Keyword { get; }

        /// <summary>
        /// Necessary array of string for the Parser.
        /// </summary>
        public abstract string[] Separators { get; }

        /// <summary>
        /// Color that represents the node.
        /// </summary>
        public abstract string VisualColor { get; }

        /// <summary>
        /// State of the node.
        /// </summary>
        public bool IsOK { get; protected set; }

        /// <summary>
        /// Method to execute the syntax analysis of the node.
        /// </summary>
        /// <param name="tokens">List of tokens to execute the Parser.</param>
        /// <param name="errors">List for the errors found during the Parser.</param>
        /// <param name="cursor">Integer denoting the position where the Parser should be begun.</param>
        /// <returns>Return true if the Parser itself I execute correctly</returns>
        public abstract bool Parser(List<Token> tokens, List<Error> errors, ref int cursor);

        /// <summary>
        /// Method to execute the semantic analysis of the node.
        /// </summary>
        /// <param name="context">Context with the symbol tables.</param>
        /// <param name="errors">List for the possible errors.</param>
        /// <returns>Return true if itself I execute correctly</returns>
        public abstract bool Checker(IContext context, List<Error> errors);

        public override string ToString()
        {
            var result = string.Format("{0} - {1}", Line, GetType().Name);
            if (!IsOK) result += " [Error]";
            return result;
        }
    }

    /// <summary>
    /// Enum that represents the types of node.
    /// </summary>
    public enum NodeTypes
    {
        Common,
        Map,
        Robot
    }
}
