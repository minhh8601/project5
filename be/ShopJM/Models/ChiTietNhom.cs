using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class ChiTietNhom
    {
        public int IdChiTietNhom { get; set; }
        public int IdNhomSanPham { get; set; }
        public int IdSanPham { get; set; }

        public virtual NhomSanPham IdNhomSanPhamNavigation { get; set; }
        public virtual SanPham IdSanPhamNavigation { get; set; }
    }
}
