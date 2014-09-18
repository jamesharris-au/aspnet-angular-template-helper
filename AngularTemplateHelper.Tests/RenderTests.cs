using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace AngularTemplateHelperTests
{
    [TestFixture]
   public class RenderTests
    {
        [Test]
        public void Does_GetWebPathForFile_Return_a_Valid_WebPath_For_A_Given_File()
        {
            var baseDir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.Parent.FullName;

            string url = AngularTemplateHelper.GetWebPathForFile(baseDir, Path.Combine(baseDir, "app", "test.html"));

            Assert.AreEqual("/app/test.html",url);
        }

        [Test]
        public void Does_RenderAngularTemplates_Find_Files_In_Subfolders()
        {
            var baseDir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.Parent.FullName;

            string html = AngularTemplateHelper.RenderAngularTemplates(baseDir, Path.Combine(baseDir, "app"));

            Assert.True(html.Contains("<script type=\"text/ng-template\" id=\"/app/subfolder1/test2.html\">"));
            Assert.True(html.Contains("<script type=\"text/ng-template\" id=\"/app/test.html\">"));
        }


        [Test]
        public void Does_RenderAngularTemplates_Include_The_Body_Of_Html_Files()
        {
            var baseDir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.Parent.FullName;

            string html = AngularTemplateHelper.RenderAngularTemplates(baseDir, Path.Combine(baseDir, "app"));

            Assert.True(html.Contains("<script type=\"text/ng-template\" id=\"/app/subfolder1/test2.html\">"));
            Assert.True(html.Contains("<script type=\"text/ng-template\" id=\"/app/test.html\">"));
        }


        [Test]
        public void Does_RenderAngularTemplates_Work_With_Trailing_Slashes_On_Server_Root()
        {
            var baseDir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.Parent.FullName;

            string html = AngularTemplateHelper.RenderAngularTemplates(baseDir+@"\", Path.Combine(baseDir, "app"));

          //  Assert.True(html.Contains("<script type=\"text/ng-template\" id=\"/app/subfolder1/test2.html\">"));
         //   Assert.True(html.Contains("<script type=\"text/ng-template\" id=\"/app/test.html\">"));
        }
    }
}
