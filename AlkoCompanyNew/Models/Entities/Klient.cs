
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
    
public partial class Klient
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Klient()
    {

        this.ObjectAssessmentGround = new HashSet<ObjectAssessmentGround>();

        this.Zayavka = new HashSet<Zayavka>();

    }


    public int K_ID { get; set; }

    public virtual string K_Fio { get; set; }

    public virtual string K_Born { get; set; }

    public virtual string K_TelNumber { get; set; }

    public virtual string K_SeriaPasporta { get; set; }

    public virtual string K_Propiska { get; set; }

    public virtual string K_NomerPasporta { get; set; }

    public string K_DataVidachiPasporta { get; set; }

    public virtual string K_Email { get; set; }

    public Nullable<int> OA_ID { get; set; }

    public Nullable<int> S_ID { get; set; }



    public virtual ObjectAssessmentAll ObjectAssessmentAll { get; set; }

    public virtual Sotrudnick Sotrudnick { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ObjectAssessmentGround> ObjectAssessmentGround { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Zayavka> Zayavka { get; set; }

}

}
