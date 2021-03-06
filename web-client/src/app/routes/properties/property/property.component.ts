import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { ToasterService, ToasterConfig } from 'angular2-toaster/angular2-toaster';
import * as moment from 'moment';
import { AdministratorService } from '../../../core/administrators/administrator.service';
import { SettingsService } from '../../../core/settings/settings.service';
import { Page } from '../../../core/common/page.model';
import { PropertyService } from '../../../core/properties/property.service';
import { PropertyViewModel } from '../../../core/properties/property.model';
import { LoginResultModel } from '../../../core/administrators/administrator.model';
import { PropertyOwnerService } from '../../../core/propertyowners/propertyowner.service';
import { PropertyOwnerViewModel } from '../../../core/propertyowners/propertyowner.model';
import { SelectItemModel } from '../../../core/common/select-item.model';

//const swal = require('sweetalert');
@Component({
    selector: 'app-property',
    templateUrl: './property.component.html',
    styleUrls: ['./property.component.scss']
})

export class PropertyComponent implements OnInit {
    sAdminLogin:boolean=false;
    is_edit: boolean = false;
    user: any = {};
    name = '';
    page = new Page();
    properties: PropertyViewModel;
    propertyOwners: SelectItemModel[] = [];
    propertyId = '';
    submitted = false;
    ownerId:string='';
    toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public propertyForm: FormGroup;
    public propertyTypeForm: FormGroup;

    constructor(private propertyService: PropertyService, 
        private propertyOwnerService:PropertyOwnerService,      
        private fb: FormBuilder,
        private toasterService: ToasterService,
        public settings: SettingsService,
        private router: Router,
        private route: ActivatedRoute) {
        this.properties = new PropertyViewModel();         
        const currentUser = JSON.parse(localStorage.getItem('currentUser')) as LoginResultModel; 
        console.log(currentUser);
        this.ownerId=currentUser.ownerId;
        this.page.pageNumber = 0;

        this.propertyOwnerService.getPropertyOwners(this.page)
        .subscribe(x => {
            x.data.forEach((y: PropertyOwnerViewModel) => {
                this.propertyOwners.push(new SelectItemModel(y.name, y.propertyOwnerId));
            });
        });
    }

    ngOnInit() {    
        this.route.paramMap.subscribe(
            parameterMap => {
                this.propertyId = parameterMap.get('id');
            });

        const page = new Page();
        page.size = 9999;

        if (this.propertyId) {
            this.propertyForm = this.fb.group({
                propertyId: [''],            
                name : ['', [Validators.required]],
                address : ['', [Validators.required]],
                rent : ['', [Validators.required]],
                area : ['', [Validators.required]],
                propertyOwnerId : ['', [Validators.required]],
                propertyOwnerName: ['']
            });
            this.propertyService.getProperty(this.propertyId)
                .subscribe(data => {    
                    console.log(data);
                    this.propertyForm.setValue(data);
                }
                );
        }
        else {
            this.propertyForm = this.fb.group({
                propertyId: [''],            
                name : ['', [Validators.required]],
                address : ['', [Validators.required]],
                rent : ['', [Validators.required]],
                area : ['', [Validators.required]],
                propertyOwnerId : ['', [Validators.required]],
                propertyOwnerName: ['']
            });
        }      
    }

    save(model: any, isValid: boolean, e: any) {
        console.log(model);
        this.submitted = true;
        e.preventDefault();
        if (isValid) {
            console.log('Form data are: ' + JSON.stringify(model));
            this.propertyService.saveProperty(model)
                .subscribe(
                    res => {
                        console.log(res);
                        if (res.success) {
                            this.toasterService.pop('success', 'Success!', 'Property has been successfully saved.');
                            this.submitted = false;
                            setTimeout(() => {
                                this.router.navigate(['/properties/list']);
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
            this.router.navigate(['/properties/list']);
    }

}