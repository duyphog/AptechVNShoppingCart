using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Api.Helpers
{
    public static class Utils
    {
        public enum Gender
        {
            Unknown = -1,
            Male = 0,
            Female = 1
        }

        public static List<Object> ConvertEnumToJObject(Type e)
        {

            var lst = new List<Object>();
            foreach (var val in Enum.GetValues(e))
            {
                lst.Add(new
                {
                    id = ((int)val).ToString(),
                    value = Enum.GetName(e, val)
                });
            }

            return lst;
        }
    }
}
