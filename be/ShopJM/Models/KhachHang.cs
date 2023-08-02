using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            HoaDonXuats = new HashSet<HoaDonXuat>();
        }

        public int IdKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        public virtual ICollection<HoaDonXuat> HoaDonXuats { get; set; }
    }
}
