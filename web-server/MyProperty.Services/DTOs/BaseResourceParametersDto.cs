using System;

namespace MyProperty.Services.DTOs
{
    public abstract class BaseResourceParametersDto
    {
        private const int maxPageSize = 20;
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
}
