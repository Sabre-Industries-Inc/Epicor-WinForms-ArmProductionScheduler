
namespace ArmProductionScheduler
{
    partial class ShowGroupBySO
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.showGroupByDataGridView = new System.Windows.Forms.DataGridView();
            this.lblGetRecordIndicator = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.showGroupByDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // showGroupByDataGridView
            // 
            this.showGroupByDataGridView.AllowUserToAddRows = false;
            this.showGroupByDataGridView.AllowUserToDeleteRows = false;
            this.showGroupByDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.showGroupByDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.showGroupByDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showGroupByDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.showGroupByDataGridView.Location = new System.Drawing.Point(12, 12);
            this.showGroupByDataGridView.Name = "showGroupByDataGridView";
            this.showGroupByDataGridView.Size = new System.Drawing.Size(776, 377);
            this.showGroupByDataGridView.TabIndex = 0;
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
            // ShowGroupBySO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 415);
            this.Controls.Add(this.lblGetRecordIndicator);
            this.Controls.Add(this.showGroupByDataGridView);
            this.Name = "ShowGroupBySO";
            this.Text = "Show Group By SO";
            ((System.ComponentModel.ISupportInitialize)(this.showGroupByDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView showGroupByDataGridView;
        private System.Windows.Forms.Label lblGetRecordIndicator;
    }
}