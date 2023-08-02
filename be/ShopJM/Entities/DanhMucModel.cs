using System;
using System.Collections.Generic;
using System.Linq;
using ShopJM.Models;
namespace ShopJM.Entities
{
    public class DanhMucModel
    {
        public int IdDanhMuc { get; set; }
        public int? IdDanhMucCha { get; set; }
        public string TenDanhMuc { get; set; }
        public List<DanhMucModel> children { get; set; }
        public string type { get; set; }
    }
}
