using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WebPackageTool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Form1));
            this.treeView1 = new TreeView();
            this.toolStrip1 = new ToolStrip();
            this.btnNew = new ToolStripButton();
            this.btnRefresh = new ToolStripButton();
            this.toolStripButton1 = new ToolStripButton();
            this.splitContainer1 = new SplitContainer();
            this.packageControl1 = new PackageControl();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            base.SuspendLayout();
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.Location = new Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new Size(0x130, 0x267);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterCheck += new TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.DrawNode += new DrawTreeNodeEventHandler(this.treeView1_DrawNode);
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.toolStrip1.ImageScalingSize = new Size(20, 20);
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.btnNew, this.btnRefresh, this.toolStripButton1 };
            this.toolStrip1.Items.AddRange(toolStripItems);
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x34c, 0x19);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.btnNew.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnNew.Image = (Image)manager.GetObject("btnNew.Image");
            this.btnNew.ImageTransparentColor = Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(60, 0x16);
            this.btnNew.Text = "新建模版";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnRefresh.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnRefresh.Image = (Image)manager.GetObject("btnRefresh.Image");
            this.btnRefresh.ImageTransparentColor = Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(60, 0x16);
            this.btnRefresh.Text = "刷新列表";
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
            this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = (Image)manager.GetObject("toolStripButton1.Image");
            this.toolStripButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new Size(0x54, 0x16);
            this.toolStripButton1.Text = "打包所选项目";
            this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0x19);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel2.Controls.Add(this.packageControl1);
            this.splitContainer1.Size = new Size(0x34c, 0x267);
            this.splitContainer1.SplitterDistance = 0x130;
            this.splitContainer1.TabIndex = 4;
            this.packageControl1.FilePath = null;
            this.packageControl1.Location = new Point(4, 4);
            this.packageControl1.Margin = new Padding(4);
            this.packageControl1.Name = "packageControl1";
            this.packageControl1.Size = new Size(0x216, 0x25f);
            this.packageControl1.TabIndex = 1;
            this.packageControl1.WebPackager = null;
            this.packageControl1.OnFileSaved += new EventHandler(this.packageControl1_OnFileSaved);
            this.packageControl1.Load += new EventHandler(this.packageControl1_Load);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x34c, 640);
            base.Controls.Add(this.splitContainer1);
            base.Controls.Add(this.toolStrip1);
            base.Name = "Form1";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "打包升级补丁工具";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}
