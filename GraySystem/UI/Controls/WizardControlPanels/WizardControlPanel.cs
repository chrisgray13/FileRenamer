#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\RFSmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\WizardControlPanel.cs-arc  $
 * $Revision:   1.4  $
 * $Author:   cgray  $
 * $Date:   May 19 2008 21:28:12  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\RFSmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\WizardControlPanel.cs-arc  $
 * 
 *    Rev 1.4   May 19 2008 21:28:12   cgray
 * BE25492 - Added a new constructor that takes a Wizard object and sets it as the wizard (parent) of the control panel.
 * 
 * Added a constructor to get the Wizard parent object of the control panel.
 * 
 * Added a call to the InitializeComponent method within the base constructor to ensure the Design-time view works correctly.
 * 
 *    Rev 1.3   Nov 08 2007 14:36:08   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.2   Feb 05 2007 09:53:08   cgray
 * BE016512 - Changed the namespace to correspond to the location of the class.  The class was moved in order to store
 * reusable wizard control panels.
 * 
 *    Rev 1.1   Jul 24 2006 23:57:12   cgray
 * BE016512 - Changed delegates to pass a WizardControlPanel as opposed to an object since they should all pass a
 * WizardControlPanel to prevent boxing.
 * 
 *    Rev 1.0   Jul 21 2006 16:55:12   cgray
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
using System.Data;
using System.Text;
using System.Windows.Forms;

#endregion


namespace GraySystem.UI.Controls.WizardControlPanels
{
   #region Delegates

   /// <summary>
   /// Used to specify how an Event Handler method should be constructed for the ShowPanel
   /// Event.
   /// </summary>
   public delegate void ShowPanelEventHandler(WizardControlPanel sender, EventArgs e);

   /// <summary>
   /// Used to specify how an Event Handler method should be constructed for the LeavePanel
   /// Event.
   /// </summary>
   public delegate bool LeavePanelEventHandler(WizardControlPanel sender, EventArgs e);

   #endregion

   /// <summary>
   /// Summary description for WizardControlPanel.
   /// </summary>
   public class WizardControlPanel : System.Windows.Forms.UserControl
   {
      #region Fields

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private Container _components = null;

      private GraySystem.UI.Forms.Wizard _wizParent;

      private bool _bSkipOnBack;

      #endregion

      #region Properties

      #region Wizard

      /// <summary>
      /// Gets the parent wizard of the control panel.
      /// </summary>
      public GraySystem.UI.Forms.Wizard Wizard
      {
         get { return (_wizParent); }
      } // end Wizard property

      #endregion

      #region Summary

      /// <summary>
      /// Gets the Summary associated with the panel by calling the Panel's ConstructSummary
      /// method.
      /// </summary>
      public string Summary
      {
         get { return (ConstructSummary()); }
      } // end Summary property

      #endregion

      #region SkipOnBack

      /// <summary>
      /// Gets or sets the value indicating whether or not the panel should be skipped if the
      /// user is attempting to go back.
      /// </summary>
      public bool SkipOnBack
      {
         get { return (_bSkipOnBack); }

         set { _bSkipOnBack = value; }
      } // end _bSkipOnBack property

      #endregion

      #endregion

      #region Events

      /// <summary>
      /// Should be raised to indicate that the panel is about to be shown to the user to allow
      /// the user to perform an preprocessing before the panel is displayed.
      /// </summary>
      public event ShowPanelEventHandler ShowPanel;

      /// <summary>
      /// Should be raised to indicate that the user is about to leave the panel to allow the user
      /// to perform an last minute validation or processing before the panel is hidden.
      /// </summary>
      public event LeavePanelEventHandler LeavePanel;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new WizardControlPanel object.
      /// </summary>
      protected WizardControlPanel()
      {
         InitializeComponent();
      } // end WizardControlPanel constructor

      /// <summary>
      /// Constructs a new WizardControlPanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      protected WizardControlPanel(GraySystem.UI.Forms.Wizard wizParent)
      {
         _wizParent = wizParent;
      } // end WizardControlPanel constructor

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
      /// Initializes the panel by adding the controls and event handlers.  Override this method to
      /// initialize the component instead of within the constructor. By doing so, the wizard will
      /// load more quickly based on the current architecture, which allows a developer to create
      /// all the screens at once in an array to also create the sequence.
      /// </summary>
      protected virtual void Initialize()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      } // end Initialize

      #endregion

      #region ConstructSummary

      /// <summary>
      /// Constructs the Summary for the panel.  Override this method in order to customize
      /// the Summary constructed for the panel.
      /// </summary>
      /// <returns>Returns the summary constructed.  By default, an empty string will be
      /// returned.</returns>
      protected virtual string ConstructSummary()
      {
         return ("");
      } // end ConstructSummary

      #endregion

      #region InitializeComponent

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardControlPanel));
         this.SuspendLayout();
         // 
         // WizardControlPanel
         // 
         this.Name = "WizardControlPanel";
         resources.ApplyResources(this, "$this");
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #region Event Raisers

      #region RaiseShowPanelEvent

      /// <summary>
      /// Initializes the Panel and calls the ShowPanel event if the event is defined.
      /// </summary>
      /// <param name="e"></param>
      public void RaiseShowPanelEvent(EventArgs e)
      {
         // Initializing the panel, which includes creating the controls and adding the event handlers.
         // By doing so here instead of within the constructor, the wizard will load more quickly based
         // on the current architecture, which allows a developer to create all the screens at once in
         // an array to also create the sequence.
         Initialize();

         if (ShowPanel != null)
         {
            ShowPanel(this, e);
         } // end if
      } // end RaiseShowPanelEvent

      #endregion

      #region RaiseLeavePanelEvent

      /// <summary>
      /// Calls the LeavePanel event if the event is defined.
      /// </summary>
      public bool RaiseLeavePanelEvent()
      {
         if (LeavePanel == null)
         {
            return (true);
         } // end if
         else
         {
            return (LeavePanel(this, EventArgs.Empty));
         } // end else
      } // end RaiseLeavePanelEvent

      #endregion

      #endregion

      #endregion
   } // end WizardControlPanel Class
} // end GraySystem.UI.Controls.WizardControlPanels Namespace