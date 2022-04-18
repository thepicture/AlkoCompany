using PropertyChanged;
using System.ComponentModel;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class ObjectAssessmentHouse : INotifyPropertyChanged
    {
        public float? OH_NewCenaProdaji { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
