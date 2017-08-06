#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\ProgressPanel.cs-arc  $
 * $Revision:   1.2  $
 * $Author:   cgray  $
 * $Date:   May 19 2008 21:29:38  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\ProgressPanel.cs-arc  $
 * 
 *    Rev 1.2   May 19 2008 21:29:38   cgray
 * BE25492 - Added a new constructor that takes a Wizard object and sets it as the wizard (parent) of the control panel.
 * 
 * Added a call to the InitializeComponent method within the base constructor to ensure the Design-time view works correctly.
 * 
 *    Rev 1.1   Nov 08 2007 14:36:02   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.0   Feb 12 2007 15:06:26   cgray
 * Initial revision.
 * 
 *
 *
 */

#endregion

#region Usings

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using GraySystem.EventArguments;

#endregion


namespace GraySystem.UI.Controls.WizardControlPanels
{
   #region Delegates

   /// <summary>
   /// Used to specify how an Event Handler method should be constructed for the Completed
   /// Event.
   /// </summary>
   /// <param name="e"></param>
   /// <param name="sender"></param>
   public delegate void CompletedEventHandler(object sender, ResultsEventArgs e);

   /// <summary>
   /// Used to specify how a method should be constructed to Set the progress Text.
   /// </summary>
   /// <param name="sText">Text relating to the progress</param>
   public delegate void SetProgressTextHandler(string sText);

   /// <summary>
   /// Used to specify how a method should be constructed to Set the progress Value.
   /// </summary>
   /// <param name="iValue">Value of progress (0 - 100)</param>
   public delegate void SetProgressValueHandler(int iValue);

   #endregion

   /// <summary>
   /// ProgressPanel Class is used to display the progress of the task to the user.
   /// </summary>
   public class ProgressPanel : WizardControlPanel
   {
      #region Fields

      #region Controls

      /// <summary>
      /// Displays a message relating to the progress of the task to the user.
      /// </summary>
      protected System.Windows.Forms.Label _lblProgress;

      /// <summary>
      /// Displays the progress of the task to the user.
      /// </summary>
      protected System.Windows.Forms.ProgressBar _prgTaskProgress;

      #endregion

      private System.ComponentModel.IContainer _components = null;

      #endregion

      #region Events

      /// <summary>
      /// Event to indicate that the task is complete.
      /// </summary>
      public event CompletedEventHandler Completed;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new ProgressPanel object.
      /// </summary>
      protected ProgressPanel() : base()
      {
         InitializeComponent();

         SkipOnBack = true;
      } // end ProgressPanel constructor

      /// <summary>
      /// Constructs a new ProgressPanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      public ProgressPanel(GraySystem.UI.Forms.Wizard wizParent) : base(wizParent)
      {
         SkipOnBack = true;
      } // end ProgressPanel constructor

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
      } // end Dipose

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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressPanel));
         this._prgTaskProgress = new System.Windows.Forms.ProgressBar();
         this._lblProgress = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // _prgTaskProgress
         // 
         resources.ApplyResources(this._prgTaskProgress, "_prgTaskProgress");
         this._prgTaskProgress.Name = "_prgTaskProgress";
         // 
         // _lblProgress
         // 
         resources.ApplyResources(this._lblProgress, "_lblProgress");
         this._lblProgress.Name = "_lblProgress";
         // 
         // ProgressPanel
         // 
         this.Controls.Add(this._lblProgress);
         this.Controls.Add(this._prgTaskProgress);
         this.Name = "ProgressPanel";
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #region StartTask

      /// <summary>
      /// Starts the task using the task's Start method.  UpdateProgress and TaskComplete
      /// events are setup to be handled.
      /// </summary>
      /// <param name="task">A task that implements the Task class for which is to be executed.</param>
      public virtual void StartTask(Task task)
      {
         Thread threadTask = new Thread(new ThreadStart(task.Start));

         task.UpdateProgress += new UpdateProgressEventHandler(UpdateProgress);
         task.TaskComplete += new TaskCompleteEventHandler(RaiseCompletedEvent);

         // Starting the Task Thread to begin the task
         threadTask.Start();
      } // end StartTask

      #endregion

      #region Event Handlers

      #region UpdateProgress

      /// <summary>
      /// UpdateProgress event for the Process, which is used to handle updating the
      /// progress of the Task.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e">Progress Update Event Arguments, which include the Progress Message
      /// and Progress Value.</param>
      void UpdateProgress(object sender, ProgressUpdateEventArgs e)
      {
         // InvokeRequired required compares the thread ID of the calling thread to the thread
         // ID of the creating thread.  If these threads are different, it returns true.
         if (_lblProgress.InvokeRequired)
         {
            Invoke(new SetProgressTextHandler(SetProgressText), new object[] { e.ProgressMsg });
         } // end if
         else
         {
            _lblProgress.Text = e.ProgressMsg;
         } // end else

         if (_prgTaskProgress.InvokeRequired)
         {
            Invoke(new SetProgressValueHandler(SetProgressValue), new object[] { e.ProgressValue });
         } // end if
         else
         {
            _prgTaskProgress.Value = e.ProgressValue;
         } // end else
      } // end UpdateProgress

      #endregion

      #region SetProgressText

      /// <summary>
      /// Sets the progress text.
      /// </summary>
      /// <param name="sText">Text used to set the progress.</param>
      protected virtual void SetProgressText(string sText)
      {
         _lblProgress.Text = sText;
      } // end SetProgressText

      #endregion

      #region SetProgressValue

      /// <summary>
      /// Used to set the value of the process's progress.
      /// </summary>
      /// <param name="iValue">Value used to update the process's progress.</param>
      protected virtual void SetProgressValue(int iValue)
      {
         _prgTaskProgress.Value = iValue;
      } // end SetProgressText

      #endregion

      #endregion

      #region Event Raisers

      #region RaiseCompletedEvent

      /// <summary>
      /// Completed event for the Process indicating that the task is complete, which raises
      /// the completed event for an object of this class to indicate completion of the task.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e">Indicates the success rate of the data export.</param>
       void RaiseCompletedEvent(object sender, ResultsEventArgs e)
      {
         if (Completed != null)
         {
            Invoke(new TaskCompleteEventHandler(Completed), new object[] { sender, e });
         } // end if
      } // end RaiseCompletedEvent

      #endregion

      #endregion

      #endregion
   } // end ProgressPanel Class
} // end GraySystem.UI.Controls.WizardControlPanels Namespace