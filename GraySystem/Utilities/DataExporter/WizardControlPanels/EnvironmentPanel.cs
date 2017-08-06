#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\EnvironmentPanel.cs-arc  $
 * $Revision:   1.0.1.0  $
 * $Author:   cgray  $
 * $Date:   Jul 25 2006 11:25:34  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\EnvironmentPanel.cs-arc  $
 * 
 *
 *
 */

#endregion

#region Usings

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICS.Database;
using ICS.Database.TableObjects.RFS;
using ICS.GUI_Library.Controls;

#endregion


namespace ICS.Utilities.DataExporter.WizardControlPanels
{
   /// <summary>
   /// EnvironmentPanel Class is used to allow the user to select the Environment(s) to
   /// which the converted data will be applied.
   /// </summary>
   public class EnvironmentPanel : ICS.GUI_Library.Controls.WizardControlPanel
   {
      #region Fields

      private System.Windows.Forms.Label _lblEnvMessage;
      private System.Windows.Forms.CheckBox _chkSelectAll;
      private System.Windows.Forms.CheckedListBox _chklstEnvironments;

      private System.ComponentModel.IContainer _components = null;

      private DAL _dataAccessLayer;

      private string[] _sEnvironments;

      #endregion

      #region Properties

      #region DataAccessLayer

      /// <summary>
      /// Sets the Data Access Layer.
      /// </summary>
      public DAL DataAccessLayer
      {
         get { return (_dataAccessLayer); }

         set { _dataAccessLayer = value; }
      } // end DataAccessLayer property

      #endregion

      #region Environments

      /// <summary>
      /// Gets an array of Environment names
      /// </summary>
      public string[] Environments
      {
         get { return (_sEnvironments); }
      } // end Environments property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new EnvironmentPanel object.
      /// </summary>
      public EnvironmentPanel() : base()
      {
      } // end EnvironmentPanel constructor

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

      #region ConstructSummary

      /// <summary>
      /// Constructs the summary for the panel, which includes the list of Environments for which
      /// the RFSmart Version 3 data will be exported.
      /// </summary>
      protected override string ConstructSummary()
      {
         StringBuilder strSummary = new StringBuilder();

         strSummary.Append("   RFSmart Version 3 Environments for which the data will be exported:" +
                           Environment.NewLine);

         foreach (string sEnvironment in _sEnvironments)
         {
            strSummary.Append("      +  " + sEnvironment + Environment.NewLine);
         } // end foreach

         return (strSummary.ToString());
      } // end ConstructSummary

      #endregion

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
         this._lblEnvMessage = new System.Windows.Forms.Label();
         this._chkSelectAll = new System.Windows.Forms.CheckBox();
         this._chklstEnvironments = new System.Windows.Forms.CheckedListBox();
         this.SuspendLayout();
         // 
         // _lblEnvMessage
         // 
         this._lblEnvMessage.Location = new System.Drawing.Point(16, 8);
         this._lblEnvMessage.Name = "_lblEnvMessage";
         this._lblEnvMessage.Size = new System.Drawing.Size(432, 40);
         this._lblEnvMessage.TabIndex = 0;
         this._lblEnvMessage.Text = "Please select the RFSmart Version 3 Environment(s) for which you would " +
                                    "like to export the data.";
         // 
         // _chkSelectAll
         // 
         this._chkSelectAll.Location = new System.Drawing.Point(24, 72);
         this._chkSelectAll.Name = "_chkSelectAll";
         this._chkSelectAll.Size = new System.Drawing.Size(192, 16);
         this._chkSelectAll.TabIndex = 1;
         this._chkSelectAll.Text = "Select All";
         this._chkSelectAll.CheckedChanged += new System.EventHandler(SelectAllEnvironments);
         // 
         // _chklstEnvironments
         // 
         this._chklstEnvironments.Location = new System.Drawing.Point(24, 96);
         this._chklstEnvironments.Name = "_chklstEnvironments";
         this._chklstEnvironments.Size = new System.Drawing.Size(184, 139);
         this._chklstEnvironments.CheckOnClick = true;
         this._chklstEnvironments.TabIndex = 2;
         this._chklstEnvironments.ItemCheck += new ItemCheckEventHandler(UncheckSelectAll);
         // 
         // EnvironmentPanel
         // 
         this.Controls.Add(this._lblEnvMessage);
         this.Controls.Add(this._chkSelectAll);
         this.Controls.Add(this._chklstEnvironments);
         this.Name = "EnvironmentPanel";
         this.LeavePanel += new ICS.GUI_Library.Controls.LeavePanelEventHandler(LeaveEnvironmentPanel);
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #region LoadEnvironments

