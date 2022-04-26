using AlkoCompanyNew.Models.Entities;
using AlkoCompanyNew.Views.Pages;
using System.Collections.Generic;

namespace AlkoCompanyNew.Models
{
    public class AppData
    {
        public static Entities.Entities Context = new Entities.Entities();
        public static AddZayvki AddZayvki_ { get; set; }
        public static Sotrudnicki Sotrudnicki_ { get; set; }
        public static Prosmotr Prosmotr_ { get; set; }
        public static AddSotrudnick AddSotrudnick_ { get; set; }
        public static Zayavki Zayvki_ { get; set; }
        public static AddZayvkiForm AddZayvkiForm_ { get; set; }
        public static List<Zayavka> Glob = new List<Zayavka>();
        public static Zayavka Zayavka1 = new Zayavka();
        public static Klient Klient1 = new Klient();
    }
}
