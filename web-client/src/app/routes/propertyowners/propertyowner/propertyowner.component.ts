import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import {
  ToasterService,
  ToasterConfig
} from "angular2-toaster/angular2-toaster";
import { SettingsService } from "../../../core/settings/settings.service";
import { Page } from "../../../core/common/page.model";
import { PropertyOwnerService } from "../../../core/propertyowners/propertyowner.service";
import { PropertyOwnerViewModel } from "../../../core/propertyowners/propertyowner.model";

//const swal = require('sweetalert');
@Component({
  selector: "app-propertyowner",
  templateUrl: "./propertyowner.component.html",
  styleUrls: ["./propertyowner.component.scss"]
})
export class PropertyOwnerComponent implements OnInit {
  isAdminLogin: boolean = false;
  is_edit: boolean = false;
  user: any = {};
  name = "";
  page = new Page();
  propertyOwners: PropertyOwnerViewModel;
  propertyOwnerId = "";
  submitted = false;
  toasterconfig: ToasterConfig = new ToasterConfig({
    positionClass: "toast-top-right",
    showCloseButton: true
  });
  public propertyOwnerForm: FormGroup;

  constructor(
    private propertyOwnerService: PropertyOwnerService,
    private fb: FormBuilder,
    private toasterService: ToasterService,
    public settings: SettingsService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.propertyOwners = new PropertyOwnerViewModel();
    this.propertyOwnerId = this.route.snapshot.params["id"];
  }

  ngOnInit() {
    // this.route.paramMap.subscribe(
    //     parameterMap => {
    //         this.tenantId = parameterMap.get('tenantId');
    //     });

    const page = new Page();
    page.size = 9999;

    console.log(this.propertyOwnerId);
    if (this.propertyOwnerId) {
      this.propertyOwnerForm = this.fb.group({
        propertyOwnerId: [""],
        name: ["", [Validators.required]],
        email: ["", [Validators.required]],
        phone: ["", [Validators.required]],
        address: ["", [Validators.required]]
      });
      this.propertyOwnerService
        .getPropertyOwner(this.propertyOwnerId)
        .subscribe(data => {
          this.propertyOwnerForm.setValue(data);
          console.log(data);
        });
    } else {
      this.propertyOwnerForm = this.fb.group({
        propertyOwnerId: [""],
        name: ["", [Validators.required]],
        email: ["", [Validators.required]],
        phone: ["", [Validators.required]],
        address: ["", [Validators.required]]
      });
    }
  }

  save(model: any, isValid: boolean, e: any) {
    console.log(model);
    this.submitted = true;
    e.preventDefault();
    if (isValid) {
      console.log("Form data are: " + JSON.stringify(model));
      this.propertyOwnerService.savePropertyOwner(model).subscribe(
        res => {
          console.log(res);
          if (res.success) {
            this.toasterService.pop(
              "success",
              "Success!",
              "Property Owner has been successfully saved."
            );
            this.submitted = false;
            setTimeout(() => {
              this.router.navigate(["/propertyowners/list"]);
            }, 2000); //5s
          }
        },
        error => {
          console.log(error);
          this.toasterService.pop("error", "Server Error!", error.error);
        }
      );
    }
  }

  onCancel() {
    this.router.navigate(["/propertyowners/list"]);
  }
}
