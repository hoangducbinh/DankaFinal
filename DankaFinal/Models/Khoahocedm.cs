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
    
    public partial class Khoahocedm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Khoahocedm()
        {
            this.ctdathangs = new HashSet<ctdathang>();
        }
    
        public int maKhoahoc { get; set; }
        public string tenKhoahoc { get; set; }
        public Nullable<decimal> donGia { get; set; }
        public string moTa { get; set; }
        public string hinhMinhhoa { get; set; }
        public Nullable<int> maTl { get; set; }
        public string tengiangvien { get; set; }
        public Nullable<System.DateTime> ngayCapnhat { get; set; }
        public Nullable<int> soLuongban { get; set; }
        public Nullable<int> soLanxem { get; set; }
        public string linkdemo { get; set; }
        public string lotrinh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ctdathang> ctdathangs { get; set; }
        public virtual theloai theloai { get; set; }
    }
}
