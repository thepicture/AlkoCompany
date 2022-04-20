using PropertyChanged;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class AnalogiHouse
    {
        public int NumberOfAnalogue { get; set; }
        public float? NewKwm { get; set; }
    }
}
