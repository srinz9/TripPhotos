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
            this.btnDestination = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.folderBrowserSource = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDestination = new System.Windows.Forms.FolderBrowserDialog();
            this.btnStart = new System.Windows.Forms.Button();
            this.richTextFailed = new System.Windows.Forms.RichTextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.chkBackup = new System.Windows.Forms.CheckBox();
            this.chkOpenFolders = new System.Windows.Forms.CheckBox();
            this.chkRenameSourceFiles = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSource.Location = new System.Drawing.Point(126, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(616, 23);
            this.txtSource.TabIndex = 0;
            this.txtSource.TextChanged += new System.EventHandler(this.txtSource_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source folder";
            // 
            // btnSource
            // 
            this.btnSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSource.Location = new System.Drawing.Point(743, 12);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(29, 22);
            this.btnSource.TabIndex = 1;
            this.btnSource.Text = "...";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // btnDestination
            // 
            this.btnDestination.Enabled = false;
            this.btnDestination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDestination.Location = new System.Drawing.Point(743, 41);
            this.btnDestination.Name = "btnDestination";
            this.btnDestination.Size = new System.Drawing.Size(29, 23);
            this.btnDestination.TabIndex = 4;
            this.btnDestination.Text = "...";
            this.btnDestination.UseVisualStyleBackColor = true;
            this.btnDestination.Click += new System.EventHandler(this.btnDestination_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination folder";
            // 
            // txtDestination
            // 
            this.txtDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestination.Enabled = false;
            this.txtDestination.Location = new System.Drawing.Point(126, 41);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(616, 23);
            this.txtDestination.TabIndex = 3;
            this.txtDestination.TextChanged += new System.EventHandler(this.txtDestination_Changed);
            // 
            // folderBrowserSource
            // 
            this.folderBrowserSource.ShowNewFolderButton = false;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(704, 200);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(68, 27);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // richTextFailed
            // 
            this.richTextFailed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextFailed.Location = new System.Drawing.Point(12, 256);
            this.richTextFailed.Name = "richTextFailed";
            this.richTextFailed.ReadOnly = true;
            this.richTextFailed.Size = new System.Drawing.Size(760, 276);
            this.richTextFailed.TabIndex = 7;
            this.richTextFailed.TabStop = false;
            this.richTextFailed.Text = "";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 233);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(760, 17);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 8;
            this.progressBar.Visible = false;
            // 
            // chkBackup
            // 
            this.chkBackup.AutoSize = true;
            this.chkBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBackup.Location = new System.Drawing.Point(126, 175);
            this.chkBackup.Name = "chkBackup";
            this.chkBackup.Size = new System.Drawing.Size(142, 19);
            this.chkBackup.TabIndex = 7;
            this.chkBackup.Text = "Backup processed files";
            this.chkBackup.UseVisualStyleBackColor = true;
            // 
            // chkOpenFolders
            // 
            this.chkOpenFolders.AutoSize = true;
            this.chkOpenFolders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOpenFolders.Location = new System.Drawing.Point(126, 200);
            this.chkOpenFolders.Name = "chkOpenFolders";
            this.chkOpenFolders.Size = new System.Drawing.Size(206, 19);
            this.chkOpenFolders.TabIndex = 9;
            this.chkOpenFolders.Text = "Open Source && Destination folders";
            this.chkOpenFolders.UseVisualStyleBackColor = true;
            // 
            // chkRenameSourceFiles
            // 
            this.chkRenameSourceFiles.AutoSize = true;
            this.chkRenameSourceFiles.Checked = true;
            this.chkRenameSourceFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRenameSourceFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRenameSourceFiles.Location = new System.Drawing.Point(126, 150);
            this.chkRenameSourceFiles.Name = "chkRenameSourceFiles";
            this.chkRenameSourceFiles.Size = new System.Drawing.Size(189, 19);
            this.chkRenameSourceFiles.TabIndex = 10;
            this.chkRenameSourceFiles.Text = "Rename at source (don\'t move)";
            this.chkRenameSourceFiles.UseVisualStyleBackColor = true;
            this.chkRenameSourceFiles.CheckedChanged += new System.EventHandler(this.chkRenameSourceFiles_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(126, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(380, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "(all files, including the ones in sub-directories, will move into this folder)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMinutes);
            this.groupBox1.Controls.Add(this.txtHours);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(618, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 94);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add";
            // 
            // txtMinutes
            // 
            this.txtMinutes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinutes.Location = new System.Drawing.Point(67, 53);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(73, 23);
            this.txtMinutes.TabIndex = 3;
            this.txtMinutes.Text = "0";
            // 
            // txtHours
            // 
            this.txtHours.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHours.Location = new System.Drawing.Point(67, 24);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(73, 23);
            this.txtHours.TabIndex = 2;
            this.txtHours.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Minutes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Hours";
            // 
            // chkProcessJpegs
            // 
            this.chkProcessJpegs.AutoSize = true;
            this.chkProcessJpegs.Checked = true;
            this.chkProcessJpegs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProcessJpegs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkProcessJpegs.Location = new System.Drawing.Point(126, 100);
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
            this.chkProcessVideos.Location = new System.Drawing.Point(222, 100);
            this.chkProcessVideos.Name = "chkProcessVideos";
            this.chkProcessVideos.Size = new System.Drawing.Size(214, 19);
            this.chkProcessVideos.TabIndex = 15;
            this.chkProcessVideos.Text = "Process Videos (avi, mp4, mov, wav)";
            this.chkProcessVideos.UseVisualStyleBackColor = true;
            this.chkProcessVideos.CheckedChanged += new System.EventHandler(this.chkProcess_CheckedChanged);
            // 
            // chkPrefixYear
            // 
            this.chkPrefixYear.AutoSize = true;
            this.chkPrefixYear.Checked = true;
            this.chkPrefixYear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrefixYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPrefixYear.Location = new System.Drawing.Point(126, 125);
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
            this.chkConvertHEIC.Location = new System.Drawing.Point(442, 125);
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
            this.chkProcessHEIC.Location = new System.Drawing.Point(442, 100);
            this.chkProcessHEIC.Name = "chkProcessHEIC";
            this.chkProcessHEIC.Size = new System.Drawing.Size(92, 19);
            this.chkProcessHEIC.TabIndex = 19;
            this.chkProcessHEIC.Text = "Process HEIC";
            this.chkProcessHEIC.UseVisualStyleBackColor = true;
            this.chkProcessHEIC.CheckedChanged += new System.EventHandler(this.ChkProcessHEIC_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 544);
            this.Controls.Add(this.chkProcessHEIC);
            this.Controls.Add(this.chkConvertHEIC);
            this.Controls.Add(this.chkPrefixYear);
            this.Controls.Add(this.chkProcessVideos);
            this.Controls.Add(this.chkProcessJpegs);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkRenameSourceFiles);
            this.Controls.Add(this.chkOpenFolders);
            this.Controls.Add(this.chkBackup);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.richTextFailed);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnDestination);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDestination);
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
        private System.Windows.Forms.Button btnDestination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserSource;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDestination;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RichTextBox richTextFailed;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox chkBackup;
        private System.Windows.Forms.CheckBox chkOpenFolders;
        private System.Windows.Forms.CheckBox chkRenameSourceFiles;
        private System.Windows.Forms.Label label3;
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
    }
}

