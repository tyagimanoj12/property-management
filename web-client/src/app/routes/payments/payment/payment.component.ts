import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToasterService, ToasterConfig } from 'angular2-toaster/angular2-toaster';
import { SettingsService } from '../../../core/settings/settings.service';
import { Page } from '../../../core/common/page.model';
import { PropertyService } from '../../../core/properties/property.service';
import { PropertyViewModel } from '../../../core/properties/property.model';
import { LoginResultModel } from '../../../core/administrators/administrator.model';
import { PropertyOwnerService } from '../../../core/propertyowners/propertyowner.service';
import { PropertyOwnerViewModel } from '../../../core/propertyowners/propertyowner.model';
import { PaymentService } from '../../../core/payments/payment.service';
import { TenantService } from '../../../core/tenants/tenant.service';
import { PaymentViewModel } from '../../../core/payments/payment.model';
import { TenantViewModel, AssignedPropertyViewModel } from '../../../core/tenants/tenant.model';
import { SelectItemModel } from '../../../core/common/select-item.model';
import { AssignedPropertyService } from "../../../core/assignedproperties/assignedproperty.service";

// const swal = require('sweetalert');
@Component({
    selector: 'app-payment',
    templateUrl: './payment.component.html',
    styleUrls: ['./payment.component.scss']
})

