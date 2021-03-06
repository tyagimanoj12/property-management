import { Page } from '../common/page.model';

export interface ITenantPagedResource {
    data: TenantViewModel[];
    page: Page;
}

export class TenantViewModel {
    tenantId: string;
    name: string;
    email?: string;
    phone: string;
    address: string;    
}

export class AssignedPropertyViewModel {
    assignedPropertyId: string;
    tenantId: string;
    propertyId: string;
    propertyName:string;
    rent: string;    
    formdata:any;
    dateFrom:string
    dateTo:string;
    rentStartDate:string;
    rentDocumentFilePath:string;
}

export class CreateTenantResponse {
    tenantId: string;
    success: boolean;
    message: string;
}


