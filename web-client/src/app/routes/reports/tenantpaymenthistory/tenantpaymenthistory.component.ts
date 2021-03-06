import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable';
import { Page } from '../../../core/common/page.model';
import { Router,ActivatedRoute } from '@angular/router';
import { ToasterService, ToasterConfig } from 'angular2-toaster';
import { PaymentViewModel } from '../../../core/payments/payment.model';
import { PaymentService } from '../../../core/payments/payment.service';
import { LoginResultModel } from '../../../core/administrators/administrator.model';
import { PropertyService } from '../../../core/properties/property.service';
import { PropertyViewModel } from '../../../core/properties/property.model';
import { TenantService } from '../../../core/tenants/tenant.service';
import { TenantViewModel } from '../../../core/tenants/tenant.model';
import { AssignedPropertyViewModel } from "../../../core/assignedproperties/assignedproperty.model";
import { AssignedPropertyService } from "../../../core/assignedproperties/assignedproperty.service";

const swal = require('sweetalert');
const _clone = (d) => JSON.parse(JSON.stringify(d));

@Component({
    selector: 'app-tenantpaymenthistory',
    templateUrl: './tenantpaymenthistory.component.html',
    styleUrls: ['./tenantpaymenthistory.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class TenantPaymentHistoryComponent implements OnInit {
    page = new Page();
    paymentrows = new Array<PaymentViewModel>();
    propertyrows = new Array<AssignedPropertyViewModel>();
    payments = new Array<PaymentViewModel>();
    tenant = new TenantViewModel();
    tenantId='';
    loader = false;
    timeout: any;
    ownerId:string='';
    toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    columns = [
        { prop: 'id', name: 'Action' },
        { prop: 'Name', name: 'name' },
        //{ prop: 'settingValue', name: 'settingValue' }
    ];
    columnsSort = [
        { prop: 'Name', name: 'name' },
        //{ prop: 'settingValue', name: 'settingValue' }
    ];

    @ViewChild(DatatableComponent) table: DatatableComponent;
    @ViewChild('myTable') tableExp: any;

    constructor(private tenantService: TenantService,        
    private assignedPropertyService: AssignedPropertyService,
        private paymentService: PaymentService,        
        private propertyService: PropertyService,
        private router: Router,
        private route: ActivatedRoute,
        private toasterService: ToasterService,) {
        this.page.pageNumber = 0;

        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel; 
       
        this.ownerId=currentUser.ownerId;
        this.page.ownerId=this.ownerId;
        console.log('hi');

        //this.page.size = 20 ; // todo: make it 10 or 20
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
        this.paymentrows = _clone(data);
    }

    setPage(pageInfo) {
        this.getData(pageInfo);
    }

    getData(pageInfo: any, query?: string,paymentType?:string) {
        this.page.pageNumber = pageInfo.offset;
        this.page.query = query ? query : '';
        if(!this.tenantId){
            this.tenantId=localStorage.getItem('tenantId')
        }
        console.log('this.tenantId');
        console.log(this.tenantId);
        this.page.tenantId=this.tenantId;
        this.loader = true;
        this.paymentService.getPayments(this.page)
            .subscribe(result => {
                        
               this.paymentrows= result.data;              
                this.payments=result.data;
                this.page = result.page;
                this.page.pageNumber = this.page.pageNumber - 1;  
                //this.cacheData(result.data);
                this.loader = false;
            });

            this.assignedPropertyService.getAssignedProperties(this.page)
            .subscribe(result => {
               
                this.propertyrows = result.data;                    
                this.page = result.page;
                this.page.pageNumber = this.page.pageNumber - 1;  
                //this.cacheData(result.data);
                this.loader = false;
            });

            this.tenantService.getTenant(this.tenantId)
                .subscribe(data => {    
                   this.tenant.address=data.address;
                   this.tenant.email=data.email;
                   this.tenant.name=data.name;
                   this.tenant.phone=data.phone;
                });
    }

    updateValue(event, cell, rowIndex) {
        console.log('inline editing rowIndex', rowIndex);
        // this.editing[rowIndex + '-' + cell] = false;
        this.paymentrows[rowIndex][cell] = event.target.value;
        this.paymentrows = [...this.paymentrows];
        console.log('UPDATED!', this.paymentrows[rowIndex][cell]);
    }

    ngOnInit() {
        this.route.paramMap.subscribe(
            parameterMap => {
                this.tenantId = parameterMap.get('id');
            });
            this.tenantId =  this.route.snapshot.params['id'];  
            localStorage.setItem('tenantId', this.tenantId);
            if(!this.tenantId){
                this.tenantId=localStorage.getItem('tenantId')
            }
            
        this.setPage({ offset: 0 });
    }

    updateFilter(event) {
        const query = event.target.value.toLowerCase();
        this.getData({ offset: 0 }, query);
    }

    // Selection
    onSelect({ selected }) {

    }

    

    onActivate(event) {
        console.log('Activate Event', event);
    }
    onEdit(event) {
        console.log(event.paymentId);
        this.router.navigate(['/payments/edit', event.paymentId]);
    }

    confirmDelete(row) {
        swal({
            title: 'Are you sure you want to delete this payment?',
            text: 'This setting will be deleted!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Yes, delete it!',
            closeOnConfirm: false
        }, () => {
            console.log(row);
            this.paymentService.deletePayment(row.id)
                .subscribe(result => {
                    if (result.success) {
                        swal('Deleted!', 'Payment has been successfully deleted.', 'success');
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
