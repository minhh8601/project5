using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class ChiTietHoaDonXuat
    {
        public int IdChiTietHoaDonXuat { get; set; }
        public int IdHoaDonXuat { get; set; }
        public int IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public double GiaBan { get; set; }
        public double? ChietKhau { get; set; }
        public int? TraLai { get; set; }

        public virtual HoaDonXuat IdHoaDonXuatNavigation { get; set; }
        public virtual SanPham IdSanPhamNavigation { get; set; }
    }
}
