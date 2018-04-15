using System;
using WallE.Hierarchy;

namespace WallE.Painters
{

    public class PainterBoxSmall : PainterBox
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(BoxSmall);
            }
        }

        protected override float Size
        {
            get
            {
                return 2.5f;
            }
        }
    }

}
