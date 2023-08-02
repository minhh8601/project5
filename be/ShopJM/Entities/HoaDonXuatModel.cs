using ShopJM.Models;
using System.Collections.Generic;
namespace ShopJM.Entities
{
    public class HoaDonXuatModel
    {
        public KhachHang khachhang { get; set; }
        public HoaDonXuat hoadon { get; set; }
        public List<ChiTietHoaDonXuat> chitiethoadon { get; set; }
    }
}
