namespace WebPackageTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public class PackageControl : UserControl
    {
        private Button btnBrowDest;
        private Button btnBrowSrc;
        private Button btnPackage;
        private Button btnSave;
        private Button btnSaveAs;
        private CheckBox chkAllFolders;
        private CheckBox chkOnlyEditTime;
        private IContainer components = null;
        private DateTimePicker dateTimePickerLastUpdate;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private RadioButton radioButton1;
        private TextBox textBoxDest;
        private TextBox textBoxExceptDir;
        private TextBox textBoxExceptExt;
        private TextBox textBoxFolders;
        private TextBox textBoxName;
        private TextBox textBoxSrc;
        private TextBox txtSiteId;
        private TextBox txtTheme;
        private WebPackageTool.WebPackager webPackager;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event EventHandler OnFileSaved;

        public PackageControl()
        {
            this.InitializeComponent();
        }

        private void btnBrowDest_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxDest.Text = dialog.SelectedPath;
            }
        }

        private void btnBrowSrc_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxSrc.Text = dialog.SelectedPath;
            }
        }

        private void btnPackage_Click(object sender, EventArgs e)
        {
            this.MakeWebPackage();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(this.bwPackage_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bwPackage_RunWorkerCompleted);
            worker.RunWorkerAsync();
            this.btnPackage.Enabled = false;
            this.btnPackage.Text = "请稍候..";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveToFile(this.FilePath);
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            this.SaveToFile("");
        }

        private void bwPackage_DoWork(object sender, DoWorkEventArgs e)
        {
            bool flag = this.webPackager.Package();
        }

        private void bwPackage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnPackage.Text = "立即打包";
            this.btnPackage.Enabled = true;
            DateTime now = DateTime.Now;
            this.dateTimePickerLastUpdate.Value = now;
            string contents = Regex.Replace(File.ReadAllText(this.webPackager.FileName), @"(?<=time(\s)*=(\s)*).+", now.ToString("yyyy-MM-dd HH:mm:ss"));
            File.WriteAllText(this.webPackager.FileName, contents, Encoding.GetEncoding("UTF-8"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox1 = new GroupBox();
            this.textBoxExceptDir = new TextBox();
            this.groupBox2 = new GroupBox();
            this.textBoxExceptExt = new TextBox();
            this.groupBox3 = new GroupBox();
            this.radioButton1 = new RadioButton();
            this.label5 = new Label();
            this.btnPackage = new Button();
            this.btnSave = new Button();
            this.textBoxName = new TextBox();
            this.textBoxSrc = new TextBox();
            this.textBoxDest = new TextBox();
            this.dateTimePickerLastUpdate = new DateTimePicker();
            this.btnSaveAs = new Button();
            this.btnBrowSrc = new Button();
            this.btnBrowDest = new Button();
            this.txtSiteId = new TextBox();
            this.label6 = new Label();
            this.label7 = new Label();
            this.txtTheme = new TextBox();
            this.chkOnlyEditTime = new CheckBox();
            this.groupBox4 = new GroupBox();
            this.textBoxFolders = new TextBox();
            this.chkAllFolders = new CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "模版名称：";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(14, 0x25);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "源文件目录：";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(14, 0x3f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "目标目录：";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 0x7e);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "上次更新时间：";
            this.groupBox1.Controls.Add(this.textBoxExceptDir);
            this.groupBox1.Location = new Point(0xfb, 0x2b);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe8, 0xf2);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "排除的目录";
            this.textBoxExceptDir.Dock = DockStyle.Fill;
            this.textBoxExceptDir.Location = new Point(3, 0x11);
            this.textBoxExceptDir.Multiline = true;
            this.textBoxExceptDir.Name = "textBoxExceptDir";
            this.textBoxExceptDir.ScrollBars = ScrollBars.Vertical;
            this.textBoxExceptDir.Size = new Size(0xe2, 0xde);
            this.textBoxExceptDir.TabIndex = 0;
            this.groupBox2.Controls.Add(this.textBoxExceptExt);
            this.groupBox2.Location = new Point(12, 0x2b);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xe9, 0xf2);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "排除的后缀名";
            this.textBoxExceptExt.Dock = DockStyle.Fill;
            this.textBoxExceptExt.Location = new Point(3, 0x11);
            this.textBoxExceptExt.Multiline = true;
            this.textBoxExceptExt.Name = "textBoxExceptExt";
            this.textBoxExceptExt.ScrollBars = ScrollBars.Vertical;
            this.textBoxExceptExt.Size = new Size(0xe3, 0xde);
            this.textBoxExceptExt.TabIndex = 0;
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new Point(4, 0xab);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x1ed, 0x123);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "打包设置";
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new Point(12, 0x15);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x59, 0x10);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "ASP.NET站点";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(13, 0x17);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "网站编号：";
            this.btnPackage.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnPackage.Location = new Point(0x106, 0x7a);
            this.btnPackage.Name = "btnPackage";
            this.btnPackage.Size = new Size(0x4b, 0x17);
            this.btnPackage.TabIndex = 3;
            this.btnPackage.Text = "立即打包";
            this.btnPackage.UseVisualStyleBackColor = true;
            this.btnPackage.Click += new EventHandler(this.btnPackage_Click);
            this.btnSave.Location = new Point(0x152, 0x7a);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x4b, 0x17);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "存储模版";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.textBoxName.Location = new Point(0x70, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            this.textBoxName.Size = new Size(0x182, 0x15);
            this.textBoxName.TabIndex = 4;
            this.textBoxSrc.Location = new Point(0x6f, 0x21);
            this.textBoxSrc.Name = "textBoxSrc";
            this.textBoxSrc.Size = new Size(0x155, 0x15);
            this.textBoxSrc.TabIndex = 4;
            this.textBoxDest.Location = new Point(0x6f, 0x3b);
            this.textBoxDest.Name = "textBoxDest";
            this.textBoxDest.Size = new Size(0x155, 0x15);
            this.textBoxDest.TabIndex = 4;
            this.dateTimePickerLastUpdate.CalendarTitleForeColor = SystemColors.ActiveCaption;
            this.dateTimePickerLastUpdate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerLastUpdate.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerLastUpdate.Location = new Point(0x6f, 0x7b);
            this.dateTimePickerLastUpdate.Name = "dateTimePickerLastUpdate";
            this.dateTimePickerLastUpdate.Size = new Size(0x97, 0x15);
            this.dateTimePickerLastUpdate.TabIndex = 5;
            this.btnSaveAs.Location = new Point(0x1a3, 0x7a);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new Size(0x4c, 0x17);
            this.btnSaveAs.TabIndex = 3;
            this.btnSaveAs.Text = "另存为模版";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new EventHandler(this.btnSaveAs_Click);
            this.btnBrowSrc.Location = new Point(0x1cb, 0x1f);
            this.btnBrowSrc.Name = "btnBrowSrc";
            this.btnBrowSrc.Size = new Size(0x26, 0x17);
            this.btnBrowSrc.TabIndex = 6;
            this.btnBrowSrc.Text = "…";
            this.btnBrowSrc.UseVisualStyleBackColor = true;
            this.btnBrowSrc.Click += new EventHandler(this.btnBrowSrc_Click);
            this.btnBrowDest.Location = new Point(0x1cb, 0x3a);
            this.btnBrowDest.Name = "btnBrowDest";
            this.btnBrowDest.Size = new Size(0x26, 0x17);
            this.btnBrowDest.TabIndex = 6;
            this.btnBrowDest.Text = "…";
            this.btnBrowDest.UseVisualStyleBackColor = true;
            this.btnBrowDest.Click += new EventHandler(this.btnBrowDest_Click);
            this.txtSiteId.Location = new Point(0x6f, 0x5b);
            this.txtSiteId.Name = "txtSiteId";
            this.txtSiteId.Size = new Size(0x97, 0x15);
            this.txtSiteId.TabIndex = 4;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(14, 0x5f);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "网站编号：";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x110, 0x5f);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "模板名称：";
            this.txtTheme.Location = new Point(0x151, 90);
            this.txtTheme.Name = "txtTheme";
            this.txtTheme.Size = new Size(0x97, 0x15);
            this.txtTheme.TabIndex = 4;
            this.chkOnlyEditTime.AutoSize = true;
            this.chkOnlyEditTime.Location = new Point(0x6f, 150);
            this.chkOnlyEditTime.Margin = new Padding(2, 2, 2, 2);
            this.chkOnlyEditTime.Name = "chkOnlyEditTime";
            this.chkOnlyEditTime.Size = new Size(0x54, 0x10);
            this.chkOnlyEditTime.TabIndex = 7;
            this.chkOnlyEditTime.Text = "仅更新时间";
            this.chkOnlyEditTime.UseVisualStyleBackColor = true;
            this.groupBox4.Controls.Add(this.textBoxFolders);
            this.groupBox4.Location = new Point(0x60, 0x1d4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x18f, 0x89);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "指定文件夹全部打包名称列表";
            this.textBoxFolders.Dock = DockStyle.Fill;
            this.textBoxFolders.Location = new Point(3, 0x11);
            this.textBoxFolders.Multiline = true;
            this.textBoxFolders.Name = "textBoxFolders";
            this.textBoxFolders.ScrollBars = ScrollBars.Vertical;
            this.textBoxFolders.Size = new Size(0x189, 0x75);
            this.textBoxFolders.TabIndex = 0;
            this.chkAllFolders.AutoSize = true;
            this.chkAllFolders.Location = new Point(0x2b, 0x1d4);
            this.chkAllFolders.Margin = new Padding(2);
            this.chkAllFolders.Name = "chkAllFolders";
            this.chkAllFolders.Size = new Size(0x30, 0x10);
            this.chkAllFolders.TabIndex = 9;
            this.chkAllFolders.Text = "启用";
            this.chkAllFolders.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.chkAllFolders);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.chkOnlyEditTime);
            base.Controls.Add(this.btnBrowDest);
            base.Controls.Add(this.btnBrowSrc);
            base.Controls.Add(this.dateTimePickerLastUpdate);
            base.Controls.Add(this.txtTheme);
            base.Controls.Add(this.txtSiteId);
            base.Controls.Add(this.textBoxDest);
            base.Controls.Add(this.textBoxSrc);
            base.Controls.Add(this.textBoxName);
            base.Controls.Add(this.btnSaveAs);
            base.Controls.Add(this.btnSave);
            base.Controls.Add(this.btnPackage);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox3);
            base.Name = "PackageControl";
            base.Size = new Size(0x1fc, 0x26e);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void MakeWebPackage()
        {
            if (this.webPackager == null)
            {
                this.webPackager = new WebPackageTool.WebPackager();
            }
            this.webPackager.WebId = this.txtSiteId.Text.Trim();
            this.webPackager.Theme = this.txtTheme.Text.Trim();
            this.webPackager.LastUpdateTime = this.dateTimePickerLastUpdate.Value;
            this.webPackager.SourcePath = this.textBoxSrc.Text.Trim();
            this.webPackager.DestinationPath = this.textBoxDest.Text.Trim();
            this.webPackager.ExceptExtensions = new List<string>();
            Regex regex = new Regex(@"\S+");
            MatchCollection matchs = regex.Matches(this.textBoxExceptExt.Text);
            foreach (Match match in matchs)
            {
                this.webPackager.ExceptExtensions.Add(match.Value);
            }
            this.webPackager.ExceptDirs = new List<string>();
            matchs = regex.Matches(this.textBoxExceptDir.Text);
            foreach (Match match2 in matchs)
            {
                this.webPackager.ExceptDirs.Add(match2.Value);
            }
            this.webPackager.AllFolders = new List<string>();
            matchs = regex.Matches(this.textBoxFolders.Text);
            foreach (Match match3 in matchs)
            {
                this.webPackager.AllFolders.Add(match3.Value);
            }
            this.webPackager.IsOnlyEditTime = this.chkOnlyEditTime.Checked;
            this.webPackager.useAllFolders = this.chkAllFolders.Checked;
        }

        private void SaveToFile(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                SaveFileDialog dialog = new SaveFileDialog {
                    InitialDirectory = Environment.CurrentDirectory + @"\projects"
                };
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
            this.MakeWebPackage();
            string str = "";
            if (!string.IsNullOrEmpty(str = this.webPackager.SaveFile(path)))
            {
                MessageBox.Show("存储失败：" + str);
            }
            else if (this.OnFileSaved != null)
            {
                this.OnFileSaved(this, null);
            }
        }

        public string FilePath { get; set; }

        public WebPackageTool.WebPackager WebPackager
        {
            get
            {
                return this.webPackager;
            }
            set
            {
                this.webPackager = value;
                if (this.webPackager != null)
                {
                    this.textBoxExceptDir.Text = "";
                    this.textBoxExceptExt.Text = "";
                    this.textBoxFolders.Text = "";
                    this.textBoxDest.Text = this.webPackager.DestinationPath;
                    foreach (string str in this.webPackager.ExceptDirs)
                    {
                        this.textBoxExceptDir.AppendText(str + Environment.NewLine);
                    }
                    foreach (string str2 in this.webPackager.ExceptExtensions)
                    {
                        this.textBoxExceptExt.AppendText(str2 + Environment.NewLine);
                    }
                    foreach (string str3 in this.webPackager.AllFolders)
                    {
                        this.textBoxFolders.AppendText(str3 + Environment.NewLine);
                    }
                    this.textBoxSrc.Text = this.webPackager.SourcePath;
                    this.dateTimePickerLastUpdate.Value = this.webPackager.LastUpdateTime;
                    this.textBoxName.Text = this.webPackager.Name;
                    this.txtSiteId.Text = this.webPackager.WebId;
                    this.txtTheme.Text = this.webPackager.Theme;
                    this.chkAllFolders.Checked = this.webPackager.useAllFolders;
                }
            }
        }
    }
}

