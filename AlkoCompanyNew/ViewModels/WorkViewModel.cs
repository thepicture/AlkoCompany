using AlkoCompanyNew.Models.Entities;
using System.Collections.ObjectModel;

namespace AlkoCompanyNew.ViewModels
{
    public class WorkViewModel : ViewModelBase
    {
        private Zayavka zayavka;

        public WorkViewModel(Zayavka zayavka)
        {
            Zayavka = zayavka;
            CalculationItems = new ObservableCollection<ObjectAssessmentHouse>
            {
                new ObjectAssessmentHouse
                {
                    IsHeader = true,
                },
                new ObjectAssessmentHouse
                {
                    Criterion = "Объект оценки"
                },
            };
        }

        public Zayavka Zayavka
        {
            get => zayavka;
            set => SetProperty(ref zayavka, value);
        }


        private ObservableCollection<ObjectAssessmentHouse> calculationItems;

        public ObservableCollection<ObjectAssessmentHouse> CalculationItems
        {
            get => calculationItems;
            set => SetProperty(ref calculationItems, value);
        }
    }
}