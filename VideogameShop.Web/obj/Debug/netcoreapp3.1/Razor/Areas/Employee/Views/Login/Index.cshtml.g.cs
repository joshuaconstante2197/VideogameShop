#pragma checksum "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "15f336f52b4760fa5b90c9dbeb707b08b88e2a1d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Employee_Views_Login_Index), @"mvc.1.0.view", @"/Areas/Employee/Views/Login/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
using VideogameShop.Library.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"15f336f52b4760fa5b90c9dbeb707b08b88e2a1d", @"/Areas/Employee/Views/Login/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ebddd7e1913cc07bd5aaaf136a1334eda451d00e", @"/Areas/Employee/Views/_ViewImports.cshtml")]
    public class Areas_Employee_Views_Login_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LoginInfo>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
  
    ViewBag.Title = "Log in";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>");
#nullable restore
#line 7 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
Write(ViewBag.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(".</h2>\r\n<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n        <section id=\"loginForm\">\r\n");
#nullable restore
#line 11 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
             using (Html.BeginForm("Index", "Login", FormMethod.Post, new { ReturnUrl = ViewBag.ReturnUrl }))
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
           Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h4>Use a local account to log in.</h4>\r\n                <hr />\r\n");
#nullable restore
#line 16 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
           Write(Html.ValidationSummary(true, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"form-group\">\r\n                    ");
#nullable restore
#line 18 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
               Write(Html.LabelFor(m => m.UserName, new { @class = "col-md-2 control-label" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    <div class=\"col-md-10\">\r\n                        ");
#nullable restore
#line 20 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
                   Write(Html.TextBoxFor(m => m.UserName, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
#nullable restore
#line 21 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
                   Write(Html.ValidationMessageFor(m => m.UserName, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n                <div class=\"form-group\">\r\n                    ");
#nullable restore
#line 25 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
               Write(Html.LabelFor(m => m.Password, new { @class = "col-md-2 control-label" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    <div class=\"col-md-10\">\r\n                        ");
#nullable restore
#line 27 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
                   Write(Html.PasswordFor(m => m.Password, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
#nullable restore
#line 28 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
                   Write(Html.ValidationMessageFor(m => m.Password, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    </div>
                </div>
                <div class=""form-group"">
                    <div class=""col-md-offset-2 col-md-10"">
                        <input type=""submit"" value=""Log in"" class=""btn btn-default"" />
                    </div>
                </div>
");
#nullable restore
#line 36 "C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Web\Areas\Employee\Views\Login\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </section>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LoginInfo> Html { get; private set; }
    }
}
#pragma warning restore 1591
