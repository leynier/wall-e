using System;
using System.Collections.Generic;
using System.Drawing;

namespace WallE.Painters
{

    public abstract class Painter
    {
        public abstract Type TypeHePaints { get; }

        public abstract bool Paint(int row, int column, int sizeCell, Graphics e, object _object);
        
        protected Brush GetBrush(int color)
        {
            switch (color)
            {
                case 1: //Red
                    return Brushes.Red;
                case 2: //Yellow
                    return Brushes.Yellow;
                case 3: //Green
                    return Brushes.Green;
                case 4: //Blue
                    return Brushes.Blue;
                case 5: //Brown
                    return Brushes.Brown;
                case 6: //Black
                    return Brushes.Black;
                default: //White
                    return Brushes.White;
            }
        }

        protected Pen GetPen()
        {
            Pen pen = new Pen(Color.Black) { Width = 3 };
            return pen;
        }

    }
    
}
