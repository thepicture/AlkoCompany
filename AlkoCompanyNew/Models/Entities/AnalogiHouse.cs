
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
    
public partial class AnalogiHouse
{

    public int AH_ID { get; set; }

    public virtual string AH_Adress { get; set; }

    public virtual string AH_IstochnickInf { get; set; }

    public virtual string AH_Contact { get; set; }

    public virtual Nullable<float> AH_Ploshad { get; set; }

    public virtual Nullable<float> AH_CenaProdazhiVse { get; set; }

    public virtual Nullable<float> AH_CenaProdazhiKvm { get; set; }

    public virtual Nullable<float> AH_KorTorg { get; set; }

    public virtual Nullable<float> AH_KorNaDatuProdazhi { get; set; }

    public virtual Nullable<float> AH_KorNaMesto { get; set; }

    public virtual string AH_TypeDopPostroyki { get; set; }

    public virtual Nullable<float> AH_KorNaDopPostroyki { get; set; }

    public virtual string AH_TypeBlogoustroistva { get; set; }

    public virtual Nullable<float> AH_KorNaBlagoustroistvo { get; set; }

    public virtual string AH_Mebel { get; set; }

    public virtual Nullable<float> AH_KorNaMebel { get; set; }

    public virtual string AH_TypeEtage { get; set; }

    public virtual Nullable<float> AH_KorNaEtage { get; set; }

    public virtual string AH_TypeMaterialSten { get; set; }

    public virtual Nullable<float> AH_KorNaMaterialSten { get; set; }

    public virtual string AH_TypeOtoplenie { get; set; }

    public virtual Nullable<float> AH_KorNaOtoplenie { get; set; }

    public virtual string AH_TypeVoda { get; set; }

    public virtual Nullable<float> AH_KorNaVoda { get; set; }

    public virtual string AH_TypeKanalizacia { get; set; }

    public virtual Nullable<float> AH_KorNaKanalizacia { get; set; }

    public virtual string AH_TypeFizSostoyanie { get; set; }

    public virtual Nullable<float> AH_KorNaFizSostoyanie { get; set; }

    public virtual Nullable<float> AH_KorNaPloshad { get; set; }

    public virtual string AH_TypeNarugnyuOtdelky { get; set; }

    public virtual Nullable<float> AH_KorNaNarugnyuOtdelky { get; set; }

    public virtual string AH_TypeVnutrenyuOtdelky { get; set; }

    public virtual Nullable<float> AH_KorNaVnutrenyuOtdelky { get; set; }

    public virtual byte[] AH_Photo { get; set; }

    public int OH_ID { get; set; }

    public virtual Nullable<decimal> AH_Correction { get; set; }

    public virtual Nullable<float> AH_PloshadZemli { get; set; }

    public virtual Nullable<float> AH_KorNaMashtabZemli { get; set; }

    public virtual Nullable<float> AH_StoimostZemliAnaloga { get; set; }



    public virtual ObjectAssessmentHouse ObjectAssessmentHouse { get; set; }

}

}
