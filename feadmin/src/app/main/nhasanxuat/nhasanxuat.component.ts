
import { AfterViewInit, Component, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from 'src/app/core/common/base-component';
declare var $: any;
@Component({
  selector: 'app-nhasanxuat',
  templateUrl: './nhasanxuat.component.html',
  styleUrls: ['./nhasanxuat.component.css']
})
export class NhasanxuatComponent extends BaseComponent implements OnInit, AfterViewInit {

  public list_nsx:any;
  public isCreate = false;
  public nsx: any;
  public formNSX: FormGroup;
  public formSearch: FormGroup;
  public hienModal: any;
  public suscessForm: any;
  constructor(injector: Injector) {
    super(injector)
    this.formSearch = new FormGroup({

      'txt_tenNhaSanXuat': new FormControl('', []),
      'txt_moTa': new FormControl('', []),

    });
   }

  ngOnInit(): void {
    this.LoadData();
  }
  get idNhaSanXuat() {
    return this.formNSX.get('txt_idNhaSanXuat')!;
  }
  get tenNhaSanXuat() {
    return this.formNSX.get('txt_tenNhaSanXuat')!;
  }

  get moTa() {
    return this.formNSX.get('txt_moTa')!;
  }

  public LoadData() {
    this._api.post('/api/NhaSanXuat/search-nsx', { hoten: this.formSearch.value['txt_tenNhaSanXuat']}).subscribe(res => {
      this.list_nsx = res.data;

    });
  }
  public DongForm() {
    $('#createModal').closest('.modal').modal('hide');
  }

  public KhoiTaoModal() {
    this.hienModal = true;
    this.isCreate = true;
    setTimeout(() => {
      $('#createModal').modal('toggle');
      this.suscessForm = true;
      this.formNSX = new FormGroup({
        'txt_tenNhaSanXuat': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(250)]),
        'txt_moTa': new FormControl('', [Validators.required]),

      }, {
      });
    });
  }

  public CapNhatModal(idNhaSanXuat: any) {
    this.hienModal = true;
    this.suscessForm = false;
    this.isCreate = false;
    setTimeout(() => {
      $('#createModal').modal('toggle');
      this._api.get('/api/NhaSanXuat/get-by-id/' + idNhaSanXuat).subscribe(res => {
        this.nsx = res.nsx;
        this.suscessForm = true;
        this.formNSX = new FormGroup({
          'txt_tenNhaSanXuat': new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(250)]),
          'txt_moTa': new FormControl('', [Validators.required]),
        }, {
        });
      });
    });
    this.suscessForm=true;
  }

  public XoaNSX(idNhaSanXuat: any) {
    var hoi = confirm("Bạn có chắc chắn muốn xóa nhà sản xuất này không?")
    if(hoi){
      this._api.delete('/api/NhaSanXuat/delete-nsx/', idNhaSanXuat).subscribe(res => {
      alert('Xóa dữ liệu thành công');
      this.LoadData();
      console.log(idNhaSanXuat);
    });
    }
  }


  OnSubmit(nsx: any) {

    if (this.formNSX.invalid) {
      return;
    }
    let obj: any = {};
    obj.nhasanxuat = {

      tenNhaSanXuat: nsx.txt_tenNhaSanXuat,
      moTa:nsx.txt_moTa,

    }
    if(this.isCreate){
      this._api.post('/api/NhaSanXuat/create-newnsx', obj).subscribe(res => {
        if (res && res.data) {
          alert('Thêm dữ liệu thành công');
          this.LoadData();
          this.DongForm();
        } else {
          alert('Error');
          this.LoadData();
          this.DongForm();
        }
      });
    }
    else{
      this._api.post('/api/NhaSanXuat/update-nsx', obj).subscribe(res => {
        if (res && res.data) {
          alert('Cập nhật dữ liệu thành công');
          this.LoadData();
          this.DongForm();
        } else {
          alert('Error')
        }
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

