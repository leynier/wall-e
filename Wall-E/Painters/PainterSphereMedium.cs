using System;
using WallE.Hierarchy;

namespace WallE.Painters
{
    public class PainterSphereMedium : PainterSphere
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(SphereMedium);
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
