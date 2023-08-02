using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using ShopJM.Services;
using ShopJM.Models;
using ShopJM.Entities;

namespace ShopJM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private ShopJMContext db = new ShopJMContext();
        [Route("search")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var result = from ctdhs in db.ChiTietDonHangs
                             join dhs in db.DonHangs on ctdhs.IdDonHang equals dhs.IdDonHang
                             join khs in db.KhachHangs on dhs.IdKhachHang equals khs.IdKhachHang
                             join sps in db.SanPhams on ctdhs.IdSanPham equals sps.IdSanPham
                             select new { sps.IdSanPham, sps.TenSanPham, dhs.IdDonHang, khs.TenKhachHang, dhs.NgayDatHang, dhs.TrangThaiDonHang };
                var kq = result.OrderBy(x => x.NgayDatHang).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                return Ok(
                         new ResponseListMessage
                         {
                             page = page,
                             totalItem = kq.Count,
                             pageSize = pageSize,
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
