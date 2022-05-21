using BooksPlace.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageInfo PageModel { get; set; } 
        public string ActionMethod { get; set; }

        //css properties
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            
            TagBuilder divElement = new TagBuilder("div");
            for (int i=1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder anchorElement = new TagBuilder("a");
                anchorElement.Attributes["href"] = urlHelper.Action(ActionMethod, new { pageNumber = i });
                anchorElement.InnerHtml.Append(i.ToString());

                if(PageClassesEnabled)
                {
                    anchorElement.AddCssClass(PageClass);
                    if(PageModel.PageNumber == i)
                    {
                        anchorElement.AddCssClass(PageClassSelected);
                    }
                }

                divElement.InnerHtml.AppendHtml(anchorElement);
            }

            output.Content.SetHtmlContent(divElement);
        }
    }
}
