#pragma checksum "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "02473dc4d9133a97bbb41c3c57d98a261589d2a6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_AdminUser_Views_Review_List), @"mvc.1.0.view", @"/Areas/AdminUser/Views/Review/List.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\_ViewImports.cshtml"
using FinalProjectGamesWebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\_ViewImports.cshtml"
using FinalProjectGamesWebApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02473dc4d9133a97bbb41c3c57d98a261589d2a6", @"/Areas/AdminUser/Views/Review/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"413f368e5dfebbeb6a91a0f912fa53c4b2c5a2be", @"/Areas/AdminUser/Views/_ViewImports.cshtml")]
    public class Areas_AdminUser_Views_Review_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Review>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Add", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "View", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Review", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
  
    ViewBag.Title = "List";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
 if (ViewBag.SortOrder == null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <style>\r\n        a.sort-user {\r\n            color: dodgerblue;\r\n        }\r\n    </style>\r\n");
#nullable restore
#line 13 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
}
else if (ViewBag.SortOrder == "user_desc")
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <style>\r\n        a.sort-user {\r\n            color: red;\r\n        }\r\n    </style>\r\n");
#nullable restore
#line 21 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <style>\r\n        a.sort-user {\r\n            color: black;\r\n        }\r\n    </style>\r\n");
#nullable restore
#line 29 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
}

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
 if (ViewBag.SortOrder == "rating")
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <style>\r\n        a.sort-rating {\r\n            color: dodgerblue;\r\n        }\r\n    </style>\r\n");
#nullable restore
#line 37 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
}
else if (ViewBag.SortOrder == "rating_desc")
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <style>\r\n        a.sort-rating {\r\n            color: red;\r\n        }\r\n    </style>\r\n");
#nullable restore
#line 45 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <style>\r\n        a.sort-rating {\r\n            color: black;\r\n        }\r\n    </style>\r\n");
#nullable restore
#line 53 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>");
#nullable restore
#line 55 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
Write(ViewBag.GameTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Reviews</h2>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "02473dc4d9133a97bbb41c3c57d98a261589d2a67778", async() => {
                WriteLiteral("Add Review");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<table class=\"table table-boardered table-striped\">\r\n    <thead>\r\n        <tr>\r\n            <th>");
#nullable restore
#line 61 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
           Write(Html.ActionLink("User", "List", new { id = ViewBag.GameId, sortOrder = ViewBag.UserSortParm }, new { @class = "sort-user" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th>");
#nullable restore
#line 62 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
           Write(Html.ActionLink("Rating", "List", new { id = ViewBag.GameId, sortOrder = ViewBag.RatingSortParm }, new { @class = "sort-rating" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th>Review Content</th>\r\n            <th> </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 68 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
         foreach (Review r in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 71 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
               Write(r.User.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 72 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
               Write(r.Rating);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 73 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
               Write(r.ReviewContent);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "02473dc4d9133a97bbb41c3c57d98a261589d2a611381", async() => {
                WriteLiteral("View");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 75 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
                                                                   WriteLiteral(r.ReviewId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 78 "C:\Users\Snipe\OneDrive\Documents\GitHub\FinalProjectAdvCSharp\FinalProjectGamesWebApp\FinalProjectGamesWebApp\Areas\AdminUser\Views\Review\List.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Review>> Html { get; private set; }
    }
}
#pragma warning restore 1591
