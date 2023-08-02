using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class NhomSanPham
    {
        public NhomSanPham()
        {
            ChiTietNhoms = new HashSet<ChiTietNhom>();
        }

        public int IdNhomSanPham { get; set; }
        public string TenNhom { get; set; }
        public bool? TrangThai { get; set; }

        public virtual ICollection<ChiTietNhom> ChiTietNhoms { get; set; }
    }
}
