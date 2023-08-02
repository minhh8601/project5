using ShopJM.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace ShopJM.Models
{
    public partial class DanhMuc
    {
        internal List<DanhMucModel> children;

        public int IdDanhMuc { get; set; }
        public int? IdDanhMucCha { get; set; }
        public string TenDanhMuc { get; set; }
        public int? Stt { get; set; }
        public bool TrangThai { get; set; }
    }
}
