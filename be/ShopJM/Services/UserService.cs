using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ShopJM.Entities;
using ShopJM.Helpers;
using ShopJM.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ShopJM.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private ShopJMContext db = new ShopJMContext();

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var result = from tks in db.TaiKhoans
                         join nds in db.NguoiDungs on tks.IdNguoiDung equals nds.IdNguoiDung
                         select new User { Role = tks.LoaiQuyen, IdNguoiDung = tks.IdNguoiDung, TaiKhoan = tks.TaiKhoan1, HoTen = nds.HoTen, MatKhau = tks.MatKhau, DiaChi = nds.DiaChi, DienThoai = nds.DienThoai, Email = nds.DienThoai };
            var user = result.SingleOrDefault(x => x.TaiKhoan == username && x.MatKhau == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.TaiKhoan.ToString()),
                    new Claim(ClaimTypes.MobilePhone, user.DienThoai.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

    }
}
