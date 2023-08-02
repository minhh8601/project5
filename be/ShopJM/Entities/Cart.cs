using System;
using System.Collections.Generic;
using System.Linq;
using ShopJM.Models;
namespace ShopJM.Entities
{
    public class Cart
    {
        public KhachHang khach { get; set; }
        public List<ChiTietDonHang> donhang { get; set; }
    }
}
