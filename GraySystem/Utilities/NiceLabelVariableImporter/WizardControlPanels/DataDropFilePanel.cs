#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\RFSV2FileLocPanel.cs-arc  $
 * $Revision:   1.2  $
 * $Author:   pmonaco  $
 * $Date:   Nov 08 2007 14:58:20  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\RFSV2FileLocPanel.cs-arc  $
 * 
 *    Rev 1.2   Nov 08 2007 14:58:20   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.1   Feb 05 2007 10:17:58   cgray
 * BE016512 - Changed the class to use the new namespace for the WizardControlPanel base class.
 * 
 *    Rev 1.0   Jul 21 2006 17:37:30   cgray
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
using System.Text;
using System.Windows.Forms;

using ICS.GUI_Library.Helpers;
using ICS.Utilities.NiceLabelVariableImporter.Properties;

#endregion


namespace ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels
{
   /// <summary>
   /// </summary>
   public partial class DataDropFilePanel : ICS.GUI_Library.Controls.WizardControlPanels.WizardControlPanel
   {
      #region Properties

      #region DataDropFilePath

      public string DataDropFilePath
      {
         get { return (_txtFromFilePath.Text); }
      } // end DataDropFilePath property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new DataDropFilePanel object.
      /// </summary>
      protected DataDropFilePanel() : base()
      {
         InitializeComponent();
      } // end DataDropFilePanel constructor

      /// <summary>
      /// Constructs a new DataDropFilePanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      public DataDropFilePanel(ICS.GUI_Library.Forms.Wizard wizParent) : base(wizParent)
      {
      } // end DataDropFilePanel constructor

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
         if (Name == String.Empty)
         {
            base.Initialize();

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
         } // end if
      } // end Initialize

      #endregion

      #region GetFilePathFromFileDialog

      private void GetFilePathFromFileDialog(object sender, EventArgs e)
      {
         OpenFileDialog openFileDialog = new OpenFileDialog();

         openFileDialog.CheckFileExists = true;
         openFileDialog.Filter = "Data Drop File (*.ddrp)|*.ddrp";
         openFileDialog.Multiselect = false;
         if (openFileDialog.ShowDialog(this) == DialogResult.OK)
         {
            _txtFromFilePath.Text = openFileDialog.FileName;
         } // end if
      } // end GetFilePathFromFileDialog

      #endregion

      #region ValidateDataDropFilePath

      private bool ValidateDataDropFilePath(ICS.GUI_Library.Controls.WizardControlPanels.WizardControlPanel sender, EventArgs e)
      {
         if (_txtFromFilePath.Text.Length == 0)
         {
            RtlMessageBox.Show(String.Empty, String.Empty);

            return (false);
         } // end if
         else if (System.IO.File.Exists(_txtFromFilePath.Text))
         {
            return (true);
         } // end else if
         else
         {
            RtlMessageBox.Show(String.Empty, String.Empty);

            return (false);
         } // end else
      } // end GetFilePathFromFileDialog

      #endregion

      #region ConstructSummary

      /// <summary>
      /// Constructs the summary for the panel, which includes the Server, Database, and UserID
      /// for the Database Connection to an RFSmart Version 2 Database.
      /// </summary>
      /// <returns>Returns the summary constructed, which includes information relating to the
      /// database connection to an instance of RFSmart Version 2.</returns>
      protected override string ConstructSummary()
      {
         StringBuilder sSummary = new StringBuilder();

         sSummary.AppendLine("   Data Drop File:");
         sSummary.AppendLine("      " + _txtFromFilePath.Text);

         return (sSummary.ToString());
      } // end ConstructSummary

      #endregion

      #endregion
   }
} // end ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels Namespace