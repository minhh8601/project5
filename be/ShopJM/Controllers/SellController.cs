using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using ShopJM.Entities;
using ShopJM.Models;
using System.Linq;

namespace ShopJM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellController : Controller
    {
        private ShopJMContext db = new ShopJMContext();
        [Route("create-hoadonxuat")]
        [HttpPost]
        public IActionResult CreateItem([FromBody] HoaDonXuatModel model)
        {
            db.KhachHangs.Add(model.khachhang);
            db.SaveChanges();

            int IdKhachHang = model.khachhang.IdKhachHang;
            HoaDonXuat dh = new HoaDonXuat();
            dh.IdKhachHang = IdKhachHang;
            //dh.IdNguoiDung = model.hoadon.IdNguoiDung;
            dh.NgayXuat = System.DateTime.Now;
            db.HoaDonXuats.Add(dh);
            db.SaveChanges();
            int IdHoaDonXuat = dh.IdHoaDonXuat;

            if (model.chitiethoadon.Count > 0)
            {
                foreach (var item in model.chitiethoadon)
                {
                    item.IdHoaDonXuat = IdHoaDonXuat;
                    db.ChiTietHoaDonXuats.Add(item);
                    //var obj = db.ChiTietKhos.SingleOrDefault(x => x.IdSanPham == item.IdSanPham);
                    //obj.SoLuong = obj.SoLuong - item.SoLuong;
                }
                db.SaveChanges();
            }

            return Ok(new { data = "OK" });
        }
        [Route("search")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {            
                int? ma_danh_muc = null;
                if (formData.Keys.Contains("ma_danh_muc") && !string.IsNullOrEmpty(Convert.ToString(formData["ma_danh_muc"]))) { ma_danh_muc = int.Parse(formData["ma_danh_muc"].ToString()); }
                var result = from r in db.SanPhams                   
                             select new { r.IdSanPham, r.TenSanPham, r.Anh, r.Gia, r.IdDanhMuc, r.NgayTao };
                var kq = result.Where(s => s.IdDanhMuc == ma_danh_muc || ma_danh_muc == null).Select(x => new { x.IdSanPham, x.TenSanPham, x.Anh, x.Gia, x.NgayTao }).ToList();
                long total = kq.Count();
                return Ok(
                           new ResponseListMessage
                           {
                               
                               data = kq.OrderByDescending(x => x.NgayTao).ToList()
                           });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
