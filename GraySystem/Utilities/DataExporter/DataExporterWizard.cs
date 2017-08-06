#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\UpgradeToolWizard.cs-arc  $
 * $Revision:   1.1  $
 * $Author:   cgray  $
 * $Date:   Jul 25 2006 00:25:00  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\UpgradeToolWizard.cs-arc  $
 *
 *
 *
 */

#endregion

#region Usings

using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

using ICS.Database;
using ICS.GUI_Library.Controls.WizardControlPanels;
using ICS.GUI_Library.EventArguments;
using ICS.GUI_Library.Forms;
using ICS.Utilities.DataExporter.WizardControlPanels;

#endregion


namespace ICS.Utilities.DataExporter
{
   /// <summary>
   /// Summary description for DataExporterWizard.
   /// </summary>
   [Serializable]
   public class DataExporterWizard : Wizard
   {
      #region Fields

      ///*************************************************************************************
      /// NOTE:  This should be updated anytime a new panel is added to the Wizard's Sequence
      ///*************************************************************************************
      ///
      /// <summary>
      /// Enum to hold the panels for easy reference
      /// </summary>
      private enum _wizPnls { Intro,
                              Environment,
                              ExportFilePath,
                              Summary,
                              Progress,
                              Finished }

      // *************************************************************************************

      #endregion

      #region Properties

      #region IntroPnl

      /// <summary>
      /// Gets the Intro Panel
      /// </summary>
      private IntroPanel IntroPnl
      {
         get { return ((IntroPanel) _wizSeq[(int) _wizPnls.Intro]); }
      } // end IntroPnl property

      #endregion

      #region EnvironmentPnl

      /// <summary>
      /// Gets the Environment Panel
      /// </summary>
      private EnvironmentPanel EnvironmentPnl
      {
         get { return ((EnvironmentPanel) _wizSeq[(int) _wizPnls.Environment]); }
      } // end EnvironmentPnl property

      #endregion

      #region ExpFilePathPnl

      /// <summary>
      /// Gets the Export File Path Panel
      /// </summary>
      private ExportFilePathPanel ExpFilePathPnl
      {
         get { return ((ExportFilePathPanel) _wizSeq[(int) _wizPnls.ExportFilePath]); }
      } // end ExpFilePathPnl property

      #endregion

      #region SummaryPnl

      /// <summary>
      /// Gets the Summary Panel
      /// </summary>
      private SummaryPanel SummaryPnl
      {
         get { return ((SummaryPanel) _wizSeq[(int) _wizPnls.Summary]); }
      } // end SummaryPnl property

      #endregion

      #region ProgressPnl

      /// <summary>
      /// Gets the Progress Panel
      /// </summary>
      private ProgressPanel ProgressPnl
      {
         get { return ((ProgressPanel) _wizSeq[(int) _wizPnls.Progress]); }
      } // end ProgressPnl property

      #endregion

      #region FinishedPnl

      /// <summary>
      /// Gets the Finished Panel
      /// </summary>
      private FinishedPanel FinishedPnl
      {
         get { return ((FinishedPanel) _wizSeq[(int) _wizPnls.Finished]); }
      } // end FinishedPnl property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a DataExporterWizard object and initializes the screen sequence and components.
      /// </summary>
      public DataExporterWizard() : base()
      {
         AddShowPanelEvents();

         InitializeComponent();
      } // end DataExporterWizard constructor

      #endregion

      #region Methods

