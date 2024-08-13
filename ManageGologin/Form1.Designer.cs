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
            dataPathDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            Proxy = new DataGridViewTextBoxColumn();
            Run = new DataGridViewButtonColumn();
            Stop = new DataGridViewButtonColumn();
            RunWithProxy = new DataGridViewButtonColumn();
            profilesBindingSource = new BindingSource(components);
            label1 = new Label();
            gologinPath = new TextBox();
            PathInputBtn = new Button();
            ExitProgramBtn = new Button();
            btnJsFolder = new Button();
            button1 = new Button();
            useScriptCheckbox = new CheckBox();
            startupWebtxtbox = new TextBox();
            label2 = new Label();
            proxyInsertBtn = new Button();
            checkProxyBtn = new Button();
            selectAllBtn = new RadioButton();
            itemsPerPageCbx = new ComboBox();
            label3 = new Label();
            proxyOpenBtn = new RadioButton();
            RunBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)GologinProfiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)profilesBindingSource).BeginInit();
            SuspendLayout();
            // 
            // GologinProfiles
            // 
            GologinProfiles.AutoGenerateColumns = false;
            GologinProfiles.BorderStyle = BorderStyle.Fixed3D;
            GologinProfiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GologinProfiles.Columns.AddRange(new DataGridViewColumn[] { Selected, sTTDataGridViewTextBoxColumn, profileNameDataGridViewTextBoxColumn, dataPathDataGridViewTextBoxColumn, Status, Proxy, Run, Stop, RunWithProxy });
            GologinProfiles.DataSource = profilesBindingSource;
            GologinProfiles.Location = new Point(12, 199);
            GologinProfiles.Name = "GologinProfiles";
            GologinProfiles.Size = new Size(1061, 366);
            GologinProfiles.TabIndex = 0;
            GologinProfiles.CellContentClick += GologinProfiles_CellContentClick;
            GologinProfiles.CellFormatting += GologinProfiles_CellFormatting;
            GologinProfiles.DataBindingComplete += GologinProfiles_DataBindingComplete;
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
            Status.Resizable = DataGridViewTriState.True;
            // 
            // Proxy
            // 
            Proxy.DataPropertyName = "Proxy";
            Proxy.HeaderText = "Proxy";
            Proxy.Name = "Proxy";
            // 
            // Run
            // 
            Run.HeaderText = "Run";
            Run.Name = "Run";
            // 
            // Stop
            // 
            Stop.HeaderText = "Stop";
            Stop.Name = "Stop";
            // 
            // RunWithProxy
            // 
            RunWithProxy.HeaderText = "Run With Proxy";
            RunWithProxy.Name = "RunWithProxy";
            RunWithProxy.Resizable = DataGridViewTriState.True;
            RunWithProxy.SortMode = DataGridViewColumnSortMode.Automatic;
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
            btnJsFolder.Enabled = false;
            btnJsFolder.Location = new Point(842, 22);
            btnJsFolder.Name = "btnJsFolder";
            btnJsFolder.Size = new Size(179, 23);
            btnJsFolder.TabIndex = 5;
            btnJsFolder.Text = "Choose javasript folder";
            btnJsFolder.UseVisualStyleBackColor = true;
            btnJsFolder.Visible = false;
            btnJsFolder.Click += btnJsFolder_Click;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(842, 51);
            button1.Name = "button1";
            button1.Size = new Size(179, 23);
            button1.TabIndex = 6;
            button1.Text = "Choose webs file";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // useScriptCheckbox
            // 
            useScriptCheckbox.AutoSize = true;
            useScriptCheckbox.Location = new Point(753, 25);
            useScriptCheckbox.Name = "useScriptCheckbox";
            useScriptCheckbox.Size = new Size(78, 19);
            useScriptCheckbox.TabIndex = 7;
            useScriptCheckbox.Text = "Use Script";
            useScriptCheckbox.UseVisualStyleBackColor = true;
            useScriptCheckbox.CheckedChanged += useScriptCheckbox_CheckedChanged;
            // 
            // startupWebtxtbox
            // 
            startupWebtxtbox.Location = new Point(141, 51);
            startupWebtxtbox.Name = "startupWebtxtbox";
            startupWebtxtbox.Size = new Size(191, 23);
            startupWebtxtbox.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 9;
            label2.Text = "Startup site";
            // 
            // proxyInsertBtn
            // 
            proxyInsertBtn.Location = new Point(894, 170);
            proxyInsertBtn.Name = "proxyInsertBtn";
            proxyInsertBtn.Size = new Size(179, 23);
            proxyInsertBtn.TabIndex = 10;
            proxyInsertBtn.Text = "Insert Multi Proxies";
            proxyInsertBtn.UseVisualStyleBackColor = true;
            proxyInsertBtn.Click += proxyInsertBtn_Click;
            // 
            // checkProxyBtn
            // 
            checkProxyBtn.Location = new Point(894, 141);
            checkProxyBtn.Name = "checkProxyBtn";
            checkProxyBtn.Size = new Size(179, 23);
            checkProxyBtn.TabIndex = 11;
            checkProxyBtn.Text = "Check Proxies";
            checkProxyBtn.UseVisualStyleBackColor = true;
            checkProxyBtn.Click += checkProxyBtn_Click;
            // 
            // selectAllBtn
            // 
            selectAllBtn.AutoSize = true;
            selectAllBtn.Location = new Point(12, 174);
            selectAllBtn.Name = "selectAllBtn";
            selectAllBtn.Size = new Size(73, 19);
            selectAllBtn.TabIndex = 12;
            selectAllBtn.TabStop = true;
            selectAllBtn.Text = "Select All";
            selectAllBtn.UseVisualStyleBackColor = true;
            selectAllBtn.CheckedChanged += selectAllBtn_CheckedChanged;
            // 
            // itemsPerPageCbx
            // 
            itemsPerPageCbx.FormattingEnabled = true;
            itemsPerPageCbx.Items.AddRange(new object[] { "1", "2", "3" });
            itemsPerPageCbx.Location = new Point(12, 145);
            itemsPerPageCbx.Name = "itemsPerPageCbx";
            itemsPerPageCbx.Size = new Size(121, 23);
            itemsPerPageCbx.TabIndex = 13;
            itemsPerPageCbx.SelectedIndexChanged += itemsPerPageCbx_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 127);
            label3.Name = "label3";
            label3.Size = new Size(183, 15);
            label3.TabIndex = 14;
            label3.Text = "Select number of profile per page";
            // 
            // proxyOpenBtn
            // 
            proxyOpenBtn.AutoSize = true;
            proxyOpenBtn.Location = new Point(91, 174);
            proxyOpenBtn.Name = "proxyOpenBtn";
            proxyOpenBtn.Size = new Size(113, 19);
            proxyOpenBtn.TabIndex = 15;
            proxyOpenBtn.TabStop = true;
            proxyOpenBtn.Text = "Open with proxy";
            proxyOpenBtn.UseVisualStyleBackColor = true;
            // 
            // RunBtn
            // 
            RunBtn.Location = new Point(738, 170);
            RunBtn.Name = "RunBtn";
            RunBtn.Size = new Size(75, 23);
            RunBtn.TabIndex = 16;
            RunBtn.Text = "Start";
            RunBtn.UseVisualStyleBackColor = true;
            RunBtn.Click += RunBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1085, 577);
            Controls.Add(RunBtn);
            Controls.Add(proxyOpenBtn);
            Controls.Add(label3);
            Controls.Add(itemsPerPageCbx);
            Controls.Add(selectAllBtn);
            Controls.Add(checkProxyBtn);
            Controls.Add(proxyInsertBtn);
            Controls.Add(label2);
            Controls.Add(startupWebtxtbox);
            Controls.Add(useScriptCheckbox);
            Controls.Add(button1);
            Controls.Add(btnJsFolder);
            Controls.Add(ExitProgramBtn);
            Controls.Add(PathInputBtn);
            Controls.Add(gologinPath);
            Controls.Add(label1);
            Controls.Add(GologinProfiles);
            Name = "Form1";
            Text = "Manage Gologin";
            Load += Form1_Load;
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
        private Button ExitProgramBtn;
        private Button btnJsFolder;
        private Button button1;
        private CheckBox useScriptCheckbox;
        private TextBox startupWebtxtbox;
        private Label label2;
        private Button proxyInsertBtn;
        private Button checkProxyBtn;
        private BindingSource profilesBindingSource;
        private RadioButton selectAllBtn;
        private ComboBox itemsPerPageCbx;
        private Label label3;
        private DataGridViewCheckBoxColumn Selected;
        private DataGridViewTextBoxColumn sTTDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn profileNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataPathDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn Proxy;
        private DataGridViewButtonColumn Run;
        private DataGridViewButtonColumn Stop;
        private DataGridViewButtonColumn RunWithProxy;
        private RadioButton proxyOpenBtn;
        private Button RunBtn;
    }
}
