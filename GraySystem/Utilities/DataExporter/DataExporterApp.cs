#region PVCS Comments

/*
 *  Copyright ICS, Inc. 2005
 *  All rights are reserved. Reproduction or transmission in whole or in part,
 *  in any form or by any means, electronic, mechanical or otherwise, is
 *  prohibited without the prior written consent of the copyright owner.
 *
 *  $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\UpgradeToolApp.cs-arc  $
 *  $Revision:   1.1  $
 *  $Author:   cgray  $
 *  $Date:   Jul 25 2006 10:53:30  $
 *
 *  $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\UpgradeToolApp.cs-arc  $
 * 
 *
 */

#endregion

#region Usings

using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

#endregion


namespace ICS.Utilities.DataExporter
{
   /// <summary>
   /// Summary description for DataExporterApp.
   /// </summary>
   public class DataExporterApp
   {
      #region Main

      /// <summary>
      /// Main entry point for the application.  It creates an Application Domain for the application
      /// in order for it to utilize both the utilities and bin directories.
      /// </summary>
      [STAThread]
      static void Main(string[] args)
      {
         try
         {
            DataExporterWizard dataExporterWiz;

            ICS.ClearCache.Download.ClearCache();

            Application.EnableVisualStyles();  // Displays Current Visual Styles
            Application.DoEvents();  // Instructs the Application to handle multiple events at once

            DirectoryInfo dirInfo = new DirectoryInfo(ConfigurationSettings.AppSettings["AppRootPath"]);
            string[] sPaths = ConfigurationSettings.AppSettings["ReferencePath"].Split(';');

            AppDomain domain = AppDomain.CreateDomain("DataExporter-RFS",
                                                      null,
                                                      dirInfo.Parent.FullName,
                                                      dirInfo.Name + "\\" + sPaths[0] + ";" +
                                                      dirInfo.Name + "\\" + sPaths[1],
                                                      true);

            AppDomain.CurrentDomain.InitializeLifetimeService();
            domain.InitializeLifetimeService();

            dataExporterWiz = (DataExporterWizard) domain.CreateInstanceAndUnwrap(domain.Load(Assembly.GetExecutingAssembly().FullName).FullName,
                                                                                  "ICS.Utilities.DataExporter.DataExporterWizard");

            // There are no command-line arguments, show the GUI; otherwise, run the application
            // in the background
            if (args.Length == 0)
            {
               dataExporterWiz.Show();
            } // end if
            else
            {
               dataExporterWiz.Run(args);
            } // end else
         } // end try
         catch (Exception ex)
         {
            LogErrors(ex);
         } // end catch
      } // end Main

      #endregion

      #region LogErrors

      /// <summary>
      /// Logs exceptions to DataExporter-RFS.log.
      /// </summary>
      /// <param name="ex">Exception that needs to be logged.</param>
      private static void LogErrors(Exception ex)
      {
         RegistryKey regSubKeyRFSV3 = null;
         StreamWriter logStreamWriter = null;

         try
         {
            regSubKeyRFSV3 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ICS\\RFSmart3");
            if (regSubKeyRFSV3 != null)
            {
               // Note:  using the explicit cast to string instead of ToString because if GetValue
               // returns null, ToString will cause an exception, this prevents the exception.
               string sLogPath = (string) regSubKeyRFSV3.GetValue("LogDirectory");

               FileInfo fileInfo = new FileInfo(sLogPath + "\\DataExporter-RFS.log");

               logStreamWriter = (fileInfo.Exists) ? fileInfo.AppendText() : fileInfo.CreateText();
               logStreamWriter.WriteLine(ex.Message);

               regSubKeyRFSV3.Close();
            } // end if
         } // end try
         catch
         {
         } // end catch
         finally
         {
            if (logStreamWriter != null)
            {
               logStreamWriter.Close();
            } // end if
         } // end finally
      } // end LogErrors

      #endregion
   } // end DataExporterApp Class
} // end ICS.Utilities.DataExporter Namespace