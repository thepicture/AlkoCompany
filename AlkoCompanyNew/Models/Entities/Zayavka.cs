
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace AlkoCompanyNew.Models.Entities
{

using System;
    using System.Collections.Generic;
    
public partial class Zayavka
{

    public int Z_ID { get; set; }

    public virtual string Z_Adress { get; set; }

    public virtual string Z_Hotelka { get; set; }

    public virtual byte[] Z_PhotoPreview { get; set; }

    public virtual string Z_TelNumber { get; set; }

    public virtual string Z_Primichania { get; set; }

    public virtual string Z_DataSostavlenia { get; set; }

    public int S_ID { get; set; }

    public int K_ID { get; set; }

    public Nullable<int> OAG_ID { get; set; }

    public Nullable<int> OAH_ID { get; set; }

    public Nullable<int> OAA_ID { get; set; }

    public int Z_StatusId { get; set; }



    public virtual Klient Klient { get; set; }

    public virtual ObjectAssessmentAll ObjectAssessmentAll { get; set; }

    public virtual ObjectAssessmentGround ObjectAssessmentGround { get; set; }

    public virtual ObjectAssessmentHouse ObjectAssessmentHouse { get; set; }

    public virtual Sotrudnick Sotrudnick { get; set; }

    public virtual ZayavkaStatus ZayavkaStatus { get; set; }

}

}
