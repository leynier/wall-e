using System.Drawing;
using WallE.Hierarchy;
using WallE.Properties;

namespace WallE.Painters
{
    public abstract class PainterPlant : Painter
    {
        public override sealed bool Paint(int row, int column, int sizeCell, Graphics e, object _object)
        {
            if (!(_object is Plant))
                return false;
            Plant plant = _object as Plant;
            var temp = sizeCell / Size;
            Bitmap bitmap;
            switch (plant.Color)
            {
                case 1: //Red
                    bitmap = Resources.plantred;
                    break;
                case 2: //Yellow
                    bitmap = Resources.plantyellow;
                    break;
                case 3: //Green
                    bitmap = Resources.plantgreen;
                    break;
                case 4: //Blue
                    bitmap = Resources.plantblue;
                    break;
                case 5: //Brown
                    bitmap = Resources.plantbrown;
                    break;
                case 6: //Black
                    bitmap = Resources.plantblack;
                    break;
                default: //White
                    bitmap = Resources.plantwhite;
                    break;
            }
            e.DrawImage(bitmap, column * sizeCell + (sizeCell - temp) / 2f, row * sizeCell + (sizeCell - temp) / 2f, temp, temp);
            return true;
        }

        protected abstract float Size { get; }
    }

}
