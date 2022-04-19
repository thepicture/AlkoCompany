using PropertyChanged;

namespace AlkoCompanyNew.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class Klient
    {
        public bool IsInflated
        {
            get
            {
                return !string.IsNullOrWhiteSpace(K_Fio)
                    && !string.IsNullOrWhiteSpace(K_Born)
                    && !string.IsNullOrWhiteSpace(K_TelNumber)
                    && !string.IsNullOrWhiteSpace(K_SeriaPasporta)
                    && !string.IsNullOrWhiteSpace(K_Propiska)
                    && !string.IsNullOrWhiteSpace(K_NomerPasporta)
                    && !string.IsNullOrWhiteSpace(K_DataVidachiPasporta)
                    && !string.IsNullOrWhiteSpace(K_Email);
            }
        }
    }
}
