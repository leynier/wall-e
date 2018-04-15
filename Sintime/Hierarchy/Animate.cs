using System;

namespace WallE.Hierarchy
{
    /// <summary>
    /// Abstract class that represents an animated object.
    /// </summary>
    public abstract class Animate : Object
    {
        #region Virtual Properties

        /// <summary>
        /// The object map.
        /// </summary>
        public virtual Map Map { get; protected set; }

        /// <summary>
        /// The object program.
        /// </summary>
        public virtual string Program { get; protected set; }

        /// <summary>
        /// The object direction.
        /// </summary>
        public virtual int Direction { get; protected set; }

        /// <summary>
        /// The object strong.
        /// </summary>
        public virtual int Strong { get; protected set; }

        /// <summary>
        /// The object time.
        /// </summary>
        public virtual int Time { get; protected set; }

        /// <summary>
        /// The object full.
        /// </summary>
        public virtual int Full { get { return Contents.Count; } }

        /// <summary>
        /// The object distance.
        /// </summary>
        public virtual int Distance
        {
            get
            {
                int distance = 0;
                var cell = GetCoordForwardCell(Row, Column, Direction);
                while (Map.CheckIndex(cell.Item1, cell.Item2) && Map[cell.Item1, cell.Item2] == null)
                {
                    distance++;
                    cell = GetCoordForwardCell(cell.Item1, cell.Item2, Direction);
                }
                return distance;
            }
        }

        /// <summary>
        /// The object numberForward.
        /// </summary>
        public virtual int NumberForward
        {
            get
            {
                var cell = GetCoordForwardCell(Row, Column, Direction);
                if (Map.CheckIndex(cell.Item1, cell.Item2) && Map[cell.Item1, cell.Item2] != null)
                    return Map[cell.Item1, cell.Item2].Number;
                return 0;
            }
        }

        /// <summary>
        /// The object shapeForward.
        /// </summary>
        public virtual int ShapeForward
        {
            get
            {
                var cell = GetCoordForwardCell(Row, Column, Direction);
                if (Map.CheckIndex(cell.Item1, cell.Item2) && Map[cell.Item1, cell.Item2] != null)
                    return Map[cell.Item1, cell.Item2].Shape;
                return 0;
            }
        }

        /// <summary>
        /// The object colorForward.
        /// </summary>
        public virtual int ColorForward
        {
            get
            {
                var cell = GetCoordForwardCell(Row, Column, Direction);
                if (Map.CheckIndex(cell.Item1, cell.Item2) && Map[cell.Item1, cell.Item2] != null)
                    return Map[cell.Item1, cell.Item2].Color;
                return 0;
            }
        }

        /// <summary>
        /// The object sizeForward
        /// </summary>
        public virtual int SizeForward
        {
            get
            {
                var cell = GetCoordForwardCell(Row, Column, Direction);
                if (Map.CheckIndex(cell.Item1, cell.Item2) && Map[cell.Item1, cell.Item2] != null)
                    return Map[cell.Item1, cell.Item2].Size;
                return 0;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an animate object.
        /// </summary>
        /// <param name="row">The object row.</param>
        /// <param name="column">The object column.</param>
        /// <param name="number">The object number.</param>
        /// <param name="size">The object size.</param>
        /// <param name="weight">The object weight.</param>
        /// <param name="shape">The object shape.</param>
        /// <param name="color">The object color.</param>
        /// <param name="direction">The object direction.</param>
        /// <param name="strong">The object strong.</param>
        /// <param name="program">The object program.</param>
        /// <param name="map">The object map.</param>
        public Animate(int row, int column, int number, int size, int weight, int shape, int color, int direction, int strong, string program, Map map) : base(row, column, number, size, weight, shape, color)
        {
            Direction = direction;
            Strong = strong;
            Program = program;
            Map = map;
            Time = 0;
        }

        #endregion

        #region Abstract Methods
        
        abstract public bool MoveForward();

        abstract public bool MoveBackward();

        abstract public void TurnRight();

        abstract public void TurnLeft();

        abstract public void Wait();
        
        #endregion

        #region Public Methods

        public Tuple<int, int> GetCoordForwardCell()
        {
            return GetCoordForwardCell(Row, Column, Direction);
        }

        public Tuple<int, int> GetCoordBackwardCell()
        {
            return GetCoordBackwardCell(Row, Column, Direction);
        }

        #endregion

        #region Protected Methods

        protected Tuple<int, int> GetCoordForwardCell(int row, int column, int direction)
        {
            int rowAdd = 0, columnAdd = 0;
            switch (direction)
            {
                case 0: // North
                    rowAdd = -1;
                    break;
                case 1: // East
                    columnAdd = 1;
                    break;
                case 2: // South
                    rowAdd = 1;
                    break;
                case 3: // West
                    columnAdd = -1;
                    break;
            }
            return new Tuple<int, int>(row + rowAdd, column + columnAdd);
        }

        protected Tuple<int, int> GetCoordBackwardCell(int row, int column, int direction)
        {
            var tuple = GetCoordForwardCell(row, column, direction);
            int rowAdd = tuple.Item1 - row;
            int columnAdd = tuple.Item2 - column;
            return new Tuple<int, int>(row - rowAdd, column - columnAdd);
        }

        #endregion
    }
}
