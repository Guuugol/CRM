//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFTestConsole
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Customer
    {
        public tbl_Customer()
        {
            this.tbl_Meeting = new HashSet<tbl_Meeting>();
            this.tbl_Task = new HashSet<tbl_Task>();
        }
    
        public System.Guid ID { get; set; }
        public System.Guid ContactID { get; set; }
        public string Name { get; set; }
    
        public virtual tbl_Contact tbl_Contact { get; set; }
        public virtual ICollection<tbl_Meeting> tbl_Meeting { get; set; }
        public virtual ICollection<tbl_Task> tbl_Task { get; set; }
    }
}
