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
    
    public partial class tbl_TaskResult
    {
        public tbl_TaskResult()
        {
            this.tbl_Task = new HashSet<tbl_Task>();
        }
    
        public System.Guid ID { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<tbl_Task> tbl_Task { get; set; }
    }
}
