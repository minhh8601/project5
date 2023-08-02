using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class ChiTietHoaDonNhap
    {
        public int IdChiTiet { get; set; }
        public int? IdSanPham { get; set; }
        public int? IdHoaDonNhap { get; set; }
        public int? SoLuong { get; set; }
        public double? DonGiaNhap { get; set; }

        public virtual HoaDonNhap IdHoaDonNhapNavigation { get; set; }
        public virtual SanPham IdSanPhamNavigation { get; set; }
    }
}
