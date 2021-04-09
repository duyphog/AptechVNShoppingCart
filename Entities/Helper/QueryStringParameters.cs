using System;
namespace Entities.Helper
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
           set => _pageSize = value < 50 ? value : maxPageSize;

           get => _pageSize;
        }
    }
}
