namespace WallE.Hierarchy
{
    public class Box : Object, IActionable, IMoveable, IChargeable
    {
        public Box(int row, int column, int number, int size, int weight, int color) : base(row, column, number, size, weight, 2, color) { }

        public virtual bool Action(Object obj)
        {
            return Charge(obj) || Move(obj);
        }

        public virtual bool Charge(Object loadable)
        {
            return Interfaces.Charge(this, loadable);
        }

        public virtual bool Move(Object animate)
        {
            return Interfaces.Move(this, animate);
        }
    }
}
