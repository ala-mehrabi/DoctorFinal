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
    
    public partial class Sabeghe
    {
        public int SabegheID { get; set; }
        public System.DateTime SabegheDateNow { get; set; }
        public System.DateTime SabegheDateAfter { get; set; }
        public int CustomerID { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
