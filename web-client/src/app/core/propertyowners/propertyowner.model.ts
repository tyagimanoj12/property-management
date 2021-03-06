import { Page } from '../common/page.model';

export interface IPropertyOwnerPagedResource {
    data: PropertyOwnerViewModel[];
    page: Page;
}

export class PropertyOwnerViewModel {
    propertyOwnerId: string;
    name: string;
    email?: string;
    phone: string;
    address: string;    
}

export class CreatePropertyOwnerResponse {
    propertyOwnerId: string;
    success: boolean;
    message: string;
}


