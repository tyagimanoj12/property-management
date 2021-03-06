import {
    Component,
    OnInit,
    ViewEncapsulation,
    ViewChild,
    Input,
    Output,
    EventEmitter
  } from "@angular/core";
  import { DatatableComponent } from "@swimlane/ngx-datatable";
  import { Page } from "../../../core/common/page.model";
  import { Router, ActivatedRoute } from "@angular/router";
  import { ToasterService, ToasterConfig } from "angular2-toaster";
  import { AssignedPropertyViewModel } from "../../../core/assignedproperties/assignedproperty.model";
  import { AssignedPropertyService } from "../../../core/assignedproperties/assignedproperty.service";
  import { LoginResultModel } from "../../../core/administrators/administrator.model";
  import { SelectItemModel } from "../../../core/common/select-item.model";
  import { PropertyViewModel } from "../../../core/properties/property.model";
  import { PropertyService } from "../../../core/properties/property.service";
  import {FormBuilder,
    FormGroup,
    Validators,
    FormArray,
    FormControl,
    FormArrayName,
    Form
  } from "@angular/forms";
  import { HttpEventType } from "@angular/common/http";
  import { DatePipe } from '@angular/common';
  
  const swal = require("sweetalert");
  const _clone = d => JSON.parse(JSON.stringify(d));
  
  @Component({
    selector: "app-assignedpropertyhistory",
    templateUrl: "./assignedpropertyhistory.component.html",
    styleUrls: ["./assignedpropertyhistory.component.scss"],
    encapsulation: ViewEncapsulation.None,
    providers: [DatePipe]
  })
  export class AssignedPropertyHistoryComponent implements OnInit {
    public progress: number;
    public message: string;
    @Output() public onUploadFinished = new EventEmitter();
    dateTo: any;
    rentStartDate: any;
    dateFrom: any;
  
    propertyName: string = "";
    ownerName: string = "";
    area: string = "";
    rent: string = "";
    address: string = "";
  
    rentDocumentFilePath:string="";
  
    page = new Page();
    rows = new Array<AssignedPropertyViewModel>();
    loader = false;
    timeout: any;
    submitted = false;
    tenantId: string = "";
    assignedPropertyId: string = "";
    propertyId = "";
    properties: SelectItemModel[] = [];
    ownerId = "";
    toasterconfig: ToasterConfig = new ToasterConfig({
      positionClass: "toast-top-right",
      showCloseButton: true
    });
    columns = [
      { prop: "id", name: "Action" },
      { prop: "Name", name: "name" }
      // { prop: 'settingValue', name: 'settingValue' }
    ];
    columnsSort = [
      { prop: "Name", name: "name" }
      // { prop: 'settingValue', name: 'settingValue' }
    ];
  
    @ViewChild(DatatableComponent) table: DatatableComponent;
    @ViewChild("myTable") tableExp: any;
  
    public assignPropertyForm: FormGroup;
  
    constructor(
      private fb: FormBuilder,
      private assignedPropertyService: AssignedPropertyService,
      private propertyService: PropertyService,
      private router: Router,
      private route: ActivatedRoute,
      private datePipe : DatePipe,
      private toasterService: ToasterService
    ) {
      this.page.pageNumber = 0;
  
      const currentUser = JSON.parse(
        localStorage.getItem("currentUser")
      ) as LoginResultModel;
      console.log(currentUser);
      this.ownerId = currentUser.ownerId;
      this.page.ownerId = this.ownerId;
  
      this.propertyService.getProperties(this.page).subscribe(x => {
        x.data.forEach((y: PropertyViewModel) => {
          this.properties.push(new SelectItemModel(y.name, y.propertyId));
        });
      });
      
    }
    
  
  
    onRowClick(event) {
      console.log(event);
      console.log(event.tenantId);
      console.log(event.propertyId);
      this.tenantId = event.tenantId;
      this.propertyId = event.propertyId;
      this.assignedPropertyId = event.assignedPropertyId;
    }
  
    resetModal(){
        this.assignPropertyForm.reset();
    }
    
    onPage(event) {
      clearTimeout(this.timeout);
      this.timeout = setTimeout(() => {
        console.log("paged!", event);
      }, 100);
    }
    toggleExpandRow(row) {
      console.log("Toggled Expand Row!", row);
      this.tableExp.rowDetail.toggleExpandRow(row);
    }
  
    onDetailToggle(event) {
      console.log("Detail Toggled", event);
    }
  
    cacheData(data) {
      this.rows = _clone(data);
    }
  
    setPage(pageInfo) {
      this.getData(pageInfo);
    }
  
    getData(pageInfo: any, query?: string) {
      this.page.pageNumber = pageInfo.offset;
      this.page.query = query ? query : "";
      this.loader = true;
      this.assignedPropertyService
        .getAssignedProperties(this.page)
        .subscribe(result => {
          console.log(result);
          this.rows = result.data;
          console.log(result.data);
          console.log(this.rows);
          console.log(this.propertyId);
          this.rows=this.rows.filter(x=>x.propertyId==this.propertyId);
          
          this.page = result.page;
          this.page.pageNumber = this.page.pageNumber - 1;
          //this.cacheData(result.data);
          this.loader = false;
        });
    }
  
    updateValue(event, cell, rowIndex) {
      console.log("inline editing rowIndex", rowIndex);
      // this.editing[rowIndex + '-' + cell] = false;
      this.rows[rowIndex][cell] = event.target.value;
      this.rows = [...this.rows];
      console.log("UPDATED!", this.rows[rowIndex][cell]);
    }
  
    ngOnInit() {
      this.setPage({ offset: 0 });
  
      this.propertyId =  this.route.snapshot.params['id'];  
  
      const page = new Page();
      page.size = 9999;
      console.log("id " + this.assignedPropertyId);
    }
  
    updateFilter(event) {
      const query = event.target.value.toLowerCase();
      this.getData({ offset: 0 }, query);
    }
  
    // Selection
    onSelect({ selected }) {}
  
    navigateToAddProperty() {
      this.router.navigate(["/assigned-properties/create"]);
    }
  
    onActivate(event) {
      console.log("Activate Event", event);
    }
    onEdit(event) {
      console.log(event.propertyId);
      this.router.navigate([
        "/assigned-properties/edit",
        event.assignedPropertyId
      ]);
    }
  
    public onChange(event): void {
      // event will give you full breif of action
      const newVal = event.target.value;
      console.log(newVal);
      this.propertyService.getProperty(newVal).subscribe(data => {
        console.log(data);
        this.propertyName = data.name;
        this.address = data.address;
        this.area = data.area;
        this.rent = data.rent;
        this.ownerName = data.propertyOwnerName;
      });
    }
  
    confirmDelete(row) {
      swal(
        {
          title: "Are you sure you want to delete this property?",
          text: "This setting will be deleted!",
          type: "warning",
          showCancelButton: true,
          confirmButtonColor: "#DD6B55",
          confirmButtonText: "Yes, delete it!",
          closeOnConfirm: false
        },
        () => {
          console.log(row);
          this.assignedPropertyService
            .deleteAssignedProperty(row.assignedPropertyId)
            .subscribe(
              result => {
                if (result.success) {
                  swal(
                    "Deleted!",
                    "Property has been successfully deleted.",
                    "success"
                  );
                  this.setPage({ offset: 0 });
                } else {
                  swal("Error", result.message, "error");
                }
              },
              error => {
                swal("Error", error.error, "error");
              }
            );
        }
      );
    }
  
    public uploadFile = files => {
      if (files.length === 0) {
        return;
      }
  
      let fileToUpload = <File>files[0];
      const formData = new FormData();
      formData.append("file", fileToUpload, fileToUpload.name);
  
      this.assignedPropertyService.upload(formData).subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round((100 * event.loaded) / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = "Upload success.";
          this.onUploadFinished.emit(event.body);
          console.log(event.body);
          this.rentDocumentFilePath="";
        }
      });
    };
  
    submitFormAssignProperty($ev, model: AssignedPropertyViewModel) {
      $ev.preventDefault();
      this.submitted = true;
  
      console.log("newmodel" + model);
  
      console.log(model);
      model.tenantId = this.tenantId;
      model.assignedPropertyId = this.assignedPropertyId;
  model.rentDocumentFilePath=this.rentDocumentFilePath;
      console.log(model);
  
      this.assignedPropertyService.saveAssignedProperty(model).subscribe(
        res => {
          if (res.success) {
            this.toasterService.pop(
              "success",
              "This record has been successfully send"
            );
            // this.submitted = false;
            const element = document.getElementById("CloseTryButton") as any;
            element.click();
          }
        },
        error => {
          console.log(error);
          this.loader = false;
          this.toasterService.pop("error", "Server Error!", error.error);
        }
      );
    }
  
   
    
  }
  