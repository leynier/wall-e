using System.Linq;

namespace WallE.Hierarchy
{
    /// <summary>
    /// Class that represents a robot.
    /// </summary>
    public class Robot : Animate, ILoadable
    {
        #region Constructors

        /// <summary>
        /// Initialize a robot.
        /// </summary>
        /// <param name="row">The object row.</param>
        /// <param name="column">The object column.</param>
        /// <param name="number">The object number.</param>
        /// <param name="color">The object color.</param>
        /// <param name="direction">The object direction.</param>
        /// <param name="program">The object program.</param>
        /// <param name="map">The object map.</param>
        public Robot(int row, int column, int number, int color, int direction, string program, Map map) : base(row, column, number, 3, 4, 4, color, direction, 4, program, map) { }

        #endregion

        #region Virtual Methods
        
        public virtual bool UpLoad(Object obj)
        {
            if (Load + obj.Size <= 1)
            {
                int toRow = GetCoordForwardCell(Row, Column, Direction).Item1;
                int toColumn = GetCoordForwardCell(Row, Column, Direction).Item2;
                Contents.Add(obj);
                Map.Remove(toRow, toColumn);
                obj.Stored = 1;
                return true;
            }
            return false;
        }

        public virtual bool DownLoad()
        {
            int toRow = GetCoordForwardCell(Row, Column, Direction).Item1;
            int toColumn = GetCoordForwardCell(Row, Column, Direction).Item2;
            if (Map.CheckIndex(toRow, toColumn) && Map[toRow, toColumn] == null && Contents.Count != 0)
            {
                Map[toRow, toColumn] = Contents.Last();
                Contents.RemoveAt(Contents.Count - 1);
                Time++;
                Map[toRow, toColumn].Stored = 0;
                return true;
            }
            Time++;
            return false;
        }
        
        #endregion

        #region Override Methods

        public override bool MoveBackward()
        {
            int toRow = GetCoordBackwardCell(Row, Column, Direction).Item1;
            int toColumn = GetCoordBackwardCell(Row, Column, Direction).Item2;
            Time++;
            return Map.Move(Row, Column, toRow, toColumn);
        }

        public override bool MoveForward()
        {
            int toRow = GetCoordForwardCell().Item1;
            int toColumn = GetCoordForwardCell().Item2;
            if (Map.CheckIndex(toRow, toColumn) && Map[toRow, toColumn] is IActionable)
                ((IActionable)Map[toRow, toColumn]).Action(this);
            Time++;
            return Map.Move(Row, Column, toRow, toColumn);
        }

        public override void TurnLeft()
        {
            Direction = (Direction + 3) % 4;
            Time++;
        }

        public override void TurnRight()
        {
            Direction = (Direction + 1) % 4;
            Time++;
        }

        public override void Wait()
        {
            Time++;
        }

        #endregion
    }
}
