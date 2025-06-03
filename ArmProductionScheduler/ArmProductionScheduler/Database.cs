using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicConnector.Extensions;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using EpicUpdater;
using EpicUpdater.Job;
using EpicUpdater.Material;
using Erp.BO;
using System.Diagnostics;
using EpicUtility.Email;

namespace ArmProductionScheduler
{
    class Database
    {

        #region -- Fields --

        private const string COMPANY = "SCC";

        private string userId;

        EpicConnector.Connection epicorConnection = null;

        /// <summary>The current epicor environment.</summary>
        private EpicConnector.Connection.Environments environment = EpicConnector.Connection.Environments.None;

        private JobHeadUpdate jobHeadUpdate;     
        private JobOperUpdate jobOperUpdate;
        

        #endregion -- Fields --

        #region -- Constructor --
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="epicorConnection">The current epicor connection.</param>
        public Database(EpicConnector.Connection.Environments env)
        {
            environment = env;

            epicorConnection = new EpicConnector.Connection(environment);           

            userId = EpicConnector.Queries.Utility.GetEpicorIdByWindowsId(epicorConnection.EpicEnvironment, Environment.UserName);
        }

        #endregion -- Constructor --

        /// <summary>
        /// Gets the available customers.
        /// </summary>
        /// <returns>The available customers.</returns>
        public DataTable GetAvailableCustomers()
        {
          
                string sql = @"
                SELECT
                        CustNum =   0
                        ,Name =     N''
                UNION
                SELECT
                        CustNum
                        ,Name
                FROM
                        Sales.Customer(NOLOCK)
                ORDER BY
                        Name";

                return EpicConnector.Queries.Utility.GetTableBySql(environment, sql);
           
        }

        /// <summary>
        /// Gets the unfirm job data from the database.
        /// </summary>
        /// <param name="plant">Required filter by plant.</param>
        /// <param name="orderNum">Optional filter by order number.</param>
        /// <param name="scheduleQueue">Required filter by schedule Queue.</param>
        /// <param name="customer">Optional filter by customer.</param>
        /// <param name="partNum">Optional filter by part number.</param>
       
        /// <returns>The unfirm job data.</returns>
        public DataTable GetUnfirmJobs(string plant, string orderNum, string scheduleQueue, string customer, string partNum)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@company", COMPANY));

            if (!string.IsNullOrEmpty(plant))
                sqlParams.Add(new SqlParameter("@plant", plant));

            if (!string.IsNullOrEmpty(orderNum))
                sqlParams.Add(new SqlParameter("@orderNum", orderNum));

            if (!string.IsNullOrEmpty(scheduleQueue))
                sqlParams.Add(new SqlParameter("@scheduleQueue", scheduleQueue));

            if (!string.IsNullOrEmpty(customer))
                sqlParams.Add(new SqlParameter("@customer", customer));

            if (!string.IsNullOrEmpty(partNum))
                sqlParams.Add(new SqlParameter("@partNum", partNum));

           
            return EpicConnector.Queries.Utility.GetTableByStoredProc(environment, "Job.GetUnfirmJobsForArm", sqlParams.ToArray());
        }



        /// <summary>
        /// Gets the job ready production data from the database.
        /// </summary>
        /// <param name="plant">Required filter by plant.</param>
        /// <param name="orderNum">Optional filter by order number.</param>
        /// <param name="scheduleQueue">Required filter by schedule Queue.</param>
        /// <param name="customer">Optional filter by customer.</param>
        /// <param name="partNum">Optional filter by part number.</param>

        /// <returns>The job ready production data.</returns>
        public DataTable GetJobsReadyProduction(string plant, string orderNum, string scheduleQueue, string customer, string partNum)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@company", COMPANY));

            if (!string.IsNullOrEmpty(plant))
                sqlParams.Add(new SqlParameter("@plant", plant));

            if (!string.IsNullOrEmpty(orderNum))
                sqlParams.Add(new SqlParameter("@orderNum", orderNum));

