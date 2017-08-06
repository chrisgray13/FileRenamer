#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\ProgressPanel.cs-arc  $
 * $Revision:   1.2  $
 * $Author:   cgray  $
 * $Date:   Jul 25 2006 10:50:44  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\ProgressPanel.cs-arc  $
 *
 *
 *
 */

#endregion

#region Usings

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;

using ICS.Database;
using ICS.GUI_Library.EventArguments;
using ICS.Utilities;

#endregion


namespace ICS.Utilities.DataExporter
{
   #region Delegates

   /// <summary>
   /// Used to specify how an Event Handler method should be constructed for the UpdateProgress
   /// Event.
   /// </summary>
   public delegate void UpdateProgressEventHandler(object sender, ProgressUpdateEventArgs e);

   /// <summary>
   /// Used to specify how an Event Handler method should be constructed for the TaskComplete
   /// Event.
   /// </summary>
   public delegate void TaskCompleteEventHandler(object sender, ResultsEventArgs e);

   #endregion

   /// <summary>
   /// Class used to export data from the RFSmart system database and ERP business database
   /// for the environments specified.  The data exported is specified within the DataToExport.xml
   /// file.
   /// </summary>
   public class DataExporter
   {
      #region Fields

      /// <summary>
      /// Data Access Layer used to connect to the System and Business databases
      /// </summary>
      private DAL _dataAccessLayer;

      /// <summary>
      /// Used to write to RFSmart logs
      /// </summary>
      private Logging _log;

      /// <summary>
      /// An array of environments for which the data is to be exported
      /// </summary>
      private string[] _sEnvironments;

      /// <summary>
      /// Path to which the export files are to be written
      /// </summary>
      private string _sExportFilePath;

      /// <summary>
      /// Used to store the results of the data export which will be used to write
      /// to the FinishedPanel
      /// </summary>
      private StringBuilder _sResults;

      /// <summary>
      /// Flag indicating the success of the data export
      /// </summary>
      private bool _bResult;

      #endregion

      #region Events

      /// <summary>
      /// Event to update the progress of the export.
      /// </summary>
      public event UpdateProgressEventHandler UpdateProgress;

      /// <summary>
      /// Event to indicate that the export is complete.
      /// </summary>
      public event TaskCompleteEventHandler TaskComplete;

      #endregion

      #region Properties

      #region DataToExportFile

      /// <summary>
      /// Gets the DataToExport file by checking each reference path within the app root path to see if
      /// DataToExport.xml exists.  If it does not exist, DataToExport.xml is returned.
      /// </summary>
      private string DataToExportFile
      {
         get
         {
            if (File.Exists(String.Format("{0}\\DataToExport.xml", Application.StartupPath)))
            {
               return (String.Format("{0}\\DataToExport.xml", Application.StartupPath));
            } // end if
            else
            {
               return ("DataToExport.xml");
            } // end else
         } // end get
      } // end DataToExportFile property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new DataExport object, which initializes the DataAccessLayer, Logging,
      /// Environments, and Export File Path and Name.
      /// </summary>
      /// <param name="dataAccessLayer">Data Access Layer for the RFSmart Version 3 Database</param>
      /// <param name="sEnvironments">An array of Environments to which the converted data
      /// will be applied</param>
      /// <param name="sExportFilePath">Path used to deposit the exported data.</param>
      public DataExporter(DAL dataAccessLayer, string[] sEnvironments, string sExportFilePath)
      {
         _dataAccessLayer = dataAccessLayer;

         _log = new Logging(LogType.System, "DataExporter-RFS");

         _sEnvironments = sEnvironments;

         _sExportFilePath = sExportFilePath + (sExportFilePath.EndsWith("\\") ? "" : "\\");

         _sResults = new StringBuilder();
      } // end DataExporter constructor

      #endregion

      #region Methods

      #region Start

      /// <summary>
      /// Exports the data from the RFSmart System database and ERP Business database for each
      /// environment specified.
      /// </summary>
      public void Start()
      {
         XmlDocument xmldocDataToExport = new XmlDocument();

         try
         {
            _bResult = true; // Setting the result flag to identify a failure

            xmldocDataToExport.Load(DataToExportFile);

            if (!Directory.Exists(_sExportFilePath))  // Create the directory if it does not exist
            {
               Directory.CreateDirectory(_sExportFilePath);
            } // end if

            // For each environment, export the data
            foreach (string sEnvironment in _sEnvironments)
            {
               ExportEnvironmentData(sEnvironment, xmldocDataToExport);
            } // end foreach
         } // end try
         catch (FileNotFoundException ex)
         {
            _log.LogException(ex);
            _bResult = false;

            RaiseUpdateProgressEvent("Unable to locate DataToExport.xml...", 0);
         } // end catch
         catch (Exception ex)
         {
            _log.LogException(ex);
            _bResult = false;
         } // end catch
         finally
         {
            RaiseUpdateProgressEvent(String.Format("Finished Exporting RFSmart Data{0}...",
                                                   (_bResult) ? "" : " with errors"),
                                     100);
            RaiseTaskCompleteEvent(_bResult);
         } // end finally
      } // end Start

