
namespace ArmProductionScheduler
{
    partial class ArmProductionScheduler
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.uJobFilter = new System.Windows.Forms.GroupBox();
            this.cbSchedulingQue = new System.Windows.Forms.ComboBox();
            this.lblSchedulingQue = new System.Windows.Forms.Label();
            this.cbCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.btnJobGetData = new System.Windows.Forms.Button();
            this.txtSONum = new System.Windows.Forms.TextBox();
            this.txtPartNum = new System.Windows.Forms.TextBox();
            this.lblSONum = new System.Windows.Forms.Label();
            this.lblPartNum = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblFrRecordIndicator = new System.Windows.Forms.Label();
            this.frJobDataGridView = new System.Windows.Forms.DataGridView();
            this.lblFrSchedulingOption = new System.Windows.Forms.Label();
            this.lblFrSchedulingQue = new System.Windows.Forms.Label();
            this.BtnAssignToSchedulingQue = new System.Windows.Forms.Button();
            this.lblToRecordIndicator = new System.Windows.Forms.Label();
            this.toJobDataGridView = new System.Windows.Forms.DataGridView();
            this.lblToSchedulingOption = new System.Windows.Forms.Label();
            this.lblToSchedulingQue = new System.Windows.Forms.Label();
            this.frSchQueContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.frSelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toSchQueContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBySOStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.userToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.machineToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.plantToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.environmentToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.showMaterialsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMaterialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frShowMaterialContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.frShowMtlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uJobFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frJobDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toJobDataGridView)).BeginInit();
            this.frSchQueContextMenuStrip.SuspendLayout();
            this.toSchQueContextMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.showMaterialsContextMenuStrip.SuspendLayout();
            this.frShowMaterialContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // uJobFilter
            // 
            this.uJobFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uJobFilter.Controls.Add(this.cbSchedulingQue);
            this.uJobFilter.Controls.Add(this.lblSchedulingQue);
            this.uJobFilter.Controls.Add(this.cbCustomer);
            this.uJobFilter.Controls.Add(this.lblCustomer);
            this.uJobFilter.Controls.Add(this.btnResetFilter);
            this.uJobFilter.Controls.Add(this.btnJobGetData);
            this.uJobFilter.Controls.Add(this.txtSONum);
            this.uJobFilter.Controls.Add(this.txtPartNum);
            this.uJobFilter.Controls.Add(this.lblSONum);
            this.uJobFilter.Controls.Add(this.lblPartNum);
            this.uJobFilter.Location = new System.Drawing.Point(12, 12);
            this.uJobFilter.Name = "uJobFilter";
            this.uJobFilter.Size = new System.Drawing.Size(963, 98);
            this.uJobFilter.TabIndex = 2;
            this.uJobFilter.TabStop = false;
            this.uJobFilter.Text = "Filters";
            // 
            // cbSchedulingQue
            // 
            this.cbSchedulingQue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSchedulingQue.FormattingEnabled = true;
            this.cbSchedulingQue.Items.AddRange(new object[] {
            "Unfirm Jobs",
            "Jobs Ready for Production",
            "Jobs Scheduled for Production",
            "Jobs Completed through Production"});
            this.cbSchedulingQue.Location = new System.Drawing.Point(387, 56);
            this.cbSchedulingQue.Name = "cbSchedulingQue";
            this.cbSchedulingQue.Size = new System.Drawing.Size(247, 21);
            this.cbSchedulingQue.TabIndex = 21;
            // 
            // lblSchedulingQue
            // 
            this.lblSchedulingQue.AutoSize = true;
            this.lblSchedulingQue.Location = new System.Drawing.Point(281, 56);
            this.lblSchedulingQue.Name = "lblSchedulingQue";
            this.lblSchedulingQue.Size = new System.Drawing.Size(98, 13);
            this.lblSchedulingQue.TabIndex = 20;
            this.lblSchedulingQue.Text = "Scheduling Queue:";
            // 
            // cbCustomer
            // 
            this.cbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCustomer.FormattingEnabled = true;
            this.cbCustomer.Location = new System.Drawing.Point(341, 26);
            this.cbCustomer.Name = "cbCustomer";
            this.cbCustomer.Size = new System.Drawing.Size(293, 21);
            this.cbCustomer.TabIndex = 17;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(281, 26);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(54, 13);
            this.lblCustomer.TabIndex = 16;
            this.lblCustomer.Text = "Customer:";
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Location = new System.Drawing.Point(640, 26);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(166, 23);
            this.btnResetFilter.TabIndex = 8;
            this.btnResetFilter.Text = "Reset Filter";
            this.btnResetFilter.UseVisualStyleBackColor = true;
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);
            // 
            // btnJobGetData
            // 
            this.btnJobGetData.Location = new System.Drawing.Point(640, 56);
            this.btnJobGetData.Name = "btnJobGetData";
            this.btnJobGetData.Size = new System.Drawing.Size(166, 23);
            this.btnJobGetData.TabIndex = 7;
            this.btnJobGetData.Text = "Get Data";
            this.btnJobGetData.UseVisualStyleBackColor = true;
            this.btnJobGetData.Click += new System.EventHandler(this.btnJobGetData_Click);
            // 
            // txtSONum
            // 
            this.txtSONum.Location = new System.Drawing.Point(50, 51);
            this.txtSONum.Name = "txtSONum";
            this.txtSONum.Size = new System.Drawing.Size(207, 20);
            this.txtSONum.TabIndex = 5;
            this.txtSONum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSONum_KeyPress);
            // 
            // txtPartNum
            // 
            this.txtPartNum.Location = new System.Drawing.Point(51, 26);
            this.txtPartNum.Name = "txtPartNum";
            this.txtPartNum.Size = new System.Drawing.Size(207, 20);
            this.txtPartNum.TabIndex = 3;
            this.txtPartNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPartNum_KeyPress);
            // 
            // lblSONum
            // 
            this.lblSONum.AutoSize = true;
            this.lblSONum.Location = new System.Drawing.Point(9, 56);
            this.lblSONum.Name = "lblSONum";
            this.lblSONum.Size = new System.Drawing.Size(35, 13);
            this.lblSONum.TabIndex = 1;
            this.lblSONum.Text = "SO #:";
            // 
            // lblPartNum
            // 
            this.lblPartNum.AutoSize = true;
            this.lblPartNum.Location = new System.Drawing.Point(10, 29);
            this.lblPartNum.Name = "lblPartNum";
            this.lblPartNum.Size = new System.Drawing.Size(39, 13);
            this.lblPartNum.TabIndex = 0;
            this.lblPartNum.Text = "Part #:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 116);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblFrRecordIndicator);
            this.splitContainer1.Panel1.Controls.Add(this.frJobDataGridView);
            this.splitContainer1.Panel1.Controls.Add(this.lblFrSchedulingOption);
            this.splitContainer1.Panel1.Controls.Add(this.lblFrSchedulingQue);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.BtnAssignToSchedulingQue);
            this.splitContainer1.Panel2.Controls.Add(this.lblToRecordIndicator);
            this.splitContainer1.Panel2.Controls.Add(this.toJobDataGridView);
            this.splitContainer1.Panel2.Controls.Add(this.lblToSchedulingOption);
            this.splitContainer1.Panel2.Controls.Add(this.lblToSchedulingQue);
            this.splitContainer1.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Size = new System.Drawing.Size(963, 400);
            this.splitContainer1.SplitterDistance = 467;
            this.splitContainer1.TabIndex = 3;
            // 
            // lblFrRecordIndicator
            // 
            this.lblFrRecordIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFrRecordIndicator.AutoSize = true;
            this.lblFrRecordIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrRecordIndicator.Location = new System.Drawing.Point(3, 381);
            this.lblFrRecordIndicator.Name = "lblFrRecordIndicator";
            this.lblFrRecordIndicator.Size = new System.Drawing.Size(65, 13);
            this.lblFrRecordIndicator.TabIndex = 8;
            this.lblFrRecordIndicator.Text = "RowCount";
            // 
            // frJobDataGridView
            // 
            this.frJobDataGridView.AllowUserToAddRows = false;
            this.frJobDataGridView.AllowUserToDeleteRows = false;
            this.frJobDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.frJobDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.frJobDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.frJobDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.frJobDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.frJobDataGridView.DefaultCellStyle = dataGridViewCellStyle11;
            this.frJobDataGridView.Location = new System.Drawing.Point(3, 27);
            this.frJobDataGridView.Name = "frJobDataGridView";
            this.frJobDataGridView.Size = new System.Drawing.Size(450, 351);
            this.frJobDataGridView.TabIndex = 7;
            this.frJobDataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.frJobDataGridView_CellBeginEdit);
            this.frJobDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.frJobDataGridView_DataError);
            this.frJobDataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.frJobDataGridView_RowPostPaint);
            this.frJobDataGridView.SelectionChanged += new System.EventHandler(this.frJobDataGridView_SelectionChanged);
            this.frJobDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frJobDataGridView_KeyDown);
            // 
            // lblFrSchedulingOption
            // 
            this.lblFrSchedulingOption.AutoSize = true;
            this.lblFrSchedulingOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrSchedulingOption.Location = new System.Drawing.Point(107, 11);
            this.lblFrSchedulingOption.Name = "lblFrSchedulingOption";
            this.lblFrSchedulingOption.Size = new System.Drawing.Size(30, 13);
            this.lblFrSchedulingOption.TabIndex = 6;
            this.lblFrSchedulingOption.Text = "N/A";
            // 
            // lblFrSchedulingQue
            // 
            this.lblFrSchedulingQue.AutoSize = true;
            this.lblFrSchedulingQue.Location = new System.Drawing.Point(3, 11);
            this.lblFrSchedulingQue.Name = "lblFrSchedulingQue";
            this.lblFrSchedulingQue.Size = new System.Drawing.Size(98, 13);
            this.lblFrSchedulingQue.TabIndex = 5;
            this.lblFrSchedulingQue.Text = "Scheduling Queue:";
            // 
            // BtnAssignToSchedulingQue
            // 
            this.BtnAssignToSchedulingQue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnAssignToSchedulingQue.Location = new System.Drawing.Point(3, 161);
            this.BtnAssignToSchedulingQue.Name = "BtnAssignToSchedulingQue";
            this.BtnAssignToSchedulingQue.Size = new System.Drawing.Size(27, 23);
            this.BtnAssignToSchedulingQue.TabIndex = 13;
            this.BtnAssignToSchedulingQue.Text = ">>";
            this.BtnAssignToSchedulingQue.UseVisualStyleBackColor = true;
            this.BtnAssignToSchedulingQue.Click += new System.EventHandler(this.BtnAssignToSchedulingQue_Click);
            // 
            // lblToRecordIndicator
            // 
            this.lblToRecordIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblToRecordIndicator.AutoSize = true;
            this.lblToRecordIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToRecordIndicator.Location = new System.Drawing.Point(28, 381);
            this.lblToRecordIndicator.Name = "lblToRecordIndicator";
            this.lblToRecordIndicator.Size = new System.Drawing.Size(65, 13);
            this.lblToRecordIndicator.TabIndex = 12;
            this.lblToRecordIndicator.Text = "RowCount";
            // 
            // toJobDataGridView
            // 
            this.toJobDataGridView.AllowUserToAddRows = false;
            this.toJobDataGridView.AllowUserToDeleteRows = false;
            this.toJobDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toJobDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle12;
            this.toJobDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toJobDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.toJobDataGridView.Location = new System.Drawing.Point(31, 27);
            this.toJobDataGridView.Name = "toJobDataGridView";
            this.toJobDataGridView.Size = new System.Drawing.Size(450, 351);
            this.toJobDataGridView.TabIndex = 11;
            this.toJobDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.toJobDataGridView_CellContentClick);
            this.toJobDataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.toJobDataGridView_RowPostPaint);
            this.toJobDataGridView.SelectionChanged += new System.EventHandler(this.toJobDataGridView_SelectionChanged);
            // 
            // lblToSchedulingOption
            // 
            this.lblToSchedulingOption.AutoSize = true;
            this.lblToSchedulingOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToSchedulingOption.Location = new System.Drawing.Point(132, 11);
            this.lblToSchedulingOption.Name = "lblToSchedulingOption";
            this.lblToSchedulingOption.Size = new System.Drawing.Size(30, 13);
            this.lblToSchedulingOption.TabIndex = 10;
            this.lblToSchedulingOption.Text = "N/A";
            // 
            // lblToSchedulingQue
            // 
            this.lblToSchedulingQue.AutoSize = true;
            this.lblToSchedulingQue.Location = new System.Drawing.Point(28, 11);
            this.lblToSchedulingQue.Name = "lblToSchedulingQue";
            this.lblToSchedulingQue.Size = new System.Drawing.Size(98, 13);
            this.lblToSchedulingQue.TabIndex = 9;
            this.lblToSchedulingQue.Text = "Scheduling Queue:";
            // 
            // frSchQueContextMenuStrip
            // 
            this.frSchQueContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frSelectAllToolStripMenuItem});
            this.frSchQueContextMenuStrip.Name = "frSchQueContextMenuStrip";
            this.frSchQueContextMenuStrip.Size = new System.Drawing.Size(123, 26);
            // 
            // frSelectAllToolStripMenuItem
            // 
            this.frSelectAllToolStripMenuItem.Name = "frSelectAllToolStripMenuItem";
            this.frSelectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.frSelectAllToolStripMenuItem.Text = "Select All";
            this.frSelectAllToolStripMenuItem.Click += new System.EventHandler(this.frSelectAllToolStripMenuItem_Click);
            // 
            // toSchQueContextMenuStrip
            // 
            this.toSchQueContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.groupBySOStripMenuItem});
            this.toSchQueContextMenuStrip.Name = "toSchQueContextMenuStrip";
            this.toSchQueContextMenuStrip.Size = new System.Drawing.Size(142, 26);
            // 
            // groupBySOStripMenuItem
            // 
            this.groupBySOStripMenuItem.Name = "groupBySOStripMenuItem";
            this.groupBySOStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.groupBySOStripMenuItem.Text = "Group By SO";
            this.groupBySOStripMenuItem.Click += new System.EventHandler(this.groupBySOStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripStatusLabel,
            this.machineToolStripStatusLabel,
            this.plantToolStripStatusLabel,
            this.environmentToolStripStatusLabel,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 517);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(987, 24);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "StatusStrip";
            // 
            // userToolStripStatusLabel
            // 
            this.userToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.userToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.userToolStripStatusLabel.Name = "userToolStripStatusLabel";
            this.userToolStripStatusLabel.Size = new System.Drawing.Size(34, 19);
            this.userToolStripStatusLabel.Text = "User";
            // 
            // machineToolStripStatusLabel
            // 
            this.machineToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.machineToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.machineToolStripStatusLabel.Name = "machineToolStripStatusLabel";
            this.machineToolStripStatusLabel.Size = new System.Drawing.Size(57, 19);
            this.machineToolStripStatusLabel.Text = "Machine";
            // 
            // plantToolStripStatusLabel
            // 
            this.plantToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.plantToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.plantToolStripStatusLabel.Name = "plantToolStripStatusLabel";
            this.plantToolStripStatusLabel.Size = new System.Drawing.Size(38, 19);
            this.plantToolStripStatusLabel.Text = "Plant";
            // 
            // environmentToolStripStatusLabel
            // 
            this.environmentToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.environmentToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.environmentToolStripStatusLabel.Name = "environmentToolStripStatusLabel";
            this.environmentToolStripStatusLabel.Size = new System.Drawing.Size(79, 19);
            this.environmentToolStripStatusLabel.Text = "Environment";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(43, 19);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // showMaterialsContextMenuStrip
            // 
            this.showMaterialsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMaterialsToolStripMenuItem});
            this.showMaterialsContextMenuStrip.Name = "showMaterialsContextMenuStrip";
            this.showMaterialsContextMenuStrip.Size = new System.Drawing.Size(155, 26);
            this.showMaterialsContextMenuStrip.Click += new System.EventHandler(this.showMaterialsContextMenuStrip_Click);
            // 
            // showMaterialsToolStripMenuItem
            // 
            this.showMaterialsToolStripMenuItem.Name = "showMaterialsToolStripMenuItem";
            this.showMaterialsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showMaterialsToolStripMenuItem.Text = "Show Materials";
            // 
            // frShowMaterialContextMenuStrip
            // 
            this.frShowMaterialContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frShowMtlToolStripMenuItem});
            this.frShowMaterialContextMenuStrip.Name = "frShowMaterialContextMenuStrip";
            this.frShowMaterialContextMenuStrip.Size = new System.Drawing.Size(155, 26);
            // 
            // frShowMtlToolStripMenuItem
            // 
            this.frShowMtlToolStripMenuItem.Name = "frShowMtlToolStripMenuItem";
            this.frShowMtlToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.frShowMtlToolStripMenuItem.Text = "Show Materials";
            this.frShowMtlToolStripMenuItem.Click += new System.EventHandler(this.frShowMtlToolStripMenuItem_Click);
            // 
            // ArmProductionScheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 541);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.uJobFilter);
            this.Name = "ArmProductionScheduler";
            this.Text = "Arm Production Scheduler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArmProductionScheduler_FormClosing);
            this.Load += new System.EventHandler(this.ArmProductionScheduler_Load);
            this.uJobFilter.ResumeLayout(false);
            this.uJobFilter.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.frJobDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toJobDataGridView)).EndInit();
            this.frSchQueContextMenuStrip.ResumeLayout(false);
            this.toSchQueContextMenuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.showMaterialsContextMenuStrip.ResumeLayout(false);
            this.frShowMaterialContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox uJobFilter;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Button btnJobGetData;
        private System.Windows.Forms.TextBox txtSONum;
        private System.Windows.Forms.Label lblSONum;
        private System.Windows.Forms.Label lblPartNum;
        private System.Windows.Forms.TextBox txtPartNum;
        private System.Windows.Forms.ComboBox cbSchedulingQue;
        private System.Windows.Forms.Label lblSchedulingQue;
        private System.Windows.Forms.ComboBox cbCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblFrRecordIndicator;
        private System.Windows.Forms.DataGridView frJobDataGridView;
        private System.Windows.Forms.Label lblFrSchedulingOption;
        private System.Windows.Forms.Label lblFrSchedulingQue;
        private System.Windows.Forms.Button BtnAssignToSchedulingQue;
        private System.Windows.Forms.Label lblToRecordIndicator;
        private System.Windows.Forms.DataGridView toJobDataGridView;
        private System.Windows.Forms.Label lblToSchedulingOption;
        private System.Windows.Forms.Label lblToSchedulingQue;
        private System.Windows.Forms.ContextMenuStrip frSchQueContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip toSchQueContextMenuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel userToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel machineToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel plantToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel environmentToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem frSelectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupBySOStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip showMaterialsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showMaterialsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip frShowMaterialContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem frShowMtlToolStripMenuItem;
    }
}

