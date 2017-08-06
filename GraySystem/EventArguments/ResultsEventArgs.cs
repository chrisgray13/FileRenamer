#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\EventArguments\ResultsEventArgs.cs-arc  $
 * $Revision:   1.0  $
 * $Author:   cgray  $
 * $Date:   Feb 12 2007 13:20:04  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\EventArguments\ResultsEventArgs.cs-arc  $
 * 
 *    Rev 1.0   Feb 12 2007 13:20:04   cgray
 * Initial revision.
 * 
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
   /// Used to store a boolean result and summary of the result of a task or whatever.
   /// </summary>
   public class ResultsEventArgs : EventArgs
   {
      #region Fields

      private bool _bResult;
      private string _sSummary;

      #endregion

      #region Properties

      #region Result

      /// <summary>
      /// Gets the ResultEventArgs' Result
      /// </summary>
      public bool Result
      {
         get { return (_bResult); }
      } // end Result property

      #endregion

      #region Summary

      /// <summary>
      /// Gets the ResultEventArgs' Summary
      /// </summary>
      public string Summary
      {
         get { return (_sSummary); }
      } // end Summary property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new ResultsEventArgs object and sets the Result value and summary.
      /// </summary>
      /// <param name="bResult">Results value</param>
      /// <param name="sSummary">Results summary</param>
      public ResultsEventArgs(bool bResult, string sSummary)
      {
         _bResult = bResult;
         _sSummary = sSummary;
      } // end ResultsEventArgs constructor

      #endregion
   } // end ResultsEventArgs Class
} // end ICS.Utilities.EventArguments Namespace