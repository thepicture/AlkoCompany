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
    
    public partial class Sotrudnick
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sotrudnick()
        {
            this.Klient = new HashSet<Klient>();
            this.Zayavka = new HashSet<Zayavka>();
        }
    
        public int S_ID { get; set; }
        public virtual string S_Fio { get; set; }
        public virtual System.DateTime S_Born { get; set; }
        public virtual string S_TelNumber { get; set; }
        public virtual string S_Post { get; set; }
        public virtual string S_Login { get; set; }
        public virtual string S_Password { get; set; }
        public virtual byte[] S_Photo { get; set; }
        public virtual string S_Email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Klient> Klient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zayavka> Zayavka { get; set; }
    }
}
