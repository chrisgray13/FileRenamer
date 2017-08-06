#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\FinishedPanel.cs-arc  $
 * $Revision:   1.1  $
 * $Author:   cgray  $
 * $Date:   Jul 24 2006 23:59:54  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\FinishedPanel.cs-arc  $
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
using System.Windows.Forms;

#endregion


namespace ICS.Utilities.DataExporter.WizardControlPanels
{
   /// <summary>
   /// FinishedPanel Class displays to the user the results of the Data Export.
   /// </summary>
   public class FinishedPanel : ICS.GUI_Library.Controls.WizardControlPanel
   {
      #region Fields

      private System.Windows.Forms.Label _lblFinishedMsg;
      private System.Windows.Forms.TextBox _txtSummary;

      private System.ComponentModel.IContainer _components = null;

      private string _sResult;

      private string _sSummary;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new FinishedPanel object.
      /// </summary>
      public FinishedPanel()
      {
      } // end FinishedPanel constructor

      #endregion

      #region Dispose

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">Determines whether or not to release both managed and unmanaged
      /// resources or only unmanaged resources.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (_components != null)
            {
               _components.Dispose();
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
         if (Name.Length == 0)
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
         this._lblFinishedMsg = new System.Windows.Forms.Label();
         this._txtSummary = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // _lblFinishedMsg
         // 
         this._lblFinishedMsg.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this._lblFinishedMsg.Location = new System.Drawing.Point(8, 8);
         this._lblFinishedMsg.Name = "_lblFinishedMsg";
         this._lblFinishedMsg.Size = new System.Drawing.Size(448, 50);
         this._lblFinishedMsg.TabIndex = 0;
         // 
         // _txtSummary
         // 
         this._txtSummary.AcceptsReturn = true;
         this._txtSummary.AcceptsTab = true;
         this._txtSummary.Cursor = System.Windows.Forms.Cursors.Default;
         this._txtSummary.Location = new System.Drawing.Point(8, 58);
         this._txtSummary.Multiline = true;
         this._txtSummary.Name = "_txtSummary";
         this._txtSummary.ReadOnly = true;
         this._txtSummary.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this._txtSummary.Size = new System.Drawing.Size(448, 180);
         this._txtSummary.TabIndex = 1;
         this._txtSummary.Text = "";
         this._txtSummary.WordWrap = false;
         // 
         // FinishedPanel
         // 
         this.Controls.Add(this._txtSummary);
         this.Controls.Add(this._lblFinishedMsg);
         this.Name = "FinishedPanel";
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #region DisplayResults

      /// <summary>
      /// Adds the text to the Finished Message Label and Summary Text to display the results to the user.
      /// </summary>
      public void DisplayResults()
      {
         _lblFinishedMsg.Text = _sResult;

         _txtSummary.Text = _sSummary;
      } // end DisplayResults

      #endregion

      #region ConstructResults

      /// <summary>
      /// Constructs the resuls by creating the Result Message and Summary Text based on the data
      /// supplied.
      /// </summary>
      /// <param name="bResult">Result Value</param>
      /// <param name="sSummary">Summary of the Result</param>
      public void ConstructResults(bool bResult, string sSummary)
      {
         _sResult = "RFSmart Version 3 data export " + (bResult ? "was Successful!" : "Failed!");

         _sSummary = sSummary;
      } // end ConstructResults

      #endregion

      #endregion
   } // end FinishedPanel Class
} // end ICS.Utilities.DataExporter.WizardControlPanels Namespace