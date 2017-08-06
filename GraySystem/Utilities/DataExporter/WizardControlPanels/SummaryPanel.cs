#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\SummaryPanel.cs-arc  $
 * $Revision:   1.1  $
 * $Author:   cgray  $
 * $Date:   Jul 25 2006 00:02:00  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\SummaryPanel.cs-arc  $
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


namespace ICS.Utilities.DataExporter.WizardControlPanels
{
   /// <summary>
   /// SummaryPanel Class is used to display a Summary of the user's selections in
   /// order that they may validate their selections before proceeding.
   /// </summary>
   public class SummaryPanel : ICS.GUI_Library.Controls.WizardControlPanel
   {
      #region Fields

      private System.Windows.Forms.TextBox _txtSummary;

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
         } // end set
      } // end Summary property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a SummaryPanel object.
      /// </summary>
      public SummaryPanel() : base()
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
         this._txtSummary = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // _txtSummary
         // 
         this._txtSummary.AcceptsReturn = true;
         this._txtSummary.AcceptsTab = true;
         this._txtSummary.Cursor = System.Windows.Forms.Cursors.Default;
         this._txtSummary.Location = new System.Drawing.Point(8, 8);
         this._txtSummary.Multiline = true;
         this._txtSummary.Name = "_txtSummary";
         this._txtSummary.ReadOnly = true;
         this._txtSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this._txtSummary.Size = new System.Drawing.Size(448, 230);
         this._txtSummary.TabIndex = 0;
         this._txtSummary.Text = "";
         // 
         // SummaryPanel
         // 
         this.Controls.Add(this._txtSummary);
         this.Name = "SummaryPanel";
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #endregion
   } // end SummaryPanel Class
} // end ICS.Utilities.DataExporter.WizardControlPanels Namespace