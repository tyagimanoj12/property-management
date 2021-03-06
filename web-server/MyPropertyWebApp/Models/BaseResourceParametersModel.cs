using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProperty.Web.Models
{
    public abstract class BaseResourceParametersModel
    {
        const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }

        private int _pageSize = 2; // TODO : make it to 10

        public string SearchQuery { get; set; }

        public string Fields { get; set; }

        public Guid? OwnerId { get; set; }

        public Guid? PropertyOwnerId { get; set; }

        public Guid? TenantId { get; set; }
    }

    public class Page
    {
        //The number of rows in the page
        public int Size { get; set; } = 1;

        //The total number of rows
        public int TotalElements { get; set; } = 1;

        //The total number of pages
        public int TotalPages { get; set; } = 1;

        //The current page number
        public int PageNumber { get; set; } = 1;
    }
}
