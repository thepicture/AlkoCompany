using PropertyChanged;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class AnalogiHouse
    {
        public int NumberOfAnalogue { get; set; }
        public float? AH_StartKwm { get; set; }
        public float? AH_KorTorgCenaAfter { get; set; }
        public float? AH_KorNaDatuProdazhiCenaAfter { get; set; }
        public float? AH_KorNaMestoCenaAfter { get; set; }
        public float? AH_KorNaDopPostroykiCenaAfter { get; set; }
        public float? AH_KorNaBlagoustroistvoCenaAfter { get; set; }
        public float? AH_KorNaMebelCenaAfter { get; set; }
        public float? AH_OrdinaryCenaAfter { get; set; }
        public float? AH_KorNaMashtabCenaAfter { get; set; }
        public float? AH_StoimostObjectaCapStroy { get; set; }
        public float? AH_StoimostObjectaCapStroyKvm { get; set; }
        public float? AH_KorNaEtageCenaAfter { get; set; }
        public float? AH_KorNaMaterialStenCenaAfter { get; set; }
        public float? AH_KorNaOtoplenieCenaAfter { get; set; }
        public float? AH_KorNaVodaCenaAfter { get; set; }
        public float? AH_KorNaKanalizaciaCenaAfter { get; set; }
        public float? AH_KorNaFizSostoyanieCenaAfter { get; set; }
        public float? AH_KorNaPloshadCenaAfter { get; set; }
        public float? AH_KorNaVnutrenyuOtdelkyCenaAfter { get; set; }
        public float? AH_KorNaNarugnyuOtdelkyCenaAfter { get; set; }
        public float? AH_ScorrCenaUlucheniy { get; set; }
        public float? AH_ScorrCenaObjects { get; set; }
        public float? AH_ScorrCenaObjectsKvm { get; set; }
        public float? AH_VesovoyKoef { get; set; }

    }
}
