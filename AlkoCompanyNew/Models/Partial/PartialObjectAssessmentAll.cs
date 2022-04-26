using PropertyChanged;
using System.Linq;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class ObjectAssessmentAll
    {
        public Zayavka FirstRequest => Zayavka.FirstOrDefault();
    }
}
