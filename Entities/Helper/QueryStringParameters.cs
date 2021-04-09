using System;
namespace Entities.Helper
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 30;

        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            set => _pageSize = value > maxPageSize ? maxPageSize : value;

            get => _pageSize;
        }
    }
}
