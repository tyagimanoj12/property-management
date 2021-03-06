import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable';
import { Page } from '../../../core/common/page.model';
import { AdministratorService } from '../../../core/administrators/administrator.service';
import { LoginResultModel } from '../../../core/administrators/administrator.model';
import { Router } from '@angular/router';
import { ToasterService, ToasterConfig } from 'angular2-toaster';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl, FormArrayName, Form } from '@angular/forms';
import { SelectItemModel } from '../../../core/common/select-item.model';
import { PropertyOwnerViewModel } from '../../../core/propertyowners/propertyowner.model';
import { PropertyOwnerService } from '../../../core/propertyowners/propertyowner.service';

const swal = require('sweetalert');
const _clone = (d) => JSON.parse(JSON.stringify(d));

@Component({
    selector: 'app-propertyowner-list',
    templateUrl: './propertyowner-list.component.html',
    styleUrls: ['./propertyowner-list.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class PropertyOwnerListComponent implements OnInit {
    ownerId:string='';
    page = new Page();
    rows = new Array<PropertyOwnerViewModel>();
    loader = false;
    submitted = false;
    timeout: any;
    tenantId:string='';
    properties: SelectItemModel[] = [];
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

    constructor(private fb: FormBuilder,
        private propertyOwnerService: PropertyOwnerService,      
        private router: Router,
        private toasterService: ToasterService) {
        this.page.pageNumber = 0;
        //this.page.size = 20 ; // todo: make it 10 or 20

        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel; 
        console.log(currentUser);
        this.ownerId=currentUser.ownerId;
        this.page.ownerId=this.ownerId;
        
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
        this.propertyOwnerService.getPropertyOwners(this.page)
            .subscribe(result => {
                console.log(result);
                this.rows = result.data;
                this.page = result.page;
                this.page.pageNumber = this.page.pageNumber - 1;  
                this.cacheData(result.data);
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

    NavigateToPaymentHistory(event){
        console.log(event);
        console.log(event.tenantId);
        this.tenantId = event.tenantId;
        //this.router.navigate(['/reports/paymenthistory', event.tenantId]);
        this.router.navigate([]).then(result => {  window.open('/reports/property-owner-detail/'+event.propertyOwnerId, '_blank'); });
    }
    
    NavigateToPropertyHistory(event){
        console.log(event);
        console.log(event.tenantId);
        this.tenantId = event.tenantId;
        //this.router.navigate(['/reports/propertyhistory', event.tenantId]);
        this.router.navigate([]).then(result => {  window.open('/reports/ownerpropertyhistory/'+event.propertyOwnerId, '_blank'); });
        //window.open('/reports/propertyhistory/'+event.tenantId, '_blank')
    }

    updateFilter(event) {
        const query = event.target.value.toLowerCase();
        this.getData({ offset: 0 }, query);
    }

    // Selection
    onSelect({ selected }) {

    }

    navigateToAddPropertyOnwer() {
        this.router.navigate(['/propertyowners/create']);
    }

    onActivate(event) {
        console.log('Activate Event', event);
    }
    onEdit(event) {
        console.log(event);
        console.log(event.propertyOwnerId);
        this.router.navigate(['/propertyowners/edit', event.propertyOwnerId]);
    }

    confirmDelete(row) {
        swal({
            title: 'Are you sure you want to delete this property owner?',
            text: 'This property owner will be deleted!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Yes, delete it!',
            closeOnConfirm: false
        }, () => {
            console.log(row);
            this.propertyOwnerService.deletePropertyOwner(row.propertyOwnerId)
                .subscribe(result => {
                    if (result.success) {
                        swal('Deleted!', 'Property owner has been successfully deleted.', 'success');
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
