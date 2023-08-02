using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class KhuyenMai
    {
        public KhuyenMai()
        {
            ChiTietKhuyenMais = new HashSet<ChiTietKhuyenMai>();
        }

        public int MaKhuyenMai { get; set; }
        public string TenKhuyenMai { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
    }
}
