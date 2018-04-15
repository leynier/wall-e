using System;
using System.Drawing;
using WallE.Properties;

namespace WallE.Painters
{
    public class PainterNull : Painter
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(object);
            }
        }

        public override bool Paint(int row, int column, int sizeCell, Graphics e, object _object)
        {
            e.DrawImage(Resources.Null, column * sizeCell, row * sizeCell, sizeCell, sizeCell);
            return true;
        }
    }

}
