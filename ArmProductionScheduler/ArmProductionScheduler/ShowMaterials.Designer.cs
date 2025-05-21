
namespace ArmProductionScheduler
{
    partial class ShowMaterials
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.showMaterialsDataGridView = new System.Windows.Forms.DataGridView();
            this.lblGetRecordIndicator = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.showMaterialsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // showMaterialsDataGridView
            // 
            this.showMaterialsDataGridView.AllowUserToAddRows = false;
            this.showMaterialsDataGridView.AllowUserToDeleteRows = false;
            this.showMaterialsDataGridView.AllowUserToOrderColumns = true;
            this.showMaterialsDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.showMaterialsDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.showMaterialsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showMaterialsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.showMaterialsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.showMaterialsDataGridView.Location = new System.Drawing.Point(12, 12);
            this.showMaterialsDataGridView.Name = "showMaterialsDataGridView";
            this.showMaterialsDataGridView.Size = new System.Drawing.Size(776, 377);
            this.showMaterialsDataGridView.TabIndex = 0;
            // 
            // lblGetRecordIndicator
            // 
            this.lblGetRecordIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGetRecordIndicator.AutoSize = true;
            this.lblGetRecordIndicator.Location = new System.Drawing.Point(12, 393);
            this.lblGetRecordIndicator.Name = "lblGetRecordIndicator";
            this.lblGetRecordIndicator.Size = new System.Drawing.Size(57, 13);
            this.lblGetRecordIndicator.TabIndex = 9;
            this.lblGetRecordIndicator.Text = "RowCount";
            // 
            // ShowMaterials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 415);
            this.Controls.Add(this.lblGetRecordIndicator);
            this.Controls.Add(this.showMaterialsDataGridView);
            this.Name = "ShowMaterials";
            this.Text = "Show Material List";
            ((System.ComponentModel.ISupportInitialize)(this.showMaterialsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView showMaterialsDataGridView;
        private System.Windows.Forms.Label lblGetRecordIndicator;
    }
}