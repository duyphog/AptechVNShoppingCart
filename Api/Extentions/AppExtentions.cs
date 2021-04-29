using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace Api.Extentions
{
    public static class AppExtentions
    {
        /// <summary>
        /// Determines whether this instance and another specified byte[] object have the same value.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="param"></param>
        /// <returns> true if euqals, false if not equals</returns>
        public static bool EqualsByteArray(this byte[] src, byte[] param)
        {
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] != param[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }
    }
}
