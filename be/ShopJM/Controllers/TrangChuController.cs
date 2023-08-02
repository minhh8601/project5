using System;
using System.Collections.Generic;
using System.Linq;
using ShopJM.Entities;
using ShopJM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopJM.Controllers
{
    [Route("api/trangchu")]
    [ApiController]
    public class TrangChuController : ControllerBase
    {
        private ShopJMContext db = new ShopJMContext();
        [Route("get-allsp")]
        [HttpGet]
        public IActionResult GetAllSP()
        {
            try
            {
                var result = db.SanPhams.Select(x => new { x.IdSanPham, x.TenSanPham, x.Gia, x.Anh }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Error");
            }
        }

        [Route("get-sp-hot")]
        [HttpGet]
        public IActionResult GetHot()
        {
            try
            {
                var result = db.SanPhams.Select(x => new { x.IdSanPham, x.TenSanPham, x.NgayTao, x.Anh }).OrderByDescending(s => s.NgayTao).Take(6).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Error");
            }
        }
        [Route("get-danhmuc")]
        [HttpGet]
        public IEnumerable<DanhMucModel> GetAllMenu()
        {
            return GetData();
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
                             select new { sp.IdSanPham, sp.TenSanPham, sp.Anh, sp.Gia, sp.IdDanhMuc, sp.NgayTao };
                var kq1 = result.Where(s => s.IdDanhMuc == id_danh_muc || id_danh_muc == null).Select(x => new { x.IdSanPham, x.TenSanPham, x.Anh, x.Gia, x.NgayTao }).ToList();
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

        [NonAction]
        private List<DanhMucModel> GetData()
        {
            var alldanhmuc = db.DanhMucs.Where(x => x.TrangThai == true).Select(x => new DanhMucModel { IdDanhMuc = x.IdDanhMuc, IdDanhMucCha = x.IdDanhMucCha, TenDanhMuc = x.TenDanhMuc }).ToList();
            var listParent = alldanhmuc.Where(ds => ds.IdDanhMucCha == null).ToList();
            foreach (var item in listParent)
            {
                item.children = GetHiearchyList(alldanhmuc, item);
            }
            return listParent;
        }
        [NonAction]
        private List<DanhMucModel> GetHiearchyList(List<DanhMucModel> lstAll, DanhMucModel node)
        {
            var listChilds = lstAll.Where(ds => ds.IdDanhMucCha == node.IdDanhMuc).ToList();
            if (listChilds.Count == 0)
                return null;
            for (int i = 0; i < listChilds.Count; i++)
            {
                var childs = GetHiearchyList(lstAll, listChilds[i]);
                listChilds[i].type = (childs == null || childs.Count == 0) ? "leaf" : "";
                listChilds[i].children = childs;
            }
            return listChilds.ToList();
        }
    }
}
