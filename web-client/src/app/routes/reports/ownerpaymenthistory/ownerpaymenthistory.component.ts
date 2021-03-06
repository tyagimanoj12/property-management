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
import { PropertyOwnerService } from '../../../core/propertyowners/propertyowner.service';
import { PropertyOwnerViewModel } from '../../../core/propertyowners/propertyowner.model';

const swal = require('sweetalert');
const _clone = (d) => JSON.parse(JSON.stringify(d));

@Component({
    selector: 'app-ownerpaymenthistory',
    templateUrl: './ownerpaymenthistory.component.html',
    styleUrls: ['./ownerpaymenthistory.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class OwnerPaymentHistoryComponent implements OnInit {
    page = new Page();
    paymentrows = new Array<PaymentViewModel>();
    propertyrows = new Array<PropertyViewModel>();
    payments = new Array<PaymentViewModel>();
    propertyOwner = new PropertyOwnerViewModel();
    propertyOwnerId='';
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

    constructor(private paymentService: PaymentService,
        private propertyOwnerService: PropertyOwnerService,
        private propertyService: PropertyService,
        private router: Router,
        private route: ActivatedRoute,
        private toasterService: ToasterService,) {
        this.page.pageNumber = 0;

        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel; 
        console.log(currentUser);
        this.ownerId=currentUser.ownerId;
        this.page.ownerId=this.ownerId;

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
        this.loader = true;
        if(!this.propertyOwnerId){
            this.propertyOwnerId=localStorage.getItem('propertyOwnerId')
        }
        this.page.propertyOwnerId=this.propertyOwnerId;
        this.paymentService.getPayments(this.page)
            .subscribe(result => {
                console.log(result);               
               this.paymentrows= result.data;
               //this.paymentrows=this.paymentrows.filter(x=>x.propertyOwnerId==this.propertyOwnerId)
                this.payments=result.data;
                this.page = result.page;
                this.page.pageNumber = this.page.pageNumber - 1;  
                this.cacheData(result.data);
                this.loader = false;
            });

            this.propertyService.getProperties(this.page)
            .subscribe(result => {
                console.log(result);
                this.propertyrows = result.data;
                console.log(this.propertyOwnerId);
                //this.rows = this.rows.filter(x=>x.propertyOwnerId==this.propertyOwnerId);
               // const dt=this.propertyrows.filter(x=>x.propertyOwnerId==this.propertyOwnerId);
               console.log(result.data)
            //    console.log(dt)
            //    this.propertyrows=dt;
                this.page = result.page;
                this.page.pageNumber = this.page.pageNumber - 1;  
                this.cacheData(result.data);
                this.loader = false;
            });

            this.propertyOwnerService
        .getPropertyOwner(this.propertyOwnerId)
        .subscribe(data => {          
          console.log(data);
          this.propertyOwner.address=data.address;
          this.propertyOwner.email=data.email;
          this.propertyOwner.name=data.name;
          this.propertyOwner.phone=data.phone;

        });
    }

    updateValue(event, cell, rowIndex) {
        console.log('inline editing rowIndex', rowIndex);
        // this.editing[rowIndex + '-' + cell] = false;
        this.propertyrows[rowIndex][cell] = event.target.value;
        this.propertyrows = [...this.propertyrows];
        console.log('UPDATED!', this.propertyrows[rowIndex][cell]);
    }

    ngOnInit() {
        this.route.paramMap.subscribe(
            parameterMap => {
                this.propertyOwnerId = parameterMap.get('id');
            });
            localStorage.setItem('propertyOwnerId', this.propertyOwnerId);
            if(!this.propertyOwnerId){
                this.propertyOwnerId=localStorage.getItem('propertyOwnerId')
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
