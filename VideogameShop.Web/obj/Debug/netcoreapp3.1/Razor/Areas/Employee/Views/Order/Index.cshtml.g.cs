#pragma checksum "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Order\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4e92e8c6d9ca177107f23a8fbc532c976a40eb9a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Employee_Views_Order_Index), @"mvc.1.0.view", @"/Areas/Employee/Views/Order/Index.cshtml")]
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
#line 1 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\_ViewImports.cshtml"
using VideogameShop.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\_ViewImports.cshtml"
using VideogameShop.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4e92e8c6d9ca177107f23a8fbc532c976a40eb9a", @"/Areas/Employee/Views/Order/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ebddd7e1913cc07bd5aaaf136a1334eda451d00e", @"/Areas/Employee/Views/_ViewImports.cshtml")]
    public class Areas_Employee_Views_Order_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Order\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<table class=""table table-bordered table-striped"">
    <tr>
        <th>Product Name</th>
        <th>Quantity</th>
        <th>Condition</th>
        <th>Date</th>
        <th>Total</th>
        <th>Customer Name</th>
        <th>Customer Phone</th>
        <th>Email</th>
    </tr>
");
#nullable restore
#line 16 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Order\Index.cshtml"
     for (int i = 0; i < Model.Rows.Count; i++)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n");
#nullable restore
#line 20 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Order\Index.cshtml"
             for (int j = 1; j < Model.Columns.Count; j++)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>");
#nullable restore
#line 22 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Order\Index.cshtml"
               Write(Model.Rows[i][j]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 23 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Order\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </tr>\r\n");
#nullable restore
#line 26 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Order\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</table>\r\n<button type=\"button\" class=\"btn btn-info\"");
            BeginWriteAttribute("href", " href=\"", 679, "\"", 715, 1);
#nullable restore
#line 29 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Order\Index.cshtml"
WriteAttributeValue("", 686, Url.Action("Upload","Order"), 686, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Update Sales from File</button>\r\n<a");
            BeginWriteAttribute("href", " href=\"", 752, "\"", 788, 1);
#nullable restore
#line 30 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Order\Index.cshtml"
WriteAttributeValue("", 759, Url.Action("Create","Order"), 759, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Add a new order</a>\r\n\r\n<hr />\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
