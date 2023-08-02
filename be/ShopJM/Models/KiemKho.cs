using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class KiemKho
    {
        public KiemKho()
        {
            ChiTietKiemKhos = new HashSet<ChiTietKiemKho>();
        }

        public int IdKiemKho { get; set; }
        public int? IdNguoiDung { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public int? TrangThaiKho { get; set; }
        public int? IdKho { get; set; }
        public string MoTa { get; set; }

        public virtual Kho IdKhoNavigation { get; set; }
        public virtual ICollection<ChiTietKiemKho> ChiTietKiemKhos { get; set; }
    }
}
