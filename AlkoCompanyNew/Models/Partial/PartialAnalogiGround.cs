using PropertyChanged;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class AnalogiGround
    {
        public int NumberOfAnalogue { get; set; }
        public float? AG_C_PriceAfter { get; set; }
        public float? AG_StartKvm { get; set; }
        public float? AG_TorgCenaAfter { get; set; }
        public float? AG_NaPravaCenaAfter { get; set; }
        public float? AG_NaDatuProdazhiCenaAfter { get; set; }
        public float? AG_NaMestoCenaAfter { get; set; }
        public float? AG_NaGasCenaAfter { get; set; }
        public float? AG_NaVodaCenaAfter { get; set; }
        public float? AG_NaElectricCenaAfter { get; set; }
        public float? AG_NaMashtabCenaAfter { get; set; }
    }
}
