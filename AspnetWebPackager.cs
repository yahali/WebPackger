using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPackageTool
{
    public class AspnetWebPackager : WebPackager
    {
        public AspnetWebPackager(string _sourcePath, string _DestinationPath, DateTime _LastUpdateTime)
        {
            //排除的扩展名
            ExceptExtensions = new List<string>();
            ExceptExtensions.Add(".cs");
            ExceptExtensions.Add(".pdb");
            ExceptExtensions.Add(".config");
            ExceptExtensions.Add(".csproj");
            ExceptExtensions.Add(".user");
            ExceptExtensions.Add(".xml");
            ExceptExtensions.Add(".manifest");

            //排除的目录
            ExceptDirs = new List<string>();
            ExceptDirs.Add("obj");

            this.SourcePath = _sourcePath;
            this.DestinationPath = _DestinationPath;
            this.LastUpdateTime = _LastUpdateTime;


            
            /*ExceptExtensions.Add("");
            ExceptExtensions.Add("");
            ExceptExtensions.Add("");
            ExceptExtensions.Add("");
            ExceptExtensions.Add("");
            ExceptExtensions.Add("");
            ExceptExtensions.Add("");
            ExceptExtensions.Add("");
            */

        }

    }
}
