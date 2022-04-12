using PropertyChanged;
using System.Linq;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class ObjectAssessmentHouse
    {
        private string[] additionalBuildingTypes;

        public bool IsAnalogue { get; set; }
        public string Action { get; set; }
        public bool IsHeader { get; set; }
        public bool IsEditable => !IsHeader;
        public string Criterion { get; set; } = "Критерий";
        public string City { get; set; }
        public byte[] Image { get; set; }
        public string[] AdditionalBuildingTypes
        {
            get
            {
                if (additionalBuildingTypes == null)
                {

                    additionalBuildingTypes = new string[]
                    {
                        "отсутствуют",
                        "присутствуют"
                    };
                    SelectedAdditionalBuilding = additionalBuildingTypes.First();
                }
                return additionalBuildingTypes;
            }

            set => additionalBuildingTypes = value;
        }
        public string SelectedAdditionalBuilding { get; set; }
        public double Correction { get; set; }
    }
}
