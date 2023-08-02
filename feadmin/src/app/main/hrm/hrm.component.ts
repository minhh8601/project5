import { BaseComponent } from 'src/app/core/common/base-component';
import { Component, OnInit, AfterViewInit, Injector } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import MatchValidation from 'src/app/core/helpers/must-match.validator';
declare var $: any;
@Component({
  selector: 'app-hrm',
  templateUrl: './hrm.component.html',
  styleUrls: ['./hrm.component.css']
})
export class HrmComponent extends BaseComponent implements OnInit, AfterViewInit {
  p:number;
  public list_users: any;
  public isCreate = false;
  public user: any;
  public formUser: FormGroup;
  public formSearch: FormGroup;
  public loaiQuyen: string = 'Admin';
  public file: any;
  public hienModal: any;
  public suscessForm: any;
  constructor(injector: Injector) {
    super(injector);
    this.formSearch = new FormGroup({
      'txt_hoten': new FormControl('', []),
      'txt_taikhoan': new FormControl('', []),
      'txt_loaiquyen': new FormControl('', []),
    });
  }

  ngOnInit(): void {
    this.LoadData();
  }
  public LoadData() {
    this._api.post('/api/user/search', {hoten: this.formSearch.value['txt_hoten']}).subscribe(res => {
      this.list_users = res.data;
    });
  }

  get hoten() {
    return this.formUser.get('txt_hoten')!;
  }
  get taikhoan() {
    return this.formUser.get('txt_taikhoan')!;
  }

  get email() {
    return this.formUser.get('txt_email')!;
  }
  get dienthoai() {
    return this.formUser.get('txt_dienthoai')!;
  }
  get ngaysinh() {
    return this.formUser.get('txt_ngaysinh')!;
  }

  get matkhau() {
    return this.formUser.get('txt_matkhau')!;
  }

  get nhaplaimatkhau() {
    return this.formUser.get('txt_nhaplai_matkhau')!;
  }

