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
    
    public partial class Noskhe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Noskhe()
        {
            this.NoskheCommidity = new HashSet<NoskheCommidity>();
            this.SellNoskhe = new HashSet<SellNoskhe>();
        }
    
        public int NoskheID { get; set; }
        public string NoskheCode { get; set; }
        public System.DateTime NoskheDate { get; set; }
        public int CustomerID { get; set; }
        public long NoskhePrice { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoskheCommidity> NoskheCommidity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SellNoskhe> SellNoskhe { get; set; }
    }
}