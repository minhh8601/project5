using ShopJM.Entities;
using ShopJM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using ShopJM.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Admin_Api_BanMayTinh.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;

        private ShopJMContext db = new ShopJMContext();
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu sai!" });

            return Ok(user);
        }

        [Route("search")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                //var page = int.Parse(formData["page"].ToString());
                //var pageSize = int.Parse(formData["pageSize"].ToString());
                var hoten = formData.Keys.Contains("hoten") ? (formData["hoten"]).ToString().Trim() : "";
                var result = from tks in db.TaiKhoans
                             join nds in db.NguoiDungs on tks.IdNguoiDung equals nds.IdNguoiDung
                             select new { HoTen = nds.HoTen, NgaySinh = nds.NgaySinh, GioiTinh = nds.GioiTinh, DiaChi = nds.DiaChi, Email = nds.Email, DienThoai = nds.DienThoai, TaiKhoan = tks.TaiKhoan1, MatKhau = tks.MatKhau, LoaiQuyen = tks.LoaiQuyen, AnhDaiDien = nds.AnhDaiDien, IdNguoiDung = nds.IdNguoiDung };
                var kq = result.Where(x => x.HoTen.Contains(hoten)).OrderByDescending(x => x.IdNguoiDung)/*.Skip(pageSize * (page - 1)).Take(pageSize)*/.ToList();
                return Ok(
                         new ResponseListMessage
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




        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int? id)
        {
            var result = from tks in db.TaiKhoans
                         join nds in db.NguoiDungs on tks.IdNguoiDung equals nds.IdNguoiDung
                         select new { HoTen = nds.HoTen, NgaySinh = nds.NgaySinh, GioiTinh = nds.GioiTinh, DiaChi = nds.DiaChi, Email = nds.Email, DienThoai = nds.DienThoai, TaiKhoan = tks.TaiKhoan1, MatKhau = tks.MatKhau, LoaiQuyen = tks.LoaiQuyen, AnhDaiDien = nds.AnhDaiDien, IdNguoiDung = nds.IdNguoiDung };
            var user = result.SingleOrDefault(x => x.IdNguoiDung == id);
            return Ok(new { user });
        }

        [Route("create-user")]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel model)
        {
            db.NguoiDungs.Add(model.nguoidung);
            db.SaveChanges();

            int IdNguoiDung = model.nguoidung.IdNguoiDung;
            model.taikhoan.IdNguoiDung = IdNguoiDung;
            db.TaiKhoans.Add(model.taikhoan);
            db.SaveChanges();
            return Ok();
        }


        [Route("update-user")]
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserModel model)
        {
            var obj_user = db.NguoiDungs.SingleOrDefault(x => x.IdNguoiDung == model.nguoidung.IdNguoiDung);
            obj_user.HoTen = model.nguoidung.HoTen;
            obj_user.DiaChi = model.nguoidung.DiaChi;
            obj_user.NgaySinh = model.nguoidung.NgaySinh;
            db.SaveChanges();

            var obj_taikhoan = db.TaiKhoans.SingleOrDefault(x => x.IdNguoiDung == model.taikhoan.IdNguoiDung);
            obj_taikhoan.TaiKhoan1 = model.taikhoan.TaiKhoan1;
            obj_taikhoan.MatKhau = model.taikhoan.MatKhau;
            db.SaveChanges();
            return Ok();
        }

        [Route("delete-user/{IdNguoiDung}")]
        [HttpDelete]
        public IActionResult DeleteUser(int? IdNguoiDung)
        {
            var obj_tk = db.TaiKhoans.SingleOrDefault(s => s.IdNguoiDung == IdNguoiDung);
            db.TaiKhoans.Remove(obj_tk);
            db.SaveChanges();
            var obj_nd = db.NguoiDungs.SingleOrDefault(s => s.IdNguoiDung == IdNguoiDung);
            db.NguoiDungs.Remove(obj_nd);
            db.SaveChanges();
            return Ok();
        }
    }
}
