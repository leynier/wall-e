using System;
using WallE.Hierarchy;

namespace WallE.Painters
{

    public class PainterBoxMedium : PainterBox
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(BoxMedium);
            }
        }

        protected override float Size
        {
            get
            {
                return 1.9f;
            }
        }
    }

}
