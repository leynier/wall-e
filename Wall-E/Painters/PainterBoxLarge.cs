using System;
using WallE.Hierarchy;

namespace WallE.Painters
{
    public class PainterBoxLarge : PainterBox
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(BoxLarge);
            }
        }

        protected override float Size
        {
            get
            {
                return 1.3f;
            }
        }
    }

}
