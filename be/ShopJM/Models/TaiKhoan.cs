using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class TaiKhoan
    {
        public int IdTaiKhoan { get; set; }
        public int? IdNguoiDung { get; set; }
        public string TaiKhoan1 { get; set; }
        public string MatKhau { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public bool? TrangThai { get; set; }
        public string LoaiQuyen { get; set; }

        public virtual NguoiDung IdNguoiDungNavigation { get; set; }
    }
}
