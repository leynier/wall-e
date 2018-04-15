namespace WallE.Hierarchy
{
    public class Plant : Object, IActionable, IChargeable
    {
        public Plant(int row, int column, int number, int size, int weight, int color) : base(row, column, number, size, weight, 3, color) { }

        public virtual bool Action(Object obj)
        {
            return Charge(obj);
        }

        public virtual bool Charge(Object loadable)
        {
            return Interfaces.Charge(this, loadable);
        }
    }
}
