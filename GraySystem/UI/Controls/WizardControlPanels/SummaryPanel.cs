#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\SummaryPanel.cs-arc  $
 * $Revision:   1.2  $
 * $Author:   cgray  $
 * $Date:   May 19 2008 21:28:58  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\SummaryPanel.cs-arc  $
 * 
 *    Rev 1.2   May 19 2008 21:28:58   cgray
 * BE25492 - Added a new constructor that takes a Wizard object and sets it as the wizard (parent) of the control panel.
 * 
 * Added a call to the InitializeComponent method within the base constructor to ensure the Design-time view works correctly.
 * 
 *    Rev 1.1   Nov 08 2007 14:36:06   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.0   Feb 12 2007 15:06:50   cgray
 * Initial revision.
 * 
 *    Rev 1.1   Jul 25 2006 00:02:00   cgray
 * BE016512 - Changed comparisions using the empty string to check that the length is zero for enhanced performance.
 * 
 *    Rev 1.0   Jul 21 2006 17:37:44   cgray
 * Initial revision.
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
   /// SummaryPanel Class is used to display a Summary of the user's selections in
   /// order that they may validate their selections before proceeding.
   /// </summary>
   public class SummaryPanel : WizardControlPanel
   {
      #region Fields

      #region Controls

      /// <summary>
      /// Displays the summary of the wizard.
      /// </summary>
      protected System.Windows.Forms.TextBox _txtSummary;

      #endregion

      private System.ComponentModel.IContainer _components = null;

      #endregion

      #region Properties

      #region Summary

      /// <summary>
      /// Sets the Summary Text.
      /// </summary>
      public new string Summary
      {
         set
         {
            _txtSummary.Text = value;
            _txtSummary.Select(0, 0);  // Called to ensure no text is selected when the user is shown the panel
         } // end set
      } // end Summary property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a SummaryPanel object.
      /// </summary>
      protected SummaryPanel() : base()
      {
         InitializeComponent();
      } // end SummaryPanel constructor

      /// <summary>
      /// Constructs a new SummaryPanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      public SummaryPanel(GraySystem.UI.Forms.Wizard wizParent) : base(wizParent)
      {
      } // end SummaryPanel constructor

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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SummaryPanel));
         this._txtSummary = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // _txtSummary
         // 
         this._txtSummary.Cursor = System.Windows.Forms.Cursors.Default;
         resources.ApplyResources(this._txtSummary, "_txtSummary");
         this._txtSummary.Name = "_txtSummary";
         this._txtSummary.ReadOnly = true;
         // 
         // SummaryPanel
         // 
         this.Controls.Add(this._txtSummary);
         this.Name = "SummaryPanel";
         this.ResumeLayout(false);
         this.PerformLayout();

      } // end InitializeComponent

      #endregion

      #endregion
   } // end SummaryPanel Class
} // end GraySystem.UI.Controls.WizardControlPanels Namespace