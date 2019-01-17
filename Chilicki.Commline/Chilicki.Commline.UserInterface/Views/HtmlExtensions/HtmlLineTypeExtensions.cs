using Chilicki.Commline.Common.Extensions;
using Chilicki.Commline.Domain.Enums;
using Chilicki.Commline.Domain.Enums.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Chilicki.Commline.UserInterface.Views.HtmlExtensions
{
    public static class HtmlLineTypeExtensions
    {       

        public static SelectList ToSelectList(this LineType selectedValueOutput)
        {
            var values = from LineType e in Enum.GetValues(typeof(LineType))
                         select new { Id = (int)e, Name = e.GetDescription() };
            return new SelectList(values, "Id", "Name", selectedValueOutput);
        }

        public static IHtmlContent LineTypeToString(this IHtmlHelper helper)
        {
            var enumValues = Enum.GetValues(typeof(LineType)).Cast<LineType>();
            var enumDictionary = enumValues.ToDictionary(value => value.GetDescription());
            var reversedEnumDictionary = enumDictionary.ReverseKeyValue();
            var descriptions = reversedEnumDictionary.Select(p => p.Value);
            var intEnumValues = Enum.GetValues(typeof(LineType)).Cast<int>();
            var composedEnumDictionary = intEnumValues.Zip(descriptions, (key, value) => new { key, value })
                .ToDictionary(x => x.key, x => x.value);

            return new HtmlString(JsonConvert.SerializeObject(composedEnumDictionary));
        }
    }
}