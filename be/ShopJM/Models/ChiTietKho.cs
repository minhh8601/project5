using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class ChiTietKho
    {
        public int IdChiTietKho { get; set; }
        public int? IdKho { get; set; }
        public int? IdSanPham { get; set; }
        public int? SoLuong { get; set; }
        public string KhayKe { get; set; }

        public virtual Kho IdKhoNavigation { get; set; }
        public virtual SanPham IdSanPhamNavigation { get; set; }
    }
}
