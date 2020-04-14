using Microsoft.AspNetCore.Razor.TagHelpers;
using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Infrastructure.TagHelpers
{
    public class ServiceTagHelper : TagHelper
    {
        //TODO https://docs.microsoft.com/ru-ru/aspnet/core/mvc/views/tag-helpers/authoring?view=aspnetcore-3.1
        public Service Info { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.SetAttribute("class", "btn btn-lg btn-secondary dropdown-toggle");
            output.Attributes.SetAttribute("href", "#");
            output.Attributes.SetAttribute("role", "button");
            output.Attributes.SetAttribute("id", Info.Name);
            output.Attributes.SetAttribute("data-toggle", "dropdown");
            output.Attributes.SetAttribute("aria-haspopup", "true");
            output.Attributes.SetAttribute("aria-expanded", "false");

            AddDropdown(Info, output);
        }

        private void AddDropdown(Service service, TagHelperOutput output)
        {
            output.PostContent.AppendHtml($"<div class=\"dropdown-menu\" aria-labelledby=\"{service.Name}\">");

            AddDropdownItems(service.Instances, output);
            AddDivider(output);
            AddDropdownItems(service.EssentialLinks, output);
            if (service.ProblemList.Count > 0)
            {
                AddDivider(output);
                AddDropdownItems(service.ProblemList, output);
            }

            output.PostContent.AppendHtml("</div>");
        }

        private void AddDropdownItems(IEnumerable<string> list, TagHelperOutput output)
        {
            foreach (string item in list)
            {
                output.PostContent.AppendHtml($"<a class=\"dropdown-item\" href=\"{item}\">{item}</a>");
            }
        }

        private void AddDivider(TagHelperOutput output)
        {
            output.PostContent.AppendHtml("<div class=\"dropdown-divider\"></div>");
        }
    }
}