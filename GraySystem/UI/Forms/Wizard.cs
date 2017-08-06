#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Forms\Wizard.cs-arc  $
 * $Revision:   1.6  $
 * $Author:   pmonaco  $
 * $Date:   Nov 08 2007 14:36:26  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Forms\Wizard.cs-arc  $
 * 
 *    Rev 1.6   Nov 08 2007 14:36:26   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.5   Feb 12 2007 13:49:56   cgray
 * BE011022 - Added a new method to allow the developer to perform the Next button click even though the user may not
 * have clicked the button.
 * 
 * Added a virtual Run method to allow developers the ability to override in an attempt to implement the wizard to also be
 * runable from the command-line as well without any user input.
 * 
 * Set the focus for items being displayed like the Finish button, Next button, or panel.
 * 
 *    Rev 1.4   Feb 05 2007 09:54:08   cgray
 * BE016512 - Changed the using for the base WizardControlPanel class to point to the new namespace.
 * 
 *    Rev 1.3   Jan 24 2007 11:22:52   cgray
 * BE011022 & BE016512 - Changed the ShowPanel method to raise the ShowPanel event for the next panel to be displayed
 * prior to attempting to display the next panel in order to keep the wizard from looking blank while the ShowPanel event handler
 * is being executed.  The cursor is changed to the wait cursor upon entering the ShowPanel method and switch back to the 
 * default upon leaving in order to indicate processing in the background to the user.
 * 
 * Changed the use of AppendLine for the StringBuilder object to just Append in order to provide compatibility with .NET
 * Framework 1.1 and 2.0.
 * 
 *    Rev 1.2   Aug 28 2006 16:51:34   cgray
 * BE016512 - Changed the Cancel button to not send a Cancel message to the parent when it is clicked allowing the wizard
 * form to determine if the user wants to close the form or not.
 * 
 *    Rev 1.1   Jul 24 2006 23:59:16   cgray
 * BE016512 - Changed the class to not initialize the _iCurrentPanel field to zero within the constructor as the CLR
 * will do that by default.
 * 
 * - Removed concatenation of strings with the '+' operator.  Instead, taking advantage of StringBuilder for added 
 *   performance.
 * 
 *    Rev 1.0   Jul 21 2006 17:07:36   cgray
 * Initial revision.
 *
 *
 */

#endregion

#region Usings

using System;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Windows.Forms;

using GraySystem.UI.Controls.WizardControlPanels;
using System.Globalization;

#endregion


namespace GraySystem.UI.Forms
{
   /// <summary>
   /// Wizard Class is derived from the System.Windows.Forms.Form class.  This should be used as
   /// a standard template for a wizard; therefore, the real wizard should inherit from this
   /// form, and it should be Serializable.
   /// </summary>
   public class Wizard : System.Windows.Forms.Form
   {
      #region Fields

      #region Form Controls

      /// <summary>
      /// PictureBox used to stored the Banner displayed at the top of the wizard
      /// </summary>
      protected PictureBox _picBanner;

      /// <summary>
      /// An array to hold the WizardControlPanels, which will be displayed to the
      /// user as they use the wizard.  NOTE:  This also acts as the Sequence in
      /// which the panels will be displayed; however, this may be overwritten
      /// based on certain events.
      /// </summary>
      protected WizardControlPanel[] _wizSeq;

      #region Buttons Panel

      /// <summary>
      /// Panel used to hold the buttons used to navigate through the wizard
      /// </summary>
      protected Panel _pnlButtons;

      /// <summary>
      /// Button used to advance the user to the next panel
      /// </summary>
      protected Button _btnNext;

      /// <summary>
      /// Button used to return the user to the previous panel
      /// </summary>
      protected Button _btnBack;

      /// <summary>
      /// Button used to cancel the Wizard by closing it
      /// </summary>
      protected Button _btnCancel;

      /// <summary>
      /// Button used to indicate that the user is finished with the wizard
      /// </summary>
      protected Button _btnFinish;

      #endregion

      #endregion

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container _components = null;

