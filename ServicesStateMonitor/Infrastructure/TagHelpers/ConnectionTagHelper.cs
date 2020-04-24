using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ServicesStateMonitor.Infrastructure.TagHelpers
{
    public class ConnectionTagHelper : TagHelper
    {
        public string Start { get; set; }

        public string End { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "line";
            string lineId = Start + End;
            string pointerId = Start + End + "Pointer";
            output.Attributes.SetAttribute("id", lineId);
            output.Attributes.SetAttribute("stroke", "black");

            output.PostElement.AppendHtml($"<polygon id=\"{pointerId}\" fill=\"black\" />");
        }
    }
}