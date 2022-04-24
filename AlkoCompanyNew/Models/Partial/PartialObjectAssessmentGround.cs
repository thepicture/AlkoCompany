using PropertyChanged;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class ObjectAssessmentGround
    {
        public float? OG_PriceKvmAfter { get; set; }
        public float? OG_C_TotalPrice { get; set; }
        public float? OG_GeneralProcent { get; set; }
    }
}
