//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkiAssistAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            this.Custody = new HashSet<Custody>();
        }
    
        public int staffId { get; set; }
        public string staffFirstName { get; set; }
        public string staffLastName { get; set; }
        public string staffContactNum { get; set; }
        public string staffContactEmail { get; set; }
        public string staffUsername { get; set; }
        public string roleType { get; set; }
        public string staffPassword { get; set; }
        public Nullable<int> TokenId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Custody> Custody { get; set; }
        public virtual Role Role { get; set; }
        public virtual Token Token { get; set; }
    }
}
