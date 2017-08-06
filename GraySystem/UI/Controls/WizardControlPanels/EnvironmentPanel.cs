#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\EnvironmentPanel.cs-arc  $
 * $Revision:   1.3  $
 * $Author:   cgray  $
 * $Date:   May 19 2008 21:35:10  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\EnvironmentPanel.cs-arc  $
 * 
 *    Rev 1.3   May 19 2008 21:35:10   cgray
 * BE25492 - Added a new constructor that takes a Wizard object and sets it as the wizard (parent) of the control panel.
 * 
 * Added a call to the InitializeComponent method within the base constructor to ensure the Design-time view works correctly.
 * 
 *    Rev 1.2   Nov 08 2007 14:36:12   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.1   Feb 12 2007 15:18:18   cgray
 * BE011022 - Removed the reference to the RFS TableObjects as it was not needed.
 * 
 *    Rev 1.0   Feb 12 2007 15:14:16   cgray
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICS.Database;
using GraySystem.UI.Properties;
using GraySystem.UI.Helpers;
using System.Globalization;

#endregion


namespace GraySystem.UI.Controls.WizardControlPanels
{
   /// <summary>
   /// EnvironmentPanel Class is used to allow the user to select Environments.  In order to use
   /// the class, the DataAccessLayer will need to be set prior to displaying the panel as well
   /// as calling LoadEnvironments.
   /// </summary>
   public class EnvironmentPanel : WizardControlPanel
   {
      #region Fields

      #region Controls

      /// <summary>
      /// Displays a message to the user indicating what they are to do in this panel.
      /// </summary>
      protected System.Windows.Forms.Label _lblEnvMessage;

      /// <summary>
      /// Allows the user to select all environments by checking the checkbox.
      /// </summary>
      protected System.Windows.Forms.CheckBox _chkSelectAll;

      /// <summary>
      /// Holds the list of environments for the user to select.
      /// </summary>
      protected System.Windows.Forms.CheckedListBox _chklstEnvironments;

      #endregion

      private System.ComponentModel.IContainer _components = null;

      /// <summary>
      /// Data Access Layer for the RFSmart Version 3 system database to get the environments.
      /// </summary>
      private DAL _dataAccessLayer;

      /// <summary>
      /// Holds a list of the selected environments.
      /// </summary>
      private string[] _sEnvironments;

      /// <summary>
      /// Used to store the heading for the panel's summary, which will be used to
      /// write the summary for the panel.
      /// </summary>
      private string _sSummaryHeading;

      #endregion

      #region Properties

      #region DataAccessLayer

      /// <summary>
      /// Gets and sets the Data Access Layer for the RFSmart System Database.
      /// </summary>
      public DAL DataAccessLayer
      {
         get { return (_dataAccessLayer); }

         set { _dataAccessLayer = value; }
      } // end DataAccessLayer property

      #endregion

      #region Environments

      /// <summary>
      /// Gets an array of Environment names selected by the user.
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
      protected EnvironmentPanel() : base()
      {
         InitializeComponent();

         _sSummaryHeading = "RFSmart Environments";
      } // end EnvironmentPanel constructor

      /// <summary>
      /// Constructs a new EnvironmentPanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      public EnvironmentPanel(GraySystem.UI.Forms.Wizard wizParent) : base(wizParent)
      {
         _sSummaryHeading = "RFSmart Environments";
      } // end EnvironmentPanel constructor

      /// <summary>
      /// Constructs a new EnvironmentPanel object and sets the heading used for the panel's summary.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      /// <param name="sSummaryHeading">Sets the heading used for the Summary.  This should be something
      /// to identify for what the environment(s) is(are) selected.  An example would be "RFSmart Version 3
      /// Environment(s) for which the data will be exported".  If nothing is specified, the default is
      /// used, which is "RFSmart Environments".</param>
      public EnvironmentPanel(GraySystem.UI.Forms.Wizard wizParent, string sSummaryHeading) : base(wizParent)
      {
         _sSummaryHeading = sSummaryHeading;
      } // end EnvironmentPanel constructor

      #endregion

      #region Dispose

      /// <summary>
      /// Clean up any resources being used, which includes the DataAccessLayer.
      /// </summary>
      /// <param name="disposing">Determines whether or not to release both managed and unmanaged
      /// resources or only unmanaged resources.</param>
      protected override void Dispose(bool disposing)
      {
         try
         {
            // If the System is not been defined, do not attempt to close the connections
            _dataAccessLayer.CloseConnections();
         } // end try
         catch
         {
         } // end catch
         finally
         {
            if (disposing)
            {
               if (_components != null)
               {
                  _components.Dispose();
               } // end if
            } // end if
         } // end finally

         base.Dispose(disposing);
      } // end Dispose

      #endregion

      #region Methods

      #region ConstructSummary

