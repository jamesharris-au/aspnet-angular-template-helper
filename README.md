aspnet-angular-template-helper
==============================

Renders angularjs templates as inline html inside your views and web pages.

Keep your project and code clean by keeping your templates in individual files, without the performance penalty and punishing users and requiring 100s of HTTP Requests for tiny html templates.

No dependencies on System.Web or MVC.

Build in your own caching logic using the generated html to improve performance.

NuGet
====
Install-Package Angular.TemplateRender

Usage
=====

Controller or General Code

*AngularTemplateHelper.RenderAngularTemplates(Server.MapPath("~/app"));*

Razor MVC Views

*@Html.Raw(AngularTemplateHelper.RenderAngularTemplates(Server.MapPath("~/app")))*

Output:

*&lt;script type="text/ng-template" id="/app/subfolder1/test2.html">some html&lt;/script>*

*&lt;script type="text/ng-template" id="/app/subfolder1/test3.html">&lt;span>more html&lt;/span>&lt;/script>*

Notes
=====

If your using a path other than /app (default convention) to store your html files, you'll need to specify the path of the "server root", so it knows what to build urls relative to.

Example:
*@Html.Raw(AngularTemplateHelper.RenderAngularTemplates(Server.MapPath("~"),Server.MapPath("~/app")))*


(Yes the Server.MapPath and non-overloading of Html Helper is a pain, but that's the price of entry for taking no dependencies - feel free to fork).
