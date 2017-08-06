#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\Task.cs-arc  $
 * $Revision:   1.2  $
 * $Author:   cgray  $
 * $Date:   May 19 2008 23:04:12  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\Task.cs-arc  $
 * 
 *    Rev 1.2   May 19 2008 23:04:12   cgray
 * BE025492 - Added a logging object field to the class.
 * 
 *    Rev 1.1   Aug 09 2007 16:19:38   pmonaco
 * BD022129 - First round of FxCop fixes.  Globalization and Security.
 * 
 *    Rev 1.0   Feb 12 2007 13:24:28   cgray
 * Initial revision.
 * 
 *
 *
 */

#endregion

#region Usings

using System;
using System.Text;

using GraySystem.EventArguments;
using System.Globalization;

#endregion


namespace GraySystem
{
   #region Delegates

   /// <summary>
   /// Used to specify how an Event Handler method should be constructed for the UpdateProgress
   /// Event.
   /// </summary>
   public delegate void UpdateProgressEventHandler(object sender, ProgressUpdateEventArgs e);

   /// <summary>
   /// Used to specify how an Event Handler method should be constructed for the TaskComplete
   /// Event.
   /// </summary>
   public delegate void TaskCompleteEventHandler(object sender, ResultsEventArgs e);

   #endregion

   /// <summary>
   /// A template class to run a task.  This class should be inherited to provide the
   /// actual function of the task, but the generic nature allows other classes like
   /// the WizardControlPanel ProgressPanel to start the task without knowing anything
   /// about the inherited class.
   /// </summary>
   public abstract class Task
   {
      #region Fields

      /// <summary>
      /// Used to store the results of the task which may be used to write to some sort of
      /// log or results screen
      /// </summary>
      protected StringBuilder _sResults;

      /// <summary>
      /// Flag indicating the success of the task
      /// </summary>
      protected bool _bResult;

      #endregion

      #region Events

      /// <summary>
      /// Event to update the progress of the task.
      /// </summary>
      public event UpdateProgressEventHandler UpdateProgress;

      /// <summary>
      /// Event to indicate that the task is complete.
      /// </summary>
      public event TaskCompleteEventHandler TaskComplete;

      #endregion

      #region Methods

      #region Start

      /// <summary>
      /// Used to start the task.  This must be overridden as each task is different; therefore,
      /// the start must be different.  However, a generic class like the WizardPanelControl
      /// ProgressPanel would use this method to start the task given the task object.
      /// </summary>
      public abstract void Start();

      #endregion

      #region Event Raisers

      #region RaiseUpdateProgressEvent

      /// <summary>
      /// Raises the UpdateProgress event, which is used to set the Progress Message and Progress
      /// Value to send to the handler.  This would be used to update a Progress bar and label
      /// like on a Progress Wizard Control Panel.
      /// </summary>
      /// <param name="sProgressMsg">Message to indicate what the progress is.</param>
      /// <param name="iProgressValue">Value between the minimum and maximum values
      /// to indicate the progress value.</param>
      protected virtual void RaiseUpdateProgressEvent(string sProgressMsg, int iProgressValue)
      {
         if (UpdateProgress != null)
         {
            UpdateProgress(this, new ProgressUpdateEventArgs(sProgressMsg, iProgressValue));
         } // end if
      } // end RaiseUpdateProgressEvent

      #endregion

      #region RaiseTaskCompleteEvent

      /// <summary>
      /// Raises an event indicating that the Task is finished.
      /// </summary>
      /// <param name="bTaskSuccess">Success rate of the task.</param>
      protected virtual void RaiseTaskCompleteEvent(bool bTaskSuccess)
      {
         if (TaskComplete != null)
         {
            TaskComplete(this, new ResultsEventArgs(bTaskSuccess, _sResults.ToString()));
         } // end if
      } // end RaiseTaskCompleteEvent

      #endregion

      #endregion

      #endregion
   } // end Task Class
} // end GraySystem Namespace