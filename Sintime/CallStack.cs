using System.Collections;
using System.Collections.Generic;

namespace WallE.Sintime
{
    /// <summary>
    /// Class that represents a stack of calls.
    /// </summary>
    public class CallStack<T> : IEnumerable<T>
    {
        #region Properties

        private Stack<IEnumerator<T>> stack;

        public int Count { get { return stack.Count; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a stack of calls.
        /// </summary>
        public CallStack(IEnumerator<T> enumerator)
        {
            stack = new Stack<IEnumerator<T>>();
            stack.Push(enumerator);
        }

        /// <summary>
        /// Initialize a stack of calls.
        /// </summary>
        public CallStack(IEnumerable<T> enumerable) : this(enumerable.GetEnumerator()) { }

        #endregion

        #region Methods

        public void Push(IEnumerator<T> enumerator)
        {
            stack.Push(enumerator);
        }

        public void Push(IEnumerable<T> enumerable)
        {
            Push(enumerable.GetEnumerator());
        }

        public IEnumerator<T> Peek()
        {
            return stack.Peek();
        }

        public IEnumerator<T> Pop()
        {
            return stack.Pop();
        }

        public void Clear()
        {
            stack.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (stack.Count != 0)
            {
                if (stack.Peek().MoveNext())
                    yield return stack.Peek().Current;
                else
                    stack.Pop();
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
