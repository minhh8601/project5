using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class ChiTietKiemKho
    {
        public int IdChiTietKiemKho { get; set; }
        public int IdSanPham { get; set; }
        public int IdKiemKho { get; set; }
        public int? SoLuongDemDuoc { get; set; }
        public int? SoLuongTinhToan { get; set; }
        public int? SoLuongThayDoi { get; set; }

        public virtual KiemKho IdKiemKhoNavigation { get; set; }
        public virtual SanPham IdSanPhamNavigation { get; set; }
    }
}
