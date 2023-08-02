import { BaseComponent } from './../../core/common/base-component';
import { Component, OnInit, AfterViewInit, Injector, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

declare var $: any;
@Component({
  selector: 'app-sanpham',
  templateUrl: './sanpham.component.html',
  styleUrls: ['./sanpham.component.css']
})
export class SanphamComponent extends BaseComponent implements OnInit, AfterViewInit {
  p:number;
  sortProperty: string = 'id';
  sortOrder = 1;
  public list_sp:any;
  public isCreate = false;
  public sp: any;
  public formSP: FormGroup;
  public formSearch: FormGroup;
  public file: any;
  public hienModal: any;
  public suscessForm: any;
  constructor(injector: Injector) {
    super(injector)
    this.formSearch = new FormGroup({
      'txt_tenSanPham': new FormControl('', []),
      'txt_gia': new FormControl('', []),
    });
   }


  ngOnInit(): void {


    this.LoadData();
  }
  get idDanhMuc() {
    return this.formSP.get('txt_idDanhMuc')!;
  }
  get tenSanPham() {
    return this.formSP.get('txt_tenSanPham')!;
  }

  get moTaSanPham() {
    return this.formSP.get('txt_moTaSanPham')!;
  }
  get anh() {
    return this.formSP.get('txt_anh')!;
  }
  get idNhaSanXuat() {
    return this.formSP.get('txt_idNhaSanXuat')!;
  }

  get donViTinh() {
    return this.formSP.get('txt_donViTinh')!;
  }

  get gia() {
    return this.formSP.get('txt_gia')!;
  }


  get ngayTao() {
    return this.formSP.get('txt_ngayTao')!;
  }


  public LoadData() {
    if(true){
      this._api.post('/api/sanpham/search-sanpham', { ten : this.formSearch.value['txt_tenSanPham']}).subscribe(res => {
        this.list_sp = res.data;
        console.log(res.data);
        console.log(this.formSearch.value['txt_tenSanPham'])
      });
    }

      if(this.formSearch.value['txt_tenSanPham']=="" && this.formSearch.value['txt_gia']!=""){
        this._api.post('/api/sanpham/search-giasanpham', { giasp : this.formSearch.value['txt_gia']}).subscribe(res => {
          this.list_sp = res.data;
          console.log(res.data);
          console.log(this.formSearch.value['txt_gia'])
        });
      }

  }

  public KhoiTaoModal() {
    this.hienModal = true;
    this.isCreate = true;
    setTimeout(() => {
      $('#createSPModal').modal('toggle');
      this.suscessForm = true;
      this.formSP = new FormGroup({
        'txt_idDanhMuc': new FormControl('', [Validators.required]),
        'txt_tenSanPham': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(250)]),
        'txt_moTaSanPham': new FormControl('', [Validators.required]),
        'txt_anh': new FormControl('', [Validators.required]),
        'txt_gia': new FormControl('', [Validators.required]),
        'txt_idNhaSanXuat': new FormControl('', [Validators.required]),
        'txt_donViTinh': new FormControl('', [Validators.required]),
        'txt_ngayTao': new FormControl('', [Validators.required]),
      });
      this.suscessForm= true;
    });
  }

  public CapNhatModel(idSanPham: any){

    this.hienModal=true;
    this.suscessForm=false;
    this.isCreate=false;
    setTimeout(() => {
      $('#createSPModal').modal('toggle');
      this._api.get('/api/sanpham/get-by-id/' + idSanPham).subscribe(res => {
        this.sp = res;
        console.log(res);
        this.formSP= new FormGroup({
          'txt_tenSanPham': new FormControl(this.sp.tenSanPham, [Validators.required, Validators.minLength(3), Validators.maxLength(250)]),
          'txt_moTaSanPham': new FormControl(this.sp.moTaSanPham, [Validators.required]),
          'txt_gia': new FormControl(this.sp.gia, [Validators.required]),
          'txt_anh': new FormControl(this.sp.anh, [Validators.required]),
          'txt_idDanhMuc': new FormControl(this.sp.idDanhMuc, [Validators.required]),
          'txt_idNhaSanXuat': new FormControl(this.sp.idNhaSanXuat, [Validators.required]),
          'txt_donViTinh': new FormControl(this.sp.donViTinh, [Validators.required]),
          'txt_ngayTao':new FormControl(this.sp.ngayTao, [Validators.required]),

        }),

        this.suscessForm=true;
      });
    });
  }
  sortBy(property: string) {
    this.sortOrder = property === this.sortProperty ? (this.sortOrder * -1) : 1;
    this.sortProperty = property;
    this.list_sp = [...this.list_sp.sort((a: any, b: any) => {
        // sort comparison function
        let result = 0;
        if (a[property] < b[property]) {
            result = -1;
        }
        if (a[property] > b[property]) {
            result = 1;
        }
        return result * this.sortOrder;
    })];
}