      #region InitializeComponent

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DataExporterWizard));
         // 
         // _picBanner
         // 
         this._picBanner.Image = ((System.Drawing.Image)(resources.GetObject("_picBanner.Image")));
         // 
         // DataExporterWizard
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(464, 382);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "DataExporterWizard";
         this.Text = "Data Exporter";
      } // end IntializeComponent

      #endregion

      #region InitializeSequence

      /// <summary>
      /// Initializes the Wizard's sequence by setting the _wizSeq array with
      /// the panels in the order in which they should appear to the user.
      /// </summary>
      protected override void InitializeSequence()
      {
         //********************************************************************
         // NOTE:  If you add a new panel to the sequence, you should also
         //        add it to the _wizPnls enumerator.
         //********************************************************************

         _wizSeq = new WizardControlPanel[] { new IntroPanel("Welcome to the ICS, Inc. RFSmart Data " +
                                                             "Exporter.  This tool is to be used to " +
                                                             "export data from RFSmart System tables " +
                                                             "and ERP Business tables."),
                                              new EnvironmentPanel("RFSmart Version 3 Environment(s) for " +
                                                                   "which the data will be exported"),
                                              new ExportFilePathPanel(),
                                              new SummaryPanel(),
                                              new ProgressPanel(),
                                              new FinishedPanel("RFSmart Version 3 data export") };

         //********************************************************************
      } // end InitializeSequence

      #endregion

      #region AddShowPanelEvents

      /// <summary>
      /// Adds the ShowPanel events to the panels that need to perform some action before they are
      /// shown.
      /// </summary>
      protected void AddShowPanelEvents()
      {
         EnvironmentPnl.ShowPanel += new ShowPanelEventHandler(ShowEnvironmentPanel);
         SummaryPnl.ShowPanel += new ShowPanelEventHandler(ShowSummaryPanel);
         ProgressPnl.ShowPanel += new ShowPanelEventHandler(ShowProgressPanel);
         FinishedPnl.ShowPanel += new ShowPanelEventHandler(ShowFinishedPanel);
      } // end AddShowPanelEvents

      #endregion

      #region GetSystemDBConnection

      private DAL GetSystemDBConnection()
      {
         // TODO:  Try registry.  If that does not work, try RFS.Config.  There may be an API
         // that will determine that for me.  Check the BackOffice code.

         return (new DAL("DataExporter-RFS"));
      } // end GetSystemDBConnection

      #endregion

      #region ConstructSummary

      /// <summary>
      /// Constructs the summary by going through each panel and appending the panel's summary to
      /// create the summary.
      /// </summary>
      /// <returns>Returns the constructed summary</returns>
      protected override string ConstructSummary()
      {
         string sSummary;

         sSummary = "Summary of the Data to be Exported:" +
                    Environment.NewLine +
                    "-----------------------------------------------------------------------" +
                    Environment.NewLine + Environment.NewLine;

         sSummary += base.ConstructSummary();

         return (sSummary);
      } // end ConstructSummary

      #endregion

      #region Run

      /// <summary>
      /// Allows the wizard to be executed from the command-line.  It needs the export path and
      /// environment ids.
      /// </summary>
      /// <param name="args">An array of command-line arguments, which should include the export path
      /// and a listing of Environment Ids.</param>
      public override void Run(string[] args)
      {
         DataExporter dataExporter;
         string[] sEnvironments;
         Logging log;

         // As long as there are at least two command-line arguments, export the data; otherwise, log
         // an error with instructions
         if (args.Length > 1)
         {
            // Getting the Environments out of the args, which should be included in elements 1 - n
            sEnvironments = new string[args.Length - 1];
            for (int i = 1; i < args.Length; i++)
            {
               sEnvironments[i - 1] = args[i];
            } // end for

            // Starting the data export by calling the ProgressPanel's StartExport method passing
            // the Data Access Layer for RFSmart Version 3, the selected environment from which the
            // data will be exported, and the file to which the data will be exported.
            dataExporter = new DataExporter(new DAL("DataExporter-RFS"), sEnvironments, args[0]);
            dataExporter.TaskComplete += new TaskCompleteEventHandler(LogResults);
            dataExporter.Start();
         } // end if
         else
         {
            log = new Logging(LogType.System, "DataExporter-RFS");
            log.WriteLine(0, "The following command line arguments are invalid:  " + String.Join(" ", args));
            log.WriteLine(0, "");
            log.WriteLine(0, "Please use the following when running DataExporter-RFS.exe from the command-line:");
            log.WriteLine(0, "");
            log.WriteLine(0, "   DataExporter-RFS [Export Path] [EnvironmentId1]...[EnvironmentIdn]");
            log.WriteLine(0, "");
         } // end else
      } // end Run

      #endregion

      #region Event Handlers

      #region ShowEnvironmentPanel

      /// <summary>
      /// Show Panel event for the Environment selection Panel, which sets the
      /// Database Connection based on the RFSmart Version 3 installation.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ShowEnvironmentPanel(WizardControlPanel sender, EventArgs e)
      {
         EnvironmentPanel envPanel = sender as EnvironmentPanel;

         if (envPanel != null)
         {
            envPanel.LoadEnvironments(new DAL("DataExporter-RFS"));
         } // end if
      } // end ShowEnvironmentPanel

      #endregion

      #region ShowSummaryPanel

      /// <summary>
      /// Show Panel event for the Summary Panel, which sets the Summary of the SummaryPanel.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ShowSummaryPanel(WizardControlPanel sender, System.EventArgs e)
      {
         ((SummaryPanel) sender).Summary = ConstructSummary();
      } // end ShowSummaryPanel

      #endregion

      #region ShowProgressPanel

      /// <summary>
      /// ShowPanel event for the ProgressPanel, which added a completed event to notify the wizard
      /// when the conversion is complete, and starts the conversion.
      /// </summary>
      /// <param name="sender">Progress Panel</param>
      /// <param name="e"></param>
      private void ShowProgressPanel(WizardControlPanel sender, EventArgs e)
      {
         ProgressPanel progressPanel = sender as ProgressPanel;

         SetActiveView(false);

         Cursor = System.Windows.Forms.Cursors.WaitCursor;

         if (progressPanel != null)
         {
            // Adding the completed event to notify the wizard when the conversion is complete
            progressPanel.Completed += new CompletedEventHandler(ConstructResults);

            // Starting the data export by calling the ProgressPanel's StartExport method passing
            // the Data Access Layer for RFSmart Version 3, the selected environment from which the
            // data will be exported, and the file to which the data will be exported.
            progressPanel.StartTask(new DataExporter(EnvironmentPnl.DataAccessLayer,
                                                     EnvironmentPnl.Environments,
                                                     ExpFilePathPnl.ExportFilePath));
          } // end if
      } // end ShowProgressPanel

      #endregion

      #region ConstructResults

      /// <summary>
      /// Completed event handler, which is called when the data export is complete.  This sets
      /// the active view of the wizard to allow the user to proceed.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ConstructResults(object sender, ResultsEventArgs e)
      {
         Cursor = System.Windows.Forms.Cursors.Default;

         SetActiveView(true, false);

         FinishedPnl.ConstructResults(e.Result, e.Summary);

         // Called to close the DB Connections
         EnvironmentPnl.Dispose();
      } // end ConstructResults

      #endregion

      #region ShowFinishedPanel

      /// <summary>
      /// Show Panel event handler for the Finished panel, which should set the results of the
      /// conversion.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ShowFinishedPanel(WizardControlPanel sender, EventArgs e)
      {
         ((FinishedPanel) sender).DisplayResults();
      } // end ShowFinishedPanel

      #endregion

      #region LogResults

      /// <summary>
      /// Logs the results to the DataExporter-RFS system log.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void LogResults(object sender, ResultsEventArgs e)
      {
         Logging log = new Logging(LogType.System, "DataExporter-RFS");

         log.WriteLine(0, "RFSmart Version 3 data export" + (e.Result ? "was Successful!" : "Failed!"));
         log.WriteLine(0, Environment.NewLine + e.Summary);
      } // end LogResults

      #endregion

      #endregion

      #endregion
   } // end DataExporterWizard Class
} // end ICS.Utilities.DataExporter Namespace