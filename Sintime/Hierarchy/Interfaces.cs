namespace WallE.Hierarchy
{
    public interface IActionable
    {
        bool Action(Object animate);
    }

    public interface IChargeable
    {
        bool Charge(Object animate);
    }

    public interface IDestroyable
    {
        bool Destroy(Object animate);
    }

    public interface ILoadable
    {
        bool UpLoad(Object obj);
        bool DownLoad();
    }

    public interface IMoveable
    {
        bool Move(Object animate);
    }

    public interface IRollable
    {
        bool Roll(Object animate);
    }

    public static class Interfaces
    {
        public static bool Charge(Object chargeable, Object loadable)
        {
            return loadable is ILoadable && (loadable as ILoadable).UpLoad(chargeable);
        }

        public static bool Move(Object moveable, Object animate)
        {
            if (!(animate is Animate)) return false;
            int fromRow = (animate as Animate).GetCoordForwardCell().Item1;
            int fromColumn = (animate as Animate).GetCoordForwardCell().Item2;
            int toRow = 2 * fromRow - (animate as Animate).Row;
            int toColumn = 2 * fromColumn - (animate as Animate).Column;
            return ((animate as Animate).Strong > moveable.Weight) && (animate as Animate).Map.Move(fromRow, fromColumn, toRow, toColumn);
        }

        public static bool Roll(Object rollable, Object animate)
        {
            if (!(animate is Animate)) return false;
            int row = (animate as Animate).GetCoordForwardCell().Item1;
            int column = (animate as Animate).GetCoordForwardCell().Item2;
            int addRow = row - (animate as Animate).Row;
            int addColumn = column - (animate as Animate).Column;
            return ((animate as Animate).Strong > rollable.Weight) && Push((animate as Animate).Map, row, column, addRow, addColumn);
        }

        private static bool Push(Map map, int row, int column, int addRow, int addColumn)
        {
            if (!map.CheckIndex(row, column)) return false;
            if (map[row, column] == null) return true;
            if (!(map[row, column] is IRollable)) return false;
            if (map[row, column].Weight > map[row - addRow, column - addColumn].Weight) return false;
            if (Push(map, row + addRow, column + addColumn, addRow, addColumn))
                return map.Move(row, column, row + addRow, column + addColumn);
            return false;
        }
    }
}
