import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { ToasterService, ToasterConfig } from 'angular2-toaster/angular2-toaster';
import * as moment from 'moment';
import { AdministratorService } from '../../../core/administrators/administrator.service';
import { SettingsService } from '../../../core/settings/settings.service';
import { Page } from '../../../core/common/page.model';
import { TenantService } from '../../../core/tenants/tenant.service';
import { TenantViewModel } from '../../../core/tenants/tenant.model';


//const swal = require('sweetalert');
@Component({
    selector: 'app-tenant',
    templateUrl: './tenant.component.html',
    styleUrls: ['./tenant.component.scss']
})

export class TenantComponent implements OnInit {

    isAdminLogin:boolean=false;
    is_edit: boolean = false;
    user: any = {};
    name = '';
    tenants: TenantViewModel;
    tenantId = '';
    submitted = false;
    toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public tenantForm: FormGroup;
    public tenantTypeForm: FormGroup;

    constructor(private tenantService: TenantService,
        private administratorService: AdministratorService,       
        private fb: FormBuilder,
        private toasterService: ToasterService,
        public settings: SettingsService,
        private router: Router,
        private route: ActivatedRoute) {
        this.tenants = new TenantViewModel();   
        this.tenantId = this.route.snapshot.params['id'];       
    }

    ngOnInit() {    
        // this.route.paramMap.subscribe(
        //     parameterMap => {
        //         this.tenantId = parameterMap.get('tenantId');
        //     });

        const page = new Page();
        page.size = 9999;

        console.log(this.tenantId);
        if (this.tenantId) {
            this.tenantForm = this.fb.group({
                tenantId: [''],            
                name : ['', [Validators.required]],
                email : ['', [Validators.required]],
                phone : ['', [Validators.required]],
                address : ['', [Validators.required]],
            });
            this.tenantService.getTenant(this.tenantId)
                .subscribe(data => {    
                    this.tenantForm.setValue(data);
                    console.log(data);
                }
                );
        }
        else {
            this.tenantForm = this.fb.group({
                tenantId: [''],            
                name : ['', [Validators.required]],
                email : ['', [Validators.required]],
                phone : ['', [Validators.required]],
                address : ['', [Validators.required]],
            });
        }      
    }
    save(model: any, isValid: boolean, e: any) {
        console.log(model);
        this.submitted = true;
        e.preventDefault();
        if (isValid) {
            console.log('Form data are: ' + JSON.stringify(model));
            this.tenantService.saveTenant(model)
                .subscribe(
                    res => {
                        console.log(res);
                        if (res.success) {
                            this.toasterService.pop('success', 'Success!', 'Tenant has been successfully saved.');
                            this.submitted = false;
                            setTimeout(() => {
                                this.router.navigate(['/tenants/list']);
                            }, 2000);  //5s
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
            this.router.navigate(['/tenants/list']);
    }
}