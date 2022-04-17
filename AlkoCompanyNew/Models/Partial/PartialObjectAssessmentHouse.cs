using PropertyChanged;
using System.ComponentModel;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class ObjectAssessmentHouse : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public double MyProperty { get; set; }
    }
}
