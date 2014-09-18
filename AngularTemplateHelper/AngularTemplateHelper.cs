using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace System.Web
{
    public static class AngularTemplateHelper
    {
        public static string RenderAngularTemplates(string folder)
        {
            //if we are using the app convention, we assume that the website root is up one level
            if ((new DirectoryInfo(folder)).Name.Equals("app",StringComparison.InvariantCultureIgnoreCase))
            {
                return RenderAngularTemplates(Directory.GetParent(folder).FullName,folder);
            }
            else // otherwise we assume that we are specifying a url that is the website root.
            {
                return RenderAngularTemplates(folder, folder);
            }
            
        }

        public static string RenderAngularTemplates(string serverRoot, string folder)
        {
            var output = new StringBuilder();
            if (serverRoot.EndsWith(@"\") || serverRoot.EndsWith("/"))
            {
                serverRoot = serverRoot.Substring(0, serverRoot.Length - 1);
            }
            foreach (var directory in Directory.GetDirectories(folder))
            {
                output.Append(RenderAngularTemplates(serverRoot,directory));
            }

            foreach (var file in Directory.GetFiles(folder,"*.html"))
            {
                output.AppendFormat("<script type=\"text/ng-template\" id=\"{0}\">{1}</script>\r\n", WebUtility.HtmlEncode(GetWebPathForFile(serverRoot,file)), File.ReadAllText(file));
                
            }

            return output.ToString();
            
        }

        public static string GetWebPathForFile(string serverRoot, string fileName)
        {
            DirectoryInfo serverRootInfo = new DirectoryInfo(serverRoot);

            List<string> folders = new List<string>();
            DirectoryInfo currentFolder=new DirectoryInfo(Path.GetDirectoryName(fileName));
            
            //keep bubbling till we hit the parent
            while (!serverRootInfo.FullName.Equals(currentFolder.FullName,StringComparison.InvariantCultureIgnoreCase))
            {
                folders.Add(currentFolder.Name);
                currentFolder = currentFolder.Parent;
            }

            StringBuilder url = new StringBuilder("/");
            //flip the order of the folders, as we start from the root when creating the url
            folders.Reverse();

            //build url
            foreach (var folder in folders)
            {
                url.AppendFormat("{0}/", folder);
            }

            //add file
            url.Append(Path.GetFileName(fileName));

            return url.ToString();
        }
    }
}
