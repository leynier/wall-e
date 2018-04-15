using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallE.Hierarchy;
using WallE.Properties;

namespace WallE.Painters
{
    public class PainterRobot : Painter
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(Robot);
            }
        }

        public override bool Paint(int row, int column, int sizeCell, Graphics e, object _object)
        {
            if (!(_object is Robot))
                return false;
            Robot bot = _object as Robot;
            var temp = sizeCell / 1.1f;
            Bitmap bitmap;
            switch (bot.Color)
            {
                case 1: //Red
                    bitmap = Resources.botred;
                    break;
                case 2: //Yellow
                    bitmap = Resources.botyellow;
                    break;
                case 3: //Green
                    bitmap = Resources.botgreen;
                    break;
                case 4: //Blue
                    bitmap = Resources.botblue;
                    break;
                case 5: //Brown
                    bitmap = Resources.botbrown;
                    break;
                case 6: //Black
                    bitmap = Resources.botblack;
                    break;
                default: //White
                    bitmap = Resources.botwhite;
                    break;
            }
            bitmap.RotateFlip((RotateFlipType)bot.Direction);
            e.DrawImage(bitmap, column * sizeCell + (sizeCell - temp) / 2f, row * sizeCell + (sizeCell - temp) / 2f, temp, temp);
            if (bot.Full > 0)
            {
                Painter painter;
                var objectStored = bot.Contents[bot.Contents.Count - 1];
                if (objectStored is BoxSmall)
                    painter = new PainterBoxSmall();
                else if (objectStored is BoxMedium)
                    painter = new PainterBoxMedium();
                else if (objectStored is BoxLarge)
                    painter = new PainterBoxLarge();
                else if (objectStored is SphereSmall)
                    painter = new PainterSphereSmall();
                else if (objectStored is SphereMedium)
                    painter = new PainterSphereMedium();
                else if (objectStored is SphereLarge)
                    painter = new PainterSphereLarge();
                else if (objectStored is PlantSmall)
                    painter = new PainterPlantSmall();
                else if (objectStored is PlantMedium)
                    painter = new PainterPlantMedium();
                else if (objectStored is PlantLarge)
                    painter = new PainterPlantLarge();
                else painter = new PainterNull();
                painter.Paint(row, column, sizeCell, e, objectStored);
            }
            return true;
        }
    }
}
