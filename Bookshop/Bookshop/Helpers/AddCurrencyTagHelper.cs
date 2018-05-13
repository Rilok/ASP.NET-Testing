using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bookshop.Helpers
{
    [HtmlTargetElement(Attributes = CurrencyAttributeName)]
    public class AddCurrencyTagHelper : TagHelper
    {
        private const string CurrencyAttributeName = "asp-add-currency";

        [HtmlAttributeName(CurrencyAttributeName)]
        public ModelExpression Curr { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            var value = Curr.ModelExplorer.GetSimpleDisplayText();
            value = value + "ZL";

            output.Content.SetContent(value);
        }
    }
}
