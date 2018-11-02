using Chilicki.Commline.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chilicki.Commline.UserInterface.Html_Extensions
{
    public static class HtmlEnumExtensions
    {
        public static MvcHtmlString EnumToString<T>(this HtmlHelper helper)
        {
            var values = Enum.GetValues(typeof(T)).Cast<int>();
            var enumDictionary = values.ToDictionary(value => Enum.GetName(typeof(T), value));
            var reversedEnumDictionary = enumDictionary.ReverseKeyValue();

            return new MvcHtmlString(JsonConvert.SerializeObject(reversedEnumDictionary));
        }
    }
}