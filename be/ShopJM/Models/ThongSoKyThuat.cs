using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class ThongSoKyThuat
    {
        public int IdThongSo { get; set; }
        public int? IdSanPham { get; set; }
        public string TenThongSo { get; set; }
        public string MoTa { get; set; }

        public virtual SanPham IdSanPhamNavigation { get; set; }
    }
}
