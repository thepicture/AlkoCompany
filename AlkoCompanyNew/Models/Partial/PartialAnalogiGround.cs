using PropertyChanged;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class AnalogiGround
    {
        public int NumberOfAnalogue { get; set; }
        public float? AG_C_PriceAfter { get; set; }
    }
}
