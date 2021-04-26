using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Enum
{
    public static class HelperExtension
    {
        public static string AsDescription(this System.Enum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var result = attributes.Length > 0 ? attributes[0].Description : value.ToString();
                return result;
            }
            catch
            {
                return value.ToString();
            }
        }
    }
}
