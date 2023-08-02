using System;
using System.Linq;
using ShopJM.Models;
using ShopJM.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ShopJM.Controllers
{
    [Route("api/sanpham")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private ShopJMContext db = new ShopJMContext();
        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var dssp = db.SanPhams.ToList();
                return Ok(dssp);
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
                var find_id = from sp in db.SanPhams
                              select new { sp.IdSanPham, sp.IdDanhMuc, sp.TenSanPham, sp.Anh, sp.MoTaSanPham, sp.Gia,sp.DonViTinh, sp.IdNhaSanXuat,sp.NgayTao };
                var result = find_id.Select(x => new { x.IdSanPham,x.IdDanhMuc, x.TenSanPham, x.Anh, x.MoTaSanPham, x.Gia, x.IdNhaSanXuat, x.NgayTao,x.DonViTinh }).Where(s => s.IdSanPham == id).FirstOrDefault();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [Route("get-sp-cung-loai/{id}")]
        [HttpGet]
        public IActionResult GetSPCungLoai(int id)
        {
            try
            {
                var find_id = from sp in db.SanPhams
                              select new { sp.IdSanPham, sp.IdDanhMuc, sp.TenSanPham, sp.Anh, sp.MoTaSanPham, sp.Gia, sp.IdNhaSanXuat, sp.NgayTao };
                var result = find_id.Select(x => new { x.IdSanPham,x.IdDanhMuc, x.TenSanPham, x.Anh, x.MoTaSanPham, x.Gia, x.IdNhaSanXuat, x.NgayTao }).Where(s => s.IdDanhMuc == id).Take(6).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [Route("get-hot")]
        [HttpGet]
        public IActionResult GetHot()
        {
            var result = db.SanPhams.Select(x => new { x.IdSanPham, x.TenSanPham, x.Anh, x.Gia }).OrderByDescending(s => s.IdSanPham).Take(6).ToList();
            return Ok(result);

        }

        [Route("create-newsp")]
        [HttpPost]
        public IActionResult CreateNew([FromBody] SPModel sps)
        {
            db.SanPhams.Add(sps.sanpham);
            db.SaveChanges();
            return Ok(new { data = "Thêm thành công" });
        }


        [Route("update-sp")]
        [HttpPost]
        public IActionResult UpdateSP([FromBody] SPModel sps)
        {
            var sp = db.SanPhams.SingleOrDefault(x => x.IdSanPham == sps.sanpham.IdSanPham);
            sp.IdDanhMuc = sps.sanpham.IdDanhMuc;
            sp.TenSanPham = sps.sanpham.TenSanPham;
            sp.Anh = sps.sanpham.Anh;
            sp.MoTaSanPham = sps.sanpham.MoTaSanPham;
            sp.Gia = sps.sanpham.Gia;
            sp.IdNhaSanXuat = sps.sanpham.IdNhaSanXuat;
            sp.DonViTinh = sps.sanpham.DonViTinh;
            sp.NgayTao = sps.sanpham.NgayTao;
            db.SaveChanges();
            return Ok(new { data = "Update thành công" });
        }

        [Route("delete-sp/{IdSanPham}")]
        [HttpDelete]
        public IActionResult DeleteSP(int? IdSanPham)
        {
            var sp = db.SanPhams.SingleOrDefault(s => s.IdSanPham == IdSanPham);
            db.SanPhams.Remove(sp);
            db.SaveChanges();      
            return Ok();
        }


        [Route("search-giasanpham")]
        [HttpPost]
        public IActionResult SearchSanPhamByGia([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                //var page = int.Parse(formData["page"].ToString());
                //var pageSize = int.Parse(formData["pageSize"].ToString());


                var giasp = formData.Keys.Contains("giasp") ? (formData["giasp"]).ToString().Trim() : "";



                var result = from sp in db.SanPhams
                        
                             select new { sp.IdSanPham, sp.IdDanhMuc, sp.MoTaSanPham, sp.DonViTinh, sp.IdNhaSanXuat, sp.TenSanPham, sp.Gia, sp.NgayTao, sp.Anh };
                var kq = result.Where(x => x.Gia == double.Parse(giasp.ToString()))/*.Skip(pageSize * (page - 1)).Take(pageSize)*/.ToList();


                return Ok(
                         new
                         {
                             //page = page,
                             //totalItem = kq.Count,
                             //pageSize = pageSize,
                             data = kq
                         });


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        //
        [Route("search-sanpham")]
        [HttpPost]
        public IActionResult SearchSanPham([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                //var page = int.Parse(formData["page"].ToString());
                //var pageSize = int.Parse(formData["pageSize"].ToString());


                var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";



                var result = from sp in db.SanPhams
                             
                             select new { sp.IdSanPham, sp.IdDanhMuc, sp.MoTaSanPham, sp.DonViTinh, sp.IdNhaSanXuat, sp.TenSanPham, sp.Gia,  sp.NgayTao, sp.Anh };
                var kq = result.Where(x => x.TenSanPham.Contains(ten)).OrderByDescending(x => x.IdSanPham)/*.Skip(pageSize * (page - 1)).Take(pageSize)*/.ToList();


                return Ok(
                         new
                         {
                             //page = page,
                             //totalItem = kq.Count,
                             //pageSize = pageSize,
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
