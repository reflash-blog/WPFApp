//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CWLOTPH.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdditiveData
    {
        public string Salt { get; set; }
        public decimal ID { get; set; }
    
        public virtual User User { get; set; }
    }
}
