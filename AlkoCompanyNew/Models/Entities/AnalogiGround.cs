//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlkoCompanyNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnalogiGround
    {
        public int AG_ID { get; set; }
        public virtual string AG_Adress { get; set; }
        public virtual string AG_IstochnickInf { get; set; }
        public virtual string AG_Contact { get; set; }
        public virtual float AG_Ploshad { get; set; }
        public virtual float AG_CenaProdazhiVse { get; set; }
        public virtual float AG_CenaProdazhiKvm { get; set; }
        public virtual float AG_KorTorg { get; set; }
        public virtual float AG_KorNaPrava { get; set; }
        public virtual float AG_KorNaDatuProdazhi { get; set; }
        public virtual float AG_KorNaMesto { get; set; }
        public virtual string AG_VozmognostPodklucheniaGas { get; set; }
        public virtual float AG_KorNaGas { get; set; }
        public virtual string AG_VozmognostPodklucheniaVoda { get; set; }
        public virtual float AG_KorNaVoda { get; set; }
        public virtual string AG_VozmognostPodklucheniaElectric { get; set; }
        public virtual float AG_KorNaElecric { get; set; }
        public virtual string AG_CategoriaZemel { get; set; }
        public virtual string AG_RazreshennoeIspolzovanie { get; set; }
        public virtual byte[] AG_Photo { get; set; }
        public int OG_ID { get; set; }
    
        public virtual ObjectAssessmentGround ObjectAssessmentGround { get; set; }
    }
}
