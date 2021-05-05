using System;
namespace Entities.Helpers
{
    public abstract class QueryStringParameters
    {
        private const int maxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
            get => _pageSize;
        }

        public int? StatusType { set; get; }
    }
}
