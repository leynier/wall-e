using System;
using WallE.Hierarchy;

namespace WallE.Painters
{
    public class PainterSphereLarge : PainterSphere
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(SphereLarge);
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
