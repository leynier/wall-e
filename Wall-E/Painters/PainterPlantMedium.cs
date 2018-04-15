using System;
using WallE.Hierarchy;

namespace WallE.Painters
{
    public class PainterPlantMedium : PainterPlant
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(PlantMedium);
            }
        }

        protected override float Size
        {
            get
            {
                return 1.4f;
            }
        }
    }

}
