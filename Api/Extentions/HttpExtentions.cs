using System.IO;
using System.Text;
using System.Threading.Tasks;
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

        public static async Task<string> GetRequestBodyAsync(this HttpRequest request,
                                             Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            var body = "";

            request.EnableBuffering();
            if (request.ContentLength == null || !(request.ContentLength > 0) || !request.Body.CanSeek) return body;

            request.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(request.Body, encoding, true, 1024, true))
                body = await reader.ReadToEndAsync();

            request.Body.Position = 0;
            return body;
        }
    }
}
