namespace WebPackageTool
{
    using System;
    using System.Collections.Generic;

    public class AspnetWebPackager : WebPackager
    {
        public AspnetWebPackager(string _sourcePath, string _DestinationPath, DateTime _LastUpdateTime)
        {
            base.ExceptExtensions = new List<string>();
            base.ExceptExtensions.Add(".cs");
            base.ExceptExtensions.Add(".pdb");
            base.ExceptExtensions.Add(".config");
            base.ExceptExtensions.Add(".csproj");
            base.ExceptExtensions.Add(".user");
            base.ExceptExtensions.Add(".xml");
            base.ExceptExtensions.Add(".manifest");
            base.ExceptDirs = new List<string>();
            base.ExceptDirs.Add("obj");
            base.SourcePath = _sourcePath;
            base.DestinationPath = _DestinationPath;
            base.LastUpdateTime = _LastUpdateTime;
        }
    }
}

