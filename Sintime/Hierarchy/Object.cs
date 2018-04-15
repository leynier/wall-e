using System.Collections.Generic;

namespace WallE.Hierarchy
{
    /// <summary>
    /// Abstract class that represents an object.
    /// </summary>
    public abstract class Object
    {
        #region Properties

        /// <summary>
        /// The object row.
        /// </summary>
        public virtual int Row { get; set; }

        /// <summary>
        /// The object column.
        /// </summary>
        public virtual int Column { get; set; }

        /// <summary>
        /// The object size.
        /// </summary>
        public virtual int Size { get; protected set; }

        /// <summary>
        /// The object number.
        /// </summary>
        public virtual int Number { get; protected set; }

        /// <summary>
        /// The object shape.
        /// </summary>
        public virtual int Shape { get; protected set; }

        /// <summary>
        /// The object color.
        /// </summary>
        public virtual int Color { get; protected set; }

        /// <summary>
        /// The object stored.
        /// </summary>
        public virtual int Stored { get; set; }

        /// <summary>
        /// The object weight.
        /// </summary>
        public virtual int Weight
        {
            get
            {
                int result = weight;
                foreach (var i in Contents)
                    result += i.Weight;
                return result;
            }
        }

        /// <summary>
        /// The object load.
        /// </summary>
        public virtual int Load
        {
            get
            {
                int load = 0;
                foreach (var i in Contents)
                    load += i.Size;
                return load;
            }
        }

        /// <summary>
        /// The content of the object.
        /// </summary>
        public List<Object> Contents { get; protected set; }

        protected int weight;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an object.
        /// </summary>
        /// <param name="row">The object row.</param>
        /// <param name="column">The object column.</param>
        /// <param name="number">The object number.</param>
        /// <param name="size">The object size.</param>
        /// <param name="weight">The object weight.</param>
        /// <param name="shape">The object shape.</param>
        /// <param name="color">The object color.</param>
        public Object(int row, int column, int number, int size, int weight, int shape, int color)
        {
            Row = row;
            Column = column;
            Number = number;
            Size = size;
            Shape = shape;
            Color = color;
            Stored = 0;
            this.weight = weight;
            Contents = new List<Object>();
        }

        #endregion
    }
}
