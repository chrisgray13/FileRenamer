#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\FinishedPanel.cs-arc  $
 * $Revision:   1.2  $
 * $Author:   cgray  $
 * $Date:   May 19 2008 21:32:18  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\FinishedPanel.cs-arc  $
 * 
 *    Rev 1.2   May 19 2008 21:32:18   cgray
 * BE25492 - Added a new constructor that takes a Wizard object and sets it as the wizard (parent) of the control panel.
 * 
 * Added a call to the InitializeComponent method within the base constructor to ensure the Design-time view works correctly.
 * 
 *    Rev 1.1   Nov 08 2007 14:36:04   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.0   Feb 12 2007 15:00:22   cgray
 * Initial revision.
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


namespace GraySystem.UI.Controls.WizardControlPanels
{
   /// <summary>
   /// FinishedPanel Class displays to the user the results of the wizard's process.
   /// </summary>
   public class FinishedPanel : WizardControlPanel
   {
      #region Fields

      #region Controls

      /// <summary>
      /// Displays a result message indicating the success or failure of the wizard's process.
      /// </summary>
      protected System.Windows.Forms.Label _lblFinishedMsg;

      /// <summary>
      /// Displays a summary of the results.
      /// </summary>
      protected System.Windows.Forms.TextBox _txtSummary;

      #endregion

      private System.ComponentModel.IContainer _components = null;

      /// <summary>
      /// Used to store the heading for the panel's result, which will be used to
      /// write the result for the process.
      /// </summary>
      private string _sResultHeading;

      private string _sResult;

      private string _sSummary;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new FinishedPanel object.
      /// </summary>
      protected FinishedPanel() : base()
      {
         InitializeComponent();

         _sResultHeading = "The task ";
      } // end FinishedPanel constructor

      /// <summary>
      /// Constructs a new FinishedPanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      public FinishedPanel(GraySystem.UI.Forms.Wizard wizParent) : base(wizParent)
      {
         _sResultHeading = "The task ";
      } // end FinishedPanel constructor

      /// <summary>
      /// Constructs a new FinishedPanel object and sets the heading used for the process's
      /// result.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      /// <param name="sResultHeading">Sets the heading used for the Result.  This should
      /// be something identifying the task being executed.  Fill in the following:
      /// "___________ was Successful!"  If nothing is specified, the default will be used,
      /// which is "The task".</param>
      public FinishedPanel(GraySystem.UI.Forms.Wizard wizParent, string sResultHeading) : base(wizParent)
      {
         _sResultHeading = sResultHeading + " ";
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinishedPanel));
         this._lblFinishedMsg = new System.Windows.Forms.Label();
         this._txtSummary = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // _lblFinishedMsg
         // 
         resources.ApplyResources(this._lblFinishedMsg, "_lblFinishedMsg");
         this._lblFinishedMsg.Name = "_lblFinishedMsg";
         // 
         // _txtSummary
         // 
         this._txtSummary.Cursor = System.Windows.Forms.Cursors.Default;
         resources.ApplyResources(this._txtSummary, "_txtSummary");
         this._txtSummary.Name = "_txtSummary";
         this._txtSummary.ReadOnly = true;
         // 
         // FinishedPanel
         // 
         this.Controls.Add(this._txtSummary);
         this.Controls.Add(this._lblFinishedMsg);
         this.Name = "FinishedPanel";
         this.ResumeLayout(false);
         this.PerformLayout();

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
         _txtSummary.Select(0, 0);  // Called to ensure no text is selected when the user is shown the panel
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
         _sResult = _sResultHeading + (bResult ? "was Successful!" : "Failed!");

         _sSummary = sSummary;
      } // end ConstructResults

      #endregion

      #endregion
   } // end FinishedPanel Class
} // end GraySystem.UI.Controls.WizardControlPanels Namespace