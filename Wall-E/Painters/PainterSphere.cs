using System.Drawing;
using WallE.Hierarchy;

namespace WallE.Painters
{
    public abstract class PainterSphere : Painter
    {
        public override sealed bool Paint(int row, int column, int sizeCell, Graphics e, object _object)
        {
            if (!(_object is Sphere))
                return false;
            Sphere sphere = _object as Sphere;
            var temp = sizeCell / Size;
            e.FillEllipse(GetBrush(sphere.Color), column * sizeCell + (sizeCell - temp) / 2f, row * sizeCell + (sizeCell - temp) / 2f, temp, temp);
            e.DrawEllipse(GetPen(), column * sizeCell + (sizeCell - temp) / 2f, row * sizeCell + (sizeCell - temp) / 2f, temp, temp);
            return true;
        }

        protected abstract float Size { get; }
    }

}
