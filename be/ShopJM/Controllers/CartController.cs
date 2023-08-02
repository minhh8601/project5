using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ShopJM.Entities;
using ShopJM.Models;

namespace ShopJM.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ShopJMContext db = new ShopJMContext();
        [Route("create-cart")]
        [HttpPost]
        public IActionResult CreateItem([FromBody] Cart model)
        {
            db.KhachHangs.Add(model.khach);
            db.SaveChanges();

            int IdKhachHang = model.khach.IdKhachHang;
            DonHang dh = new DonHang();
            dh.IdKhachHang = IdKhachHang;
            dh.NgayDatHang = System.DateTime.Now;
            dh.TrangThaiDonHang = 1;
            db.DonHangs.Add(dh);
            db.SaveChanges();
            int IdDonHang = dh.IdDonHang;

            if (model.donhang.Count > 0)
            {
                foreach (var item in model.donhang)
                {
                    item.IdDonHang = IdDonHang;
                    db.ChiTietDonHangs.Add(item);
                }
                db.SaveChanges();
            }

            return Ok(new { data = "Sussess!" });
        }
    }
}
