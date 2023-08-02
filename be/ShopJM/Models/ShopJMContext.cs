using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShopJM.Models
{
    public partial class ShopJMContext : DbContext
    {
        public ShopJMContext()
        {
        }

        public ShopJMContext(DbContextOptions<ShopJMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }
        public virtual DbSet<ChiTietHoaDonXuat> ChiTietHoaDonXuats { get; set; }
        public virtual DbSet<ChiTietKho> ChiTietKhos { get; set; }
        public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
        public virtual DbSet<ChiTietKiemKho> ChiTietKiemKhos { get; set; }
        public virtual DbSet<ChiTietNhom> ChiTietNhoms { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<GiamGium> GiamGia { get; set; }
        public virtual DbSet<HoaDonNhap> HoaDonNhaps { get; set; }
        public virtual DbSet<HoaDonXuat> HoaDonXuats { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<Kho> Khos { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<KiemKho> KiemKhos { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public virtual DbSet<NhomSanPham> NhomSanPhams { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Silde> Sildes { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThongSoKyThuat> ThongSoKyThuats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-I70BQHV3;Database=ShopJM;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");

            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.HasKey(e => e.IdChiTietDonHang);

                entity.ToTable("ChiTietDonHang");

                entity.HasOne(d => d.IdDonHangNavigation)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.IdDonHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietDonHang_DonHang");
            });

            modelBuilder.Entity<ChiTietHoaDonNhap>(entity =>
            {
                entity.HasKey(e => e.IdChiTiet);

                entity.ToTable("ChiTietHoaDonNhap");

                entity.Property(e => e.IdChiTiet).ValueGeneratedNever();

                entity.HasOne(d => d.IdHoaDonNhapNavigation)
                    .WithMany(p => p.ChiTietHoaDonNhaps)
                    .HasForeignKey(d => d.IdHoaDonNhap)
                    .HasConstraintName("FK_ChiTietHoaDonNhap_HoaDonNhap");

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.ChiTietHoaDonNhaps)
                    .HasForeignKey(d => d.IdSanPham)
                    .HasConstraintName("FK_ChiTietHoaDonNhap_SanPham");
            });

            modelBuilder.Entity<ChiTietHoaDonXuat>(entity =>
            {
                entity.HasKey(e => e.IdChiTietHoaDonXuat);

                entity.ToTable("ChiTietHoaDonXuat");

                entity.HasOne(d => d.IdHoaDonXuatNavigation)
                    .WithMany(p => p.ChiTietHoaDonXuats)
                    .HasForeignKey(d => d.IdHoaDonXuat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietHoaDonXuat_HoaDonXuat");

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.ChiTietHoaDonXuats)
                    .HasForeignKey(d => d.IdSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietHoaDonXuat_SanPham");
            });

            modelBuilder.Entity<ChiTietKho>(entity =>
            {
                entity.HasKey(e => e.IdChiTietKho);

                entity.ToTable("ChiTietKho");

                entity.Property(e => e.KhayKe).HasMaxLength(1500);

                entity.HasOne(d => d.IdKhoNavigation)
                    .WithMany(p => p.ChiTietKhos)
                    .HasForeignKey(d => d.IdKho)
                    .HasConstraintName("FK_ChiTietKho_Kho");

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.ChiTietKhos)
                    .HasForeignKey(d => d.IdSanPham)
                    .HasConstraintName("FK_ChiTietKho_SanPham");
            });

            modelBuilder.Entity<ChiTietKhuyenMai>(entity =>
            {
                entity.HasKey(e => e.IdChiTietKhuyenMai);

                entity.ToTable("ChiTietKhuyenMai");

                entity.Property(e => e.IdChiTietKhuyenMai).ValueGeneratedNever();

                entity.Property(e => e.NgayBatDau).HasColumnType("datetime");

                entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.ChiTietKhuyenMais)
                    .HasForeignKey(d => d.IdSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietKhuyenMai_SanPham");

                entity.HasOne(d => d.MaKhuyenMaiNavigation)
                    .WithMany(p => p.ChiTietKhuyenMais)
                    .HasForeignKey(d => d.MaKhuyenMai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietKhuyenMai_KhuyenMai");
            });

            modelBuilder.Entity<ChiTietKiemKho>(entity =>
            {
                entity.HasKey(e => e.IdChiTietKiemKho);

                entity.ToTable("ChiTietKiemKho");

                entity.Property(e => e.IdChiTietKiemKho).ValueGeneratedNever();

                entity.HasOne(d => d.IdKiemKhoNavigation)
                    .WithMany(p => p.ChiTietKiemKhos)
                    .HasForeignKey(d => d.IdKiemKho)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietKiemKho_KiemKho");

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.ChiTietKiemKhos)
                    .HasForeignKey(d => d.IdSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietKiemKho_SanPham");
            });

            modelBuilder.Entity<ChiTietNhom>(entity =>
            {
                entity.HasKey(e => e.IdChiTietNhom);

                entity.ToTable("ChiTietNhom");

                entity.HasOne(d => d.IdNhomSanPhamNavigation)
                    .WithMany(p => p.ChiTietNhoms)
                    .HasForeignKey(d => d.IdNhomSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietNhom_NhomSanPham");

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.ChiTietNhoms)
                    .HasForeignKey(d => d.IdSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietNhom_SanPham");
            });

            modelBuilder.Entity<DanhMuc>(entity =>
            {
                entity.HasKey(e => e.IdDanhMuc);

                entity.ToTable("DanhMuc");

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.TenDanhMuc)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.IdDonHang);

                entity.ToTable("DonHang");

                entity.Property(e => e.NgayDatHang).HasColumnType("datetime");
            });

            modelBuilder.Entity<GiamGium>(entity =>
            {
                entity.HasKey(e => e.MaGiamGia);

                entity.Property(e => e.MaGiamGia).ValueGeneratedNever();

                entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.GiamGia)
                    .HasForeignKey(d => d.IdSanPham)
                    .HasConstraintName("FK_GiamGia_SanPham");
            });

            modelBuilder.Entity<HoaDonNhap>(entity =>
            {
                entity.HasKey(e => e.IdHoaDonNhap);

                entity.ToTable("HoaDonNhap");

                entity.Property(e => e.IdHoaDonNhap).ValueGeneratedNever();

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.Property(e => e.SoHoaDon)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdNguoiDungNavigation)
                    .WithMany(p => p.HoaDonNhaps)
                    .HasForeignKey(d => d.IdNguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HoaDonNhap_NguoiDung");

                entity.HasOne(d => d.IdNhaCungCapNavigation)
                    .WithMany(p => p.HoaDonNhaps)
                    .HasForeignKey(d => d.IdNhaCungCap)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HoaDonNhap_NhaCungCap");
            });

            modelBuilder.Entity<HoaDonXuat>(entity =>
            {
                entity.HasKey(e => e.IdHoaDonXuat);

                entity.ToTable("HoaDonXuat");

                entity.Property(e => e.NgayXuat).HasColumnType("datetime");

                entity.Property(e => e.SoHoaDon)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdKhachHangNavigation)
                    .WithMany(p => p.HoaDonXuats)
                    .HasForeignKey(d => d.IdKhachHang)
                    .HasConstraintName("FK_HoaDonXuat_KhachHang");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.IdKhachHang);

                entity.ToTable("KhachHang");

                entity.Property(e => e.DiaChi).HasMaxLength(1500);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TenKhachHang)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Kho>(entity =>
            {
                entity.HasKey(e => e.IdKho);

                entity.ToTable("Kho");

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TenKho)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<KhuyenMai>(entity =>
            {
                entity.HasKey(e => e.MaKhuyenMai);

                entity.ToTable("KhuyenMai");

                entity.Property(e => e.MaKhuyenMai).ValueGeneratedNever();

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.TenKhuyenMai).HasMaxLength(250);
            });

            modelBuilder.Entity<KiemKho>(entity =>
            {
                entity.HasKey(e => e.IdKiemKho);

                entity.ToTable("KiemKho");

                entity.Property(e => e.IdKiemKho).ValueGeneratedNever();

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");

                entity.HasOne(d => d.IdKhoNavigation)
                    .WithMany(p => p.KiemKhos)
                    .HasForeignKey(d => d.IdKho)
                    .HasConstraintName("FK_KiemKho_Kho");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.IdNguoiDung);

                entity.ToTable("NguoiDung");

                entity.Property(e => e.AnhDaiDien)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi).HasMaxLength(1500);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.GioiTinh).HasMaxLength(20);

                entity.Property(e => e.HoTen).HasMaxLength(250);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.IdNhaCungCap);

                entity.ToTable("NhaCungCap");

                entity.Property(e => e.IdNhaCungCap).ValueGeneratedNever();

                entity.Property(e => e.DiaChi).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TenNhaCungCap).HasMaxLength(250);
            });

            modelBuilder.Entity<NhaSanXuat>(entity =>
            {
                entity.HasKey(e => e.IdNhaSanXuat);

                entity.ToTable("NhaSanXuat");

                entity.Property(e => e.MoTa).HasColumnType("ntext");

                entity.Property(e => e.TenNhaSanXuat)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<NhomSanPham>(entity =>
            {
                entity.HasKey(e => e.IdNhomSanPham);

                entity.ToTable("NhomSanPham");

                entity.Property(e => e.TenNhom).HasMaxLength(250);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.IdSanPham);

                entity.ToTable("SanPham");

                entity.Property(e => e.Anh)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DonViTinh)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MoTaSanPham)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.TenSanPham)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Silde>(entity =>
            {
                entity.HasKey(e => e.IdSilde);

                entity.ToTable("Silde");

                entity.Property(e => e.Anh)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.IdTaiKhoan);

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.LoaiQuyen)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NgayBatDau).HasColumnType("datetime");

                entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

                entity.Property(e => e.TaiKhoan1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TaiKhoan");

                entity.HasOne(d => d.IdNguoiDungNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.IdNguoiDung)
                    .HasConstraintName("FK_TaiKhoan_NguoiDung");
            });

            modelBuilder.Entity<ThongSoKyThuat>(entity =>
            {
                entity.HasKey(e => e.IdThongSo);

                entity.ToTable("ThongSoKyThuat");

                entity.Property(e => e.IdThongSo).ValueGeneratedNever();

                entity.Property(e => e.MoTa).HasMaxLength(500);

                entity.Property(e => e.TenThongSo).HasMaxLength(150);

                entity.HasOne(d => d.IdSanPhamNavigation)
                    .WithMany(p => p.ThongSoKyThuats)
                    .HasForeignKey(d => d.IdSanPham)
                    .HasConstraintName("FK_ThongSoKyThuat_SanPham");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
