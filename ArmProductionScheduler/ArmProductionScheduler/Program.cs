using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmProductionScheduler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length >= 2)
            {
                var env = EpicConnector.Connection.Environments.None;
                var plant = EpicConnector.Connection.Plants.None;
                var errorBuilder = new System.Text.StringBuilder();

                foreach (string arg in args)
                {
                    if (Enum.TryParse(arg, out EpicConnector.Connection.Environments tempEnv))
                    {
                        env = tempEnv;
                    }
                    else if (Enum.TryParse(arg, out EpicConnector.Connection.Plants tempPlant))
                    {
                        plant = tempPlant; 
                    }
                    else 
                    {
                        errorBuilder.AppendLine("Invalid Argument: " + arg);
                    }
                }

                if (env == EpicConnector.Connection.Environments.None)
                {
                    errorBuilder.AppendLine("Missing Argument: Environment");
                }

                if (plant == EpicConnector.Connection.Plants.None)
                {
                    errorBuilder.AppendLine("Missing Argument: Plant");
                }

                if (errorBuilder.Length > 0)
                {
                    MessageBox.Show(errorBuilder.ToString());
                    return;
                }

                Application.Run(new ArmProductionScheduler(env, plant));

            }

            
        }
    }
}
