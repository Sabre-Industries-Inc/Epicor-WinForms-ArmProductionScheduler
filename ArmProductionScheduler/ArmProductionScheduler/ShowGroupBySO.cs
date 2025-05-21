using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmProductionScheduler
{
    public partial class ShowGroupBySO : Form
    {
        #region -- Fields --
        /// <summary>Database helper object.</summary>
        private Database database = null;

        /// <summary>The loaded data.</summary>
        private DataTable data = null;   


        #endregion -- Fields --

        public ShowGroupBySO(EpicConnector.Connection.Environments environment)
        {
            InitializeComponent();

            SetEnvironment(environment);
        }

        /// <summary>
        /// Sets the environment information.
        /// </summary>
        /// <param name="environment">The new environment to use.</param>
        private void SetEnvironment(EpicConnector.Connection.Environments environment)
        {
           
            this.database = new Database(environment);
            this.showGroupByDataGridView.DataSource = null;           
            this.data = null;
        }

        public void GetRecords(string partNum, string orderNum, string plant, string customer, string scheduleQue)
        {

            this.data = this.database.GetJobsScheduledProductionGroupBySO(partNum, orderNum, plant, customer, scheduleQue);
            var rowCount = 0;


            rowCount = this.data.Rows.Count;
            this.lblGetRecordIndicator.Text = rowCount.ToString();


            this.showGroupByDataGridView.DataSource = this.data;
            this.showGroupByDataGridView.Enabled = false;
         
        }
    }
}
