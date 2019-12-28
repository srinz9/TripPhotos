namespace Sri.TripPhotos
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSource = new System.Windows.Forms.Button();
            this.folderBrowserSource = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDestination = new System.Windows.Forms.FolderBrowserDialog();
            this.btnStart = new System.Windows.Forms.Button();
            this.richTextFailed = new System.Windows.Forms.RichTextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkProcessJpegs = new System.Windows.Forms.CheckBox();
            this.chkProcessVideos = new System.Windows.Forms.CheckBox();
            this.chkPrefixYear = new System.Windows.Forms.CheckBox();
            this.chkConvertHEIC = new System.Windows.Forms.CheckBox();
            this.chkProcessHEIC = new System.Windows.Forms.CheckBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSource.Location = new System.Drawing.Point(65, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(608, 23);
            this.txtSource.TabIndex = 0;
            this.txtSource.TextChanged += new System.EventHandler(this.txtSource_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source";
            // 
            // btnSource
            // 
            this.btnSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSource.Location = new System.Drawing.Point(679, 12);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(26, 22);
            this.btnSource.TabIndex = 1;
            this.btnSource.Text = "...";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // folderBrowserSource
            // 
            this.folderBrowserSource.ShowNewFolderButton = false;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(637, 89);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(68, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // richTextFailed
            // 
            this.richTextFailed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextFailed.Location = new System.Drawing.Point(12, 114);
            this.richTextFailed.Name = "richTextFailed";
            this.richTextFailed.ReadOnly = true;
            this.richTextFailed.Size = new System.Drawing.Size(693, 257);
            this.richTextFailed.TabIndex = 7;
            this.richTextFailed.TabStop = false;
            this.richTextFailed.Text = "";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 92);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(619, 16);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 8;
            this.progressBar.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMinutes);
            this.groupBox1.Controls.Add(this.txtHours);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(516, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 44);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add";
            // 
            // txtMinutes
            // 
            this.txtMinutes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinutes.Location = new System.Drawing.Point(135, 15);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(46, 23);
            this.txtMinutes.TabIndex = 3;
            this.txtMinutes.Text = "0";
            // 
            // txtHours
            // 
            this.txtHours.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHours.Location = new System.Drawing.Point(37, 15);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(46, 23);
            this.txtHours.TabIndex = 2;
            this.txtHours.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(96, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Mins";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Hrs";
            // 
            // chkProcessJpegs
            // 
            this.chkProcessJpegs.AutoSize = true;
            this.chkProcessJpegs.Checked = true;
            this.chkProcessJpegs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProcessJpegs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkProcessJpegs.Location = new System.Drawing.Point(65, 42);
            this.chkProcessJpegs.Name = "chkProcessJpegs";
            this.chkProcessJpegs.Size = new System.Drawing.Size(83, 19);
            this.chkProcessJpegs.TabIndex = 14;
            this.chkProcessJpegs.Text = "Process jpg";
            this.chkProcessJpegs.UseVisualStyleBackColor = true;
            this.chkProcessJpegs.CheckedChanged += new System.EventHandler(this.chkProcess_CheckedChanged);
            // 
            // chkProcessVideos
            // 
            this.chkProcessVideos.AutoSize = true;
            this.chkProcessVideos.Checked = true;
            this.chkProcessVideos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProcessVideos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkProcessVideos.Location = new System.Drawing.Point(264, 41);
            this.chkProcessVideos.Name = "chkProcessVideos";
            this.chkProcessVideos.Size = new System.Drawing.Size(205, 19);
            this.chkProcessVideos.TabIndex = 15;
            this.chkProcessVideos.Text = "Process Videos (avi,mp4,mov,wav)";
            this.chkProcessVideos.UseVisualStyleBackColor = true;
            this.chkProcessVideos.CheckedChanged += new System.EventHandler(this.chkProcess_CheckedChanged);
            // 
            // chkPrefixYear
            // 
            this.chkPrefixYear.AutoSize = true;
            this.chkPrefixYear.Checked = true;
            this.chkPrefixYear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrefixYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPrefixYear.Location = new System.Drawing.Point(65, 67);
            this.chkPrefixYear.Name = "chkPrefixYear";
            this.chkPrefixYear.Size = new System.Drawing.Size(78, 19);
            this.chkPrefixYear.TabIndex = 17;
            this.chkPrefixYear.Text = "Prefix Year";
            this.chkPrefixYear.UseVisualStyleBackColor = true;
            // 
            // chkConvertHEIC
            // 
            this.chkConvertHEIC.AutoSize = true;
            this.chkConvertHEIC.Checked = true;
            this.chkConvertHEIC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConvertHEIC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkConvertHEIC.Location = new System.Drawing.Point(166, 67);
            this.chkConvertHEIC.Name = "chkConvertHEIC";
            this.chkConvertHEIC.Size = new System.Drawing.Size(128, 19);
            this.chkConvertHEIC.TabIndex = 18;
            this.chkConvertHEIC.Text = "Convert HEIC to jpg";
            this.chkConvertHEIC.UseVisualStyleBackColor = true;
            // 
            // chkProcessHEIC
            // 
            this.chkProcessHEIC.AutoSize = true;
            this.chkProcessHEIC.Checked = true;
            this.chkProcessHEIC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProcessHEIC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkProcessHEIC.Location = new System.Drawing.Point(166, 42);
            this.chkProcessHEIC.Name = "chkProcessHEIC";
            this.chkProcessHEIC.Size = new System.Drawing.Size(92, 19);
            this.chkProcessHEIC.TabIndex = 19;
            this.chkProcessHEIC.Text = "Process HEIC";
            this.chkProcessHEIC.UseVisualStyleBackColor = true;
            this.chkProcessHEIC.CheckedChanged += new System.EventHandler(this.ChkProcessHEIC_CheckedChanged);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(660, 374);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 15);
            this.lblVersion.TabIndex = 20;
            this.lblVersion.Text = "Version";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 391);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.chkProcessHEIC);
            this.Controls.Add(this.chkConvertHEIC);
            this.Controls.Add(this.chkPrefixYear);
            this.Controls.Add(this.chkProcessVideos);
            this.Controls.Add(this.chkProcessJpegs);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.richTextFailed);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSource);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trip Photos";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserSource;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDestination;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RichTextBox richTextFailed;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMinutes;
        private System.Windows.Forms.TextBox txtHours;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkProcessJpegs;
        private System.Windows.Forms.CheckBox chkProcessVideos;
        private System.Windows.Forms.CheckBox chkPrefixYear;
        private System.Windows.Forms.CheckBox chkConvertHEIC;
        private System.Windows.Forms.CheckBox chkProcessHEIC;
        private System.Windows.Forms.Label lblVersion;
    }
}

