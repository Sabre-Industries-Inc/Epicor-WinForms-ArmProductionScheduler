using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ArmProductionScheduler
{
    class Logging
    {
		/// <summary>
		/// Logs a message to the log file.
		/// </summary>
		/// <param name="level">The logging level.</param>
		/// <param name="message">The message to log.</param>
		/// <param name="args">The arguments for the message.</param>

		public static void LogMessage(TraceLevel level, string message, params object[] args)
		{
			var msg = args.Length == 0 ? message : string.Format(message, args);
			var logMsg = string.Format("[{0}][{1}]: {2}", GetShortLogLevel(level), DateTime.Now.ToString(), msg);
			File.AppendAllText(GetLogFilePath(), logMsg + System.Environment.NewLine);
		}

		#region -- Helper Methods --

		/// <summary>
		/// Gets the log file path.
		/// </summary>
		/// <returns>The log file path.</returns>

		private static string GetLogFilePath()
		{
			// Clean old logs
			CleanLogs();

			var directoryPath = GetLogFolderPath();

			var fileName = string.Format("ArmProductionScheduler_{0}.txt", DateTime.Now.ToString("MM_dd_yyyy"));

			return Path.Combine(directoryPath, fileName);

		}

		/// <summary>
		/// Cleans the old log files.
		/// </summary>

		private static void CleanLogs()
		{
			int days = 14;

			DirectoryInfo source = new DirectoryInfo(GetLogFolderPath());

			foreach (FileInfo fi in source.GetFiles())
			{
				if (fi.CreationTime < (DateTime.Now - new TimeSpan(days, 0, 0, 0)))
					fi.Delete();
			}

		}

		/// <summary>
		/// Builds and returns the log folder path.
		/// </summary>
		/// <returns>The log folder path.</returns>

		private static string GetLogFolderPath()
		{
			var directoryPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData), Path.Combine("MassPartReplacement", "Logs"));

			if (!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);

			return directoryPath;
		}




		/// <summary>
		/// Gets the log level as one character.
		/// </summary>
		/// <param name="level">The logging level.</param>
		/// <returns>The log level as one character.</returns>

		private static char GetShortLogLevel(TraceLevel level)
		{
			switch (level)
			{
				case TraceLevel.Error:
					return 'E';
				case TraceLevel.Info:
					return 'I';
				case TraceLevel.Verbose:
					return 'V';
				case TraceLevel.Warning:
					return 'W';
				default:
					return '?';
			}
		}

		#endregion -- Helper Methods --

	}
}
