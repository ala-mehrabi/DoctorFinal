//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoctorFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shahrestan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shahrestan()
        {
            this.Doctor = new HashSet<Doctor>();
        }
    
        public int ShahrestanID { get; set; }
        public string ShahrestanName { get; set; }
        public int OstanID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctor> Doctor { get; set; }
        public virtual Ostan Ostan { get; set; }
    }
}
