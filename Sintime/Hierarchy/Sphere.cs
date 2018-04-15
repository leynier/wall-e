namespace WallE.Hierarchy
{
    public class Sphere : Object, IActionable, IChargeable, IMoveable, IRollable
    {
        public Sphere(int row, int column, int number, int size, int weight, int color) : base(row, column, number, size, weight, 1, color) { }

        public virtual bool Action(Object obj)
        {
            return Charge(obj) || Move(obj) || Roll(obj);
        }

        public virtual bool Charge(Object loadable)
        {
            return Interfaces.Charge(this, loadable);
        }

        public virtual bool Move(Object animate)
        {
            return Interfaces.Move(this, animate);
        }

        public virtual bool Roll(Object animate)
        {
            return Interfaces.Roll(this, animate);
        }
    }
}
