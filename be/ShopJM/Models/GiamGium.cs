using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class GiamGium
    {
        public int MaGiamGia { get; set; }
        public int? IdSanPham { get; set; }
        public double? PhanTram { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public bool? TrangThai { get; set; }

        public virtual SanPham IdSanPhamNavigation { get; set; }
    }
}
