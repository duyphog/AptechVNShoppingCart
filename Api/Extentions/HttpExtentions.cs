using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Api.Extentions
{
    public static class HttpExtentions
    {
        public static void AddPagination(this HttpResponse httpResponse, int totalItems, int pageSize, int currentPage, int totalPage, bool hasPrevious, bool hasNext)
        {
            var metadata = new
            {
                TotalItems = totalItems,
                PageSize = pageSize,
                CurrentPages = currentPage,
                TotalPages = totalPage,
                HasPrevious = hasPrevious,
                HasNext = hasNext
            };

            httpResponse.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            httpResponse.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
        }
    }
}
