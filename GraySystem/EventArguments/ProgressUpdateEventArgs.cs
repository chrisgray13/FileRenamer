#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\EventArguments\ProgressUpdateEventArgs.cs-arc  $
 * $Revision:   1.0  $
 * $Author:   cgray  $
 * $Date:   Feb 12 2007 13:20:18  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\EventArguments\ProgressUpdateEventArgs.cs-arc  $
 * 
 *    Rev 1.0   Feb 12 2007 13:20:18   cgray
 * Initial revision.
 * 
 *    Rev 1.0   Jul 21 2006 16:56:18   cgray
 * Initial revision.
 *
 *
 */

#endregion

#region Usings

using System;

#endregion


namespace GraySystem.EventArguments
{
   /// <summary>
   /// Used to store a progress value and message of a task or whatever.  This might be most
   /// useful to update a progress bar for a wizard.
   /// </summary>
   public class ProgressUpdateEventArgs : EventArgs
   {
      #region Fields

      private string _sProgressMsg;
      private int _iProgressValue;

      #endregion

      #region Properties

      #region ProgressMsg

      /// <summary>
      /// Gets the Progress Message, which is to indicates the progress is.
      /// </summary>
      public string ProgressMsg
      {
         get { return (_sProgressMsg); }
      } // end ProgressMsg property

      #endregion

      #region ProgressValue

      /// <summary>
      /// Gets the Progress Value, which should be between the minimum and maximum values
      /// to indicate the progress level.
      /// </summary>
      public int ProgressValue
      {
         get { return (_iProgressValue); }
      } // end ProgressValue property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new ProgressUpdateEventArgs object and sets the Progress Message and
      /// Progress Value.
      /// </summary>
      /// <param name="sProgressMsg">Message to indicate what the progress is.</param>
      /// <param name="iProgressValue">Value between the minimum and maximum values
      /// to indicate the progress value.</param>
      public ProgressUpdateEventArgs(string sProgressMsg, int iProgressValue)
      {
         _sProgressMsg = sProgressMsg;
         _iProgressValue = iProgressValue;
      } // end ProgressUpdateEventArgs constructor

      #endregion
   } // end ProgressUpdateEventArgs Class
} // end ICS.Utilities.EventArguments Namespace