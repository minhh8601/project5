using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class HoaDonNhap
    {
        public HoaDonNhap()
        {
            ChiTietHoaDonNhaps = new HashSet<ChiTietHoaDonNhap>();
        }

        public int IdHoaDonNhap { get; set; }
        public string SoHoaDon { get; set; }
        public DateTime NgayNhap { get; set; }
        public int IdNguoiDung { get; set; }
        public int IdNhaCungCap { get; set; }

        public virtual NguoiDung IdNguoiDungNavigation { get; set; }
        public virtual NhaCungCap IdNhaCungCapNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }
    }
}
