#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\NiceLabelVariableImportWizard\NiceLabelVariableImportWizardWizard.cs-arc  $
 * $Revision:   1.4  $
 * $Author:   pmonaco  $
 * $Date:   Nov 08 2007 14:57:26  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\NiceLabelVariableImportWizard\NiceLabelVariableImportWizardWizard.cs-arc  $
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
using System.Threading;
using System.Resources;
using System.Globalization;
using Microsoft.Win32;

using ICS.GUI_Library.Controls;
using ICS.GUI_Library.EventArguments;
using ICS.GUI_Library.Forms;
using ICS.GUI_Library.Helpers;
using ICS.Utilities.NiceLabelVariableImporter.Properties;
using ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels;

using WizardCtrlPnlNmspc = ICS.GUI_Library.Controls.WizardControlPanels;

#endregion


namespace ICS.Utilities.NiceLabelVariableImporter
{
   /// <summary>
   /// Summary description for NiceLabelVariableImportWizardWizard.
   /// </summary>
   public partial class NiceLabelVariableImportWizard : Wizard
   {
      #region Fields

      ///*************************************************************************************
      /// NOTE:  This should be updated anytime a new panel is added to the Wizard's Sequence
      ///*************************************************************************************
      ///
      /// <summary>
      /// Enum to hold the panels for easy reference
      /// </summary>
      private enum _wizPnls
      {
         Intro,
         DataDropFile,
         LabelFormat,
         Summary,
         Progress,
         Finished
      }

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

      #region DataDropFilePnl

      /// <summary>
      /// Gets the DataDropFile Panel
      /// </summary>
      private DataDropFilePanel DataDropFilePnl
      {
         get { return ((DataDropFilePanel) _wizSeq[(int) _wizPnls.DataDropFile]); }
      } // end DataDropFilePnl property

      #endregion

      #region LabelFormatPnl

      /// <summary>
      /// Gets the LabelFormat Panel
      /// </summary>
      private LabelFormatPanel LabelFormatPnl
      {
         get { return ((LabelFormatPanel) _wizSeq[(int) _wizPnls.LabelFormat]); }
      } // end LabelFormatPnl property

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
      /// Constructs an NiceLabelVariableImportWizardWizard object and initializes the screen sequence and components.
      /// </summary>
      public NiceLabelVariableImportWizard() : base()
      {
         Resources.Culture = CultureInfo.CurrentUICulture;
#if DEBUG
         Resources.Culture = CultureInfo.CurrentCulture;
#endif

         AddShowPanelEvents();

         InitializeComponent();
      } // end NiceLabelVariableImportWizardWizard constructor

      #endregion

      #region Methods

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

         _wizSeq = new WizardCtrlPnlNmspc.WizardControlPanel[] { new IntroPanel(this),
                                                                 new DataDropFilePanel(this),
                                                                 new LabelFormatPanel(this),
                                                                 new SummaryPanel(this),
                                                                 new ProgressPanel(this),
                                                                 new FinishedPanel(this) };

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
         SummaryPnl.ShowPanel += new WizardCtrlPnlNmspc.ShowPanelEventHandler(ShowSummaryPanel);
         ProgressPnl.ShowPanel += new WizardCtrlPnlNmspc.ShowPanelEventHandler(ShowProgressPanel);
         FinishedPnl.ShowPanel += new WizardCtrlPnlNmspc.ShowPanelEventHandler(ShowFinishedPanel);
      } // end AddShowPanelEvents

      #endregion


      void LoadWizard(object sender, System.EventArgs e)
      {
         Show();
      }

      #region ConstructSummary

      /// <summary>
      /// Constructs the summary by going through each panel and appending the panel's summary to
      /// create the summary.
      /// </summary>
      /// <returns>Returns the constructed summary</returns>
      protected override string ConstructSummary()
      {
         string sSummary;

         sSummary = "Summary to import variables from a data drop file to a NiceLabel label format:" +
                    Environment.NewLine +
                    "-----------------------------------------------------------------------" +
                    Environment.NewLine + Environment.NewLine;

         sSummary += base.ConstructSummary();

         return (sSummary);
      } // end ConstructSummary

      #endregion

      #region Event Handlers

      #region ShowSummaryPanel

      /// <summary>
      /// Show Panel event for the Summary Panel, which sets the Summary of the SummaryPanel.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ShowSummaryPanel(WizardCtrlPnlNmspc.WizardControlPanel sender, System.EventArgs e)
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
      private void ShowProgressPanel(WizardCtrlPnlNmspc.WizardControlPanel sender, EventArgs e)
      {
         ProgressPanel progressPanel = sender as ProgressPanel;

         SetActiveView(false);

         Cursor = System.Windows.Forms.Cursors.WaitCursor;

         if (progressPanel != null)
         {
            // Adding the completed event to notify the wizard when the conversion is complete
            progressPanel.Completed += new ICS.GUI_Library.Controls.WizardControlPanels.CompletedEventHandler(VariableImportComplete);

            // Starting the data export by calling the ProgressPanel's StartExport method passing
            // the Data Access Layer for RFSmart Version 3, the selected environment from which the
            // data will be exported, and the file to which the data will be exported.
            progressPanel.StartTask(new VariableImporter(DataDropFilePnl.DataDropFilePath,
                                                         LabelFormatPnl.LabelFormatFilePath));
         } // end if
      } // end ShowProgressPanel

      #endregion

      #region VariableImportComplete

      /// <summary>
      /// Completed event handler, which is called when the conversion is complete.  This sets
      /// the active view of the wizard to allow the user to proceed.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void VariableImportComplete(object sender, ICS.Utilities.EventArguments.ResultsEventArgs e)
      {
         Cursor = System.Windows.Forms.Cursors.Default;

         SetActiveView(true, false);

         FinishedPnl.ConstructResults(e.Result, e.Summary);

      } // end VariableImportComplete

      #endregion

      #region ShowFinishedPanel

      /// <summary>
      /// Show Panel event handler for the Finished panel, which should set the results of the
      /// conversion.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ShowFinishedPanel(WizardCtrlPnlNmspc.WizardControlPanel sender, EventArgs e)
      {
         ((FinishedPanel) sender).DisplayResults();
      } // end ShowFinishedPanel

      #endregion

      #endregion

      #endregion
   } // end NiceLabelVariableImportWizardWizard Class
} // end ICS.Utilities.NiceLabelVariableImportWizard