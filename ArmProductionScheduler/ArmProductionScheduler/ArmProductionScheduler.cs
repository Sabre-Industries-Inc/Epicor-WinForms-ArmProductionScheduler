using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EpicConnector.Extensions;
using System.Reflection;
using System.IO;
using System.Threading;


namespace ArmProductionScheduler
{
    public partial class ArmProductionScheduler : Form
    {

        #region -- Fields --
        /// <summary>Database helper object.</summary>
        private Database database = null;

        /// <summary>The loaded data.</summary>
        private DataTable data = null;
        private DataTable data2 = null;

        private string plant = string.Empty;

        EpicConnector.Connection.Environments env;
        EpicConnector.Connection.Plants plt;

        #endregion -- Fields --

        #region -- Constructor --
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="environment">The epicor environment to use.</param>
        /// <param name="plant">The epicor plant to use.</param>
        public ArmProductionScheduler(EpicConnector.Connection.Environments environment, EpicConnector.Connection.Plants plant)
        {
            InitializeComponent();

            this.userToolStripStatusLabel.Text = Environment.UserName;
            this.machineToolStripStatusLabel.Text = Environment.MachineName;
            this.plantToolStripStatusLabel.Text = plant.ToString();
         

            this.Text += " - " + Assembly.GetExecutingAssembly().GetName().Version;

            //set the environment
            SetEnvironment(environment);

            this.plant = plant.ToString();
            this.env = environment;
            this.plt = plant;

        }

        #endregion -- Constructor --


        #region -- Events --

        /// <summary>
        /// Handles the form load event.
        /// It will pre-populate the data needed on the form.
        /// </summary>
        /// <param name="sender">See MSDN.</param>
        /// <param name="e">See MSDN.</param>
        private void ArmProductionScheduler_Load(object sender, EventArgs e)
        {
            EnableControls(false);

            Task.Factory.StartNew(() =>
            {
               
                  
                    SetStatus("Loading available customers...");
                    var customersData = this.database.GetAvailableCustomers();

                    this.Invoke(new Action(() =>
                    {
                        this.cbCustomer.DataSource = customersData;
                        this.cbCustomer.DisplayMember = "Name";
                        this.cbCustomer.ValueMember = "CustNum";
                    }));

                    this.Invoke(new Action(() =>
                    {
                         this.cbSchedulingQue.Text = "Unfirm Jobs";
                        
                    }));



            })
            .ContinueWith((prevTask) =>
            {
                //always reset the UI
                this.Invoke(new Action(() => { this.Cursor = Cursors.Default; }));
                SetStatus("Ready");
                EnableControls(true);
            });

        }

        #endregion -- Events --


        /// <summary>
        /// Sets the environment information.
        /// </summary>
        /// <param name="environment">The new environment to use.</param>
        private void SetEnvironment(EpicConnector.Connection.Environments environment)
        {
            this.environmentToolStripStatusLabel.Text = environment.ToString();
            this.database = new Database(environment);
            this.frJobDataGridView.DataSource = null;
            this.toJobDataGridView.DataSource = null;
            this.data = null;
        }

        /// <summary>
        /// Sets the status to a new message.
        /// </summary>
        /// <param name="msg">The new status message.</param>

        private void SetStatus(string msg)
        {
            this.Invoke(new Action(() => { this.toolStripStatusLabel.Text = msg; }));
        }

        /// <summary>
        /// Enables/Disables the controls.
        /// </summary>
        /// <param name="enable">True if the controls should be enabled.</param>
        private void EnableControls(bool enable = true)
        {

            this.Invoke(new Action(() =>
            {
                this.txtPartNum.Enabled = enable;
                this.txtSONum.Enabled = enable;
                this.btnJobGetData.Enabled = enable;
                this.BtnAssignToSchedulingQue.Enabled = enable;
                this.btnResetFilter.Enabled = enable;
                this.frJobDataGridView.Enabled = enable;
                this.toJobDataGridView.Enabled = enable;
                this.cbCustomer.Enabled = enable;
                this.cbSchedulingQue.Enabled = enable;
                

            }));
        }

