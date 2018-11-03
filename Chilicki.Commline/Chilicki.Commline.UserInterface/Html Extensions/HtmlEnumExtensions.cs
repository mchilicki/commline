using Chilicki.Commline.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chilicki.Commline.UserInterface.HtmlExtensions
{
    public static class HtmlEnumExtensions
    {
        public static MvcHtmlString EnumToString<TEnum>(this HtmlHelper helper)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<int>();
            var enumDictionary = values.ToDictionary(value => Enum.GetName(typeof(TEnum), value));
            var reversedEnumDictionary = enumDictionary.ReverseKeyValue();

            return new MvcHtmlString(JsonConvert.SerializeObject(reversedEnumDictionary));
        }

        public static SelectList ToSelectList<TEnum>(this TEnum selectedValueOutput)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                            select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", selectedValueOutput);
        }
    }
}