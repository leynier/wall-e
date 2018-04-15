using System;
using WallE.Hierarchy;

namespace WallE.Painters
{
    public class PainterPlantSmall : PainterPlant
    {
        public override Type TypeHePaints
        {
            get
            {
                return typeof(PlantSmall);
            }
        }

        protected override float Size
        {
            get
            {
                return 2f;
            }
        }
    }

}
