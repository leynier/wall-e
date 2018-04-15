using System;

namespace WallE.Hierarchy
{
    /// <summary>
    /// Class that represents a map.
    /// </summary>
    public class Map
    {
        #region Properties

        private Object[,] table;

        public int Rows { get { return table.GetLength(0); } }

        public int Columns { get { return table.GetLength(1); } }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize a map.
        /// </summary>
        /// <param name="rows">Rows of the map.</param>
        /// <param name="columns">Columns of the map.</param>
        public Map(int rows = 10, int columns = 20)
        {
            Restart(rows, columns);
        }

        /// <summary>
        /// Restart the map.
        /// </summary>
        /// <param name="rows">Rows of the map.</param>
        /// <param name="columns">Columns of the map.</param>
        public bool Restart(int rows = 10, int columns = 20)
        {
            if (!CheckSize(rows, columns))
                return false;
            table = new Object[rows, columns];
            return true;
        }

        public Object this[int row, int column]
        {
            get
            {
                if (!CheckIndex(row, column))
                    throw new IndexOutOfRangeException();
                return table[row, column];
            }
            set
            {
                if (!CheckIndex(row, column))
                    throw new IndexOutOfRangeException();
                table[row, column] = value;
            }
        }

        public Object CreateObject(int row, int column, int number, int size, int shape, int color)
        {
            if (!CheckIndex(row, column) || this[row, column] != null) return null;
            switch (shape)
            {
                case 1: // Sphere
                    switch (size)
                    {
                        case 1: // SphereSmall
                            return this[row, column] = new SphereSmall(row, column, number, color);
                        case 2: // SphereMedium
                            return this[row, column] = new SphereMedium(row, column, number, color);
                        case 3: // SphereLarge
                            return this[row, column] = new SphereLarge(row, column, number, color);
                    }
                    break;
                case 2: // Box
                    switch (size)
                    {
                        case 1: // BoxSmall
                            return this[row, column] = new BoxSmall(row, column, number, color);
                        case 2: // BoxMedium
                            return this[row, column] = new BoxMedium(row, column, number, color);
                        case 3: // BoxLarge
                            return this[row, column] = new BoxLarge(row, column, number, color);
                    }
                    break;
                case 3: // Plant
                    switch (size)
                    {
                        case 1: // PlantSmall
                            return this[row, column] = new PlantSmall(row, column, number, color);
                        case 2: // PlantMedium
                            return this[row, column] = new PlantMedium(row, column, number, color);
                        case 3: // PlantLarge
                            return this[row, column] = new PlantLarge(row, column, number, color);
                    }
                    break;
            }
            return null;
        }

        public Robot CreateRobot(int row, int column, int number, int color, int direction, string program)
        {
            if (!CheckIndex(row, column) || this[row, column] != null) return null;
            this[row, column] = new Robot(row, column, number, color, direction, program, this);
            return this[row, column] as Robot;
        }

        public bool Move(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (CheckIndex(fromRow, fromColumn) && CheckIndex(toRow, toColumn) && this[fromRow, fromColumn] != null && this[toRow, toColumn] == null)
            {
                this[toRow, toColumn] = this[fromRow, fromColumn];
                this[fromRow, fromColumn] = null;
                this[toRow, toColumn].Row = toRow;
                this[toRow, toColumn].Column = toColumn;
                foreach (var i in this[toRow, toColumn].Contents)
                {
                    i.Row = toRow;
                    i.Column = toColumn;
                }
                return true;
            }
            return false;
        }

        public bool Remove(int row, int column)
        {
            if (CheckIndex(row, column))
            {
                this[row, column] = null;
                return true;
            }
            return false;
        }

        public bool CheckIndex(int row, int column)
        {
            return row >= 0 && column >= 0 && row < Rows && column < Columns;
        }

        protected bool CheckSize(int rows, int columns)
        {
            return rows > 0 && columns > 0;
        }

        #endregion
    }
}
