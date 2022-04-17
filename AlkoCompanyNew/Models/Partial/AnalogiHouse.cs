using PropertyChanged;
using System.ComponentModel;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class AnalogiHouse : INotifyPropertyChanged
    {
        public int NumberOfAnalogue { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public double MyProperty { get; set; }
    }
}