      /// <summary>
      /// Holds the value of the current panel being displayed to the user
      /// </summary>
      protected int _iCurrentPanel;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new Wizard object and initializes the panel sequence and components and adds
      /// any Show Panel events needed.
      /// </summary>
      protected Wizard()
      {
         InitializeSequence();

         // Required for Windows Form Designer support
         InitializeComponent();
      } // end Wizard constructor

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

      #region InitializeLifetimeService

      /// <summary>
      /// Obtains a lifetime service object to control the lifetime policy for this instance.
      /// </summary>
      /// <returns>A Lease.</returns>
      public override object InitializeLifetimeService()
      {
         ILease lease = (ILease) base.InitializeLifetimeService();
         if (lease.CurrentState == LeaseState.Initial)
         {
            lease.InitialLeaseTime = TimeSpan.Zero;
         } // end if

         return (lease);
      } // end InitializeLifetimeService

      #endregion

      #region InitializeComponent

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wizard));
         this._picBanner = new System.Windows.Forms.PictureBox();
         this._btnNext = new System.Windows.Forms.Button();
         this._btnBack = new System.Windows.Forms.Button();
         this._btnCancel = new System.Windows.Forms.Button();
         this._pnlButtons = new System.Windows.Forms.Panel();
         ((System.ComponentModel.ISupportInitialize)(this._picBanner)).BeginInit();
         this._pnlButtons.SuspendLayout();
         this.SuspendLayout();
         // 
         // _picBanner
         // 
         resources.ApplyResources(this._picBanner, "_picBanner");
         this._picBanner.Name = "_picBanner";
         this._picBanner.TabStop = false;
         // 
         // _btnNext
         // 
         resources.ApplyResources(this._btnNext, "_btnNext");
         this._btnNext.Name = "_btnNext";
         this._btnNext.Click += new System.EventHandler(this.GoForward);
         // 
         // _btnBack
         // 
         resources.ApplyResources(this._btnBack, "_btnBack");
         this._btnBack.Name = "_btnBack";
         this._btnBack.Click += new System.EventHandler(this.GoBack);
         // 
         // _btnCancel
         // 
         resources.ApplyResources(this._btnCancel, "_btnCancel");
         this._btnCancel.Name = "_btnCancel";
         this._btnCancel.Click += new System.EventHandler(this.Cancel);
         // 
         // _pnlButtons
         // 
         this._pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this._pnlButtons.Controls.Add(this._btnCancel);
         this._pnlButtons.Controls.Add(this._btnBack);
         this._pnlButtons.Controls.Add(this._btnNext);
         resources.ApplyResources(this._pnlButtons, "_pnlButtons");
         this._pnlButtons.Name = "_pnlButtons";
         // 
         // Wizard
         // 
         resources.ApplyResources(this, "$this");
         this.Controls.Add(this._picBanner);
         this.Controls.Add(this._pnlButtons);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Name = "Wizard";
         ((System.ComponentModel.ISupportInitialize)(this._picBanner)).EndInit();
         this._pnlButtons.ResumeLayout(false);
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #region InitializeSequence

      /// <summary>
      /// Initializes the Wizard's sequence.  This method must be overrided by every
      /// derived class.
      /// </summary>
      protected virtual void InitializeSequence() { }


      #endregion

      #region Run

      /// <summary>
      /// Allows the wizard to be executed from the command-line.  This method needs to be overriden in the
      /// inherited class and called from Main if command-line arguments are specified.  The method should
      /// then call the task instantiated for the wizard.  Note:  without overriding this method, it will
      /// just show the wizard.
      /// </summary>
      /// <param name="args">An array of command-line arguments.</param>
      public virtual void Run(string[] args)
      {
         Show();
      } // end Run

      #endregion

      #region ConstructSummary

      /// <summary>
      /// Constructs the summary by going through each panel and appending the panel's summary to
      /// create the summary.  Override this method to include a custom header and/or footer to your
      /// summary, but make sure the base is still called to construct the body relating to the
      /// panels.  NOTE:  Be sure to override the ConstructSummary method of the WizardControlPanel
      /// in order to have a custom summary added for each intended panel.
      /// </summary>
      /// <returns>Returns the constructed summary</returns>
      protected virtual string ConstructSummary()
      {
         StringBuilder sSummary = new StringBuilder();
         string sTempSummary = "";  // Used so the Summary does not have to be created twice
         int iNumPanels = _wizSeq.Length;

         for (int i = 0; i < iNumPanels; i++)
         {
            sTempSummary = _wizSeq[i].Summary;
            if (sTempSummary.Length > 0)
            {
               //*************************************************************************************
               // NOTE:  Be sure to override the ConstructSummary WizardControlPanel method in order
               //        have a custom summary added for the panel.
               //*************************************************************************************
               sSummary.Append(sTempSummary + Environment.NewLine + Environment.NewLine);
            } // end if
         } // end for

         return (sSummary.ToString());
      } // end ConstructSummary

      #endregion

      #region Show

      /// <summary>
      /// Show the wizard starting at the first panel of the Wizard's Sequence.
      /// </summary>
      public new void Show()
      {
         Show(0);
      } // end Show

      /// <summary>
      /// Show the wizard starting at the specified panel.
      /// </summary>
      /// <param name="iStartPanel">Index of the panel with which the wizard should start</param>
      public void Show(int iStartPanel)
      {
         _iCurrentPanel = iStartPanel;

         // Hide the back button if the wizard is starting at the first panel
         if ((_iCurrentPanel == 0) || _wizSeq[_iCurrentPanel].SkipOnBack)
         {
            _btnBack.Enabled = false;
         } // end if

         ShowPanel(_wizSeq[_iCurrentPanel]);

         ShowDialog();
      } // end Show

      /// <summary>
      /// Show the wizard starting at the specified panel.
      /// </summary>
      /// <param name="startPanel"></param>
      public void Show(WizardControlPanel startPanel)
      {
         int iStartPanel;

         // If the panel was not found, notify the user; otherwise, show the wizard with panel
         iStartPanel = FindPanel(startPanel);
         if (iStartPanel == -1)
         {
            System.Windows.Forms.MessageBox.Show(this,
                                                 "Unable to locate the starting point of the wizard!",
                                                 "Startup Error",
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Error);
         } // end if
         else
         {
            Show(iStartPanel);
         } // end else
      } // end Show

      #endregion

      #region Panel Navigation

      #region ShowNextPanel

      /// <summary>
      /// Determines the next panel and displays it to the user.  If the user is at the end,
      /// the wizard is closed.
      /// </summary>
      protected void ShowNextPanel()
      {
         ShowNextPanel(_iCurrentPanel + 1);
      } // end ShowNextPanel

      /// <summary>
      /// Determines the next panel and displays it to the user.  If the user is at the end,
      /// the wizard is closed.
      /// </summary>
      /// <param name="iNextPanel">Index of the next panel to be displayed</param>
      protected void ShowNextPanel(int iNextPanel)
      {
         _iCurrentPanel = iNextPanel;

         // This should never be the case, but if the next panel is not located, let the user know
         if (_iCurrentPanel == -1)
         {
            System.Windows.Forms.MessageBox.Show(this,
                                                 "Unable to locate the next panel!",
                                                 "Next Error",
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Error);
         } // end if
         else if (_iCurrentPanel < _wizSeq.Length)
         {
            // If the current panel is the second panel, enable the back button
            if ((_iCurrentPanel == 1) && !_wizSeq[_iCurrentPanel].SkipOnBack)
            {
               _btnBack.Enabled = true;
            } // end if

            // If the current panel is the last panel, add the finish button
            if (_iCurrentPanel == (_wizSeq.Length - 1))
            {
               AddFinishButton();
            } // end if

            ShowPanel(_wizSeq[_iCurrentPanel]);
         } // end else if
         else
         {
            Close();
         } // end else
      } // end ShowNextPanel

      /// <summary>
      /// Displays the specified next panel to the user.
      /// </summary>
      /// <param name="nextPanel">Next Panel to be displayed to the user.</param>
      protected void ShowNextPanel(WizardControlPanel nextPanel)
      {
         ShowNextPanel(FindPanel(nextPanel));
      } // end ShowNextPanel

      #endregion

      #region ShowPreviousPanel

      /// <summary>
      /// Determines the previous panel and displays it to the user.  However, the Current Panel
      /// is not updated until the Previous Panel is diplayed because the ShowPanel method only
      /// removes the last panel if the Current Panel is not zero.
      /// </summary>
      protected void ShowPreviousPanel()
      {
         ShowPreviousPanel(_iCurrentPanel - 1);
      } // end ShowPreviousPanel

      /// <summary>
      /// Determines the previous panel and displays it to the user.  However, the Current Panel
      /// is not updated until the Previous Panel is diplayed because the ShowPanel method only
      /// removes the last panel if the Current Panel is not zero.
      /// </summary>
      /// <param name="iPrevPanel">Index for the previous panel to be displayed</param>
      protected void ShowPreviousPanel(int iPrevPanel)
      {
         _iCurrentPanel = iPrevPanel;

         // This should never be the case, but if the previous panel is not located, let the user know
         if (_iCurrentPanel == -1)
         {
            System.Windows.Forms.MessageBox.Show(this,
                                                 "Unable to locate the previous panel!",
                                                 "Back Error",
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Error);
         } // end if
         else
         {
            _iCurrentPanel = 0;

            for (int i = iPrevPanel; i > 0; i--)
            {
               if (!_wizSeq[_iCurrentPanel].SkipOnBack)
               {
                  _iCurrentPanel = iPrevPanel;
                  break;
               } // end if
            } // end for

            if (_iCurrentPanel == 0)
            {
               _btnBack.Enabled = false;
            } // end if

            ShowPanel(_wizSeq[_iCurrentPanel], true);
         } // end else
      } // end ShowPreviousPanel

      /// <summary>
      /// Displays the specified previous panel to the user.
      /// </summary>
      /// <param name="prevPanel">Previous Panel to be displayed to the user.</param>
      protected void ShowPreviousPanel(WizardControlPanel prevPanel)
      {
         ShowPreviousPanel(FindPanel(prevPanel));
      } // end ShowPreviousPanel

      #endregion

      #region ShowPanel

      /// <summary>
      /// Removes the current panel and displays the new one.  If it is the first
      /// panel, then the Back Button is disabled.
      /// </summary>
      /// <param name="newPanel">New Panel to be displayed.</param>
      private void ShowPanel(WizardControlPanel newPanel)
      {
         ShowPanel(newPanel, false);
      } // end ShowPanel

      /// <summary>
      /// Removes the current panel and displays the new one.  If it is the first
      /// panel, then the Back Button is disabled.
      /// </summary>
      /// <param name="newPanel">New Panel to be displayed.</param>
      /// <param name="bGoingBack">Indicates whether or not the user is going back</param>
      private void ShowPanel(WizardControlPanel newPanel, bool bGoingBack)
      {
         Cursor.Current = Cursors.WaitCursor;

         // Raise the ShowPanel event so that wizard knows what panel is about to be displayed.
         newPanel.RaiseShowPanelEvent(EventArgs.Empty);

         SuspendLayout();

         // If the user is on a screen that is set to be skipped if the user decides to
         // go back, then the back button should not even be enabled.
         if (newPanel.SkipOnBack)
         {
            _btnBack.Enabled = false;
         } // end if

         if (_iCurrentPanel > 0 || bGoingBack)
         {
            // Removing the last added control, which should always be the wizard's panel.
            Controls.RemoveAt(Controls.Count - 1);
         } // end if

         newPanel.Location = new Point(0, 72);

         Controls.Add(newPanel);
         newPanel.Focus();

         ResumeLayout(false);

         Cursor.Current = Cursors.Default;
      } // end ShowPanel

      #endregion

      #region RaiseNextButtonClick

      /// <summary>
      /// Used to allow wizard control panels to automatically proceed to the next
      /// panel without having the user press the Next button.
      /// </summary>
      public void RaiseNextButtonClick()
      {
         _btnNext.PerformClick();
      } // end RaiseNextButtonClick

      #endregion

      #endregion

      #region FindPanel

      /// <summary>
      /// Find the panel within the array of panels.
      /// </summary>
      /// <param name="panel">Panel for which to search</param>
      /// <returns>Returns the index of the panel's location if found; otherwise, -1</returns>
      protected int FindPanel(WizardControlPanel panel)
      {
         int iPanelLoc;
         bool bFound = false;

         // Interating through the array of Panels seaching for the starting point
         for (iPanelLoc = 0; iPanelLoc < _wizSeq.Length; iPanelLoc++)
         {
            if (panel.Equals(_wizSeq[iPanelLoc]))
            {
               bFound = true;
               break;
            } // end if
         } // end for

         return (bFound ? iPanelLoc : -1);
      } // end FindPanel

      #endregion

      #region Wizard Views

      #region SetActiveView

      /// <summary>
      /// Sets the View of the Wizard based on the user's activity, which includes enabling or disabling
      /// the Next and Back button.
      /// </summary>
      /// <param name="bActive">Determines if the user is active or inactive.  The wizard should be
      /// considered active as long as the wizard is not in the middle of processing information.
      /// For example, a progress panel where work is being done is considering an inactive state;
      /// therefore, this value should be false.</param>
      protected void SetActiveView(bool bActive)
      {
         SetActiveView(bActive, bActive);
      } // end SetActiveView

      /// <summary>
      /// Sets the View of the Wizard based on the user's activity, which includes enabling or disabling
      /// the Next and Back button.
      /// </summary>
      /// <param name="bActive">Determines if the user is active or inactive.  The wizard should be
      /// considered active as long as the wizard is not in the middle of processing information.
      /// For example, a progress panel where work is being done is considering an inactive state;
      /// therefore, this value should be false.</param>
      /// <param name="bAllowBack">Determines whether or not the back button should be enabled.
      /// This would be used for things like once the ProgressPanel is complete, and the user
      /// should only go forward.</param>
      protected void SetActiveView(bool bActive, bool bAllowBack)
      {
         _btnNext.Enabled = bActive;
         _btnNext.Focus();

         _btnBack.Enabled = bActive & bAllowBack;
      } // end SetActiveView

      #endregion

      #region AddFinishButton

      /// <summary>
      /// Constructs the finish button and adds it to the button panel.  It also hides the next
      /// button and disables the back button.
      /// </summary>
      private void AddFinishButton()
      {
         _btnFinish = new System.Windows.Forms.Button();
         _pnlButtons.SuspendLayout();
         SuspendLayout();

         _btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.System;
         _btnFinish.Location = new System.Drawing.Point(368, 24);
         _btnFinish.Name = "_btnFinish";
         _btnFinish.TabIndex = 0;
         _btnFinish.Text = "Finish";
         _btnFinish.Visible = true;
         _btnFinish.Click += new EventHandler(Finish);

         _pnlButtons.Controls.Add(_btnFinish);

         _btnNext.Visible = false;

         _pnlButtons.ResumeLayout(false);
         ResumeLayout(false);
      } // end AddFinishButton

      #endregion

      #endregion

      #region Event Handlers

      #region GoForward

      /// <summary>
      /// Click event for the Next Button, which calls the ShowNextPanel method to display the
      /// next panel in the Wizard.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      void GoForward(object sender, EventArgs e)
      {
         if (_wizSeq[_iCurrentPanel].RaiseLeavePanelEvent())
         {
            ShowNextPanel();
         } // end if
      } // end GoForward

      #endregion

      #region GoBack

      /// <summary>
      /// Click event for the Back Button, which calls the PreviousPanel method to display the
      /// previous panel in the Wizard.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      void GoBack(object sender, EventArgs e)
      {
         ShowPreviousPanel();
      } // end GoBack

      #endregion

      #region Cancel

      /// <summary>
      /// Click event for the Cancel Button, which displays a close confirmation dialog window
      /// to the user.  If they confirm, the Close method is called to close the wizard.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      void Cancel(object sender, EventArgs e)
      {
         if (System.Windows.Forms.MessageBox.Show(this,
                                                  "Are you sure you wish to exit?",
                                                  "Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question) == DialogResult.Yes)
         {
            Close();
         } // end if
      } // end Cancel

      #endregion

      #region Finish

      /// <summary>
      /// Click event for the Finish Button, which calls the Close method to close the wizard.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      void Finish(object sender, EventArgs e)
      {
         Close();
      } // end Finish

      #endregion

      #endregion

      #endregion
   } // end Wizard Class
} // end GraySystem.UI.Forms Namespace