//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace William_Santisteban_02_08_2016.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CLUB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLUB()
        {
            this.DEPORTE = new HashSet<DEPORTE>();
        }
    
        public int ID_CLUB { get; set; }
        public string DESC_CLUB { get; set; }
        public Nullable<int> ID_REGLAMENTO { get; set; }
    
        public virtual REGLAMENTO REGLAMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEPORTE> DEPORTE { get; set; }
    }
}
