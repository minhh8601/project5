using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietHoaDonNhaps = new HashSet<ChiTietHoaDonNhap>();
            ChiTietHoaDonXuats = new HashSet<ChiTietHoaDonXuat>();
            ChiTietKhos = new HashSet<ChiTietKho>();
            ChiTietKhuyenMais = new HashSet<ChiTietKhuyenMai>();
            ChiTietKiemKhos = new HashSet<ChiTietKiemKho>();
            ChiTietNhoms = new HashSet<ChiTietNhom>();
            GiamGia = new HashSet<GiamGium>();
            ThongSoKyThuats = new HashSet<ThongSoKyThuat>();
        }

        public int IdSanPham { get; set; }
        public int IdDanhMuc { get; set; }
        public string TenSanPham { get; set; }
        public string MoTaSanPham { get; set; }
        public string Anh { get; set; }
        public double Gia { get; set; }
        public int IdNhaSanXuat { get; set; }
        public string DonViTinh { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual ICollection<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }
        public virtual ICollection<ChiTietHoaDonXuat> ChiTietHoaDonXuats { get; set; }
        public virtual ICollection<ChiTietKho> ChiTietKhos { get; set; }
        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
        public virtual ICollection<ChiTietKiemKho> ChiTietKiemKhos { get; set; }
        public virtual ICollection<ChiTietNhom> ChiTietNhoms { get; set; }
        public virtual ICollection<GiamGium> GiamGia { get; set; }
        public virtual ICollection<ThongSoKyThuat> ThongSoKyThuats { get; set; }
    }
}
