using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WebPackageTool
{
    public class WebPackager
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模版文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 源文件目录
        /// </summary>
        public string SourcePath { get; set; }

        /// <summary>
        /// 目标目录
        /// </summary>
        public string DestinationPath { get; set; }

        /// <summary>
        /// 网站ID
        /// </summary>
        public string WebId { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// 排除的文件列表
        /// </summary>
        public List<string> ExceptFiles { get; set; }

        /// <summary>
        /// 排除的目录
        /// </summary>
        public List<string> ExceptDirs { get; set; }

        /// <summary>
        /// 排除的文件类型
        /// </summary>
        public List<string> ExceptExtensions { get; set; }

        /// <summary>
        /// 上次更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 是否只打包更新时间
        /// </summary>
        public bool IsOnlyEditTime { get; set; }
        public bool useAllFolders { get; set; }
        public List<string> AllFolders { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WebPackager(PackageControl pc)
        {
            ExceptFiles = new List<string>();
            ExceptDirs = new List<string>();
            ExceptExtensions = new List<string>();
            AllFolders = new List<string>();
            LastUpdateTime = DateTime.Now.Date.AddDays(-3);
            WebId = "";
            pcFrom = pc;
        }

        public PackageControl pcFrom = null;

        /// <summary>
        /// 开始打包
        /// </summary>
        /// <returns></returns>
        public bool Package()
        {
            //先删除原目录
            return CopyFiles(SourcePath, DestinationPath, true);
        }

        /// <summary>
        /// 判别是否在排除的后缀名中
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool ExistsInExceptExtensions(string filename)
        {
            foreach (var item in ExceptExtensions)
            {
                if (filename.EndsWith(item) || filename.Replace("/", "\\").EndsWith(item))
                {
                    return true;
                }
            }

            return false;
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

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="dir"></param>
        public bool CopyFiles(string srcDir, string destDir, bool isfirst)
        {
            try
            {
                string rootDir = this.SourcePath;   //根目录

                if (!Directory.Exists(srcDir))
                {
                    Log($"目录：{srcDir}未找到！");
                    return false;
                }

                //获取当前目录下面的子目录
                string[] dirs = Directory.GetDirectories(srcDir);
                string[] files = Directory.GetFiles(srcDir);

                if (dirs.Length <= 0 && files.Length <= 0 && isfirst)
                {
                    Log($"目录：{srcDir}，下没有文件！");
                    return false;
                }
                if (isfirst)
                {
                    Log("=================================");
                    Log($"开始打包：{Name}，{srcDir} -> {destDir}\r\n");
                }

                //如果目标目录不存在，则创建目录  
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }
                foreach (string item in dirs)
                {
                    //特殊目录处理；
                    if (!string.IsNullOrEmpty(this.WebId))
                    {
                        if (srcDir.EndsWith(rootDir + "\\files"))
                        {
                            if (!item.EndsWith(rootDir + "\\files\\" + this.WebId))
                            {
                                continue;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(this.Theme))
                    {
                        string reg = rootDir.Replace("\\", "\\\\") + "\\\\pit\\\\Tpl\\\\[^\\\\]+$";
                        //非当前站点主题目录排除
                        if (Regex.Match(srcDir, reg, RegexOptions.IgnoreCase).Success)
                        {
                            if (!item.EndsWith(this.Theme) && !item.EndsWith("default"))
                            {
                                continue;
                            }
                        }
                        //非当前站点主题目录排除[Theme目录]
                        reg = rootDir.Replace("\\", "\\\\") + "\\\\pit\\\\Themes$";
                        if (Regex.Match(srcDir, reg, RegexOptions.IgnoreCase).Success)
                        {
                            if (!item.EndsWith(this.Theme) && !item.EndsWith("default"))
                            {
                                continue;
                            }
                        }

                    }
                    //END 目录特殊处理
                    var tmp = item.Replace(SourcePath, "");
                    tmp = tmp.Substring(1).ToLower();
                    if (this.ExceptDirs.Find(s => s.ToLower() == tmp) == null)

                        //排除指定的目录
                        if (ExceptDirs.Find(s => s == tmp) == null)
                        {
                            string dirName = destDir + item.Substring(item.LastIndexOf("\\"));
                            CopyFiles(item, dirName, false);
                        }
                }

                //拷贝该目录下面的文件
                foreach (string fl in files)
                {
                    FileInfo file = new FileInfo(fl);
                    if (!ExistsInExceptExtensions(fl))
                    {
                        if (this.useAllFolders && ExistsInAllFolders(fl))
                        {
                            File.Copy(fl, destDir + @"\" + file.Name, true);
                        }
                        else if (IsOnlyEditTime) //只打包更新时间
                        {
                            if (file.LastWriteTime >= LastUpdateTime)
                            {
                                File.Copy(fl, destDir + "\\" + file.Name, true);
                            }
                        }
                        else
                        {
                            if (file.CreationTime >= LastUpdateTime || file.LastWriteTime >= LastUpdateTime)
                            {
                                File.Copy(fl, destDir + "\\" + file.Name, true);
                            }
                        }
                    }

                    /*
                    if ((file.CreationTime >= LastUpdateTime || file.LastWriteTime >= LastUpdateTime) && !ExistsInExceptExtensions(fl))
                    {
                        File.Copy(fl, destDir + "\\" + file.Name, true);
                    }*/
                }

                //删除没有内容的目录
                DeleteEmptyDir(destDir, isfirst);

                if (isfirst)
                {
                    Log($"目录：{destDir} 打包执行完成");
                    Log("-------------------------------------");
                }

                return true;
            }
            catch (Exception ex)
            {
                Log("打包异常：" + ex.ToString());
            }
            return false;
        }

        private void Log(string str)
        {
            if (pcFrom != null)
                pcFrom.WriteLog(str);
        }

        /// <summary>
        /// 删除空目录
        /// </summary>
        /// <param name="dir"></param>
        public void DeleteEmptyDir(string dir, bool isfirst)
        {
            //获取当前目录下面的子目录
            string[] dirs = Directory.GetDirectories(dir);
            foreach (string item in dirs)
            {
                DeleteEmptyDir(item, false);
            }

            //获取文件
            string[] files = Directory.GetFiles(dir);
            //在获取一次目录，若有目录或文件存在，则不能删除该目录，否则可以进行删除
            dirs = Directory.GetDirectories(dir);

            if (files.Length + dirs.Length > 0)
            {
                return;
            }
            else
            {
                Directory.Delete(dir);
                if (isfirst)
                {
                    Log($"目录：{dir}，下没有文件！已被删除！");
                }
            }

        }

        /// <summary>
        /// 根据模版内容建立实例
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static WebPackager FromFile(string file, PackageControl pc)
        {
            if (!System.IO.File.Exists(file))
            {
                return null;
            }

            string[] rows = System.IO.File.ReadAllLines(file, Encoding.GetEncoding("UTF-8"));

            WebPackager pack = new WebPackager(pc);
            pack.ExceptDirs = new List<string>();
            pack.ExceptExtensions = new List<string>();
            pack.ExceptFiles = new List<string>();
            pack.AllFolders = new List<string>();
            FileInfo fileInfo = new FileInfo(file);
            pack.Name = fileInfo.Name.Replace(".pack", "");
            pack.FileName = file;
            pack.useAllFolders = false;

            //一行一行的读取
            foreach (string item in rows)
            {
                string row = item.Trim();
                //#号代表注释
                if (!row.StartsWith("#"))
                {
                    Match m;

                    //源文件目录
                    if ((m = Regex.Match(row, "(?<=src(\\s)*=(\\s)*).+")).Success)
                    {
                        pack.SourcePath = m.Value;
                    }
                    else if ((m = Regex.Match(row, "(?<=dest(\\s)*=(\\s)*).+")).Success)
                    {
                        pack.DestinationPath = m.Value;
                    }
                    else if ((m = Regex.Match(row, "(?<=time(\\s)*=(\\s)*).+")).Success)
                    {
                        DateTime dt;
                        if (DateTime.TryParse(m.Value, out dt))
                        {
                            pack.LastUpdateTime = dt;
                        }
                    }
                    else if ((m = Regex.Match(row, "(?<=web_id(\\s)*=(\\s)*).+")).Success)
                    {
                        pack.WebId = m.Value;
                    }
                    else if ((m = Regex.Match(row, "(?<=theme(\\s)*=(\\s)*).+")).Success)
                    {
                        pack.Theme = m.Value;
                    }
                    else if ((m = Regex.Match(row, "(?<=except_dir(\\s)*=(\\s)*).+")).Success)
                    {
                        pack.ExceptDirs.Add(m.Value);
                    }
                    else if ((m = Regex.Match(row, "(?<=except_extension(\\s)*=(\\s)*).+")).Success)
                    {
                        pack.ExceptExtensions.Add(m.Value);
                    }
                    else if (!(m = Regex.Match(row, @"(?<=all_enable(\s)*=(\s)*).+")).Success && (m = Regex.Match(row, @"(?<=all_folders(\s)*=(\s)*).+")).Success)
                    {
                        pack.AllFolders.Add(m.Value);
                    }
                }
            }

            return pack;
        }

        /// <summary>
        /// 存储模版
        /// </summary>
        /// <param name="filepath"></param>
        public string SaveFile(string filepath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("time=" + this.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine("src=" + this.SourcePath);
            sb.AppendLine("dest=" + this.DestinationPath);
            sb.AppendLine("web_id=" + this.WebId);
            sb.AppendLine("theme=" + this.Theme);
            foreach (string item in this.ExceptDirs)
            {
                sb.AppendLine("except_dir=" + item);
            }
            foreach (string item in ExceptExtensions)
            {
                sb.AppendLine("except_extension=" + item);
            }
            foreach (string str3 in this.AllFolders)
            {
                sb.AppendLine("all_folders=" + str3);
            }


            //存储文件
            FileStream fs = new FileStream(filepath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(sb.ToString());
                sw.Flush();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }

            return "";
        }
    }
}
