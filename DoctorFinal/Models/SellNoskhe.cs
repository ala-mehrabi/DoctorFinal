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
    
    public partial class SellNoskhe
    {
        public int SellNoskheID { get; set; }
        public System.DateTime SellNoskheDate { get; set; }
        public bool SellNoskheIsCheck { get; set; }
        public int NoskheID { get; set; }
        public int DoctorID { get; set; }
        public long DoctorInCome { get; set; }
        public string CustomerFullName { get; set; }
        public long CustomerMobile { get; set; }
        public string CustomerAddress { get; set; }
        public int CustomerID { get; set; }
    
        public virtual Noskhe Noskhe { get; set; }
    }
}