      #endregion

      #region ExportEnvironmentData

      /// <summary>
      /// Exports the data for the specified Environment while logging the results.  After the
      /// data is exported, the exported data files are compressed into a .zip file.
      /// </summary>
      /// <param name="sEnvironment">Environment for which the data is to exported.</param>
      /// <param name="xmldocDataToExport">Xml Document containing a listing of data to be
      /// exported.</param>
      private void ExportEnvironmentData(string sEnvironment, XmlDocument xmldocDataToExport)
      {
         StringBuilder strExportedFiles = new StringBuilder();
         XmlNode xmlNode;

         // Logging the data export results header for the specified environment
         _sResults.AppendFormat("===================================={0}", Environment.NewLine);
         _sResults.AppendFormat("  {0} - Data Export Results{1}", sEnvironment, Environment.NewLine);
         _sResults.AppendFormat("===================================={0}{0}", Environment.NewLine);

         // Exporting the system data
         xmlNode = xmldocDataToExport.SelectSingleNode("/DataToExport/Tables[@type='System']");
         if (xmlNode != null) // Only export if their is data specified to export
         {
            ExportData(xmlNode, "System", sEnvironment, ref strExportedFiles);
         } // end if

         // Export the business data
         xmlNode = xmldocDataToExport.SelectSingleNode("/DataToExport/Tables[@type='Business']");
         if (xmlNode != null) // Only export if their is data specified to export
         {
            // Setting the business data database connection using the environment configuration
            _dataAccessLayer.SetDataDBConnection(GetEnvironmentConfiguration(sEnvironment));

            ExportData(xmlNode, "Business", sEnvironment, ref strExportedFiles);
         } // end if

         // Zipping the exported data files into a .zip for the environment
         ZipExportedDataFiles(sEnvironment, strExportedFiles);
      } // end ExportEnvironmentData

      #endregion

      #region GetEnvironmentConfiguration

      /// <summary>
      /// Gets the configuration for the specified environment, which contains information relating
      /// to the environment's business data base connection.
      /// </summary>
      /// <param name="sEnvironment">Environment for which the configuration is needed.</param>
      /// <returns>Returns the configuration of the environment specified if fetched; otherwise,
      /// null.</returns>
      private Configuration GetEnvironmentConfiguration(string sEnvironment)
      {
         Configuration config = null;

         _dataAccessLayer.System.Query = String.Format("SELECT Settings FROM RFS_Environment WHERE EnvironmentID=\'{0}\'",
                                                       sEnvironment);

         if (_dataAccessLayer.System.DataReader.Read())
         {
            // Sets Configuration for user
            config = new ICS.Utilities.Configuration(_dataAccessLayer.System.DataReader.GetString(0), sEnvironment);
         } // end if

         _dataAccessLayer.System.DataReader.Close();

         return (config);
      } // end GetEnvironmentConfiguration

      #endregion

      #region ExportData

      /// <summary>
      /// Exports the data from the RFSmart System database and ERP Business database.
      /// </summary>
      /// <param name="xmlTablesNode">Xml Node containing the Tables and Columns to be exported</param>
      /// <param name="sDBDataType">Database data type (System or Business)</param>
      /// <param name="sEnvironment">Environment to be exported, which is needed for the System
      /// data to ensure that only the data for that environment is exported.  If this is not
      /// provided, null will be used as the default value to exclude the constraint.</param>
      /// <param name="strExportedFiles">A StringBuilder to store the exported data file names, which will
      /// be used to identify what is to be zipped.</param>
      private void ExportData(XmlNode xmlTablesNode, string sDBDataType,
                              string sEnvironment, ref StringBuilder strExportedFiles)
      {
         DataTableExporter dataTableExporter = new DataTableExporter(_dataAccessLayer, _sExportFilePath, sDBDataType);
         string sTableName = "";
         int iProgressVal = 100 / xmlTablesNode.ChildNodes.Count;
         int i = 0;

         try
         {
            RaiseUpdateProgressEvent(String.Format("Exporting RFSmart {0} data...", sDBDataType.ToLower()), 0);
            _sResults.AppendFormat("   {0} Tables:{1}", sDBDataType, Environment.NewLine);

            foreach (XmlNode xmlTableNode in xmlTablesNode.ChildNodes)  // For each table, export the data
            {
               sTableName = xmlTableNode.Attributes["name"].Value;  // Identifying the table name
               strExportedFiles.Append(sTableName + ".txt ");  // Adding the export data file
               RaiseUpdateProgressEvent(String.Format("Exporting {0}...", sTableName), i++ * iProgressVal);

               // Export the data and log results
               if (dataTableExporter.ExportDataTableUsingDataReader(sTableName,
                                                                    GetColumnsToSelect(xmlTableNode.FirstChild),
                                                                    sEnvironment))
               {
                  _sResults.AppendFormat("      {0} - Success{1}", sTableName, Environment.NewLine);
               } // end if
               else
               {
                  _sResults.AppendFormat("      {0} - Failed{1}", sTableName, Environment.NewLine);
                  _bResult = false;
               } // end else
            } // end foreach
         } // end try
         catch (Exception ex)
         {
            _log.LogException(ex);

            _sResults.AppendFormat("      {0} - Failed (see logs for more information){1}",
                                   sTableName, Environment.NewLine);
            _bResult = false;
         } // end catch
         finally
         {
            _sResults.Append(Environment.NewLine);
        //    dataTableExporter.Dispose();
         } // end finally
      } // end ExportData