      /// <summary>
      /// Constructs the summary for the panel, which includes a heading  and the list of Environments.
      /// In order to specify the heading, override the SummaryHeading property.
      /// </summary>
      protected override string ConstructSummary()
      {
         StringBuilder strSummary = new StringBuilder();

         strSummary.Append("   " + _sSummaryHeading + ":" + Environment.NewLine);

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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnvironmentPanel));
         this._lblEnvMessage = new System.Windows.Forms.Label();
         this._chkSelectAll = new System.Windows.Forms.CheckBox();
         this._chklstEnvironments = new System.Windows.Forms.CheckedListBox();
         this.SuspendLayout();
         // 
         // _lblEnvMessage
         // 
         resources.ApplyResources(this._lblEnvMessage, "_lblEnvMessage");
         this._lblEnvMessage.Name = "_lblEnvMessage";
         // 
         // _chkSelectAll
         // 
         resources.ApplyResources(this._chkSelectAll, "_chkSelectAll");
         this._chkSelectAll.Name = "_chkSelectAll";
         this._chkSelectAll.CheckedChanged += new System.EventHandler(this.SelectAllEnvironments);
         // 
         // _chklstEnvironments
         // 
         this._chklstEnvironments.CheckOnClick = true;
         resources.ApplyResources(this._chklstEnvironments, "_chklstEnvironments");
         this._chklstEnvironments.Name = "_chklstEnvironments";
         this._chklstEnvironments.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.UncheckSelectAll);
         this._chklstEnvironments.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GoForwardIfEnterPressed);
         // 
         // EnvironmentPanel
         // 
         this.Controls.Add(this._lblEnvMessage);
         this.Controls.Add(this._chkSelectAll);
         this.Controls.Add(this._chklstEnvironments);
         this.Name = "EnvironmentPanel";
         this.LeavePanel += new GraySystem.UI.Controls.WizardControlPanels.LeavePanelEventHandler(this.LeaveEnvironmentPanel);
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #region LoadEnvironments

      /// <summary>
      /// Loading the Environments from the RFS_Environment table into the Checkbox-Listbox
      /// control for the Environments.
      /// </summary>
      /// <param name="dataAccessLayer">Sets the Data Access Layer for the database connection in
      /// order to load the environments.</param>
      public void LoadEnvironments(DAL dataAccessLayer)
      {
         try
         {
            if (_dataAccessLayer != null)
            {
               _dataAccessLayer.CloseConnections();
            } // end if
         } // end try
         catch
         {
         } // end catch
         finally
         {
            _dataAccessLayer = dataAccessLayer;

            LoadEnvironments();
         } // end finally
      } // end LoadEnvironments

      /// <summary>
      /// Loading the Environments from the RFS_Environment table into the Checkbox-Listbox
      /// control for the Environments.
      /// </summary>
      public void LoadEnvironments()
      {
         if (_dataAccessLayer == null)
         {
            RtlMessageBox.Show(GUILibStrings.msgSpecifyDBConn,
                            GUILibStrings.errorDB);
         } // end if
         else
         {
            // Clearing the chklstEnvironments control
            _chklstEnvironments.Items.Clear();

            // Fetching the environments
            _dataAccessLayer.System.Query = "SELECT EnvironmentID FROM RFS_Environment";
            _dataAccessLayer.System.Log.WriteLine(2, "Fetch Statement: SELECT EnvironmentID FROM RFS_Environment");

            // Iterating through each row adding the environments
            while (_dataAccessLayer.System.DataReader.Read())
            {
               _chklstEnvironments.Items.Add(_dataAccessLayer.System.DataReader.GetString(0));
            } // end while

            _dataAccessLayer.System.Log.WriteLine(2, "Dataset Loaded.  Row Count: " +
               _chklstEnvironments.Items.Count.ToString(CultureInfo.InvariantCulture));

            _dataAccessLayer.System.DataReader.Close();
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

            ((GraySystem.UI.Forms.Wizard) Parent).RaiseNextButtonClick();
         } // end if
      } // end GoForwardIfEnterPressed

      #endregion

      #region LeaveEnvironmentPanel

      /// <summary>
      /// Leave Panel event for the EnvironmentPanel, which is used to ensure that the user has
      /// selected at least one RFSmart environment.  If an environment is not selected, the user
      /// will be shown an error message.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      /// <returns>Returns true if the user has selected at least one RFSmart environment;
      /// otherwise, false.</returns>
      private bool LeaveEnvironmentPanel(WizardControlPanel sender, EventArgs e)
      {
         if (SetSelectedEnvs() > 0)
         {
            return (true);
         } // end if
         else
         {
            RtlMessageBox.Show(GUILibStrings.msgSelectEnv, GUILibStrings.errorEnv);

            return (false);
         } // end else
      } // end LeaveEnvironmentPanel

      #endregion

      #endregion

      #endregion
   } // end EnvironmentPanel Class
} // end GraySystem.UI.Controls.WizardControlPanels Namespace