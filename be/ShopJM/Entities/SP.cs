using System;
using System.Collections.Generic;
using System.Linq;
namespace ShopJM.Entities
{
    public class SP
    {
        public int IdSanPham { get; set; }
        public int IdDanhMuc { get; set; }
        public string TenSanPham { get; set; }
        public string Anh { get; set; }
        public string MoTaSanPham { get; set; }
        public double Gia { get; set; }
        public int IdNhaSanXuat { get; set; }
        public string DonViTinh { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
