
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
    
public partial class ObjectAssessmentGround
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ObjectAssessmentGround()
    {

        this.AnalogiGround = new HashSet<AnalogiGround>();

    }


    public int OG_ID { get; set; }

    public virtual string OG_Adress { get; set; }

    public virtual string OG_KadNomer { get; set; }

    public virtual float OG_Ploshad { get; set; }

    public virtual string OG_KategoriaZemel { get; set; }

    public virtual string OG_RazreshennoeIspolzovanie { get; set; }

    public virtual float OG_CenaKvm { get; set; }

    public virtual float OG_CenaVse { get; set; }

    public virtual byte[] OG_Photo { get; set; }

    public int K_ID { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<AnalogiGround> AnalogiGround { get; set; }

    public virtual Klient Klient { get; set; }

}

}
