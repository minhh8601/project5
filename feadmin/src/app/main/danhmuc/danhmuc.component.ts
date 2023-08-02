import { AfterViewInit, Component, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'src/app/core/common/base-component';
declare var $: any;
@Component({
  selector: 'app-danhmuc',
  templateUrl: './danhmuc.component.html',
  styleUrls: ['./danhmuc.component.css']
})
export class DanhmucComponent extends BaseComponent implements OnInit, AfterViewInit {

  public list_dm:any;
  public isCreate = false;
  public dm: any;
  public formDM: FormGroup;
  public formSearch: FormGroup;
  public file: any;
  public hienModal: any;
  public suscessForm: any;
  constructor(injector: Injector) {
    super(injector)
    this.formSearch = new FormGroup({
      'txt_tenDanhMuc': new FormControl('', []),
    });
   }

  ngOnInit(): void {
    // this._api.get('/api/sanpham/get-all').subscribe(res => {
    //   this.list_dm = res;
    //   debugger;

    // });
    this.LoadData();
  }
  get idDanhMuc() {
    return this.formDM.get('txt_idDanhMuc')!;
  }
  get idDanhMucCha() {
    return this.formDM.get('txt_idDanhMucCha')!;
  }

  get tenDanhMuc() {
    return this.formDM.get('txt_tenDanhMuc')!;
  }
  get stt() {
    return this.formDM.get('txt_stt')!;
  }
  get trangThai() {
    return this.formDM.get('txt_trangThai')!;
  }


  public LoadData() {
    this._api.post('/api/danhmucsanpham/search-danhmuc', { page: 1, pageSize: 15,hoten: this.formSearch.value['txt_tenDanhMuc']}).subscribe(res => {
      this.list_dm = res.data;
      console.log(res.data);

    });
  }

  public KhoiTaoModal() {
    this.hienModal = true;
    this.isCreate = true;
    setTimeout(() => {
      $('#createdmModal').modal('toggle');
      this.suscessForm = true;
      this.formDM = new FormGroup({
        'txt_idDanhMuc': new FormControl('', [Validators.required]),
        'txt_idDanhMucCha': new FormControl('', [Validators.required]),
        'txt_tenDanhMuc': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(150)]),
        'txt_stt': new FormControl('', [Validators.required]),
        'txt_trangThai': new FormControl('', [Validators.required]),

      });
      this.suscessForm= true;
    });
  }

  public CapNhatModel(idDanhMuc: any) {
    this.hienModal = true;
    this.suscessForm = false;
    this.isCreate = false;
    setTimeout(() => {
      $('#createdmModal').modal('toggle');
      this._api.get('/api/danhmucsanpham/get-by-id/' + idDanhMuc).subscribe(res => {
        this.dm = res.dm;
        this.suscessForm = true;
        this.formDM = new FormGroup({
          // 'txt_idDanhMuc': new FormControl('', [Validators.required]),
          'txt_idDanhMucCha': new FormControl('', [Validators.required]),
          'txt_tenDanhMuc': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(150)]),
          'txt_stt': new FormControl('', [Validators.required]),
          'txt_trangThai': new FormControl('',[]),
        });

      });
    });
  }


  public DongForm() {
    $('#createdmModal').closest('.modal').modal('hide');
  }

  public upload(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      this.file = event.target.files[0];
    }
  }



  OnSubmit(dm: any) {

    if (this.formDM.invalid) {
      return;
    }
    let dmsp: any = {};
    dmsp.danhmuc = {
      
      idDanhMucCha:dm.txt_idDanhMucCha,
      tenDanhMuc: dm.txt_tenDanhMuc,
      stt:dm.txt_stt,
      trangThai:dm.txt_trangThai,
    }
    if(this.isCreate){

      this._api.post('/api/danhmucsanpham/create-newdm', dmsp).subscribe(res => {
        if (res && res.data) {

          alert('Thêm dữ liệu thành công');
          this.LoadData();
          this.DongForm();

        }
        else {
          alert('Error');
          this.LoadData();
          this.DongForm();
        }
      });

    }
    else{
      this._api.post('/api/danhmucsanpham/update-dm', dmsp).subscribe(res => {
        if (res && res.data) {
          alert('Cập nhật dữ liệu thành công');
          this.LoadData();
          this.DongForm();
        } else {
          alert('Error');
          this.DongForm();
        }
      });
    }


  }
  public Xoadm(idDanhMuc: any) {
    var hoi = confirm("Bạn có chắc chắn muốn xóa sản phẩm này không?")
    if(hoi){
      this._api.delete('/api/danhmucsanpham/delete-sp', idDanhMuc).subscribe(res => {
      alert('Xóa dữ liệu thành công');
      this.LoadData();
      console.log(idDanhMuc);
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