  public ThemMoiModal() {
    this.hienModal = true;
    this.isCreate = true;
    setTimeout(() => {
      $('#createUserModal').modal('toggle');
      this.suscessForm = true;
      this.formUser = new FormGroup({
        'txt_hoten': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(250)]),
        'txt_ngaysinh': new FormControl('', [Validators.required]),
        'txt_gioitinh': new FormControl('Nam', [Validators.required]),
        'txt_diachi': new FormControl('', []),
        'txt_email': new FormControl('', [Validators.email]),
        'txt_dienthoai': new FormControl('', [Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]),
        'txt_taikhoan': new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(50)]),
        'txt_matkhau': new FormControl('', [this.pwdCheckValidator]),
        'txt_nhaplai_matkhau': new FormControl('', [Validators.required]),
        'txt_loaiquyen': new FormControl('', []),
        'txt_anh': new FormControl('', []),
        'txt_thongtinkhac': new FormControl('', []),
      }, {
        validators: [MatchValidation.match('txt_matkhau', 'txt_nhaplai_matkhau')]
      });
    });
  }


  public CapNhatModal(maNguoiDung: any) {
    this.hienModal = true;
    this.suscessForm = false;
    this.isCreate = false;
    setTimeout(() => {
      $('#createUserModal').modal('toggle');
      this._api.get('/api/user/get-by-id/' + maNguoiDung).subscribe(res => {
        this.user = res.user;
        this.suscessForm = true;
        this.formUser = new FormGroup({
          'txt_hoten': new FormControl(this.user.hoTen, [Validators.required, Validators.minLength(3), Validators.maxLength(250)]),
          'txt_ngaysinh': new FormControl('', [Validators.required]),
          'txt_gioitinh': new FormControl(this.user.gioiTinh, [Validators.required]),
          'txt_diachi': new FormControl(this.user.diaChi, []),
          'txt_email': new FormControl(this.user.email, [Validators.email]),
          'txt_dienthoai': new FormControl(this.user.dienThoai, [Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]),
          'txt_taikhoan': new FormControl(this.user.taiKhoan, [Validators.required, Validators.minLength(5), Validators.maxLength(50)]),
          'txt_matkhau': new FormControl(this.user.matKhau, [this.pwdCheckValidator]),
          'txt_nhaplai_matkhau': new FormControl(this.user.matKhau, [Validators.required]),
          'txt_loaiquyen': new FormControl('', []),
          'txt_anh': new FormControl(this.user.anhDaiDien, []),
          'txt_thongtinkhac': new FormControl('', []),
        }, {
          validators: [MatchValidation.match('txt_matkhau', 'txt_nhaplai_matkhau')]
        });
        this.loaiQuyen = this.user.loaiQuyen;
        this.formUser.get('txt_ngaysinh')?.patchValue(this.formatDate(new Date(this.user.ngaySinh)));
      });
    });
  }

  public Xoa(idNguoiDung: any) {
    this._api.delete('/api/user/delete-user', idNguoiDung).subscribe(res => {
      alert('Xóa dữ liệu thành công');
      this.LoadData();
    });
  }
  public closeModal() {
    $('#createUserModal').closest('.modal').modal('hide');
  }
  public pwdCheckValidator(control: any) {
    var filteredStrings = { search: control.value, select: '@#!$%&*' }
    var result = (filteredStrings.select.match(new RegExp('[' + filteredStrings.search + ']', 'g')) || []).join('');
    if (control.value.length < 6 || !result) {
      return { matkhau: true };
    } else {
      return null;
    }
  }



  public upload(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      this.file = event.target.files[0];
    }
  }
  public findInvalidControls() {
    const invalid = [];
    const controls = this.formUser.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }
    return invalid;
  }


  OnSubmit(vl: any) {
    console.log(this.findInvalidControls())
    if (this.formUser.invalid) {
      return;
    }
    let obj: any = {};
    obj.nguoidung = {
      HoTen: vl.txt_hoten,
      NgaySinh: vl.txt_ngaysinh,
      GioiTinh: vl.txt_gioitinh,
      DiaChi: vl.txt_diachi,
      Email: vl.txt_email,
      DienThoai: vl.txt_dienthoai,
      TrangThai: true
    }
    obj.taikhoan = {
      TaiKhoan1: vl.txt_taikhoan,
      MatKhau: vl.txt_matkhau,
      Email: vl.txt_email,
      DienThoai: vl.txt_dienthoai,
      LoaiQuyet: this.loaiQuyen,
      TrangThai: true
    }
    if (this.isCreate) {
      if (this.file) {
        this._api.uploadFileSingle('/api/upload/upload-single', 'user', this.file).subscribe((res: any) => {
          if (res && res.body && res.body.filePath) {
            obj.nguoidung.AnhDaiDien = res.body.filePath;
            this._api.post('/api/user/create-user', obj).subscribe(res => {
              if (res && res.data) {
                alert('Thêm dữ liệu thành công');
                this.LoadData();
                this.closeModal();
              } else {
                alert('Có lỗi')
              }
            });
          }
        });
      } else {
        this._api.post('/api/user/create-user', obj).subscribe(res => {
          if (res && res.data) {
            alert('Thêm dữ liệu thành công');
            this.LoadData();
            this.closeModal();
          } else {
            alert('Có lỗi')
          }
        });
      }
    } else {
      obj.nguoidung.idNguoiDung = this.user.idNguoiDung;
      obj.taikhoan.idNguoiDung = this.user.idNguoiDung;
      if (this.file) {
        this._api.uploadFileSingle('/api/upload/upload-single', 'user', this.file).subscribe((res: any) => {
          if (res && res.body && res.body.filePath) {
            obj.nguoidung.AnhDaiDien = res.body.filePath;
            this._api.post('/api/user/update-user', obj).subscribe(res => {
              if (res && res.data) {
                alert('Cập nhật dữ liệu thành công');
                this.LoadData();
                this.closeModal();
              } else {
                alert('Có lỗi')
              }
            });
          }
        });
      } else {
        this._api.post('/api/user/update-user', obj).subscribe(res => {
          if (res && res.data) {
            alert('Cập nhật dữ liệu thành công');
            this.LoadData();
            this.closeModal();
          } else {
            alert('Có lỗi')
          }
        });
      }
    }

  }
  ngAfterViewInit() {
    this.loadScripts('./assets/plugins/common/common.min.js','./assets/js/custom.min.js',
    './assets/js/settings.js','./assets/js/gleek.js','./assets/js/styleSwitcher.js',
    './assets//plugins/chart.js/Chart.bundle.min.js','./assets/plugins/circle-progress/circle-progress.min.js','./assets/plugins/d3v3/index.js',
    './assets/plugins/topojson/topojson.min.js','./assets/plugins/datamaps/datamaps.world.min.js','./assets/plugins/raphael/raphael.min.js',
    './assets/plugins/morris/morris.min.js','./assets/plugins/moment/moment.min.js',
    './assets/plugins/pg-calendar/js/pignose.calendar.min.js','./assets/plugins/chartist/js/chartist.min.js',
    './assets/plugins/chartist-plugin-tooltips/js/chartist-plugin-tooltip.min.js','./assets/js/dashboard/dashboard-1.js');
  }

}
