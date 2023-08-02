using System;
using System.Collections.Generic;
using System.Linq;
using ShopJM.Entities;
using ShopJM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopJM.Controllers
{
    [Route("api/danhmucsanpham")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private ShopJMContext db = new ShopJMContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var dsdm = db.DanhMucs.ToList();
                return Ok(dsdm);
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
                var find_id = from sp in db.DanhMucs
                              select new { sp.IdDanhMuc, sp.IdDanhMucCha, sp.TenDanhMuc, sp.Stt, sp.TrangThai };
                var result = find_id.Select(x => new { x.IdDanhMuc, x.IdDanhMucCha, x.TenDanhMuc, x.Stt, x.TrangThai}).Where(s => s.IdDanhMuc == id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [Route("create-newdm")]
        [HttpPost]
        public IActionResult CreateNew([FromBody] DMModel sps)
        {
            try
            {
                var sp = new DanhMuc
                {
                    IdDanhMuc = sps.danhMuc.IdDanhMuc,
                    IdDanhMucCha = sps.danhMuc.IdDanhMucCha,
                    TenDanhMuc = sps.danhMuc.TenDanhMuc,
                    Stt = sps.danhMuc.Stt,
                    TrangThai = sps.danhMuc.TrangThai,
                    
                    
                };
                db.Add(sp);
                db.SaveChanges();
                return Ok(sp);
            }
            catch
            {
                return BadRequest();
            }
        }


        [Route("update-dm")]
        [HttpPut]
        public IActionResult UpdateDanhMuc([FromBody] DMModel model)
        {
            var dm = db.DanhMucs.SingleOrDefault(x => x.IdDanhMuc == model.danhMuc.IdDanhMuc);
            dm.IdDanhMucCha = model.danhMuc.IdDanhMucCha;
            dm.TenDanhMuc = model.danhMuc.TenDanhMuc;
            dm.Stt = model.danhMuc.Stt;
            dm.TrangThai = model.danhMuc.TrangThai;
            db.SaveChanges();

            return Ok();
        }

        [Route("delete-sp/{IdDanhMuc}")]
        [HttpDelete]
        public IActionResult DeleteDanhMuc(int? IdDanhMuc)
        {
            var dm = db.DanhMucs.SingleOrDefault(s => s.IdDanhMuc == IdDanhMuc);
            db.DanhMucs.Remove(dm);
            db.SaveChanges();
            return Ok();
        }
        [Route("search-danhmuc")]
        [HttpPost]
        public IActionResult SearchDanhMuc([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var tendanhmuc = formData.Keys.Contains("tendanhmuc") ? (formData["tendanhmuc"]).ToString().Trim() : "";
                var result = from sps in db.DanhMucs
                             select new { IdDanhMuc = sps.IdDanhMuc, IdDanhMucCha = sps.IdDanhMucCha, TenDanhMuc = sps.TenDanhMuc, Stt = sps.Stt, TrangThai = sps.TrangThai };
                var kq = result.Where(x => x.TenDanhMuc.Contains(tendanhmuc)).OrderByDescending(x => x.IdDanhMuc).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
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

        [Route("search")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                int? id_danh_muc = null;
                string loc = "";
                if (formData.Keys.Contains("loc") && !string.IsNullOrEmpty(Convert.ToString(formData["loc"]))) { loc = formData["loc"].ToString(); }
                if (formData.Keys.Contains("id_danh_muc") && !string.IsNullOrEmpty(Convert.ToString(formData["id_danh_muc"]))) { id_danh_muc = int.Parse(formData["id_danh_muc"].ToString()); }
                var result = from sp in db.SanPhams           
                             select new { sp.IdSanPham, sp.TenSanPham, sp.Anh, sp.Gia, sp.IdDanhMuc, sp.NgayTao, sp.MoTaSanPham,sp.DonViTinh };
                var kq1 = result.Where(s => s.IdDanhMuc == id_danh_muc || id_danh_muc == null).Select(x => new { x.IdSanPham,x.IdDanhMuc,x.MoTaSanPham, x.TenSanPham, x.Anh, x.Gia, x.NgayTao,x.DonViTinh }).ToList();
                long total = kq1.Count();
                dynamic kq2 = null;
                switch (loc)
                {
                    case "TD":
                        kq2 = kq1.OrderBy(x => x.Gia).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                        break;
                    case "GD":
                        kq2 = kq1.OrderByDescending(x => x.Gia).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                        break;
                    default:
                        kq2 = kq1.OrderByDescending(x => x.NgayTao).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                        break;
                }
                return Ok(
                           new KQ
                           {
                               page = page,
                               totalItem = total,
                               pageSize = pageSize,
                               data = kq2
                           }
                         );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