      /// <summary>
      /// Loading the Environments from the RFS_Environment table into the Checkbox-Listbox
      /// control for the Environments.
      /// </summary>
      public void LoadEnvironments()
      {
         if (_dataAccessLayer == null)
         {
            MessageBox.Show("Please specify the database connection in order to load the environments!",
                            "Database Connection Error");
         } // end if
         else
         {
            RFS_Environment tblEnvmnts = new RFS_Environment(_dataAccessLayer, "");

            // Clearing the chklstEnvironments control
            _chklstEnvironments.Items.Clear();

            // Fetching the environments
            tblEnvmnts.ResetAll();
            if (tblEnvmnts.Fetch())
            {
               // Iterating through each row adding the environments
               foreach (DataRow row in tblEnvmnts.Rows)
               {
                  _chklstEnvironments.Items.Add(row["EnvironmentID"]);
               } // end foreach
            } // end if
         } // end else
      } // end LoadEnvironments

      #endregion

      #region SetSelectedEnvs

      /// <summary>
      /// Sets the selected Environments array with the CheckedItems.
      /// </summary>
      /// <returns>Returns the number of selected environments</returns>
      private int SetSelectedEnvs()
      {
         int iSelectedEnvs = _chklstEnvironments.CheckedItems.Count;

         // Initializing the Environments array
         _sEnvironments = new string[iSelectedEnvs];

         // Looping through each checked environment and adding it to the array of environments
         for (int i = 0; i < iSelectedEnvs; i++)
         {
            _sEnvironments[i] = _chklstEnvironments.CheckedItems[i].ToString();
         } // end for

         return (iSelectedEnvs);
      } // end SetSelectedEnvs

      #endregion

      #region Event Handlers

      #region SelectAllEnvironments

      /// <summary>
      /// Select all environments within the Checkbox-Listbox control containing
      /// the environments only when the SelectAll checkbox is checked.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void SelectAllEnvironments(object sender, EventArgs e)
      {
         // Only check all environments if the user specified to do so by checking the checkbox
         if (_chkSelectAll.Checked)
         {
            for (int i = 0; i < _chklstEnvironments.Items.Count; i++)
            {
               _chklstEnvironments.SetItemChecked(i, true);
            } // end foreach
         } // end if
      } // end SelectAllEnvironments

      #endregion

      #region UncheckSelectAll

      /// <summary>
      /// Uncheck the SelectAll checkbox if a user unchecks an environment to allow the user to
      /// easily re-select all.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void UncheckSelectAll(object sender, ItemCheckEventArgs e)
      {
         // If the current environment checkbox was unselected, unselect the SelectAll checkbox
         // to allow the user to easily press it to re-select all checkboxes
         if (e.NewValue == CheckState.Unchecked)
         {
            _chkSelectAll.Checked = false;
         } // end if
      } // end UncheckSelectAll

      #endregion

      #region LeaveEnvironmentPanel

      /// <summary>
      /// Leave Panel event for the EnvironmentPanel, which is used to ensure that the user has
      /// selected at least one RFSmart Version 3 environment.  If an environment is not selected,
      /// the user will be shown an error message.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      /// <returns>Returns true if the user has selected at least one RFSmart Version 3 environment;
      /// otherwise, false.</returns>
      private bool LeaveEnvironmentPanel(WizardControlPanel sender, EventArgs e)
      {
         if (SetSelectedEnvs() > 0)
         {
            return (true);
         } // end if
         else
         {
            MessageBox.Show("Please select at least one Environment for which the data\n" +
                            "will be exported.", "Environment Error");

            return (false);
         } // end else
      } // end LeaveEnvironmentPanel

      #endregion

      #endregion

      #endregion
   } // end EnvironmentPanel Class
} // end ICS.Utilities.DataExporter.WizardControlPanels Namespace