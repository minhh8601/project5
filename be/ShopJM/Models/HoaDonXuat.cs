using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class HoaDonXuat
    {
        public HoaDonXuat()
        {
            ChiTietHoaDonXuats = new HashSet<ChiTietHoaDonXuat>();
        }

        public int IdHoaDonXuat { get; set; }
        public string SoHoaDon { get; set; }
        public DateTime? NgayXuat { get; set; }
        public int? IdKhachHang { get; set; }
        public int? IdNguoiDung { get; set; }

        public virtual KhachHang IdKhachHangNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDonXuat> ChiTietHoaDonXuats { get; set; }
    }
}
