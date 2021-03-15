using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WebPackageTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
            this.GetTemplates();
        }

        private ToolStripButton btnNew;
        private ToolStripButton btnRefresh;
        private SolidBrush greenBrush = new SolidBrush(Color.Green);
        private List<WebPackager> lstPacker;
        private PackageControl packageControl1;
        private SolidBrush redBrush = new SolidBrush(Color.Red);
        private SplitContainer splitContainer1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private TreeView treeView1;


        private void btnNew_Click(object sender, EventArgs e)
        {
            this.packageControl1.WebPackager = new WebPackager();
            this.packageControl1.FilePath = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.GetTemplates();
        }

        private void bwPackage_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (WebPackager packager in this.lstPacker)
            {
                packager.Package();
            }
        }

        private void bwPackage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripButton1.Text = "打包所选项目";
            this.toolStripButton1.Enabled = true;
            foreach (WebPackager packager in this.lstPacker)
            {
                string contents = Regex.Replace(File.ReadAllText(packager.FileName), @"(?<=time(\s)*=(\s)*).+", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                File.WriteAllText(packager.FileName, contents, Encoding.GetEncoding("UTF-8"));
            }
        }

        private void GetTemplates()
        {
            this.treeView1.Nodes.Clear();
            string[] directories = Directory.GetDirectories(Application.StartupPath + @"\projects");
            foreach (string str in directories)
            {
                TreeNode node = new TreeNode(str.Substring(str.LastIndexOf(@"\") + 1));
                string[] strArray4 = Directory.GetFiles(str);
                foreach (string str3 in strArray4)
                {
                    FileInfo info = new FileInfo(str3);
                    if (info.Extension == ".pack")
                    {
                        TreeNode node2 = new TreeNode(info.Name.Substring(0, info.Name.Length - 5))
                        {
                            Tag = "2" + str3
                        };
                        node.Nodes.Add(node2);
                    }
                }
                this.treeView1.Nodes.Add(node);
            }
            string[] files = Directory.GetFiles(Application.StartupPath + @"\projects");
            foreach (string str4 in files)
            {
                FileInfo info2 = new FileInfo(str4);
                if (info2.Extension == ".pack")
                {
                    TreeNode node3 = new TreeNode(info2.Name.Substring(0, info2.Name.Length - 5))
                    {
                        Tag = "2" + str4
                    };
                    this.treeView1.Nodes.Add(node3);
                }

            }
        }

        private void makeWebPacker(TreeNode node)
        {
            if (node.Checked && (node.Tag != null))
            {
                string str = node.Tag.ToString();
                if (str.Substring(0, 1) == "2")
                {
                    WebPackager item = WebPackager.FromFile(str.Substring(1));
                    this.lstPacker.Add(item);
                }
            }
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    this.makeWebPacker(node2);
                }
            }
        }

        private void packageControl1_Load(object sender, EventArgs e)
        {
        }

        private void packageControl1_OnFileSaved(object sender, EventArgs e)
        {
            this.GetTemplates();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.lstPacker = new List<WebPackager>();
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                this.makeWebPacker(node);
            }
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(this.bwPackage_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bwPackage_RunWorkerCompleted);
            worker.RunWorkerAsync();
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Text = "请稍候..";
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
            }
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Tag != null)
            {
                string str = node.Tag.ToString();
                if (str.Substring(0, 1) == "2")
                {
                    string file = str.Substring(1);
                    WebPackager packager = WebPackager.FromFile(file);
                    FileInfo info = new FileInfo(file);
                    this.packageControl1.WebPackager = packager;
                    this.packageControl1.FilePath = file;
                }
            }
        }
    }
}

