//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataBaseWCF
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_MeetingResult
    {
        public tbl_MeetingResult()
        {
            this.tbl_Meeting = new HashSet<tbl_Meeting>();
        }
    
        public System.Guid ID { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<tbl_Meeting> tbl_Meeting { get; set; }
    }
}
