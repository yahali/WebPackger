namespace WebPackageTool
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class WebPackager
    {

        public WebPackager()
        {
            this.ExceptFiles = new List<string>();
            this.ExceptDirs = new List<string>();
            this.ExceptExtensions = new List<string>();
            this.AllFolders = new List<string>();
            this.LastUpdateTime = DateTime.Now.Date.AddDays(-3.0);
            this.WebId = "";
        }

        public void CopyFiles(string srcDir, string destDir)
        {
            if (!string.IsNullOrEmpty(srcDir) && !string.IsNullOrEmpty(destDir))
            {
                string sourcePath = this.SourcePath;
                string[] directories = Directory.GetDirectories(srcDir);
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }
                foreach (string str2 in directories)
                {
                    string tmp;
                    if ((string.IsNullOrEmpty(this.WebId) || !srcDir.EndsWith(sourcePath + @"\files")) || str2.EndsWith(sourcePath + @"\files\" + this.WebId))
                    {
                        if (!string.IsNullOrEmpty(this.Theme))
                        {
                            string pattern = sourcePath.Replace(@"\", @"\\") + @"\\pit\\Tpl\\[^\\]+$";
                            if (Regex.Match(srcDir, pattern, RegexOptions.IgnoreCase).Success && (!str2.EndsWith(this.Theme) && !str2.EndsWith("default")))
                            {
                                continue;
                            }
                            pattern = sourcePath.Replace(@"\", @"\\") + @"\\pit\\Themes$";
                            if (Regex.Match(srcDir, pattern, RegexOptions.IgnoreCase).Success && (!str2.EndsWith(this.Theme) && !str2.EndsWith("default")))
                            {
                                continue;
                            }
                        }
                        tmp = str2.Replace(this.SourcePath, "");
                        tmp = tmp.Substring(1).ToLower();
                        if (this.ExceptDirs.Find(s => s.ToLower() == tmp) == null)
                        {
                            string str4 = destDir + str2.Substring(str2.LastIndexOf(@"\"));
                            this.CopyFiles(str2, str4);
                        }
                    }
                }
                string[] files = Directory.GetFiles(srcDir);
                foreach (string str5 in files)
                {
                    FileInfo info = new FileInfo(str5);
                    if (!this.ExistsInExceptExtensions(str5))
                    {
                        if (this.useAllFolders && this.ExistsInAllFolders(str5))
                        {
                            File.Copy(str5, destDir + @"\" + info.Name, true);
                        }
                        else if (this.IsOnlyEditTime)
                        {
                            if (info.LastWriteTime >= this.LastUpdateTime)
                            {
                                File.Copy(str5, destDir + @"\" + info.Name, true);
                            }
                        }
                        else if ((info.CreationTime >= this.LastUpdateTime) || (info.LastWriteTime >= this.LastUpdateTime))
                        {
                            File.Copy(str5, destDir + @"\" + info.Name, true);
                        }
                    }
                }
                this.DeleteEmptyDir(destDir);
            }
        }

        public void DeleteEmptyDir(string dir)
        {
            string[] directories = Directory.GetDirectories(dir);
            foreach (string str in directories)
            {
                this.DeleteEmptyDir(str);
            }
            string[] files = Directory.GetFiles(dir);
            directories = Directory.GetDirectories(dir);
            if ((files.Length + directories.Length) <= 0)
            {
                Directory.Delete(dir);
            }
        }

        private bool ExistsInAllFolders(string filename)
        {
            string str = filename.Replace("/", @"\").ToLower();
            foreach (string str2 in this.AllFolders)
            {
                if (str.Contains(@"\" + str2.ToLower() + @"\"))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ExistsInExceptExtensions(string filename)
        {
            foreach (string str in this.ExceptExtensions)
            {
                if (filename.EndsWith(str) || filename.Replace("/", @"\").EndsWith(str))
                {
                    return true;
                }
            }
            return false;
        }

        public static WebPackager FromFile(string file)
        {
            if (!File.Exists(file))
            {
                return null;
            }
            string[] strArray = File.ReadAllLines(file, Encoding.GetEncoding("UTF-8"));
            WebPackager packager = new WebPackager {
                ExceptDirs = new List<string>(),
                ExceptExtensions = new List<string>(),
                ExceptFiles = new List<string>(),
                AllFolders = new List<string>()
            };
            FileInfo info = new FileInfo(file);
            packager.Name = info.Name.Replace(".pack", "");
            packager.FileName = file;
            packager.useAllFolders = false;
            foreach (string str in strArray)
            {
                string input = str.Trim();
                if (!input.StartsWith("#"))
                {
                    Match match;
                    if ((match = Regex.Match(input, @"(?<=src(\s)*=(\s)*).+")).Success)
                    {
                        packager.SourcePath = match.Value;
                    }
                    else if ((match = Regex.Match(input, @"(?<=dest(\s)*=(\s)*).+")).Success)
                    {
                        packager.DestinationPath = match.Value;
                    }
                    else if ((match = Regex.Match(input, @"(?<=time(\s)*=(\s)*).+")).Success)
                    {
                        DateTime time;
                        if (DateTime.TryParse(match.Value, out time))
                        {
                            packager.LastUpdateTime = time;
                        }
                    }
                    else if ((match = Regex.Match(input, @"(?<=web_id(\s)*=(\s)*).+")).Success)
                    {
                        packager.WebId = match.Value;
                    }
                    else if ((match = Regex.Match(input, @"(?<=theme(\s)*=(\s)*).+")).Success)
                    {
                        packager.Theme = match.Value;
                    }
                    else if ((match = Regex.Match(input, @"(?<=except_dir(\s)*=(\s)*).+")).Success)
                    {
                        packager.ExceptDirs.Add(match.Value);
                    }
                    else if ((match = Regex.Match(input, @"(?<=except_extension(\s)*=(\s)*).+")).Success)
                    {
                        packager.ExceptExtensions.Add(match.Value);
                    }
                    else if (!(match = Regex.Match(input, @"(?<=all_enable(\s)*=(\s)*).+")).Success && (match = Regex.Match(input, @"(?<=all_folders(\s)*=(\s)*).+")).Success)
                    {
                        packager.AllFolders.Add(match.Value);
                    }
                }
            }
            return packager;
        }

        public bool Package()
        {
            this.CopyFiles(this.SourcePath, this.DestinationPath);
            return true;
        }

        public string SaveFile(string filepath)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("time=" + this.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            builder.AppendLine("src=" + this.SourcePath);
            builder.AppendLine("dest=" + this.DestinationPath);
            builder.AppendLine("web_id=" + this.WebId);
            builder.AppendLine("theme=" + this.Theme);
            foreach (string str in this.ExceptDirs)
            {
                builder.AppendLine("except_dir=" + str);
            }
            foreach (string str2 in this.ExceptExtensions)
            {
                builder.AppendLine("except_extension=" + str2);
            }
            foreach (string str3 in this.AllFolders)
            {
                builder.AppendLine("all_folders=" + str3);
            }
            FileStream stream = new FileStream(filepath, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);
            try
            {
                writer.Write(builder.ToString());
                writer.Flush();
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            finally
            {
                writer.Close();
                stream.Close();
            }
            return "";
        }

        public List<string> AllFolders { get; set; }

        public string DestinationPath { get; set; }

        public List<string> ExceptDirs { get; set; }

        public List<string> ExceptExtensions { get; set; }

        public List<string> ExceptFiles { get; set; }

        public string FileName { get; set; }

        public bool IsOnlyEditTime { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public string Name { get; set; }

        public string SourcePath { get; set; }

        public string Theme { get; set; }

        public bool useAllFolders { get; set; }

        public string WebId { get; set; }
    }
}

