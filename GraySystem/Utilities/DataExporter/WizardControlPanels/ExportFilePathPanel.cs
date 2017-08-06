#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\ExportFilePathPanel.cs-arc  $
 * $Revision:   1.0  $
 * $Author:   cgray  $
 * $Date:   Jul 21 2006 17:37:40  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\ExportFilePathPanel.cs-arc  $
 * 
 *
 *
 */

#endregion

#region Usings

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ICS.GUI_Library.Controls.WizardControlPanels;

#endregion


namespace ICS.Utilities.DataExporter.WizardControlPanels
{
   /// <summary>
   /// ExportFilePathPanel Class allows a user to specify a file path, which will be used
   /// to store the compressed exported data files exported in the same format used by the
   /// ObjectWizard.
   /// </summary>
   public class ExportFilePathPanel : WizardControlPanel
   {
      #region Fields

      #region Controls

      private System.Windows.Forms.Button _btnBrowse;
      private System.Windows.Forms.Label _lblExportFilePath;
      private System.Windows.Forms.Label _lblExportMessage;
      private System.Windows.Forms.TextBox _txtExportFilePath;

      #endregion

      private System.ComponentModel.IContainer components = null;

      #endregion

      #region Properties

      #region ExportFilePath

      /// <summary>
      /// Gets the file path to which the data will be exported.
      /// </summary>
      public string ExportFilePath
      {
         get { return (_txtExportFilePath.Text.Trim()); }
      } // end ExportFilePath property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new ExportFilePathPanel object.
      /// </summary>
      public ExportFilePathPanel() : base()
      {
      } // end ExportFilePathPanel constructor

      #endregion

      #region Dispose

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (components != null)
            {
               components.Dispose();
            } // end if
         } // end if

         base.Dispose(disposing);
      } // end Dispose

      #endregion

      #region Methods

      #region Initialize

      /// <summary>
      /// Initializes the panel by adding the controls and event handlers.
      /// </summary>
      protected override void Initialize()
      {
         // Checking to see if the panel has been initialized yet, which is denoted by the Name
         // being set as initially it is blank.
         if (Name == "")
         {
            base.Initialize();

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
         } // end if
      } // end Initialize

      #endregion

      #region InitializeComponent

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this._btnBrowse = new System.Windows.Forms.Button();
         this._txtExportFilePath = new System.Windows.Forms.TextBox();
         this._lblExportFilePath = new System.Windows.Forms.Label();
         this._lblExportMessage = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // _btnBrowse
         // 
         this._btnBrowse.Location = new System.Drawing.Point(411, 134);
         this._btnBrowse.Name = "_btnBrowse";
         this._btnBrowse.Size = new System.Drawing.Size(24, 24);
         this._btnBrowse.TabIndex = 3;
         this._btnBrowse.Text = "...";
         this._btnBrowse.Click += new System.EventHandler(this.ShowDirectoryDialog);
         // 
         // _txtExportFilePath
         // 
         this._txtExportFilePath.Location = new System.Drawing.Point(64, 136);
         this._txtExportFilePath.Name = "_txtExportFilePath";
         this._txtExportFilePath.Size = new System.Drawing.Size(344, 20);
         this._txtExportFilePath.TabIndex = 2;
         this._txtExportFilePath.Text = "";
         this._txtExportFilePath.KeyPress += new KeyPressEventHandler(GoForwardIfEnterPressed);
         // 
         // _lblExportFilePath
         // 
         this._lblExportFilePath.Location = new System.Drawing.Point(30, 138);
         this._lblExportFilePath.Name = "_lblExportFilePath";
         this._lblExportFilePath.Size = new System.Drawing.Size(32, 16);
         this._lblExportFilePath.TabIndex = 1;
         this._lblExportFilePath.Text = "Path:";
         this._lblExportFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // _lblExportMessage
         // 
         this._lblExportMessage.Location = new System.Drawing.Point(24, 16);
         this._lblExportMessage.Name = "_lblExportMessage";
         this._lblExportMessage.Size = new System.Drawing.Size(416, 72);
         this._lblExportMessage.TabIndex = 0;
         this._lblExportMessage.Text = @"Please enter the path for the RFSmart Version 3 exported data.  " +
                                        "Alternatively, use the browse button to select the path from a " +
                                        "directory dialog box.";
         // 
         // ExportFilePathPanel
         // 
         this.Controls.Add(this._btnBrowse);
         this.Controls.Add(this._txtExportFilePath);
         this.Controls.Add(this._lblExportFilePath);
         this.Controls.Add(this._lblExportMessage);
         this.Name = "ExportFilePathPanel";
         this.Size = new System.Drawing.Size(464, 216);
         this.LeavePanel += new LeavePanelEventHandler(ValidateExportFilePathProvided);
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #region ConstructSummary

      /// <summary>
      /// Constructs the summary for the panel, which includes the export file name to which the
      /// exported data will be compressed.
      /// </summary>
      protected override string ConstructSummary()
      {
         StringBuilder strSummary = new StringBuilder();

         strSummary.Append("   Exported Data Path:" + Environment.NewLine);
         strSummary.Append("      " + _txtExportFilePath.Text.Trim() + Environment.NewLine);

         return (strSummary.ToString());
      } // end ConstructSummary

      #endregion

      #region Event Handlers

      #region ShowDirectoryDialog

      /// <summary>
      /// Shows the FolderBrowserDialog in order to allow the user to specify a directory to
      /// which the exported data will be exported.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ShowDirectoryDialog(object sender, EventArgs e)
      {
         FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

         folderBrowserDialog.ShowNewFolderButton = true;
         if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
         {
            _txtExportFilePath.Text = folderBrowserDialog.SelectedPath;
         } // end if
      } // end ShowDirectoryDialog

      #endregion

      #region ValidateExportFilePathProvided

      /// <summary>
      /// LeavePanel Event Handler, which validates that an Export File was specified before the user
      /// leaves the panel.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      /// <returns>Returns true if an export file was specified; otherwise, false.</returns>
      private bool ValidateExportFilePathProvided(WizardControlPanel sender, EventArgs e)
      {
         // Checking to ensure that an export file path has been specified
         if (_txtExportFilePath.Text.Trim().Length == 0)
         {
            MessageBox.Show(this, "Please specify a directory to which the export data will be exported.",
                           "Missing Export Path", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return (false);
         } // end  if
         else
         {
            return (true);
         } // end else
      } // end ValidateExportFilePathProvided

      #endregion

      #region GoForwardIfEnterPressed

      /// <summary>
      /// Checks to see if the key pressed by the user was the [Enter] key.  If it was, it is treated
      /// as though the user clicked the Next button on the wizard.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void GoForwardIfEnterPressed(object sender, KeyPressEventArgs e)
      {
         if (e.KeyChar == (char) 13)
         {
            // Set to indicate that the key press was handled and the underlying event handler
            // does not need to be executed.  This prevents a warning beep from occurring because
            // the user pressed [Enter] in a control that does not allow [Enter]
            e.Handled = true;

            ((ICS.GUI_Library.Forms.Wizard) Parent).RaiseNextButtonClick();
         } // end if
      } // end GoForwardIfEnterPressed

      #endregion

      #endregion

      #endregion
   } // end ExportFilePathPanel Class
} // end ICS.Utilities.DataExporter.WizardControlPanels Namespace