        /// <summary>
        /// Validates the filters in the form.
        /// </summary>
        /// <returns>True if the filters are valid.</returns>
        private bool ValidateFilters()
        {
            int tmpInt = 0;
            if (!string.IsNullOrEmpty(this.txtSONum.Text.Trim()) && !int.TryParse(this.txtSONum.Text, out tmpInt))
            {
                this.txtSONum.Focus();
                MessageBox.Show("Invalid Order Number.", "Invalid Filter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            this.txtPartNum.Text = string.Empty;
            this.txtSONum.Text = string.Empty;
            this.cbCustomer.SelectedIndex = -1;
            this.cbSchedulingQue.Text = "Unfirm Jobs";
            this.frJobDataGridView.DataSource = null;
            this.toJobDataGridView.DataSource = null;

        }

        private void btnJobGetData_Click(object sender, EventArgs e)
        {
            if (!ValidateFilters())
                return;

            var updateTask = new Task(new Action(() => { }));
            var modifiedRows = GetChangedRows();

            if (modifiedRows.Length > 0)
            {
                if (MessageBox.Show("There are pending changes. Would you like to save them?", "Pending Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    updateTask = UpdateData(modifiedRows, false);
            }

            updateTask
            .ContinueWith((prevTask) =>
            {
                //only try to populate data if the update was successful
                if (!prevTask.IsFaulted)
                {
                    this.Invoke(new Action(() => { this.Cursor = Cursors.WaitCursor; }));

                    EnableControls(false);
                    PopulateData();
                }
            })
            .ContinueWith((prevTask) =>
            {
                //always reset the UI
                this.Invoke(new Action(() => { this.Cursor = Cursors.Default; }));
                SetStatus("Ready");
                EnableControls(true);
            });

            updateTask.Start();

        }

        /// <summary>
        /// Gets the actual rows that have been modified.
        /// </summary>
        /// <returns>The array of rows that been modified.</returns>

        private DataRow[] GetChangedRows()
        {
            List<DataRow> modifiedRows = new List<DataRow>();

            if (this.data != null)
            {
                foreach (DataRow row in this.data.Rows)
                {
                    if (row.RowState == DataRowState.Modified && !IsRowInOriginalState(row))
                        modifiedRows.Add(row);
                }
            }

            return modifiedRows.ToArray();
        }

        /// <summary>
        /// Checks if a row is back to its original state.
        /// </summary>
        /// <param name="row">The row to check.</param>
        /// <returns>True if the data in the row matches the original values.</returns>

        private bool IsRowInOriginalState(DataRow row)
        {
            foreach (DataColumn col in row.Table.Columns)
            {
                if (col.ColumnName == "modified" || col.ColumnName == "updated")
                    continue;
                

                var original = row[col, DataRowVersion.Original];
                var current = row[col, DataRowVersion.Current];

                if (!original.Equals(current))
                    return false;
            }

            //if it makes here, then all the rows were equal
            return true;
        }


        /// <summary>
        /// Updates the modified data.
        /// </summary>
        /// <param name="modifiedRows">The modified data rows.</param>
        /// <param name="showMsg">True if the message boxes should show.</param>
        /// <param name="closeWhenFinished">True if the form should close when the update is complete.</param>
        /// <returns>The async update task.</returns>
        private Task UpdateData(DataRow[] modifiedRows, bool showMsg = true, bool closeWhenFinished = false)
        {
            var t = new Task(() =>
            {
                this.Invoke(new Action(() => { this.Cursor = Cursors.WaitCursor; }));

                EnableControls(false);

                //update each modified record
                foreach (var modifiedRow in modifiedRows)
                {

                    if (modifiedRow["Select"].ToBoolean() == true)
                    {
                        var jobNum = modifiedRow["JobNum"].ToString();
                        SetStatus(string.Format("Updating job '{0}'...", jobNum));

                        if (!this.database.UpdateJobRecords(modifiedRow))
                            break;
                    }
                }
            });

            t.ContinueWith((prevTask) =>
            {
                if (closeWhenFinished)
                    this.Invoke(new Action(() => { this.Close(); }));
                else
                {
                    if (showMsg)                    
                      MessageBox.Show("Selected Data Moved Successfully");
                    
                       

                    //always reset the UI
                    PopulateData();
                    this.Invoke(new Action(() => { this.Cursor = Cursors.Default; }));
                    SetStatus("Ready");
                    EnableControls(true);

                }

            });

            return t;
        }

        /// <summary>
        /// Populates the data on the form.
        /// </summary>

        private void PopulateData()
        {
            SetStatus("Populating Grid Data...");

            var plant = "";
            var orderNum = "";
            var partNum = "";
            var scheduleQue = "";     
            var customer = "";
            var frRowCount = 0;
            var toRowCount = 0;
           

            this.Invoke(new Action(() =>
            {
                plant = this.plant;
                orderNum = string.IsNullOrEmpty(this.txtSONum.Text.Trim()) ? string.Empty : this.txtSONum.Text.Trim();                
                scheduleQue = this.cbSchedulingQue.Text.Trim();
                customer = this.cbCustomer.Text == "" ? string.Empty : this.cbCustomer.SelectedValue.ToString().Trim();
                partNum = this.txtPartNum.Text.Trim();

                if (scheduleQue == "Unfirm Jobs")
                {
                    this.lblFrSchedulingOption.Text = "Unfirm Jobs";
                    this.lblToSchedulingOption.Text = "Jobs Ready for Production";
                }
                else if (scheduleQue == "Jobs Ready for Production")
                {
                    this.lblFrSchedulingOption.Text = "Jobs Ready for Production";
                    this.lblToSchedulingOption.Text = "Jobs Scheduled for Production";
                }
                else if (scheduleQue == "Jobs Scheduled for Production")
                {
                    this.lblFrSchedulingOption.Text = "Jobs Scheduled for Production";
                    this.lblToSchedulingOption.Text = "Jobs Completed through Production";
                }
                else
                {
                    this.lblFrSchedulingOption.Text = "Jobs Completed through Production";
                    this.lblToSchedulingOption.Text = "N/A";
                }

            }));

               if (scheduleQue == "Unfirm Jobs")
               {
                  this.data = this.database.GetUnfirmJobs(plant, orderNum, scheduleQue, customer, partNum);
                  this.data2 = this.database.GetJobsReadyProduction(plant,orderNum, scheduleQue, customer, partNum);                  
                  

               }
               else if (scheduleQue == "Jobs Ready for Production")
               {
                  this.data = this.database.GetJobsReadyProduction(plant, orderNum, scheduleQue, customer, partNum);
                  this.data2 = this.database.GetJobsScheduledProduction(plant, orderNum, scheduleQue, customer, partNum);
                  
               }
              else if (scheduleQue == "Jobs Scheduled for Production")
              {
                 this.data = this.database.GetJobsScheduledProduction(plant, orderNum, scheduleQue, customer, partNum);
                 this.data2 = this.database.GetJobsCompletedProduction(plant, orderNum, scheduleQue, customer, partNum);
                
              }
              else
              {
                this.data = this.database.GetJobsCompletedProduction(plant, orderNum, scheduleQue, customer, partNum);
                this.data2 = null;
               
              } 


            this.Invoke(new Action(() =>
            {
                if (this.data != null)
                {
                    frRowCount = this.data.Rows.Count;
                    
                }
                else
                {
                    frRowCount = 0;
                }
                this.lblFrRecordIndicator.Text = frRowCount.ToString();

                if (this.data2 != null)
                {
                    toRowCount = this.data2.Rows.Count;
                }
                else 
                {
                    toRowCount = 0;
                }
               
                this.lblToRecordIndicator.Text = toRowCount.ToString();

                this.frJobDataGridView.DataSource = null;
                this.toJobDataGridView.DataSource = null;

                this.frJobDataGridView.DataSource = this.data;
                this.toJobDataGridView.DataSource = this.data2;
                FormatGrid();
            }));

        }


        /// <summary>
        /// Formats the grid to make it look pretty.
        /// </summary>

        private void FormatGrid()
        {
            var scheduleQue = "";
            scheduleQue = this.cbSchedulingQue.Text.Trim();

            if (frJobDataGridView.DataSource == null || frJobDataGridView.Columns.Count == 0)
            {
                return;
            }

            List<DataGridViewColumn> removeColumns = new List<DataGridViewColumn>();
            List<DataGridViewColumn> addColumns = new List<DataGridViewColumn>(); 

            for (int idx = 0; idx < this.frJobDataGridView.Columns.Count; idx++)
            {

                DataGridViewColumn col = this.frJobDataGridView.Columns[idx];

                switch (col.Name)
                {
                    case "Select":
                        {
                            removeColumns.Add(col);
                            var newCol = new DataGridViewCheckBoxColumn();                           
                            newCol.Name = "Select";
                            newCol.DataPropertyName = "Select";
                            newCol.HeaderText = "Select";
                            newCol.SortMode = DataGridViewColumnSortMode.Automatic;
                           
                            newCol.Width = 55;
                            if (scheduleQue == "Unfirm Jobs" || scheduleQue == "Jobs Ready for Production")
                            {
                               addColumns.Add(newCol);
                                
                            }
                            newCol.DisplayIndex = idx;
                            break;
                        }
                    case "JobNum":
                        col.ReadOnly = true;
                        col.HeaderText = "Job #";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 110;
                        col.DisplayIndex = idx;
                        break;

                    case "Customer":
                        col.ReadOnly = true;
                        col.HeaderText = "Customer";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 150;
                        col.DisplayIndex = idx;
                        break;                

                    case "OnSiteDate":
                        col.ReadOnly = true;
                        col.HeaderText = "On Site Date";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 80;
                        col.DisplayIndex = idx;
                        break;

                    case "LD":
                        col.ReadOnly = true;
                        col.HeaderText = "LD";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 40;
                        col.DisplayIndex = idx;
                        break;

                    case "PartNum":
                        col.ReadOnly = true;
                        col.HeaderText = "Part #";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 80;
                        col.DisplayIndex = idx;
                        break;

                    case "NetWeight":
                        col.ReadOnly = true;
                        col.HeaderText = "Part Weight";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 50;
                        col.DisplayIndex = idx;
                        break;

                    case "PartDescription":
                        col.ReadOnly = true;
                        col.HeaderText = "Part Desc.";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 150;
                        col.DisplayIndex = idx;
                        break;

                    case "ShaftNum":
                        col.ReadOnly = true;
                        col.HeaderText = "Shaft #";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        col.DisplayIndex = idx;                       
                        break;

                    case "FinishType":
                        col.ReadOnly = true;
                        col.HeaderText = "Finish Type";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 40;
                        col.DisplayIndex = idx;
                        break;

                    case "Qty":
                        col.ReadOnly = true;
                        col.HeaderText = "Qty";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 40;
                        col.DisplayIndex = idx;
                        break;

                    case "ReqDueDate":  
                       if (scheduleQue == "Jobs Ready for Production" || scheduleQue == "Jobs Scheduled for Production")
                        {
                            col.HeaderText = "Start Date";
                            col.DefaultCellStyle.BackColor = Color.White;
                            col.ReadOnly = false;
                        }
                        else
                        {
                            col.HeaderText = "Req Due Date";
                            col.DefaultCellStyle.BackColor = Color.White;
                            col.ReadOnly = false;
                        }

                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            col.ReadOnly = true;
                            col.DefaultCellStyle.BackColor = Color.Silver;
                        }
                        col.DisplayIndex = idx;
                        col.Width = 80;
                        break;

                    case "StartSeq":                      
                        col.DisplayIndex = idx;
                        col.HeaderText = "Seq Start #";
                        col.Width = 40;                       
                        break;

                    case "EndSeq":                      
                        col.DisplayIndex = idx;
                        col.HeaderText = "Seq End #";
                        col.Width = 40;                       
                        break;

                    case "Inspection":                       
                        col.DisplayIndex = idx;
                        col.HeaderText = "Inspection %";
                        col.Width = 60;
                        col.ReadOnly = true; 
                        break;

                    case "ScheduleLine":                     
                        col.DisplayIndex = idx;
                        col.HeaderText = "Schedule Line";
                        col.Width = 60;                        
                        break;

                    case "FitupProdStd":                     
                        col.DisplayIndex = idx;
                        col.HeaderText = "Fit Up Prod Std";
                        col.Width = 60;                       
                        break;

                    case "WeldOutProdStd":                    
                        col.DisplayIndex = idx;
                        col.HeaderText = "Weld Out Prod Std";
                        col.Width = 60;
                        break;

                    case "OrderNum":                     
                        col.DisplayIndex = idx;
                        col.ReadOnly = true;
                        col.HeaderText = "SO #";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 70;                        
                        break;

                    case "ParentSite":
                        col.DisplayIndex = idx;
                        col.ReadOnly = true;
                        col.HeaderText = "Parent Site";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 70;
                        break;

                    case "NoOfAttachments":                       
                        col.ReadOnly = true;
                        col.DisplayIndex = idx;
                        col.HeaderText = "# of Attachments (per arm)";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "MtlIssued":                       
                        col.ReadOnly = true;
                        col.DisplayIndex = idx;
                        col.HeaderText = "Mtl Issued";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 40;
                        break;

                    case "FitupCmpProdQty":                       
                        col.ReadOnly = true;
                        col.DisplayIndex = idx;
                        col.HeaderText = "Fit Up (Qty Cmp/Prod Qty)";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "FitupEstHrs":                        
                        col.ReadOnly = true;
                        col.DisplayIndex = idx;
                        col.HeaderText = "Fit Up Est. Hrs";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "FitupActHrs": 
                        col.ReadOnly = true;
                        col.DisplayIndex = idx;
                        col.HeaderText = "Fit Up Act. Hrs";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "WeldCmpProdQty":                       
                        col.ReadOnly = true;
                        col.DisplayIndex = idx;
                        col.HeaderText = "Weld Out (Qty Cmp/Prod Qty)";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "WeldEstHrs":                        
                        col.ReadOnly = true;
                        col.DisplayIndex = idx;
                        col.HeaderText = "Weld Out Est. Hrs";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "WeldActHrs":                        
                        col.ReadOnly = true;
                        col.DisplayIndex = idx;
                        col.HeaderText = "Weld Out Act. Hrs";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "QCReleased":                        
                        col.ReadOnly = true;
                        col.DisplayIndex = idx;
                        col.HeaderText = "QC Rel'd (Rel Qty/Prod Qty)";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "FinalQCReleasedData":                       
                        col.DisplayIndex = idx;
                        col.ReadOnly = true;
                        col.HeaderText = "Final QC Rel'd Date";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 80;                  
                        break;

                    case "GalvDate":                      
                        col.DisplayIndex = idx;
                        col.ReadOnly = true;
                        col.HeaderText = "Galv Complete Date";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 80;                       
                        break;

                    case "YardStagingBin":
                       
                        col.DisplayIndex = idx;
                        col.ReadOnly = true;
                        col.HeaderText = "Yard Staging Bin";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;                       
                        break;



                    default:
                        col.Visible = false;
                        break;
                }

            }

            //remove any unwanted columns
            foreach (var col in removeColumns)
            {  
                this.frJobDataGridView.Columns.RemoveAt(col.DisplayIndex);
            } 


            //add any of the new columns
            foreach (var col in addColumns)
            {
                if (col.Name == "Select")
                {
                    this.frJobDataGridView.Columns.Insert(0, col);
                }
                else
                {
                    this.frJobDataGridView.Columns.Add(col);
                }
                
            }
                  
               
            if (scheduleQue == "Unfirm Jobs" || scheduleQue == "Jobs Ready for Production")
            this.frJobDataGridView.Columns["Select"].HeaderCell.ContextMenuStrip = this.frSchQueContextMenuStrip;


            if (toJobDataGridView.DataSource == null || toJobDataGridView.Columns.Count == 0)
            {
                return;
            }

            removeColumns.Clear();
            addColumns.Clear();

            for (int idx = 0; idx < this.toJobDataGridView.Columns.Count; idx++)
            {

                DataGridViewColumn col = this.toJobDataGridView.Columns[idx];

                switch (col.Name)
                {

                    case "Select":
                        {
                            removeColumns.Add(col);
                            var newCol = new DataGridViewCheckBoxColumn();
                            newCol.Name = "Select";
                            newCol.DataPropertyName = "Select";
                            newCol.HeaderText = "Select";
                            newCol.SortMode = DataGridViewColumnSortMode.Automatic;
                            newCol.Width = 55;
                            break;
                        }
                    case "OrderNum":
                        {
                            
                            col.Visible = false;
                            var newCol = new DataGridViewLinkColumn();                           
                            newCol.HeaderText = "SO #";
                            newCol.Name = "linkColumn";                           
                            newCol.DataPropertyName = "OrderNum";                          
                            newCol.Width = 70;
                            newCol.ReadOnly = true;
                            newCol.UseColumnTextForLinkValue = false;
                            addColumns.Add(newCol);
                        }
                        break;

                    case "Customer":
                        col.ReadOnly = true;
                        col.HeaderText = "Customer";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 150;
                        break;

                    case "OnSiteDate":
                        col.ReadOnly = true;
                        col.HeaderText = "On Site Date";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 80;
                        break;

                    case "LD":
                        col.ReadOnly = true;
                        col.HeaderText = "LD";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 40;
                        break;

                    case "ParentSite":
                        col.ReadOnly = true;
                        col.HeaderText = "Parent Site";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 70;
                        break;

                    case "JobNum":
                        col.ReadOnly = true;
                        col.HeaderText = "Job #";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 90;
                        break;

                    case "PartNum":
                        col.ReadOnly = true;
                        col.HeaderText = "Part #";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 80;
                        break;

                    case "NetWeight":
                        col.ReadOnly = true;
                        col.HeaderText = "Part Weight";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 50;
                        break;

                    case "PartDescription":
                        col.ReadOnly = true;
                        col.HeaderText = "Part Desc.";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 150;
                        break;

                    case "ShaftNum":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "Shaft #";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "NoOfAttachments":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "# of Attachments (per arm)";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "FinishType":
                        col.ReadOnly = true;
                        col.HeaderText = "Finish Type";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 40;
                        break;

                    case "Qty":
                        col.ReadOnly = true;
                        col.HeaderText = "Qty";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 40;
                        break;

                    case "FinalQCReleasedData":
                        removeColumns.Add(col);
                        col.ReadOnly = true;
                        col.HeaderText = "Final QC Rel'd Date";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 80;
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            addColumns.Add(col);
                        }

                        break;

                    case "GalvDate":
                        removeColumns.Add(col);
                        col.ReadOnly = true;
                        col.HeaderText = "Galv Complete Date";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 80;
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            addColumns.Add(col);
                        }
                        break;

                    case "YardStagingBin":
                        removeColumns.Add(col);
                        col.ReadOnly = true;
                        col.HeaderText = "Yard Staging Bin";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            addColumns.Add(col);
                        }
                        break;

                    case "ReqDueDate":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "Start Date";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 80;
                        break;

                    case "FitupCmpProdQty":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "Fit Up (Qty Cmp/Prod Qty)";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "FitupEstHrs":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "Fit Up Est. Hrs";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "FitupActHrs":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "Fit Up Act. Hrs";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "WeldCmpProdQty":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "Weld Out (Qty Cmp/Prod Qty)";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "WeldEstHrs":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "Weld Out Est. Hrs";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "WeldActHrs":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "Weld Out Act. Hrs";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "QCReleased":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "QC Rel'd (Rel Qty/Prod Qty)";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;



                    case "MtlIssued":
                        if (scheduleQue == "Jobs Scheduled for Production")
                        {
                            removeColumns.Add(col);
                        }
                        col.ReadOnly = true;
                        col.HeaderText = "Mtl Issued";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 40;
                        break;

                    case "CustID":
                        removeColumns.Add(col);
                        col.ReadOnly = true;
                        col.HeaderText = "CustID";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "CustNum":
                        removeColumns.Add(col);
                        col.ReadOnly = true;
                        col.HeaderText = "CustNum";
                        col.DefaultCellStyle.BackColor = Color.Silver;
                        col.Width = 60;
                        break;

                    case "StartSeq":
                        removeColumns.Add(col);
                        col.HeaderText = "Seq Start #";
                        col.Width = 40;
                        break;

                    case "EndSeq":
                        removeColumns.Add(col);
                        col.HeaderText = "Seq End #";
                        col.Width = 40;
                        break;                

                    case "ScheduleLine":
                        removeColumns.Add(col);
                        col.HeaderText = "Schedule Line";
                        col.Width = 60;
                        break;

                    case "FitupProdStd":
                        removeColumns.Add(col);
                        col.HeaderText = "Fit Up Prod Std";
                        col.Width = 60;
                        break;

                    case "WeldOutProdStd":
                        removeColumns.Add(col);
                        col.HeaderText = "Weld Out Prod Std";
                        col.Width = 60;
                        break;

                 

                    default:
                        col.Visible = false;                                           
                        break;                 
                }

            }

            //remove any unwanted columns
            foreach (var col in removeColumns)
                this.toJobDataGridView.Columns.Remove(col);

            //add any of the new columns
            foreach (var col in addColumns)
            {
                if (col.Name == "linkColumn")
                {
                    this.toJobDataGridView.Columns.Insert(0, col);
                }
                else
                {
                    this.toJobDataGridView.Columns.Add(col);
                }
            }
               
            if (scheduleQue == "Jobs Ready for Production")
            this.toJobDataGridView.Columns["linkColumn"].HeaderCell.ContextMenuStrip = this.toSchQueContextMenuStrip;
        

        }

