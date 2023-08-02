using System;
using System.Collections.Generic;
using System.Linq;
using ShopJM.Entities;
using ShopJM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopJM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaSanXuatController : ControllerBase

    {
        private ShopJMContext db = new ShopJMContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var dssx = db.NhaSanXuats.ToList();
                return Ok(dssx);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var find_id = from sp in db.NhaSanXuats
                              select new { sp.IdNhaSanXuat, sp.MoTa };
                var result = find_id.Select(x => new { x.IdNhaSanXuat, x.MoTa }).Where(s => s.IdNhaSanXuat == id).FirstOrDefault();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [Route("create-newnsx")]
        [HttpPost]
        public IActionResult CreateNew([FromBody] NhaSXModel sps)
        {
            db.NhaSanXuats.Add(sps.nhaSanXuat);
            db.SaveChanges();
            return Ok(new { data = "Thêm thành công" });
        }

        [Route("update-nsx")]
        [HttpPost]
        public IActionResult UpdateNSX([FromBody] NhaSXModel nsxs)
        {
            var sp = db.NhaSanXuats.SingleOrDefault(x => x.IdNhaSanXuat == nsxs.nhaSanXuat.IdNhaSanXuat);
            sp.TenNhaSanXuat = nsxs.nhaSanXuat.TenNhaSanXuat;
            sp.MoTa = nsxs.nhaSanXuat.MoTa;
            
            db.SaveChanges();
            return Ok();
        }

        [Route("delete-nsx/{IdNhaSanXuat}")]
        [HttpDelete]
        public IActionResult DeleteNSX(int? IdNhaSanXuat)
        {
            var nsx = db.NhaSanXuats.SingleOrDefault(s => s.IdNhaSanXuat == IdNhaSanXuat);
            db.NhaSanXuats.Remove(nsx);
            db.SaveChanges();
            return Ok();
        }

        [Route("search-nsx")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                
                var tennsx = formData.Keys.Contains("tennsx") ? (formData["tennsx"]).ToString().Trim() : "";
                var result = from sps in db.NhaSanXuats
                             select new { IdNhaSanXuat = sps.IdNhaSanXuat, TenNhaSanXuat = sps.TenNhaSanXuat, MoTa = sps.MoTa };
                var kq = result.Where(x => x.TenNhaSanXuat.Contains(tennsx)).OrderByDescending(x => x.IdNhaSanXuat).ToList();
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
