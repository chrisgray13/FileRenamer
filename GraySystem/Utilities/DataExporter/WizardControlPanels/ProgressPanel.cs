#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\ProgressPanel.cs-arc  $
 * $Revision:   1.2  $
 * $Author:   cgray  $
 * $Date:   Jul 25 2006 10:50:44  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\ProgressPanel.cs-arc  $
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

using ICS.GUI_Library.EventArguments;

#endregion


namespace ICS.Utilities.DataExporter.WizardControlPanels
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
   public class ProgressPanel : ICS.GUI_Library.Controls.WizardControlPanels.WizardControlPanel
   {
      #region Fields

      #region Controls

      private System.Windows.Forms.Label _lblProgress;
      private System.Windows.Forms.ProgressBar _prgProcessProgress;

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
      public ProgressPanel() : base()
      {
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
         this._prgProcessProgress = new System.Windows.Forms.ProgressBar();
         this._lblProgress = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // _prgProcessProgress
         // 
         this._prgProcessProgress.Location = new System.Drawing.Point(32, 32);
         this._prgProcessProgress.Name = "_prgProcessProgress";
         this._prgProcessProgress.Size = new System.Drawing.Size(392, 24);
         this._prgProcessProgress.TabIndex = 0;
         // 
         // _lblProgress
         // 
         this._lblProgress.Location = new System.Drawing.Point(40, 72);
         this._lblProgress.Name = "_lblProgress";
         this._lblProgress.Size = new System.Drawing.Size(384, 24);
         this._lblProgress.TabIndex = 1;
         // 
         // ProgressPanel
         // 
         this.Controls.Add(this._lblProgress);
         this.Controls.Add(this._prgProcessProgress);
         this.Name = "ProgressPanel";
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #region StartTask

      /// <summary>
      /// Starts the task by creating a DataExporter object and running its ExportData method
      /// within a new Thread.
      /// </summary>
      /// <param name="dataExporter"></param>
      public void StartTask(DataExporter dataExporter)
      {
         Thread threadExporter = new Thread(new ThreadStart(dataExporter.Start));

         dataExporter.UpdateProgress += new UpdateProgressEventHandler(UpdateProgress);
         dataExporter.TaskComplete += new TaskCompleteEventHandler(RaiseCompletedEvent);

         // Starting Data Export Thread to begin Data Export
         threadExporter.Start();
      } // end StartTask

      #endregion

      #region Event Handlers

      #region UpdateProgress

      /// <summary>
      /// UpdateProgress event for the Process, which is used to handle updating the
      /// progress of the Process.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e">Progress Update Event Arguments, which include the Progress Message
      /// and Progress Value.</param>
      private void UpdateProgress(object sender, ProgressUpdateEventArgs e)
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

         if (_prgProcessProgress.InvokeRequired)
         {
            Invoke(new SetProgressValueHandler(SetProgressValue), new object[] { e.ProgressValue });
         } // end if
         else
         {
            _prgProcessProgress.Value = e.ProgressValue;
         } // end else
      } // end UpdateProgress

      #endregion

      #region SetProgressText

      /// <summary>
      /// Sets the progress text.
      /// </summary>
      /// <param name="sText">Text used to set the progress.</param>
      private void SetProgressText(string sText)
      {
         _lblProgress.Text = sText;
      } // end SetProgressText

      #endregion

      #region SetProgressValue

      /// <summary>
      /// Used to set the value of the process's progress.
      /// </summary>
      /// <param name="iValue">Value used to update the process's progress.</param>
      private void SetProgressValue(int iValue)
      {
         _prgProcessProgress.Value = iValue;
      } // end SetProgressText

      #endregion

      #endregion

      #region Event Raisers

      #region RaiseCompletedEvent

      /// <summary>
      /// Completed event for the Process indicating that the process is complete, which raise
      /// the completed event for an object of this class to indicate completion of the task.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e">Indicates the success rate of the data export.</param>
      private void RaiseCompletedEvent(object sender, ResultsEventArgs e)
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
} // end ICS.Utilities.DataExporter.WizardControlPanels Namespace