        /// <summary>
        /// Handles the button click for the AssignToSchedulingQue button.
        /// It will update the modified data in the database.
        /// </summary>
        /// <param name="sender">See MSDN.</param>
        /// <param name="e">See MSDN.</param>
        private void BtnAssignToSchedulingQue_Click(object sender, EventArgs e)
        {
            var modifiedRows = GetChangedRows();

            if (modifiedRows.Length <= 0)
            {
                MessageBox.Show("0 records have been modified. Unable to Update.");
                return;
            }

            foreach (var modifiedRow in modifiedRows)
            {
                //make sure the select column is checked
                var scheduleQue = modifiedRow["ParameterScheduleQueue"].ToString().Trim();

                if (scheduleQue == "Unfirm Jobs" || scheduleQue == "Jobs Ready for Production")
                {
                    bool RowSelect = modifiedRow["Select"].ToBoolean();
                    if (!RowSelect)
                    {
                        MessageBox.Show("Please check the select column.");
                        return;
                    }

                    if (modifiedRow["ReqDueDate"].ToString().Trim() == null || modifiedRow["ReqDueDate"].ToString().Trim() == string.Empty)
                    {
                        MessageBox.Show("Request By Date is required for selected job: " + modifiedRow["JobNum"].ToString().Trim());
                        return;
                    }

                }

                if (scheduleQue == "Unfirm Jobs")
                {


                    //make sure the Sequence Start is valid
                    int SeqStart = 0;
                    var SeqS = modifiedRow["StartSeq"].ToString().Trim();
                    if (!int.TryParse(SeqS, out SeqStart))
                    {
                        MessageBox.Show("There are 1 or more rows that have an invalid Start Seq. Please assign a valid number to those rows.");
                        return;
                    }

                    if (Convert.ToInt32(SeqS) <= 0)
                    {
                        MessageBox.Show("Start Sequence should be greater than zero for selected job: " + modifiedRow["JobNum"].ToString().Trim());
                        return;
                    }

                    //make sure the Sequence End is valid
                    int SeqEnd = 0;
                    var SeqE = modifiedRow["EndSeq"].ToString().Trim();
                    if (!int.TryParse(SeqE, out SeqEnd))
                    {
                        MessageBox.Show("There are 1 or more rows that have an invalid End Seq. Please assign a valid number to those rows.");
                        return;
                    }

                    if (Convert.ToInt32(SeqE) <= 0)
                    {
                        MessageBox.Show("End Sequence should be greater than zero for selected job: " + modifiedRow["JobNum"].ToString().Trim());
                        return;
                    }

                    if (Convert.ToInt32(SeqE) < Convert.ToInt32(SeqS))
                    {
                        MessageBox.Show("End Sequence should be less than Start Sequence for selected job: " + modifiedRow["JobNum"].ToString().Trim());
                        return;
                    }



                    if (modifiedRow["ScheduleLine"].ToString().Trim() == null)
                    {
                        MessageBox.Show("Schedule Line is required for selected job: " + modifiedRow["JobNum"].ToString().Trim());
                        return;
                    }

                    //make sure the fitup prod std is valid
                    decimal fProdStd = 0M;
                    var fitupProdStd = modifiedRow["FitupProdStd"].ToString().Trim();
                    if (!decimal.TryParse(fitupProdStd, out fProdStd))
                    {
                        MessageBox.Show("There are 1 or more rows that have an invalid Fitup prod std value. Please assign a valid number to those rows.");
                        return;
                    }

                    //make sure the weld prod std is valid
                    decimal wProdStd = 0M;
                    var weldOutProdStd = modifiedRow["WeldOutProdStd"].ToString().Trim();
                    if (!decimal.TryParse(weldOutProdStd, out wProdStd))
                    {
                        MessageBox.Show("There are 1 or more rows that have an invalid Weld prod std value. Please assign a valid number to those rows.");
                        return;
                    }
                }
            }

            UpdateData(modifiedRows).Start();
        }


