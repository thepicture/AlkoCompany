using PropertyChanged;
using System.ComponentModel;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class AnalogiHouse : INotifyPropertyChanged
    {
        public int NumberOfAnalogue { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public float? NewCena { get; set; }
        public float? NewKwm { get; set; }
    }
}
