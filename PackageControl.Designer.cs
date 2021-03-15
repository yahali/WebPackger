using System.Drawing;
using System.Windows.Forms;

namespace WebPackageTool
{
    partial class PackageControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxExceptDir = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxExceptExt = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxFolders = new System.Windows.Forms.TextBox();
            this.btnPackage = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxSrc = new System.Windows.Forms.TextBox();
            this.textBoxDest = new System.Windows.Forms.TextBox();
            this.dateTimePickerLastUpdate = new System.Windows.Forms.DateTimePicker();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnBrowSrc = new System.Windows.Forms.Button();
            this.btnBrowDest = new System.Windows.Forms.Button();
            this.txtSiteId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTheme = new System.Windows.Forms.TextBox();
            this.chkOnlyEditTime = new System.Windows.Forms.CheckBox();
            this.chkAllFolders = new System.Windows.Forms.CheckBox();
            this.txtboxLog = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "模版名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "源文件目录：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "目标目录：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "上次更新时间：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxExceptDir);
            this.groupBox1.Location = new System.Drawing.Point(270, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 183);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "排除的目录";
            // 
            // textBoxExceptDir
            // 
            this.textBoxExceptDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExceptDir.Location = new System.Drawing.Point(3, 17);
            this.textBoxExceptDir.Multiline = true;
            this.textBoxExceptDir.Name = "textBoxExceptDir";
            this.textBoxExceptDir.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxExceptDir.Size = new System.Drawing.Size(243, 163);
            this.textBoxExceptDir.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxExceptExt);
            this.groupBox2.Location = new System.Drawing.Point(24, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(222, 183);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "排除的后缀名";
            // 
            // textBoxExceptExt
            // 
            this.textBoxExceptExt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExceptExt.Location = new System.Drawing.Point(3, 17);
            this.textBoxExceptExt.Multiline = true;
            this.textBoxExceptExt.Name = "textBoxExceptExt";
            this.textBoxExceptExt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxExceptExt.Size = new System.Drawing.Size(216, 163);
            this.textBoxExceptExt.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.chkAllFolders);
            this.groupBox3.Location = new System.Drawing.Point(16, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(775, 224);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "打包设置";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxFolders);
            this.groupBox4.Location = new System.Drawing.Point(542, 59);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(227, 150);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "指定文件夹全部打包名称列表";
            // 
            // textBoxFolders
            // 
            this.textBoxFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFolders.Location = new System.Drawing.Point(3, 17);
            this.textBoxFolders.Multiline = true;
            this.textBoxFolders.Name = "textBoxFolders";
            this.textBoxFolders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFolders.Size = new System.Drawing.Size(221, 130);
            this.textBoxFolders.TabIndex = 0;
            // 
            // btnPackage
            // 
            this.btnPackage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPackage.Location = new System.Drawing.Point(558, 123);
            this.btnPackage.Name = "btnPackage";
            this.btnPackage.Size = new System.Drawing.Size(75, 23);
            this.btnPackage.TabIndex = 3;
            this.btnPackage.Text = "立即打包";
            this.btnPackage.UseVisualStyleBackColor = true;
            this.btnPackage.Click += new System.EventHandler(this.btnPackage_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(634, 123);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "存储模版";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(124, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            this.textBoxName.Size = new System.Drawing.Size(668, 21);
            this.textBoxName.TabIndex = 4;
            // 
            // textBoxSrc
            // 
            this.textBoxSrc.Location = new System.Drawing.Point(123, 33);
            this.textBoxSrc.Name = "textBoxSrc";
            this.textBoxSrc.Size = new System.Drawing.Size(587, 21);
            this.textBoxSrc.TabIndex = 4;
            // 
            // textBoxDest
            // 
            this.textBoxDest.Location = new System.Drawing.Point(123, 59);
            this.textBoxDest.Name = "textBoxDest";
            this.textBoxDest.Size = new System.Drawing.Size(587, 21);
            this.textBoxDest.TabIndex = 4;
            // 
            // dateTimePickerLastUpdate
            // 
            this.dateTimePickerLastUpdate.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateTimePickerLastUpdate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerLastUpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerLastUpdate.Location = new System.Drawing.Point(123, 123);
            this.dateTimePickerLastUpdate.Name = "dateTimePickerLastUpdate";
            this.dateTimePickerLastUpdate.Size = new System.Drawing.Size(262, 21);
            this.dateTimePickerLastUpdate.TabIndex = 5;
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(715, 123);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(76, 23);
            this.btnSaveAs.TabIndex = 3;
            this.btnSaveAs.Text = "另存为模版";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnBrowSrc
            // 
            this.btnBrowSrc.Location = new System.Drawing.Point(729, 33);
            this.btnBrowSrc.Name = "btnBrowSrc";
            this.btnBrowSrc.Size = new System.Drawing.Size(63, 23);
            this.btnBrowSrc.TabIndex = 6;
            this.btnBrowSrc.Text = "…";
            this.btnBrowSrc.UseVisualStyleBackColor = true;
            this.btnBrowSrc.Click += new System.EventHandler(this.btnBrowSrc_Click);
            // 
            // btnBrowDest
            // 
            this.btnBrowDest.Location = new System.Drawing.Point(729, 63);
            this.btnBrowDest.Name = "btnBrowDest";
            this.btnBrowDest.Size = new System.Drawing.Size(63, 23);
            this.btnBrowDest.TabIndex = 6;
            this.btnBrowDest.Text = "…";
            this.btnBrowDest.UseVisualStyleBackColor = true;
            this.btnBrowDest.Click += new System.EventHandler(this.btnBrowDest_Click);
            // 
            // txtSiteId
            // 
            this.txtSiteId.Location = new System.Drawing.Point(123, 91);
            this.txtSiteId.Name = "txtSiteId";
            this.txtSiteId.Size = new System.Drawing.Size(151, 21);
            this.txtSiteId.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "网站编号：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(284, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "模板名称：";
            // 
            // txtTheme
            // 
            this.txtTheme.Location = new System.Drawing.Point(349, 90);
            this.txtTheme.Name = "txtTheme";
            this.txtTheme.Size = new System.Drawing.Size(361, 21);
            this.txtTheme.TabIndex = 4;
            // 
            // chkOnlyEditTime
            // 
            this.chkOnlyEditTime.AutoSize = true;
            this.chkOnlyEditTime.Location = new System.Drawing.Point(404, 125);
            this.chkOnlyEditTime.Margin = new System.Windows.Forms.Padding(2);
            this.chkOnlyEditTime.Name = "chkOnlyEditTime";
            this.chkOnlyEditTime.Size = new System.Drawing.Size(84, 16);
            this.chkOnlyEditTime.TabIndex = 7;
            this.chkOnlyEditTime.Text = "仅更新时间";
            this.chkOnlyEditTime.UseVisualStyleBackColor = true;
            // 
            // chkAllFolders
            // 
            this.chkAllFolders.AutoSize = true;
            this.chkAllFolders.Location = new System.Drawing.Point(545, 38);
            this.chkAllFolders.Margin = new System.Windows.Forms.Padding(2);
            this.chkAllFolders.Name = "chkAllFolders";
            this.chkAllFolders.Size = new System.Drawing.Size(48, 16);
            this.chkAllFolders.TabIndex = 9;
            this.chkAllFolders.Text = "启用";
            this.chkAllFolders.UseVisualStyleBackColor = true;
            this.chkAllFolders.CheckedChanged += new System.EventHandler(this.chkAllFolders_CheckedChanged);
            // 
            // txtboxLog
            // 
            this.txtboxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtboxLog.Location = new System.Drawing.Point(16, 401);
            this.txtboxLog.Multiline = true;
            this.txtboxLog.Name = "txtboxLog";
            this.txtboxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtboxLog.Size = new System.Drawing.Size(776, 381);
            this.txtboxLog.TabIndex = 10;
            this.txtboxLog.Text = "生成日志：";
            // 
            // PackageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtboxLog);
            this.Controls.Add(this.chkOnlyEditTime);
            this.Controls.Add(this.btnBrowDest);
            this.Controls.Add(this.btnBrowSrc);
            this.Controls.Add(this.dateTimePickerLastUpdate);
            this.Controls.Add(this.txtTheme);
            this.Controls.Add(this.txtSiteId);
            this.Controls.Add(this.textBoxDest);
            this.Controls.Add(this.textBoxSrc);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPackage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Name = "PackageControl";
            this.Size = new System.Drawing.Size(808, 800);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxExceptDir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxExceptExt;
        private System.Windows.Forms.TextBox textBoxFolders;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPackage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxSrc;
        private System.Windows.Forms.TextBox textBoxDest;
        private System.Windows.Forms.DateTimePicker dateTimePickerLastUpdate;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnBrowSrc;
        private System.Windows.Forms.Button btnBrowDest;
        private System.Windows.Forms.TextBox txtSiteId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTheme;
        private System.Windows.Forms.CheckBox chkOnlyEditTime;
        private CheckBox chkAllFolders;
        private TextBox txtboxLog;
    }
}
