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
    
    public partial class StudentGuardian
    {
        public int studentId { get; set; }
        public int guardianId { get; set; }
        public string studentguardianRelationship { get; set; }
        public Nullable<System.DateTime> studentguardianExpiry { get; set; }
    
        public virtual Guardian Guardian { get; set; }
        public virtual Student Student { get; set; }
    }
}