using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        public int IdDonHang { get; set; }
        public int? IdKhachHang { get; set; }
        public DateTime? NgayDatHang { get; set; }
        public int? TrangThaiDonHang { get; set; }

        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
