using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class Kho
    {
        public Kho()
        {
            ChiTietKhos = new HashSet<ChiTietKho>();
            KiemKhos = new HashSet<KiemKho>();
        }

        public int IdKho { get; set; }
        public string TenKho { get; set; }
        public string DiaChi { get; set; }

        public virtual ICollection<ChiTietKho> ChiTietKhos { get; set; }
        public virtual ICollection<KiemKho> KiemKhos { get; set; }
    }
}
