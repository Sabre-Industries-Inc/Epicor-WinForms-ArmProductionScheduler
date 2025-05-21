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
    public partial class ShowMaterials : Form
    {
        #region -- Fields --
        /// <summary>Database helper object.</summary>
        private Database database = null;

        /// <summary>The loaded data.</summary>
        private DataTable data = null;   


        #endregion -- Fields --

        public ShowMaterials(EpicConnector.Connection.Environments environment)
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
            this.showMaterialsDataGridView.DataSource = null;           
            this.data = null;
        }

        public void GetRecords(string jobNum , string plant)
        {

            this.data = this.database.ShowMaterials(jobNum, plant);
            var rowCount = 0;


            rowCount = this.data.Rows.Count;
            this.lblGetRecordIndicator.Text = rowCount.ToString();


            this.showMaterialsDataGridView.DataSource = this.data;
            this.showMaterialsDataGridView.Enabled = false;
         
        }
    }
}