            if (!string.IsNullOrEmpty(scheduleQueue))
                sqlParams.Add(new SqlParameter("@scheduleQueue", scheduleQueue));

            if (!string.IsNullOrEmpty(customer))
                sqlParams.Add(new SqlParameter("@customer", customer));

            if (!string.IsNullOrEmpty(partNum))
                sqlParams.Add(new SqlParameter("@partNum", partNum));


            return EpicConnector.Queries.Utility.GetTableByStoredProc(environment, "Job.GetJobsReadyProductionForArm", sqlParams.ToArray());
        }

        /// <summary>
        /// Gets the job scheduled production data from the database.
        /// </summary>
        /// <param name="plant">Required filter by plant.</param>
        /// <param name="orderNum">Optional filter by order number.</param>
        /// <param name="scheduleQueue">Required filter by schedule Queue.</param>
        /// <param name="customer">Optional filter by customer.</param>
        /// <param name="partNum">Optional filter by part number.</param>

        /// <returns>The job scheduled production data.</returns>
        public DataTable GetJobsScheduledProduction(string plant, string orderNum, string scheduleQueue, string customer, string partNum)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@company", COMPANY));

            if (!string.IsNullOrEmpty(plant))
                sqlParams.Add(new SqlParameter("@plant", plant));

            if (!string.IsNullOrEmpty(orderNum))
                sqlParams.Add(new SqlParameter("@orderNum", orderNum));

            if (!string.IsNullOrEmpty(scheduleQueue))
                sqlParams.Add(new SqlParameter("@scheduleQueue", scheduleQueue));

            if (!string.IsNullOrEmpty(customer))
                sqlParams.Add(new SqlParameter("@customer", customer));

            if (!string.IsNullOrEmpty(partNum))
                sqlParams.Add(new SqlParameter("@partNum", partNum));


            return EpicConnector.Queries.Utility.GetTableByStoredProc(environment, "Job.GetJobsScheduledProductionForArm", sqlParams.ToArray());
        }


        /// <summary>
        /// Gets the job completed production data from the database.
        /// </summary>
        /// <param name="plant">Required filter by plant.</param>
        /// <param name="orderNum">Optional filter by order number.</param>
        /// <param name="scheduleQueue">Required filter by schedule Queue.</param>
        /// <param name="customer">Optional filter by customer.</param>
        /// <param name="partNum">Optional filter by part number.</param>

        /// <returns>The job completed production data.</returns>
        public DataTable GetJobsCompletedProduction(string plant, string orderNum, string scheduleQueue, string customer, string partNum)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@company", COMPANY));

            if (!string.IsNullOrEmpty(plant))
                sqlParams.Add(new SqlParameter("@plant", plant));

            if (!string.IsNullOrEmpty(orderNum))
                sqlParams.Add(new SqlParameter("@orderNum", orderNum));

            if (!string.IsNullOrEmpty(scheduleQueue))
                sqlParams.Add(new SqlParameter("@scheduleQueue", scheduleQueue));

            if (!string.IsNullOrEmpty(customer))
                sqlParams.Add(new SqlParameter("@customer", customer));

            if (!string.IsNullOrEmpty(partNum))
                sqlParams.Add(new SqlParameter("@partNum", partNum));


            return EpicConnector.Queries.Utility.GetTableByStoredProc(environment, "Job.GetJobsCompletedProductionForArm", sqlParams.ToArray());
        }

        
        public DataTable SetInspectionData()
        {

            string sql = "SELECT Key2, CAST(Key3 AS DECIMAL(10,2)) Key3 From Utility.UD40 WHERE Company = @company AND Key1 = @key1  ORDER BY Number01";


            var sqlParams = new SqlParameter[]
            {
                new SqlParameter("@company", COMPANY),
                new SqlParameter("@key1", "InspectionPct")
               


            };


            return EpicConnector.Queries.Utility.GetTableBySql(environment, sql, sqlParams);
        
        }


        /// <summary>
        /// Updates the job mass part replcement record.
        /// </summary>
        /// <param name="modifiedRow">The job mass part replacement record to update.</param>
        /// <returns>True on success.</returns>
        /// 

        public bool UpdateJobRecords(DataRow modifiedRow)
        {
            var jobNum = modifiedRow["JobNum"].ToString();
            var plant = modifiedRow["ParameterPlant"].ToString();
            var scheduleQue = modifiedRow["ParameterScheduleQueue"].ToString();
            int FitupOprSeq = 0;
            int WeldOprSeq = 0;

            if (scheduleQue == "Unfirm Jobs")
            {
                 FitupOprSeq = int.Parse(modifiedRow["FitupOprSeq"].ToString());
                 WeldOprSeq = int.Parse(modifiedRow["WeldOprSeq"].ToString());
            }
           

           

            Erp.BO.JobEntryDataSet jeDs = null;

            try
            {
                epicorConnection = new EpicConnector.Connection(environment);

                //first make sure out connection to Epicor is using the right plant
                EpicConnector.Connection.Plants plantX = EpicConnector.Connection.Plants.TXALV;
                Enum.TryParse<EpicConnector.Connection.Plants>(plant, out plantX);
                epicorConnection.SetPlant(plantX);

                var errorMessage = string.Empty;
                jobHeadUpdate = new JobHeadUpdate();
                jobOperUpdate = new JobOperUpdate();

                jeDs = jobHeadUpdate.GetByID(jobNum, ref epicorConnection);

                var jobHeadRow = jeDs.JobHead.Rows[0] as Erp.BO.JobEntryDataSet.JobHeadRow;

                
                var columns = new List<EpicorSetterColumn>();
                if (scheduleQue == "Unfirm Jobs")
                {
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.JobEngineered, false));
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.JobReleased, false));
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.JobFirm, true));
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.ReqDueDate, DateTime.Parse(modifiedRow["ReqDueDate"].ToString())));
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.SequenceStart, int.Parse(modifiedRow["StartSeq"].ToString())));
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.SequenceEnd, int.Parse(modifiedRow["EndSeq"].ToString())));
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.InspectionPct, modifiedRow["Inspection"].ToString()));
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.ScheduleLine, modifiedRow["ScheduleLine"].ToString()));

                }

                if (scheduleQue == "Jobs Ready for Production")
                {
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.JobEngineered, false));
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.JobReleased, false));
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.ReqDueDate, DateTime.Parse(modifiedRow["ReqDueDate"].ToString())));
                    columns.Add(new EpicorSetterColumn(JobHeadUpdate.SchedLocked, true));
                }

             


                if (!jobHeadUpdate.Update(jobNum, columns, null, ref epicorConnection, ref jeDs, out errorMessage))
                    throw new Exception(errorMessage);

                jobNum = jeDs.JobHead[0].JobNum.ToString();

               
                if (scheduleQue == "Unfirm Jobs")
                {
                    // Update Fitup Operation Prod Standard
                    columns.Clear();
                    columns.Add(new EpicorSetterColumn(JobOperUpdate.ProdStandard, modifiedRow["FitupProdStd"].ToString()));

                    if (!jobOperUpdate.Update(jobNum, 0, FitupOprSeq, columns, null, ref epicorConnection, ref jeDs, out errorMessage))
                    {
                        throw new Exception(errorMessage);
                    }

                    // Update Weld Operation Prod Standard
                    columns.Clear();
                    columns.Add(new EpicorSetterColumn(JobOperUpdate.ProdStandard, modifiedRow["WeldOutProdStd"].ToString()));

                    if (!jobOperUpdate.Update(jobNum, 0, WeldOprSeq, columns, null, ref epicorConnection, ref jeDs, out errorMessage))
                    {
                        throw new Exception(errorMessage);
                    }
                }
               

               

                               
                //put the job engineered and released 
                columns.Clear();
                columns.Add(new EpicorSetterColumn(JobHeadUpdate.JobEngineered, true));
                columns.Add(new EpicorSetterColumn(JobHeadUpdate.JobReleased, true));

                columns.Add(new EpicorSetterColumn(JobHeadUpdate.ChangeDescription, string.Format("Arm Production Scheduler Tool - Modified by user '{0}' at '{1:MM/dd/yy hh:mm:ss tt}'", userId, DateTime.Now)));

                
                if (!jobHeadUpdate.Update(jobNum, columns, null, ref epicorConnection, ref jeDs, out errorMessage))
                    throw new Exception(errorMessage);

                modifiedRow.AcceptChanges();
                if (scheduleQue == "Unfirm Jobs")
                {
                    JobTravelerSTSArmsSSRS(jobNum, scheduleQue);
                }

                if (scheduleQue == "Jobs Ready for Production")
                {
                    MaterialQueueKitupsSSRS(jobNum, scheduleQue);
                }
                
                return true;



            }
            catch (Exception e)
            {
                Logging.LogMessage(TraceLevel.Error, "Failed to update the job '{0}' - {1}", jobNum, e.ToString());

                MessageBox.Show(string.Format("Failed to update the job '{0}'. {1}", jobNum, e.Message)); 
                return true;
            }
            finally
            {
                epicorConnection?.Dispose();
                epicorConnection = null;

                jeDs?.Dispose();
                jeDs = null; 
            } 
        }

        public void JobTravelerSTSArmsSSRS(string jobNum, string scheduleQue)
        {
            try
            {

                string filePath = $"{EpicUtility.Report.TempExportFolder}\\JobTraveler_STSArms_SSRS_{jobNum}_{DateTime.Today.ToString("yyyyMMdd")}.pdf";


                Logging.LogMessage(TraceLevel.Info,
                      $"Generating JobTraveler_STSArms_SSRS_{jobNum} Report, {DateTime.Today.ToShortDateString()}");

                RunReport(filePath, jobNum, scheduleQue);


                Logging.LogMessage(TraceLevel.Info,
                       $"Emailing JobTraveler_STSArms_SSRS_{jobNum} Report to distribution group \"JobTravelerSTSArms\"");

                EmailReport(filePath, jobNum, scheduleQue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_JobTravelerSTSArmsSSRS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MaterialQueueKitupsSSRS(string jobNum, string scheduleQue)
        {
           try
            { 
            string filePath = $"{EpicUtility.Report.TempExportFolder}\\MaterialQueueKitups_SSRS_{jobNum}_{DateTime.Today.ToString("yyyyMMdd")}.pdf";


            Logging.LogMessage(TraceLevel.Info,
                  $"Generating MaterialQueueKitups_SSRS_{jobNum} Report, {DateTime.Today.ToShortDateString()}");

            RunReport(filePath, jobNum, scheduleQue);


            Logging.LogMessage(TraceLevel.Info,
                   $"Emailing MaterialQueueKitups_SSRS_{jobNum} Report to distribution group \"MaterialQueueKitups\"");

            EmailReport(filePath, jobNum, scheduleQue);

        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_MaterialQueueKitupsSSRS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void RunReport(string filePath, string jobNum, string schQue)
        {
            try
            {

                string reportPath = "";

                if (schQue == "Unfirm Jobs")
                {
                     reportPath = $"Reports/Sabre/Live/STSJobTravelersArmsMultiSeq";
                }
                else if (schQue == "Jobs Ready for Production")
                {
                     reportPath = $"Reports/Sabre/Live/MaterialQueue_KitUps";
                }
            

                using (var ssrs = new EpicUtility.Reporting.SSRS(environment))
                {

                    var parameters = new Dictionary<string, string>();  
                    if (schQue == "Unfirm Jobs")
                    {
                        parameters.Add("JobNum", jobNum);
                    }
                    else if (schQue == "Jobs Ready for Production")
                    {
                        parameters.Add("jobNum", jobNum);
                    }
                   
               

                    ssrs.ExportReport(reportPath, filePath, parameters, EpicUtility.ReportTypes.Pdf);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_RunReport", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void EmailReport(string filePath, string jobNum, string schQue)
        {
            try
            {
                var DistributedList = "";
                string subject = "";
                string body = "";
                if (schQue == "Unfirm Jobs")
                {
                    subject = $"Job Traveler - STS Arms - {jobNum}";
                    body = $"This is Job Traveler - STS Arms report for job: {jobNum}";
                    DistributedList = EmailGroups.GetEmailByGroup(environment, "JobTravelerSTSArms");
                }
                else if (schQue == "Jobs Ready for Production")
                {
                    subject = $"Material Queue Kitups - {jobNum}";
                    body = $"This is Material Queue Kitups report for job: {jobNum}";
                    DistributedList = EmailGroups.GetEmailByGroup(environment, "MaterialQueueKitups");
                }


                var attachments = new List<System.IO.FileInfo>();

                attachments.Add(new System.IO.FileInfo(filePath));

                SendEmail.Send(DistributedList, "", "", subject, attachments, body, true);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error_EmailReport", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Gets the job ready production data group by SO from the database.
        /// </summary>
        /// <param name="plant">Required filter by plant.</param>
        /// <param name="orderNum">Optional filter by order number.</param>
        /// <param name="scheduleQueue">Required filter by schedule Queue.</param>
        /// <param name="customer">Optional filter by customer.</param>
        /// <param name="partNum">Optional filter by part number.</param>

        /// <returns>The job ready production data group by SO.</returns>
        public DataTable GetJobsScheduledProductionGroupBySO(string partNum, string orderNum, string plant, string customer, string scheduleQueue)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@company", COMPANY));

            if (!string.IsNullOrEmpty(partNum))
                sqlParams.Add(new SqlParameter("@partNum", partNum));

            if (!string.IsNullOrEmpty(orderNum))
                sqlParams.Add(new SqlParameter("@orderNum", orderNum));

            if (!string.IsNullOrEmpty(plant))
                sqlParams.Add(new SqlParameter("@plant", plant));

            if (!string.IsNullOrEmpty(customer))
                sqlParams.Add(new SqlParameter("@customer", customer));

            if (!string.IsNullOrEmpty(scheduleQueue))
                sqlParams.Add(new SqlParameter("@scheduleQueue", scheduleQueue));

           
           

            return EpicConnector.Queries.Utility.GetTableByStoredProc(environment, "Job.GetDataGroupBySOForArm", sqlParams.ToArray());
        }


        /// <summary>
        /// Gets the job Mtls records from the database.
        /// </summary>
        /// <param name="jobNum">Required filter by jobNum.</param>
        /// <param name="plant">Required filter by plant.</param>
       

        /// <returns>The job materials data.</returns>
        public DataTable ShowMaterials(string jobNum, string plant)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@company", COMPANY));

            if (!string.IsNullOrEmpty(jobNum))
                sqlParams.Add(new SqlParameter("@jobNum", jobNum));

            if (!string.IsNullOrEmpty(plant))
                sqlParams.Add(new SqlParameter("@plant", plant));

            return EpicConnector.Queries.Utility.GetTableByStoredProc(environment, "Job.GetJobMaterialDetailsForArms", sqlParams.ToArray());
        }


        public string ExtractEOPSQuote(string SO)
        {

            string sql = @"                
                    SELECT

                        Substring (EOPSQuoteNum, 1, CharIndex('-', EOPSQuoteNum)-1) EOPSQuotePrefix

                    FROM

                        Sales.OrderHed
                    WHERE

                        Company = @company
                        AND OrderNum = @orderNum
                        ";




            var sqlParams = new SqlParameter[]
            {
                new SqlParameter("@company", COMPANY),
                new SqlParameter("@orderNum", SO)

            };
            return EpicConnector.Queries.Utility.Scalar<string>(environment, sql, sqlParams);
            
        }


    }
}
