import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { ToasterService, ToasterConfig } from 'angular2-toaster/angular2-toaster';
import * as moment from 'moment';
import { AdministratorService } from '../../../core/administrators/administrator.service';
import { SettingsService } from '../../../core/settings/settings.service';
import { Page } from '../../../core/common/page.model';
import { AssignedPropertyService } from '../../../core/assignedproperties/assignedproperty.service';
import { AssignedPropertyViewModel } from '../../../core/assignedproperties/assignedproperty.model';
import { LoginResultModel } from '../../../core/administrators/administrator.model';

//const swal = require('sweetalert');
@Component({
    selector: 'app-assignedproperty',
    templateUrl: './assignedproperty.component.html',
    styleUrls: ['./assignedproperty.component.scss']
})

export class AssignedPropertyComponent implements OnInit {
   
    sAdminLogin:boolean=false;
    is_edit: boolean = false;
    user: any = {};
    name = '';
    assignedproperties: AssignedPropertyViewModel;
    assignedPropertyId = '';
    submitted = false;
    ownerId:string='';
    toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public assignedPropertyForm: FormGroup;
    public assignedPropertyTypeForm: FormGroup;

    constructor(private assignedPropertyService: AssignedPropertyService,       
        private fb: FormBuilder,
        private toasterService: ToasterService,
        public settings: SettingsService,
        private router: Router,
        private route: ActivatedRoute) {
        this.assignedproperties = new AssignedPropertyViewModel();         
        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel; 
        console.log(currentUser);
        this.ownerId=currentUser.ownerId;
    }

    ngOnInit() {    
        this.route.paramMap.subscribe(
            parameterMap => {
                this.assignedPropertyId = parameterMap.get('id');
            });

        const page = new Page();
        page.size = 9999;

        if (this.assignedPropertyId) {
            this.assignedPropertyForm = this.fb.group({
                propertyId: [''],            
                name : ['', [Validators.required]],
                address : ['', [Validators.required]],
                rent : ['', [Validators.required]],
                area : ['', [Validators.required]],
                ownerId:this.ownerId
            });
            this.assignedPropertyService.getAssignedProperty(this.assignedPropertyId)
                .subscribe(data => {    
                    this.assignedPropertyForm.setValue(data);
                }
                );
        }
        else {
            this.assignedPropertyForm = this.fb.group({
                propertyId: [''],            
                name : ['', [Validators.required]],
                address : ['', [Validators.required]],
                rent : ['', [Validators.required]],
                area : ['', [Validators.required]],
                ownerId:this.ownerId
            });
        }      
    }

    save(model: any, isValid: boolean, e: any) {
        console.log(model);
        this.submitted = true;
        e.preventDefault();
        if (isValid) {
            console.log('Form data are: ' + JSON.stringify(model));
            this.assignedPropertyService.saveAssignedProperty(model)
                .subscribe(
                    res => {
                        console.log(res);
                        if (res.success) {
                            this.toasterService.pop('success', 'Success!', 'Property has been successfully saved.');
                            this.submitted = false;
                            setTimeout(() => {
                                this.router.navigate(['/asssigned-properties/list']);
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
            this.router.navigate(['/assigned-properties/list']);
    }

}