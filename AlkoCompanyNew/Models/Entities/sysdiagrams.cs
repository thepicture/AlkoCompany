
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
    
public partial class sysdiagrams
{

    public virtual string name { get; set; }

    public int principal_id { get; set; }

    public int diagram_id { get; set; }

    public virtual Nullable<int> version { get; set; }

    public virtual byte[] definition { get; set; }

}

}
