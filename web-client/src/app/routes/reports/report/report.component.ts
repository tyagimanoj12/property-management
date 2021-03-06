import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable';
import { Page } from '../../../core/common/page.model';
import { Router } from '@angular/router';
import { ToasterService, ToasterConfig } from 'angular2-toaster';
import { PaymentViewModel } from '../../../core/payments/payment.model';
import { PaymentService } from '../../../core/payments/payment.service';
import { LoginResultModel } from '../../../core/administrators/administrator.model';

const swal = require('sweetalert');
const _clone = (d) => JSON.parse(JSON.stringify(d));

@Component({
    selector: 'app-report-list',
    templateUrl: './report.component.html',
    styleUrls: ['./report.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class ReportComponent implements OnInit {
    page = new Page();
    rows = new Array<PaymentViewModel>();
    payments = new Array<PaymentViewModel>();
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
        private router: Router,
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
        this.rows = _clone(data);
    }

    setPage(pageInfo) {
        this.getData(pageInfo);
    }

    getData(pageInfo: any, query?: string,paymentType?:string) {
        this.page.pageNumber = pageInfo.offset;
        this.page.query = query ? query : '';
        this.loader = true;
        this.paymentService.getPayments(this.page)
            .subscribe(result => {
                console.log(result);               
               this.rows= result.data;
               this.rows=this.rows.filter(x=>x.paymentType=='Debit')
                this.payments=result.data;
                this.page = result.page;
                this.page.pageNumber = this.page.pageNumber - 1;  
                //this.cacheData(result.data);
                this.loader = false;
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

    navigateToAddPayment(type:any) {
        console.log(type);
        console.log(this.rows);     
        this.rows=this.payments;   
        this.rows = this.rows.filter(x=>x.paymentType==type);
        //this.router.navigate(['/reports/list']);
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
