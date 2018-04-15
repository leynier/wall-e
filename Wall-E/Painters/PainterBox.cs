using System.Drawing;
using WallE.Hierarchy;

namespace WallE.Painters
{

    public abstract class PainterBox : Painter
    {
        public override sealed bool Paint(int row, int column, int sizeCell, Graphics e, object _object)
        {
            if (!(_object is Box))
                return false;
            Box box = _object as Box;
            var temp = sizeCell / Size;
            e.FillRectangle(GetBrush(box.Color), column * sizeCell + (sizeCell - temp) / 2f, row * sizeCell + (sizeCell - temp) / 2f, temp, temp);
            e.DrawRectangle(GetPen(), column * sizeCell + (sizeCell - temp) / 2f, row * sizeCell + (sizeCell - temp) / 2f, temp, temp);
            return true;
        }

        protected abstract float Size { get; }
    }

}
