using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            HoaDonNhaps = new HashSet<HoaDonNhap>();
        }

        public int IdNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        public virtual ICollection<HoaDonNhap> HoaDonNhaps { get; set; }
    }
}
