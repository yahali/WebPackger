using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace WebPackageTool
{
    public partial class PackageControl : UserControl
    {
        private WebPackager webPackager;
        public string FilePath { get; set; }
        public event EventHandler OnFileSaved;

        public WebPackager WebPackager
        {
            get { return webPackager; }
            set
            {
                webPackager = value;

                if (webPackager != null)
                {
                    textBoxExceptDir.Text = "";
                    textBoxExceptExt.Text = "";
                    textBoxFolders.Text = "";
                    textBoxDest.Text = webPackager.DestinationPath;
                    foreach (string item in webPackager.ExceptDirs)
                    {
                        textBoxExceptDir.AppendText(item + System.Environment.NewLine);
                    }

                    //
                    foreach (string item in webPackager.ExceptExtensions)
                    {
                        textBoxExceptExt.AppendText(item + System.Environment.NewLine);
                    }
                    foreach (string str3 in webPackager.AllFolders)
                    {
                        this.textBoxFolders.AppendText(str3 + Environment.NewLine);
                    }

                    textBoxSrc.Text = webPackager.SourcePath;
                    dateTimePickerLastUpdate.Value = webPackager.LastUpdateTime;
                    textBoxName.Text = webPackager.Name;
                    txtSiteId.Text = webPackager.WebId;
                    txtTheme.Text = webPackager.Theme;
                    chkAllFolders.Checked = this.webPackager.useAllFolders;
                }
            }
        }

        public PackageControl()
        {
            InitializeComponent();
            SetMethod = SetTextAdd;
        }

        public void WriteLog(string log, bool isReplace = false)
        {
            txtboxLog.BeginInvoke(SetMethod, log, isReplace);
            //if (txtboxLog.InvokeRequired)
            //    txtboxLog.Invoke(SetMethod, log);
        }

        private delegate void InvokeText(string str, bool isReplace);
        private InvokeText SetMethod;
        private void SetTextAdd(string str, bool isReplace)
        {
            if (isReplace)
                txtboxLog.Text = str;
            else
            {
                var log = txtboxLog.Text + "\r\n" + str;
                txtboxLog.Text = log;
            }
        }

        /// <summary>
        /// 获取配置对象
        /// </summary>
        /// <returns></returns>
        void MakeWebPackage()
        {
            if (webPackager == null)
            {
                webPackager = new WebPackager(this);
            }
            //网站ID
            webPackager.WebId = txtSiteId.Text.Trim();
            //网站模板名称
            webPackager.Theme = txtTheme.Text.Trim();
            //
            webPackager.LastUpdateTime = dateTimePickerLastUpdate.Value;
            //原目录
            webPackager.SourcePath = textBoxSrc.Text.Trim();
            //目标目录
            webPackager.DestinationPath = textBoxDest.Text.Trim();
            //排除的后缀名
            webPackager.ExceptExtensions = new List<string>();
            Regex reg = new Regex("\\S+");
            MatchCollection matchs = reg.Matches(textBoxExceptExt.Text);
            foreach (Match item in matchs)
            {
                webPackager.ExceptExtensions.Add(item.Value);
            }

            //排除的目录
            webPackager.ExceptDirs = new List<string>();
            reg = new Regex("\\S+");
            matchs = reg.Matches(textBoxExceptDir.Text);
            foreach (Match item in matchs)
            {
                webPackager.ExceptDirs.Add(item.Value);
            }

            //是否只打包更新时间
            webPackager.IsOnlyEditTime = chkOnlyEditTime.Checked;
            webPackager.useAllFolders = this.chkAllFolders.Checked;
        }


        private void btnPackage_Click(object sender, EventArgs e)
        {
            txtboxLog.Text = "生成日志：";
            MakeWebPackage();

            //定义后台处理线程
            BackgroundWorker bwPackage = new BackgroundWorker();
            bwPackage.DoWork += new DoWorkEventHandler(bwPackage_DoWork);
            bwPackage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwPackage_RunWorkerCompleted);
            bwPackage.RunWorkerAsync();
            btnPackage.Enabled = false;
            btnPackage.Text = "请稍候..";
        }
        void bwPackage_DoWork(object sender, DoWorkEventArgs e)
        {
            bool succ = webPackager.Package();
            if (succ)
            {
                string content = File.ReadAllText(webPackager.FileName);
                //将当前时间写入到文件中去
                string str = Regex.Replace(content, "(?<=time(\\s)*=(\\s)*).+", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                File.WriteAllText(webPackager.FileName, str, Encoding.GetEncoding("UTF-8"));
                WriteLog($"error 打包完成，本项目打包失败");
            }
            WriteLog($"打包完成，本项目打包成功");
        }
        void bwPackage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPackage.Text = "立即打包";
            btnPackage.Enabled = true;
        }

        /// <summary>
        /// 浏览源文件目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowSrc_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            if (folderDlg.ShowDialog() == DialogResult.OK)
            {
                textBoxSrc.Text = folderDlg.SelectedPath;
            }
        }

        private void btnBrowDest_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            if (folderDlg.ShowDialog() == DialogResult.OK)
            {
                textBoxDest.Text = folderDlg.SelectedPath;
            }
        }

        /*存储模版*/
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveToFile(FilePath);
        }

        /*另存为*/
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveToFile("");
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="path"></param>
        void SaveToFile(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.InitialDirectory = System.Environment.CurrentDirectory + "\\projects";
                if (!Directory.Exists(dialog.InitialDirectory))
                {
                    Directory.CreateDirectory(dialog.InitialDirectory);
                }
                dialog.AddExtension = true;
                dialog.FileName = "新建打包模版";
                dialog.Filter = "打包升级模版文件|*.pack";
                dialog.DefaultExt = ".pack";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.FileName;
                }
                else
                {
                    return;
                }
            }


            //存储
            MakeWebPackage();
            string res = "";
            if (!string.IsNullOrEmpty(res = webPackager.SaveFile(path)))
            {
                MessageBox.Show("存储失败：" + res);
            }
            else
            {
                if (OnFileSaved != null)
                {
                    OnFileSaved(this, null);
                }
            }
        }

        private void chkAllFolders_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
