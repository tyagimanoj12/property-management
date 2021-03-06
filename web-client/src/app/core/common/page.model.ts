/**
 * An object used to get page information from the server
 */
export class Page {
    // The number of elements in the page
    size = 20;
    // The total number of elements
    totalElements = 0;
    // The total number of pages
    totalPages = 0;
    // The current page number
    pageNumber = 0;
    // query
    query: string;
    // fileds;
    fields: string;
      // accountId
      accountId: string;
    // tenantId
    tenantId: string;
    // ownerId
    ownerId?: string;
     // propertyOwnerId
     propertyOwnerId?: string;


}
