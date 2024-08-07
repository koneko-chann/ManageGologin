namespace ManageGologin
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            GologinProfiles = new DataGridView();
            Selected = new DataGridViewCheckBoxColumn();
            sTTDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            profileNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            proxyDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dataPathDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            RunBtn = new DataGridViewButtonColumn();
            profilesBindingSource = new BindingSource(components);
            label1 = new Label();
            gologinPath = new TextBox();
            PathInputBtn = new Button();
            ExitProgramBtn = new Button();
            btnJsFolder = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)GologinProfiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)profilesBindingSource).BeginInit();
            SuspendLayout();
            // 
            // GologinProfiles
            // 
            GologinProfiles.AutoGenerateColumns = false;
            GologinProfiles.BorderStyle = BorderStyle.Fixed3D;
            GologinProfiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GologinProfiles.Columns.AddRange(new DataGridViewColumn[] { Selected, sTTDataGridViewTextBoxColumn, profileNameDataGridViewTextBoxColumn, proxyDataGridViewTextBoxColumn, dataPathDataGridViewTextBoxColumn, Status, RunBtn });
            GologinProfiles.DataSource = profilesBindingSource;
            GologinProfiles.Location = new Point(12, 199);
            GologinProfiles.Name = "GologinProfiles";
            GologinProfiles.Size = new Size(1009, 328);
            GologinProfiles.TabIndex = 0;
            // 
            // Selected
            // 
            Selected.HeaderText = "Select";
            Selected.Name = "Selected";
            Selected.Resizable = DataGridViewTriState.True;
            Selected.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // sTTDataGridViewTextBoxColumn
            // 
            sTTDataGridViewTextBoxColumn.DataPropertyName = "STT";
            sTTDataGridViewTextBoxColumn.HeaderText = "STT";
            sTTDataGridViewTextBoxColumn.Name = "sTTDataGridViewTextBoxColumn";
            // 
            // profileNameDataGridViewTextBoxColumn
            // 
            profileNameDataGridViewTextBoxColumn.DataPropertyName = "ProfileName";
            profileNameDataGridViewTextBoxColumn.HeaderText = "ProfileName";
            profileNameDataGridViewTextBoxColumn.Name = "profileNameDataGridViewTextBoxColumn";
            // 
            // proxyDataGridViewTextBoxColumn
            // 
            proxyDataGridViewTextBoxColumn.DataPropertyName = "Proxy";
            proxyDataGridViewTextBoxColumn.HeaderText = "Proxy";
            proxyDataGridViewTextBoxColumn.Name = "proxyDataGridViewTextBoxColumn";
            // 
            // dataPathDataGridViewTextBoxColumn
            // 
            dataPathDataGridViewTextBoxColumn.DataPropertyName = "DataPath";
            dataPathDataGridViewTextBoxColumn.HeaderText = "DataPath";
            dataPathDataGridViewTextBoxColumn.Name = "dataPathDataGridViewTextBoxColumn";
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.Name = "Status";
            Status.ReadOnly = true;
            // 
            // RunBtn
            // 
            RunBtn.HeaderText = "Action";
            RunBtn.Name = "RunBtn";
            RunBtn.Text = "Run";
            RunBtn.ToolTipText = "Run";
            // 
            // profilesBindingSource
            // 
            profilesBindingSource.DataSource = typeof(Models.Profiles);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(123, 15);
            label1.TabIndex = 1;
            label1.Text = "Path to gologin folder";
            // 
            // gologinPath
            // 
            gologinPath.Location = new Point(141, 9);
            gologinPath.Name = "gologinPath";
            gologinPath.Size = new Size(191, 23);
            gologinPath.TabIndex = 2;
            // 
            // PathInputBtn
            // 
            PathInputBtn.Location = new Point(338, 9);
            PathInputBtn.Name = "PathInputBtn";
            PathInputBtn.Size = new Size(75, 23);
            PathInputBtn.TabIndex = 3;
            PathInputBtn.Text = "Input";
            PathInputBtn.UseVisualStyleBackColor = true;
            PathInputBtn.Click += PathInputBtn_Click;
            // 
            // ExitProgramBtn
            // 
            ExitProgramBtn.Location = new Point(419, 9);
            ExitProgramBtn.Name = "ExitProgramBtn";
            ExitProgramBtn.Size = new Size(75, 23);
            ExitProgramBtn.TabIndex = 4;
            ExitProgramBtn.Text = "Quit";
            ExitProgramBtn.UseVisualStyleBackColor = true;
            ExitProgramBtn.Click += ExitProgramBtn_Click;
            // 
            // btnJsFolder
            // 
            btnJsFolder.Location = new Point(513, 8);
            btnJsFolder.Name = "btnJsFolder";
            btnJsFolder.Size = new Size(179, 23);
            btnJsFolder.TabIndex = 5;
            btnJsFolder.Text = "Choose javasript folder";
            btnJsFolder.UseVisualStyleBackColor = true;
            btnJsFolder.Click += btnJsFolder_Click;
            // 
            // button1
            // 
            button1.Location = new Point(698, 8);
            button1.Name = "button1";
            button1.Size = new Size(116, 23);
            button1.TabIndex = 6;
            button1.Text = "Choose webs file";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1033, 539);
            Controls.Add(button1);
            Controls.Add(btnJsFolder);
            Controls.Add(ExitProgramBtn);
            Controls.Add(PathInputBtn);
            Controls.Add(gologinPath);
            Controls.Add(label1);
            Controls.Add(GologinProfiles);
            Name = "Form1";
            Text = "Manage Gologin";
            ((System.ComponentModel.ISupportInitialize)GologinProfiles).EndInit();
            ((System.ComponentModel.ISupportInitialize)profilesBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView GologinProfiles;
        private Label label1;
        private TextBox gologinPath;
        private Button PathInputBtn;
        private BindingSource profilesBindingSource;
        private Button ExitProgramBtn;
        private DataGridViewCheckBoxColumn Selected;
        private DataGridViewTextBoxColumn sTTDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn profileNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn proxyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataPathDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewButtonColumn RunBtn;
        private Button btnJsFolder;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button button1;
    }
}
