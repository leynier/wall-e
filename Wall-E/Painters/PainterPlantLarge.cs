using System;
using WallE.Hierarchy;

namespace WallE.Painters
{
    public class PainterPlantLarge : PainterPlant
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(PlantLarge);
            }
        }

        protected override float Size
        {
            get
            {
                return 1.1f;
            }
        }
    }

}