      #endregion

      #region GetColumnsToSelect

      /// <summary>
      /// Gets the columns to select from the Xml Node as a string delimited by a comma such that the
      /// result may be added directly into a SELECT clause.
      /// </summary>
      /// <param name="xmlColumnsNode">Xml Node containing a list of column nodes.</param>
      /// <returns>Returns a list of columns delimited by a comma based on the columns found.  If
      /// nothing is found, "*" is returned.</returns>
      private string GetColumnsToSelect(XmlNode xmlColumnsNode)
      {
         StringBuilder sColumnsToSelect = new StringBuilder();

         // For each column node, append the column to the set of columns with a comma
         foreach (XmlNode xmlColumnNode in xmlColumnsNode.ChildNodes)
         {
            // If the columns to select is blank, or the column node is blank, do not
            // prefix the column with a comma
            if ((sColumnsToSelect.Length > 0) && (xmlColumnNode.InnerText.Length > 0))
            {
               sColumnsToSelect.Append(", ");
            } // end if

            sColumnsToSelect.Append(xmlColumnNode.InnerText);
         } // end foreach

         return ((sColumnsToSelect.Length == 0) ? "*" : sColumnsToSelect.ToString());
      } // end GetColumnsToSelect

      #endregion

      #region ZipExportedDataFiles

      /// <summary>
      /// Compress the exported data files into a .zip file.
      /// </summary>
      /// <param name="sEnvironment">Environment for which the data files were exported.</param>
      /// <param name="strExportedFiles">A StringBuilder containing a list of the data files exported.</param>
      private void ZipExportedDataFiles(string sEnvironment, StringBuilder strExportedFiles)
      {
         System.Diagnostics.Process proc = new System.Diagnostics.Process();

         try
         {
            RaiseUpdateProgressEvent(String.Format("Compressing exported data for {0}...", sEnvironment), 100);

            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "zip";

            // Creating the command-line arguments for the zip application
            proc.StartInfo.Arguments = String.Format("-m -9 {0}-DataExporter.zip {1}",
                                                     sEnvironment, strExportedFiles.ToString());
            proc.StartInfo.WorkingDirectory = _sExportFilePath;  // Ensure the correct directory is specified
            proc.StartInfo.UseShellExecute = false;  // Hide Shell Command Window
            proc.StartInfo.CreateNoWindow = true;

            _log.WriteLine(2, "Compressing the exported data files via the following command:");
            _log.WriteLine(2, String.Format("  zip -m -9 {0}-DataExporter.zip {1}",
                                            sEnvironment, strExportedFiles.ToString()));

            // Begin the compression
            proc.Start();

            _sResults.AppendFormat("   Exported files were compressed into {0}{1}-DataExporter.zip{2}{2}",
                                   _sExportFilePath, sEnvironment, Environment.NewLine);
         } // end try
         catch (Exception ex)
         {
            _log.LogException(ex);
         } // end catch
      } // end ZipExportedDataFiles

      #endregion

      #region Event Raisers

      #region RaiseUpdateProgressEvent

      /// <summary>
      /// Raises the UpdateProgress event, which is used to set the Progress Message and Progress
      /// Value to send to the handler.
      /// </summary>
      /// <param name="sProgressMsg">Message to indicate what the progress is.</param>
      /// <param name="iProgressValue">Value between the minimum and maximum values
      /// to indicate the progress value.</param>
      private void RaiseUpdateProgressEvent(string sProgressMsg, int iProgressValue)
      {
         if (UpdateProgress != null)
         {
            UpdateProgress(this, new ProgressUpdateEventArgs(sProgressMsg, iProgressValue));
         } // end if
      } // end RaiseUpdateProgressEvent

      #endregion

      #region RaiseTaskCompleteEvent

      /// <summary>
      /// Raises an event indicating that the Task is finished.
      /// </summary>
      /// <param name="bTaskSuccess">Success rate of the task.</param>
      private void RaiseTaskCompleteEvent(bool bTaskSuccess)
      {
         if (TaskComplete != null)
         {
            TaskComplete(this, new ResultsEventArgs(bTaskSuccess, _sResults.ToString()));
         } // end if
      } // end RaiseTaskCompleteEvent

      #endregion

      #endregion

      #endregion
   } // end DataExporter Class
} // end ICS.Utilities.DataExporter Namespace