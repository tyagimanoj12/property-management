import { Component, OnInit, ViewEncapsulation, ViewChild, Input, Output, EventEmitter  } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable';
import { Page } from '../../../core/common/page.model';
import { AdministratorService } from '../../../core/administrators/administrator.service';
import { LoginResultModel } from '../../../core/administrators/administrator.model';
import { Router } from '@angular/router';
import { ToasterService, ToasterConfig } from 'angular2-toaster';
import { TenantViewModel, AssignedPropertyViewModel } from '../../../core/tenants/tenant.model';
import { TenantService } from '../../../core/tenants/tenant.service';
import { AssignedPropertyService } from '../../../core/assignedproperties/assignedproperty.service';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl, FormArrayName, Form } from '@angular/forms';
import { PropertyService } from '../../../core/properties/property.service';
import { SelectItemModel } from '../../../core/common/select-item.model';
import { PropertyViewModel } from '../../../core/properties/property.model';
import { Observable } from 'rxjs';
import { HttpEventType, HttpClient } from '@angular/common/http';

const swal = require('sweetalert');
const _clone = (d) => JSON.parse(JSON.stringify(d));

@Component({
    selector: 'app-tenant-list',
    templateUrl: './tenant-list.component.html',
    styleUrls: ['./tenant-list.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class TenantListComponent implements OnInit {
    public progress: number;
    public message: string;
   @Output() public onUploadFinished = new EventEmitter();
    dateTo:any;
    rentStartDate:any;
    dateFrom:any;

    propertyName:string='';
    ownerName:string='';
    area:string='';
    rent:string='';
    address:string='';
    file: any;
    formData:any;
    ownerId: string = '';
    page = new Page();
    rows = new Array<TenantViewModel>();
    loader = false;
    submitted = false;
    timeout: any;
    tenantId: string = '';
    properties: SelectItemModel[] = [];
    toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    columns = [
        { prop: 'id', name: 'Action' },
        { prop: 'Name', name: 'name' },
        // { prop: 'settingValue', name: 'settingValue' }
    ];
    columnsSort = [
        { prop: 'Name', name: 'name' },
        // { prop: 'settingValue', name: 'settingValue' }
    ];
    @ViewChild(DatatableComponent) table: DatatableComponent;
    @ViewChild('myTable') tableExp: any;
    @ViewChild('fileInput') fileInput:any;
    propertyData: any;
    public assignPropertyForm: FormGroup;

    constructor(private fb: FormBuilder,
        private http: HttpClient,
        private tenantService: TenantService,
        private assignedPropertyService: AssignedPropertyService,
        private propertyService: PropertyService,
        private router: Router,
        private toasterService: ToasterService,
        private authService: AdministratorService) {
        this.page.pageNumber = 0;
        // this.page.size = 20 ; // todo: make it 10 or 20

        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel;
        console.log(currentUser);
        this.ownerId = currentUser.ownerId;
        this.page.ownerId = this.ownerId;

        this.propertyService.getProperties(this.page)
            .subscribe(x => {
                x.data.forEach((y: PropertyViewModel) => {
                    this.properties.push(new SelectItemModel(y.name, y.propertyId));
                });
            });


        this.assignPropertyForm = this.fb.group({
            propertyId: ['', Validators.required],
            tenantId: '',
            dateFrom: '',
            dateTo: '',
            rentStartDate: '',            
            rent:''


        });
    }

    public uploadFile = (files) => {
        if (files.length === 0) {
          return;
        }
     
        let fileToUpload = <File>files[0];
        const formData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);
     
        this.http.post('http://7e124334.ngrok.io/api/upload', formData, {reportProgress: true, observe: 'events'})
          .subscribe(event => {
            if (event.type === HttpEventType.UploadProgress)
              this.progress = Math.round(100 * event.loaded / event.total);
            else if (event.type === HttpEventType.Response) {
                debugger;
              this.message = 'Upload success.';
              this.filePath=   this.onUploadFinished.emit(event.body);             
            }
          });
      }

      public response: {dbPath: ''};

      filePath:any;

    submitFormAssignProperty($ev, model: AssignedPropertyViewModel) {
        $ev.preventDefault();
        this.submitted = true;    
        // this.formData = new FormData();        
        // this.formData.append(this.file.name, this.file);   
        // model.formdata=this.formData;  

        //   console.log('form data');  
        // console.log(this.file);
        // console.log(this.file.name);
        // console.log('form data');
        // console.log(this.formData);

        
        model.tenantId = this.tenantId;

        console.log(model);

        this.assignedPropertyService.saveAssignedProperty(model).subscribe(
            res => {
                if (res.success) {
                    this.toasterService.pop('success', 'This record has been successfully send');
                    // this.submitted = false;
                    const element = document.getElementById('CloseTryButton') as any;
                    element.click();
                }
            },
            error => {
                console.log(error);
                this.loader = false;
                this.toasterService.pop('error', 'Server Error!', error.error);
            });

    }

    uploadPhoto(){
        console.log('hi');
        let nativeElement: HTMLInputElement = this.fileInput.nativeElement;
        this.assignedPropertyService.upload(nativeElement.files); 
      }
    
    postMethod(files: any) {
        
        this.file= files.item(0); 
        this.formData = new FormData();
        for (let file of this.file)
        this.formData.append(file.name, file);       
        console.log(this.file);
        console.log(this.file.name);
        return false; 
        }

    onPage(event) {
        clearTimeout(this.timeout);
        this.timeout = setTimeout(() => {
            console.log('paged!', event);
        }, 100);
    }
    toggleExpandRow(row) {
        console.log('Toggled Expand Row!', row);
        this.tableExp.rowDetail.toggleExpandRow(row);
    }

    onDetailToggle(event) {
        console.log('Detail Toggled', event);
    }

    cacheData(data) {
        this.rows = _clone(data);
    }

    setPage(pageInfo) {
        this.getData(pageInfo);
    }

    getData(pageInfo: any, query?: string) {
        this.page.pageNumber = pageInfo.offset;
        this.page.query = query ? query : '';
        this.loader = true;
        this.tenantService.getTenants(this.page)
            .subscribe(result => {
                console.log(result);
                this.rows = result.data;
                this.page = result.page;
                this.page.pageNumber = this.page.pageNumber - 1;
                this.cacheData(result.data);
                this.loader = false;
            });
    }

    public onChange(event): void {  // event will give you full breif of action
        const newVal = event.target.value;
        console.log(newVal);
        this.propertyService.getProperty(newVal)
        .subscribe(data => {
           console.log(data);           
           this.propertyName=data.name;
           this.address=data.address;
           this.area=data.area;
           this.rent=data.rent;
           this.ownerName=data.propertyOwnerName;
           
        });
      }

    updateValue(event, cell, rowIndex) {
        console.log('inline editing rowIndex', rowIndex);
        // this.editing[rowIndex + '-' + cell] = false;
        this.rows[rowIndex][cell] = event.target.value;
        this.rows = [...this.rows];
        console.log('UPDATED!', this.rows[rowIndex][cell]);
    }

    ngOnInit() {
        this.setPage({ offset: 0 });
    }

    updateFilter(event) {
        const query = event.target.value.toLowerCase();
        this.getData({ offset: 0 }, query);
    }

    // Selection
    onSelect({ selected }) {

    }

    navigateToAddTenant() {
        this.router.navigate(['/tenants/create']);
    }

    onActivate(event) {
        console.log('Activate Event', event);
    }
    onEdit(event) {
        console.log(event);
        console.log(event.tenantId);
        this.router.navigate(['/tenants/edit', event.tenantId]);
    }

    onRowClick(event) {
        console.log(event);
        console.log(event.tenantId);
        this.tenantId = event.tenantId;
    }
    NavigateToPaymentHistory(event){
        console.log(event);
        console.log(event.tenantId);
        this.tenantId = event.tenantId;
        //this.router.navigate(['/reports/paymenthistory', event.tenantId]);
        this.router.navigate([]).then(result => {  window.open('/reports/tenant-detail/'+event.tenantId, '_blank'); });
    }
    
    NavigateToPropertyHistory(event){
        console.log(event);
        console.log(event.tenantId);
        this.tenantId = event.tenantId;
        //this.router.navigate(['/reports/propertyhistory', event.tenantId]);
        this.router.navigate([]).then(result => {  window.open('/reports/propertyhistory/'+event.tenantId, '_blank'); });
        //window.open('/reports/propertyhistory/'+event.tenantId, '_blank')
    }

    confirmDelete(row) {
        swal({
            title: 'Are you sure you want to delete this vendor?',
            text: 'This setting will be deleted!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Yes, delete it!',
            closeOnConfirm: false
        }, () => {
            console.log(row);
            this.tenantService.deleteTenant(row.tenantId)
                .subscribe(result => {
                    if (result.success) {
                        swal('Deleted!', 'Tenant has been successfully deleted.', 'success');
                        this.setPage({ offset: 0 });
                    } else {
                        swal('Error', result.message, 'error');
                    }
                }, error => {
                    swal('Error', error.error, 'error');
                });
        });
    }
}