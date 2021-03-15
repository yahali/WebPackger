using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace WebPackageTool
{
    public partial class Form1 : Form
    {
        private List<WebPackager> lstPacker;
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        public Form1()
        {
            InitializeComponent();

            GetTemplates();
        }

        /// <summary>
        /// 获取模版
        /// </summary>
        void GetTemplates()
        {
            treeView1.Nodes.Clear();
            string[] dirs = System.IO.Directory.GetDirectories(Application.StartupPath + "\\projects");
            foreach (string item in dirs)
            {
                string dirname = item.Substring(item.LastIndexOf("\\") + 1);
                TreeNode node = new TreeNode(dirname);


                //
                string[] files = System.IO.Directory.GetFiles(item);
                foreach (string fl in files)
                {
                    FileInfo info = new FileInfo(fl);
                    if (info.Extension == ".pack")
                    {
                        TreeNode nd = new TreeNode(info.Name.Substring(0, info.Name.Length - 5));
                        nd.Tag = "2" + fl;
                        node.Nodes.Add(nd);
                    }
                }

                //node.Expand();
                //
                treeView1.Nodes.Add(node);
            }

            //
            string[] files2 = System.IO.Directory.GetFiles(Application.StartupPath + "\\projects");
            foreach (string fl in files2)
            {
                FileInfo info = new FileInfo(fl);
                if (info.Extension == ".pack")
                {
                    TreeNode nd = new TreeNode(info.Name.Substring(0, info.Name.Length - 5));
                    nd.Tag = "2" + fl;
                    treeView1.Nodes.Add(nd);
                }
            }

        }





        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Tag == null)
            {
                return;
            }

            string tag = node.Tag.ToString();
            string tp = tag.Substring(0, 1);
            //文件节点
            if (tp == "2")
            {
                string filePath = tag.Substring(1);
                //string content = File.ReadAllText(filePath, Encoding.GetEncoding("GB2312"));
                WebPackager pack = WebPackager.FromFile(filePath, packageControl1);

                FileInfo file = new FileInfo(filePath);
                packageControl1.WebPackager = pack;
                packageControl1.FilePath = filePath;
            }
        }

        /// <summary>
        /// 新建模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            packageControl1.WebPackager = new WebPackager(null);
            packageControl1.FilePath = "";
        }
        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetTemplates();
        }

        //存储文件后，刷新列表
        private void packageControl1_OnFileSaved(object sender, EventArgs e)
        {
            GetTemplates();
        }

        /// <summary>
        /// 递归创建
        /// </summary>
        /// <param name="node"></param>
        private void makeWebPacker(TreeNode node)
        {
            if (node.Checked)
            {
                if (node.Tag != null)
                {
                    string tag = node.Tag.ToString();
                    string tp = tag.Substring(0, 1);
                    //文件节点
                    if (tp == "2")
                    {
                        string filePath = tag.Substring(1);
                        //string content = File.ReadAllText(filePath, Encoding.GetEncoding("GB2312"));
                        WebPackager pack = WebPackager.FromFile(filePath, packageControl1);
                        lstPacker.Add(pack);
                    }
                }
            }

            //子节点
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode item in node.Nodes)
                {
                    makeWebPacker(item);
                }
            }
        }

        /// <summary>
        /// 打包选中的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            packageControl1.WriteLog("生成日志：", true);

            //定义打包对象列表
            lstPacker = new List<WebPackager>();
            foreach (TreeNode node in treeView1.Nodes)
            {
                makeWebPacker(node);
            }

            //定义后台处理线程
            BackgroundWorker bwPackage = new BackgroundWorker();
            bwPackage.DoWork += new DoWorkEventHandler(bwPackage_DoWork);
            bwPackage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwPackage_RunWorkerCompleted);
            bwPackage.RunWorkerAsync();
            toolStripButton1.Enabled = false;
            toolStripButton1.Text = "请稍候..";
        }

        void bwPackage_DoWork(object sender, DoWorkEventArgs e)
        {
            var count = 0;
            var succount = 0;
            foreach (WebPackager pack in lstPacker)
            {
                count++;
                var suc = pack.Package();
                if (suc)
                {
                    string content = File.ReadAllText(pack.FileName);
                    //将当前时间写入到文件中去
                    string str = Regex.Replace(content, "(?<=time(\\s)*=(\\s)*).+", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    File.WriteAllText(pack.FileName, str, Encoding.GetEncoding("UTF-8"));
                    succount++;
                }
            }

            //bool succ = webPackager.Package();
            packageControl1.WriteLog($"打包全部完成，成功打包{succount}个项目，共计{count}个项目");
        }
        void bwPackage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripButton1.Text = "打包所选项目";
            toolStripButton1.Enabled = true;
            //foreach (WebPackager pack in lstPacker)
            //{
            //    string content = File.ReadAllText(pack.FileName);
            //    //将当前时间写入到文件中去
            //    string str = Regex.Replace(content, "(?<=time(\\s)*=(\\s)*).+", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //    File.WriteAllText(pack.FileName, str, Encoding.GetEncoding("UTF-8"));
            //}
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode item in e.Node.Nodes)
            {
                item.Checked = e.Node.Checked;
            }
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {

        }

        private void packageControl1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
