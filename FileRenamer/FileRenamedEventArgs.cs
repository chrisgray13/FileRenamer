#region Usings

using System;

#endregion


namespace FileRenamer
{
   /// <summary>
   /// Provides information pertaining to the results of a File renaming operation.
   /// </summary>
   public class FileRenamedEventArgs : EventArgs
   {
      #region Fields

      private string _sOriginalFileName;
      private string _sNewFileName;
      private bool _bResult;
      private string _sMessage;

      #endregion

      #region Properties

      #region OriginalFileName

      /// <summary>
      /// Gets the File Name prior to a rename operation
      /// </summary>
      public string OriginalFileName
      {
         get { return (_sOriginalFileName); }
      } // end OriginalFileName property

      #endregion

      #region NewFileName

      /// <summary>
      /// Gets the File Name to which the file is to be renamed
      /// </summary>
      public string NewFileName
      {
         get { return (_sNewFileName); }
      } // end NewFileName property

      #endregion

      #region Result

      /// <summary>
      /// Gets the result of the rename operation
      /// </summary>
      public bool Result
      {
         get { return (_bResult); }
      } // end Result property

      #endregion

      #region Message

      /// <summary>
      /// Gets the Message associated with the rename operation
      /// </summary>
      public string Message
      {
         get { return (_sMessage); }
      } // end Message property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Instantiates a FileRenamedEventArgs object and sets the OriginalFileName, NewFileName,
      /// Result, and Message.
      /// </summary>
      /// <param name="sOriginalFileName">Original File Name prior an attempt to rename</param>
      /// <param name="sNewFileName">File Name to which the file will attempt to be changed</param>
      /// <param name="bResult">Result of the rename operation</param>
      /// <param name="sMessage">Any message associated with the rename operation</param>
      public FileRenamedEventArgs(string sOriginalFileName, string sNewFileName,
                                  bool bResult, string sMessage) : base()
      {
         _sOriginalFileName = sOriginalFileName;
         _sNewFileName = sNewFileName;
         _bResult = bResult;
         _sMessage = sMessage;
      } // end FileRenamedEventArgs

      #endregion

      #region Methods

      #region ToString

      /// <summary>
      /// Gets a message explaining the results of the renaming operation in the form of a string.
      /// </summary>
      /// <returns>Returns a message explaining the results of the renaming operation.</returns>
      public override string ToString()
      {
         if (_bResult)
         {
            return ("Successfully renamed " + _sOriginalFileName + " to " + _sNewFileName);
         } // end if
         else
         {
            return ("Failed to rename " + _sOriginalFileName + " to " + _sNewFileName +
                    " because of the following reason:" + Environment.NewLine + 
                    "\t" + _sMessage);
         } // end else
      } // end ToString

      #endregion

      #endregion
   } // end FileRenamedEventArgs Class
} // end FileRenamer Namespace