export class PaymentComponent implements OnInit {
    propertyId:string;
    amount:string;
    isshow=false;
    tenantId='';
    propertyOwnerId='';
    sAdminLogin = false;
    is_edit = false;
    user: any = {};
    name = '';
    page = new Page();
    propertyOwners: SelectItemModel[] = [];
    properties: SelectItemModel[] = [];
    tenants: SelectItemModel[] = [];
    payments: PaymentViewModel;
    paymentId = '';
    submitted = false;
    ownerId = '';
    toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });

    public paymentForm: FormGroup;
    paymentType: any;

    constructor(private propertyService: PropertyService,
        private assignedPropertyService:AssignedPropertyService,
        private tenantService: TenantService,
        private propertyOwnerService: PropertyOwnerService,
        private paymentService: PaymentService,
        private fb: FormBuilder,
        private toasterService: ToasterService,
        public settings: SettingsService,
        private router: Router,
        private route: ActivatedRoute) {
        this.payments = new PaymentViewModel();
        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel;
        console.log(currentUser);
        // this.ownerId = currentUser.ownerId;
        this.page.pageNumber = 0;

        this.propertyOwnerService.getPropertyOwners(this.page)
        .subscribe(x => {
            x.data.forEach((y: PropertyOwnerViewModel) => {
                this.propertyOwners.push(new SelectItemModel(y.name, y.propertyOwnerId));
            });
        });

        

        this.tenantService.getTenants(this.page)
        .subscribe(x => {
            x.data.forEach((y: TenantViewModel) => {
                this.tenants.push(new SelectItemModel(y.name, y.tenantId));
            });
        });


    }

    ngOnInit() {
        this.route.paramMap.subscribe(
            parameterMap => {
                this.paymentId = parameterMap.get('id');
            });

        const page = new Page();
        page.size = 9999;

        if (this.paymentId) {
            this.paymentForm = this.fb.group({
                paymentId: [''],
                debit : [''],
                credit : [''],
                propertyId : ['', [Validators.required]],
                propertyOwnerId : [''],
                tenantId : [''],
                amount : ['', [Validators.required]],
                paymentType : '',
                propertyName:'',
                propertyOwnerName:'',
                tenantName:''

            });
            this.paymentService.getPayment(this.paymentId)
                .subscribe(data => {
                    console.log(data);                               
                    this.paymentForm.setValue(data);
                    if(data.paymentType==1){
                        this.isshow=true;
                    }
                    else{
                        this.isshow=false;
                    }   

                    this.propertyId=data.propertyId;
                    this.tenantId=data.tenantId;

                    this.propertyService.getProperties(this.page)
                    .subscribe(x => {
                        x.data.forEach((y: PropertyViewModel) => {
                            console.log(y.propertyOwnerId);
                            console.log(data.propertyOwnerId);
                            if(y.propertyOwnerId==data.propertyOwnerId)
                            {
                                this.properties.push(new SelectItemModel(y.name, y.propertyId));
                            }
                            
                        });
                    });
                }
                );
        } else {
            this.paymentForm = this.fb.group({
                paymentId: [''],
                debit : [''],
                credit : [''],
                propertyId : ['', [Validators.required]],
                propertyOwnerId : [''],
                tenantId : [''],
                amount : ['', [Validators.required]],
                paymentType : ['', [Validators.required]],
                propertyName:'',
                propertyOwnerName:'',
                tenantName:''
            });
        }
    }

    save(model: any, isValid: boolean, e: any) {
        console.log(model);
        if(model.paymentType==1){
            model.debit=true;
            model.credit=false;
        }
        else{
            model.credit=true;
            model.debit=false;
        }

        this.submitted = true;
        e.preventDefault();
        if (isValid) {
            console.log('Form data are: ' + JSON.stringify(model));
            this.paymentService.savePayment(model)
                .subscribe(
                    res => {
                        console.log(res);
                        if (res.success) {
                            this.toasterService.pop('success', 'Success!', 'Payment has been successfully saved.');
                            this.submitted = false;
                            setTimeout(() => {
                                this.router.navigate(['/payments/list']);
                            }, 2000);  // 5s
                        }
                    },
                    error => {
                        console.log(error);
                        this.toasterService.pop('error', 'Server Error!', error.error);
                    }
                );
        }
    }

    onCancel() {
            this.router.navigate(['/payments/list']);
    }

    public onChange(event): void {  // event will give you full breif of action
        const newVal = event.target.value;
        console.log(newVal);
        if(newVal==1){
            this.isshow=true;
        }
        else{
            this.isshow=false;
        }
       
      }

      public onPropertyChange(event): void {  // event will give you full breif of action
        const newVal = event.target.value;
        console.log('propertyid');
        console.log(newVal);
        this.assignedPropertyService.getAssignedProperties(this.page)
        .subscribe(x => {console.log(x.data);
            x.data.forEach((y: AssignedPropertyViewModel) => {
                // console.log('newval');
                // console.log(newVal);
                // console.log('propertyid');
                // console.log(y.propertyId);
                
                if(newVal==y.propertyId){
                    // console.log('rent');
                    // console.log(y.rent);
                    this.amount=y.rent;
                }                              
                
            });
        });
       
      }
      pushToArray(arr, obj) {
        const index = arr.findIndex((e) => e.id === obj.id);
    
        if (index === -1) {
            arr.push(obj);
        } else {
            arr[index] = obj;
        }
    }

      public onTenantChange(event): void {  // event will give you full breif of action
        this.properties=[];
        const newVal = event.target.value;
        console.log(newVal);
        this.assignedPropertyService.getAssignedProperties(this.page)
        .subscribe(x => {console.log(x.data);
            x.data.forEach((y: AssignedPropertyViewModel) => {
                console.log('propertyname');
                console.log(y.propertyName);    
                if(newVal==y.tenantId){
                    this.properties.push(new SelectItemModel(y.propertyName, y.propertyId));  
                } 
                         
                //this.pushToArray(this.properties,new SelectItemModel(y.propertyName, y.propertyId))                
                
            });
        });
        
      }

      public onOwnerChange(event): void {  // event will give you full breif of action
        this.properties=[];
        const newVal = event.target.value;
        console.log(newVal);
        this.propertyService.getProperties(this.page)
        .subscribe(x => {
            x.data.forEach((y: PropertyViewModel) => {
                console.log(y.propertyOwnerId);
                console.log(newVal);
                if(y.propertyOwnerId==newVal)
                {
                    this.properties.push(new SelectItemModel(y.name, y.propertyId));
                }
                
            });
        });
        
      }

}