        /// <summary>
        /// Handles the key press event.
        /// If enter or return is pressed, it will simulate the GetData click.
        /// </summary>
        /// <param name="sender">See MSDN.</param>
        /// <param name="e">See MSDN.</param>
        private void txtPartNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
                {
                    btnJobGetData_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_txtPartNum_KeyPress", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSONum_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
                {
                    btnJobGetData_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_txtSONum_KeyPress", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the data error event when the user leaves a cell and does not have valid data in that cell.
        /// This method will warn the user about the issue.
        /// </summary>
        /// <param name="sender">See MSDN.</param>
        /// <param name="e">See MSDN.</param>
        private void frJobDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                //Don't throw an exception when we're done
                e.ThrowException = false;

                //Display an error message
                string txt = string.Format("Error with the column '{0}'{1}{2}", this.frJobDataGridView.Columns[e.ColumnIndex].HeaderText, Environment.NewLine, e.Exception.Message);
                MessageBox.Show(txt, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //if this is true, then the user is trapped in this cell
                e.Cancel = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_frJobDataGridView_DataError", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Handles the key down event for the data grid.
        /// It will add functionality for copy/paste to make it easier on the user.
        /// </summary>
        /// <param name="sender">See MSDN.</param>
        /// <param name="e">See MSDN.</param>
        private void frJobDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control)
                {
                    if (e.KeyCode == Keys.V)
                    {
                        string pasteData = Clipboard.GetText();
                        foreach (DataGridViewCell selectedCell in this.frJobDataGridView.SelectedCells)
                        {
                            e.Handled = true;
                            selectedCell.Value = pasteData;
                        }
                    }
                    else if (e.KeyCode == Keys.C)
                    {
                        //get the first selected row  and copy its contents
                        if (this.frJobDataGridView.SelectedCells.Count > 0)
                        {
                            Clipboard.SetText(this.frJobDataGridView.SelectedCells[0].Value.ToString());
                            e.Handled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_frJobDataGridView_KeyDown", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Handles the form closing event.
        /// It will prompt the user if there are pending changes.
        /// </summary>
        /// <param name="sender">See MSDN.</param>
        /// <param name="e">See MSDN.</param>
        private void ArmProductionScheduler_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                var modifiedRows = GetChangedRows();

                if (modifiedRows.Length > 0)
                {
                    if (MessageBox.Show("There are pending changes. Would you like to save them?", "Pending Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //let the async update method close the form when it is finished
                        e.Cancel = true;

                        //save the data
                        UpdateData(modifiedRows, false, true).Start();
                    }
                }

                if (!e.Cancel)
                {
                    data?.Dispose();
                    data = null;

                    data2?.Dispose();
                    data2 = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_ArmProductionScheduler_FormClosing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void toJobDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                var row = this.toJobDataGridView.Rows[e.RowIndex];

                //get the job color value
                var jobColor = row.Cells["JobColor"].Value.ToString();

                //Job Color Concept
                if (jobColor == "Orange")
                    row.DefaultCellStyle.ForeColor = Color.Orange;
                else if (jobColor == "Black")
                    row.DefaultCellStyle.ForeColor = Color.Black;
                else if (jobColor == "Green")
                    row.DefaultCellStyle.ForeColor = Color.Green;
                else if (jobColor == "Red")
                    row.DefaultCellStyle.ForeColor = Color.Red;
                else
                    row.DefaultCellStyle.ForeColor = Color.Empty; //default Fore color (needed for when we go from modified to unmodified)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_toJobDataGridView_RowPostPaint", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Handler for when the Select All option is clicked for the select context menu.
        /// </summary>
        /// <param name="sender">See MSDN.</param>
        /// <param name="e">See MSDN.</param>
        private void frSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in this.data.Rows)
                    row["Select"] = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_frSelectAllToolStripMenuItem_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBySOStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                if (toJobDataGridView.DataSource == null || toJobDataGridView.Columns.Count == 0)
                {
                    return;
                }


                var row = this.toJobDataGridView.Rows[0];
                var plant = "";
                var orderNum = "";
                var partNum = "";
                var scheduleQue = "";
                var customer = "";


                plant = row.Cells["ParameterPlant"].Value.ToString();
                orderNum = string.IsNullOrEmpty(row.Cells["ParameterOrderNum"].Value.ToString().Trim()) ? string.Empty : row.Cells["ParameterOrderNum"].Value.ToString().Trim();
                partNum = row.Cells["ParameterPartNum"].Value.ToString().Trim();
                scheduleQue = row.Cells["ParameterScheduleQueue"].Value.ToString().Trim();
                customer = string.IsNullOrEmpty(row.Cells["ParameterCustomer"].Value.ToString().Trim()) ? string.Empty : row.Cells["ParameterCustomer"].Value.ToString().Trim();

                CallShowGroupBySO(partNum, orderNum, plant, customer, scheduleQue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_groupBySOStripMenuItem_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frJobDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if (frJobDataGridView.DataSource == null || frJobDataGridView.Columns.Count == 0)
                {
                    return;
                }
                if (cbSchedulingQue.SelectedItem.ToString() == "Unfirm Jobs")
                {
                    bool columnExists = frJobDataGridView.Columns.Contains("Inspection");

                    if (columnExists && ( frJobDataGridView.Columns["Inspection"].Index == 13))
                    {

                        int rowIndx = 0;
                        rowIndx = frJobDataGridView.CurrentRow.Index;

                        var selectedRow = (frJobDataGridView.Rows[rowIndx].DataBoundItem as DataRowView).Row;

                        var cbOprSeq = new DataGridViewComboBoxCell();
                        cbOprSeq.DataSource = this.database.SetInspectionData();
                        cbOprSeq.DisplayMember = "Key2";
                        cbOprSeq.ValueMember = "Key3";

                        frJobDataGridView.Rows[rowIndx].Cells["Inspection"] = cbOprSeq;
                        frJobDataGridView.Rows[rowIndx].Cells["Inspection"].ReadOnly = false;

                    }                  

                }

                if (this.frJobDataGridView.CurrentRow != null)
                {
                    this.frJobDataGridView.CurrentRow.ContextMenuStrip = this.frShowMaterialContextMenuStrip;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_frJobDataGridView_SelectionChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void frJobDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }

        private void frJobDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {

                var row = this.frJobDataGridView.Rows[e.RowIndex];



                var scheduleQue = row.Cells["ParameterScheduleQueue"].Value.ToString();
                if (scheduleQue != "Unfirm Jobs")
                {
                    //get the job color value
                    var jobColor = row.Cells["JobColor"].Value.ToString();

                    //Job Color Concept
                    if (jobColor == "Orange")
                        row.DefaultCellStyle.ForeColor = Color.Orange;
                    else if (jobColor == "Black")
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    else if (jobColor == "Green")
                        row.DefaultCellStyle.ForeColor = Color.Green;
                    else if (jobColor == "Red")
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    else
                        row.DefaultCellStyle.ForeColor = Color.Empty; //default Fore color (needed for when we go from modified to unmodified)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_frJobDataGridView_RowPostPaint", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void showMaterialsContextMenuStrip_Click(object sender, EventArgs e)
        {
            if (toJobDataGridView.DataSource == null || toJobDataGridView.Columns.Count == 0)
            {
                return;
            }

            if (this.toJobDataGridView.CurrentRow != null)
            {
                var row = this.toJobDataGridView.CurrentRow;
                var plant = "";
                var jobNum = "";

                plant = this.plant;
                jobNum = row.Cells["JobNum"].Value.ToString().Trim();

                CallShowMaterial(jobNum, plant);
            }
         
        }

        private void toJobDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if (toJobDataGridView.DataSource == null || toJobDataGridView.Columns.Count == 0)
                {
                    return;
                }

                if (this.toJobDataGridView.CurrentRow != null)
                {
                    this.toJobDataGridView.CurrentRow.ContextMenuStrip = this.showMaterialsContextMenuStrip;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_toJobDataGridView_SelectionChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frShowMtlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (frJobDataGridView.DataSource == null || frJobDataGridView.Columns.Count == 0)
                {
                    return;
                }

                if (this.frJobDataGridView.CurrentRow != null)
                {
                    var row = this.frJobDataGridView.CurrentRow;
                    var plant = "";
                    var jobNum = "";

                    plant = this.plant;
                    jobNum = row.Cells["JobNum"].Value.ToString().Trim();

                    CallShowMaterial(jobNum, plant);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to Open show material form. /n {0}", ex.Message), "Show Materials Dialog");
            }

        }

        private void CallShowMaterial(string jobNum, string plant)
        {
            ShowMaterials showMaterials = null;
            try
            {
                
                showMaterials = new ShowMaterials(env);
                showMaterials.GetRecords(jobNum, plant);
                showMaterials.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to get show material data. /n {0}", ex.Message), "Show Materials Dialog");
            }
            finally
            {
                if (showMaterials != null)
                {
                    showMaterials.Dispose();
                    showMaterials = null;
                }
            }
        }

        private void CallShowGroupBySO(string partNum, string orderNum, string plant, string customer, string scheduleQue)
        {
            ShowGroupBySO showGroupBySO = null;
            try
            {
                showGroupBySO = new ShowGroupBySO(env);
                showGroupBySO.GetRecords(partNum, orderNum, plant, customer, scheduleQue);
                showGroupBySO.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to get group by data. /n {0}", ex.Message), "Group By SO Dialog");
            }
            finally
            {
                if (showGroupBySO != null)
                {
                    showGroupBySO.Dispose();
                    showGroupBySO = null;
                }
            }
        }



        private void toJobDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (toJobDataGridView.Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex >= 0)
                {
                    string SO = toJobDataGridView.Rows[e.RowIndex].Cells["OrderNum"].Value.ToString();
                    string EOPSQuoteNumPrefix = "";
                    EOPSQuoteNumPrefix = this.database.ExtractEOPSQuote(SO);

                    string nextEOPSQuoteNumPrefix = "";
                    string folderPath = "";
                    string nextFolderPath = "";

                    if (!string.IsNullOrEmpty(EOPSQuoteNumPrefix))
                    {
                        folderPath = $@"\\txal-netapp1b.sabre.local\QDrive\STS Production and Approval Drawings\20{EOPSQuoteNumPrefix} jobs\{EOPSQuoteNumPrefix}-{SO}\fabrication production";

                        nextEOPSQuoteNumPrefix = (Convert.ToInt32(EOPSQuoteNumPrefix) + 1).ToString();
                        nextFolderPath = $@"\\txal-netapp1b.sabre.local\QDrive\STS Production and Approval Drawings\20{nextEOPSQuoteNumPrefix} jobs\{nextEOPSQuoteNumPrefix}-{SO}\fabrication production";

                        if (Directory.Exists(folderPath))
                        {
                            System.Diagnostics.Process.Start("explorer.exe", folderPath);
                            Thread.Sleep(3000);
                        }
                        else if (Directory.Exists(nextFolderPath))
                        {
                            System.Diagnostics.Process.Start("explorer.exe", nextFolderPath);
                            Thread.Sleep(3000);
                        }
                        else
                        {
                            MessageBox.Show($"Folder is not exist {folderPath}");
                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_toJobDataGridView_CellContentClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
