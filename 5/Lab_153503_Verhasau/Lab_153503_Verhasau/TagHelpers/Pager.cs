using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lab_153503_Verhasau.TagHelpers;

[HtmlTargetElement("pager")]

public class PagerTagHelper : TagHelper
{

    private readonly LinkGenerator _linkGenerator;
    private readonly HttpContext _httpContext;

    public PagerTagHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {
        _linkGenerator = linkGenerator;
        _httpContext = httpContextAccessor.HttpContext!;
    }

    [HtmlAttributeName("current-page")]
    public int CurrentPage { get; set; }

    [HtmlAttributeName("total-pages")]
    public int TotalPages { get; set; }

    [HtmlAttributeName("category")]
    public string? Category { get; set; }

    [HtmlAttributeName("current-category")]
    public string? CurrentCategory { get; set; }

    [HtmlAttributeName("admin")]
    public bool Admin { get; set; }

    private RouteValueDictionary GetRouteValues(int pageNo)
    {
        RouteValueDictionary values;
        if (Admin)
        {
            values = new RouteValueDictionary
                {
                    { "pageNo", pageNo },
                };
        }
        else
        {
            values = new RouteValueDictionary
                {
                    { "category", Category },
                    { "currentCategory", CurrentCategory },
                    { "pageNumber", pageNo }
                };
        }

        return values;

    }

    private string? GetUrl(int pageNo)
    {
        return _linkGenerator.GetPathByPage(_httpContext, values: GetRouteValues(pageNo));
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "nav";
        output.Attributes.SetAttribute("class", "col-sm-4 offset-2");

        var ulTag = new TagBuilder("ul");
        ulTag.AddCssClass("pagination");

        var previousAvailable = CurrentPage > 0;
        var nextAvailable = CurrentPage < TotalPages - 1;

        var previousLiTag = new TagBuilder("li");
        previousLiTag.AddCssClass(previousAvailable ? "page-item" : "page-item disabled");

        var previousLink = new TagBuilder("a");
        previousLink.AddCssClass("page-link");
        previousLink.Attributes["aria-label"] = "Previous";

        if (previousAvailable)
        {
            var previousUrl = GetUrl(CurrentPage - 1);
            previousLink.Attributes["href"] = previousUrl;
        }

        var previousSpan = new TagBuilder("span");
        previousSpan.InnerHtml.Append("\u00AB");

        previousLink.InnerHtml.AppendHtml(previousSpan);
        previousLiTag.InnerHtml.AppendHtml(previousLink);
        ulTag.InnerHtml.AppendHtml(previousLiTag);

        for (int i = 0; i < TotalPages; i++)
        {
            var liTag = new TagBuilder("li");
            liTag.AddCssClass("page-item");
            if (CurrentPage == i)
            {
                liTag.AddCssClass("active");
            }

            var link = new TagBuilder("a");

            link.AddCssClass("page-link");

            var url = GetUrl(i);
            link.Attributes["href"] = url;
            link.InnerHtml.Append((i + 1).ToString());

            liTag.InnerHtml.AppendHtml(link);

            ulTag.InnerHtml.AppendHtml(liTag);

        }

        var nextLiTag = new TagBuilder("li");
        nextLiTag.AddCssClass(nextAvailable ? "page-item" : "page-item disabled");

        var nextLink = new TagBuilder("a");
        nextLink.AddCssClass("page-link");
        nextLink.Attributes["aria-label"] = "Next";

        if (nextAvailable)
        {
            var nextUrl = GetUrl(CurrentPage + 1);
            nextLink.Attributes["href"] = nextUrl;
        }

        var nextSpan = new TagBuilder("span");
        nextSpan.InnerHtml.Append("\u00BB");
        nextLink.InnerHtml.AppendHtml(nextSpan);
        nextLiTag.InnerHtml.AppendHtml(nextLink);

        ulTag.InnerHtml.AppendHtml(nextLiTag);

        output.Content.AppendHtml(ulTag);

    }
}
