using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class ChiTietDonHang
    {
        public int IdChiTietDonHang { get; set; }
        public int IdDonHang { get; set; }
        public int IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public double GiaMua { get; set; }

        public virtual DonHang IdDonHangNavigation { get; set; }
    }
}
