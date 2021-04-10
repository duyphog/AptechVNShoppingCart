using System;
namespace Api.Models
{
    public class AppResponse<T>
    {
        public AppResponse(T data)
        {
            //Ok = success;
            Data = data;
        }

        //public bool Ok { get; set; }
            
        public T Data { get; set; }
    }
}
