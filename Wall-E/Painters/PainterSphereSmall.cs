using System;
using WallE.Hierarchy;

namespace WallE.Painters
{

    public class PainterSphereSmall : PainterSphere
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(SphereSmall);
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
