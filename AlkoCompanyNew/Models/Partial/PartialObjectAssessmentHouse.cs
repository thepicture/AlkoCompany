using PropertyChanged;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class ObjectAssessmentHouse
    {
        public float? OH_NewCenaProdaji { get; set; }
        public float? OH_GeneralProcent { get; set; }
      
    }
}
