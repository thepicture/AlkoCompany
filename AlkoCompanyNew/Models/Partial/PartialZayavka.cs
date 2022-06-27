using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class Zayavka
    {
        public string IsCompleteString => IsComplete ? "Выполнена" : "В обработке";
        public bool IsComplete
        {
            get
            {
                if (ObjectAssessmentAll == null) return false;
                return ObjectAssessmentAll.OA_PloshadZemli != null
                && ObjectAssessmentAll.OA_PloshadDom != null
                && ObjectAssessmentAll.OA_CenaAll != null
                && ObjectAssessmentAll.OA_CenaZemliKvm != null
                && ObjectAssessmentAll.OA_CenaDomVse != null
                && ObjectAssessmentAll.OA_CenaZemliVse != null
                && ObjectAssessmentAll.OA_CenaDomVse != null
                && ObjectAssessmentAll.OA_CenaDomKvm != null
                && ObjectAssessmentAll.OA_CenaAllKvm != null;
            }
        }
    }
}
