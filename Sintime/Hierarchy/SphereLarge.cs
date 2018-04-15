namespace WallE.Hierarchy
{
    public class SphereLarge : Sphere
    {
        public SphereLarge(int row, int column, int number, int color) : base(row, column, number, 3, 3, color) { }

        public override bool Action(Object obj)
        {
            return Charge(obj) || Move(obj);
        }
    }
}
