using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class ChiTietKhuyenMai
    {
        public int IdChiTietKhuyenMai { get; set; }
        public int IdSanPham { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int MaKhuyenMai { get; set; }
        public bool TrangThai { get; set; }

        public virtual SanPham IdSanPhamNavigation { get; set; }
        public virtual KhuyenMai MaKhuyenMaiNavigation { get; set; }
    }
}
