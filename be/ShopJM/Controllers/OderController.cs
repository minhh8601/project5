using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using ShopJM.Models;
using ShopJM.Entities;
namespace ShopJM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OderController : ControllerBase
    {
        private ShopJMContext db = new ShopJMContext();

        [Route("search")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {            
                var result = from c in db.ChiTietDonHangs
                             join d in db.DonHangs on c.IdDonHang equals d.IdDonHang
                             join k in db.KhachHangs on d.IdKhachHang equals k.IdKhachHang
                             join s in db.SanPhams on c.IdSanPham equals s.IdSanPham
                             select new { s.IdSanPham, s.TenSanPham, d.IdDonHang, k.TenKhachHang, d.NgayDatHang, d.TrangThaiDonHang };
                var kq = result.OrderBy(x => x.NgayDatHang).ToList();
                return Ok(
                         new ResponseListMessage
                         {
                             
                             data = kq
                         });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
