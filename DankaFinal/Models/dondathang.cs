//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DankaFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class dondathang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dondathang()
        {
            this.ctdathangs = new HashSet<ctdathang>();
        }
    
        public int soDh { get; set; }
        public Nullable<int> maKh { get; set; }
        public Nullable<System.DateTime> ngayDh { get; set; }
        public Nullable<decimal> triGia { get; set; }
        public Nullable<bool> htThanhtoan { get; set; }
        public Nullable<bool> htGiaohang { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ctdathang> ctdathangs { get; set; }
        public virtual khachhang khachhang { get; set; }
    }
}