sortIcon(property: string) {
    if (property === this.sortProperty) {
        return this.sortOrder === 1 ? '☝️' : '👇';
    }
    return '';
}



  public DongForm() {
    $('#createSPModal').closest('.modal').modal('hide');
  }

  public upload(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      this.file = event.target.files[0];
    }
  }

  public findInvalidControls() {
    const invalid = [];
    const controls = this.formSP.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }
    return invalid;
  }

  OnSubmit(sp: any) {
    console.log(this.findInvalidControls())
    if (this.formSP.invalid) {
      return;
    }
    let obj: any = {};
    obj.sanpham = {
      idDanhMuc: sp.txt_idDanhMuc,
      tenSanPham: sp.txt_tenSanPham,
      moTaSanPham:sp.txt_moTaSanPham,
      anh:sp.txt_anh,
      gia:sp.txt_gia,
      idNhaSanXuat:sp.txt_idNhaSanXuat,
      donViTinh:sp.txt_donViTinh,
      ngayTao:sp.txt_ngayTao
    }

    if (this.isCreate) {
      if (this.file) {
        this._api.uploadFileSingle('/api/upload/upload-single', 'sanpham', this.file).subscribe((res: any) => {
          if (res && res.body && res.body.filePath) {
            obj.sanpham.Anh = res.body.filePath;
            this._api.post('/api/sanpham/create-newsp', obj).subscribe(res => {
              if (res && res.data) {
                alert('Thêm dữ liệu thành công');
                this.LoadData();
                this.DongForm();
              } else {
                alert('Có lỗi')
              }
            });
          }
        });
      } else {
        this._api.post('/api/sanpham/create-newsp', obj).subscribe(res => {
          if (res && res.data) {
            alert('Thêm dữ liệu thành công');
            this.LoadData();
            this.DongForm();
          } else {
            alert('Có lỗi')
          }
        });
      }
    } else {
      obj.sanpham.IdSanPham = this.sp.idSanPham;
      if (this.file) {
        this._api.uploadFileSingle('/api/upload/upload-single', 'sanpham', this.file).subscribe((res: any) => {
          if (res && res.body && res.body.filePath) {
            obj.sanpham.Anh = res.body.filePath;
            this._api.post('/api/sanpham/update-sp', obj).subscribe(res => {
              if (res && res.data) {
                alert('Cập nhật dữ liệu thành công');
                this.LoadData();
                this.DongForm();
              } else {
                alert('Có lỗi')
              }
            });
          }
        });
      } else {

        this._api.post('/api/sanpham/update-sp', obj).subscribe(res => {
          if (res && res.data) {
            alert('Cập nhật dữ liệu thành công');
            this.LoadData();
            this.DongForm();
          } else {
            alert('Có lỗi')
          }
        });
      }
    }

  }


  public XoaSP(idSanPham: any) {
    var hoi = confirm("Bạn có chắc chắn muốn xóa sản phẩm này không?")
    if(hoi){
      this._api.delete('/api/sanpham/delete-sp', idSanPham).subscribe(res => {
      alert('Xóa dữ liệu thành công');
      this.LoadData();
      console.log(idSanPham);
    });
    }
  }
  ngAfterViewInit(): void {
    this.loadScripts('./assets/plugins/common/common.min.js','./assets/js/custom.min.js',
    './assets/js/settings.js','./assets/js/gleek.js','./assets/js/styleSwitcher.js',
    './assets//plugins/chart.js/Chart.bundle.min.js','./assets/plugins/circle-progress/circle-progress.min.js','./assets/plugins/d3v3/index.js',
    './assets/plugins/topojson/topojson.min.js','./assets/plugins/datamaps/datamaps.world.min.js','./assets/plugins/raphael/raphael.min.js',
    './assets/plugins/morris/morris.min.js','./assets/plugins/moment/moment.min.js',
    './assets/plugins/pg-calendar/js/pignose.calendar.min.js','./assets/plugins/chartist/js/chartist.min.js',
    './assets/plugins/chartist-plugin-tooltips/js/chartist-plugin-tooltip.min.js','./assets/js/dashboard/dashboard-1.js');

  }
}
