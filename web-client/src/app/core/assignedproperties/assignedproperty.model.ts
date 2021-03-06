import { Page } from '../common/page.model';

export interface IAssignedPropertyPagedResource {
    data: AssignedPropertyViewModel[];
    page: Page;
}

export class AssignedPropertyViewModel {
    assignedPropertyId: string;
    tenantId: string;
    propertyId: string;
    rent: string; 
    dateFrom: string; 
    dateTo: string; 
    rentStartDate:string;   
    formdata:any;
    rentDocumentFilePath:string;
    
}

export class CreateAssignedPropertyResponse {
    assignedPropertyId: string;
    success: boolean;
    message: string;
}