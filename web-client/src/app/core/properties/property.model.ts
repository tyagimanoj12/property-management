import { Page } from '../common/page.model';

export interface IPropertyPagedResource {
    data: PropertyViewModel[];
    page: Page;
}

export class PropertyViewModel {
    propertyId: string;
    name: string;
    address?: string;
    rent: string;
    area: string;
    propertyOwnerId: string;
    propertyOwnerName:string;
}

export class CreatePropertyResponse {
    propertyId: string;
    success: boolean;
    message: string;
}
