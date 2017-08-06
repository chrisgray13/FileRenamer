#region Usings

using System;

#endregion


namespace FileRenamer
{
   public delegate void RenamingFinished(object sender, string[] sFileNames);

   /// <summary>
   /// Summary description for Renamer.
   /// </summary>
   public class Renamer
   {
      #region Fields

      private string[] _sFileNames;
      private string _sNamingOption;
      private string _sPrefix;
      private string _sSuffixMask;
      private string _sSortingOption;
      private bool _bTestRun;

      public event FileRenamedEventHandler FileRenamed;
      public event RenamingFinished Finished;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new instance of the Renamer class and sets the File Names, Rename Value,
      /// Rename Prefix, and Suffix Mask used in the renaming process.
      /// </summary>
      /// <param name="sFileNames">An array of file names including their path</param>
      /// <param name="sNamingOption">The basis of the new name.  This could be a valid property or
      /// custom text.</param>
      /// <param name="sPrefix">Prefix used for all new file names.</param>
      /// <param name="sSuffixMask">Suffix appended to the new file name, which is in the form of
      /// a valid mask and allows for things like numbering.</param>
      /// <param name="sSortingOption">Used to determine how the files should be sorted prior to
      /// renaming.  This ensures the suffix mask is used appropriately.</param>
      /// <param name="bTestRun"></param>
      public Renamer(string[] sFileNames, string sNamingOption, string sPrefix, string sSuffixMask,
                     string sSortingOption, bool bTestRun)
      {
         _sFileNames = sFileNames;

         _sNamingOption = sNamingOption;
         _sPrefix = sPrefix;
         _sSuffixMask = sSuffixMask;
         _sSortingOption = sSortingOption;
         _bTestRun = bTestRun;
      } // end Renamer constructor

      #endregion

      #region Methods

      #region RenameFiles

      public void RenameFiles()
      {
         PowerFileCollection powerFiles = new PowerFileCollection(_sFileNames);

         powerFiles.FileRenamed += new FileRenamedEventHandler(SendRenamedFileToLog);

         powerFiles.Sort(_sSortingOption, "ASC");
         powerFiles.Rename(_sNamingOption, _sPrefix, _sSuffixMask, _bTestRun);

         Finished(this, powerFiles.NewFileNames);
      } // end RenameFiles

      #endregion

      #region Event Handlers

      #region SendRenamedFileToLog

      /// <summary>
      /// Sends a file renamed message inorder to log it.
      /// </summary>
      /// <param name="sender">Object throwing the event</param>
      /// <param name="e">Event arguements relating to a the File renaming operation</param>
      private void SendRenamedFileToLog(object sender, FileRenamedEventArgs e)
      {
         FileRenamed(sender, e);
      } // end SendRenamedFileToLog

      #endregion

      #endregion

      #endregion
   } // end Renamer Class
} // end FileRenamer Namespace