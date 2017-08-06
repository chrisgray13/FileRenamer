#region Usings

using System;

using GraySystem.IO;

#endregion


namespace FileRenamer
{
    public delegate void RenamingFinished(object sender, string[] sFileNames);

    /// <summary>
    /// Summary description for RenamerProcess.
    /// </summary>
    public class RenamerProcess
    {
        #region Fields

        private string[] _sFileNames;
        private string[] _sOldFileNames;
        private RenamingTypes _renamingType;
        private string _sNamingOption;
        private string _sRenamingTemplate;
        private int _timeOffset;
        private string _sSortingOption;
        private bool _bTestRun;

        public event FileRenamedEventHandler FileRenamed;
        public event RenamingFinished Finished;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sFileNames"></param>
        /// <param name="sOldFileNames"></param>
        public RenamerProcess(string[] sFileNames, string[] sOldFileNames)
        {
            _sFileNames = sFileNames;
            _sOldFileNames = sOldFileNames;

            _sNamingOption = "UNDO";
        } // end RenamerProcess

        /// <summary>
        /// Constructs a new instance of the RenamerProcess class and sets the File Names, Rename Value,
        /// Rename Prefix, and Suffix Mask used in the renaming process.
        /// </summary>
        /// <param name="sFileNames">An array of file names including their path</param>
        /// <param name="sFilenameTemplate"></param>
        /// <param name="sSortingOption">Used to determine how the files should be sorted prior to
        /// renaming.  This ensures the suffix mask is used appropriately.</param>
        /// <param name="bTestRun"></param>
        public RenamerProcess(string[] sFileNames, RenamingTypes renamingType, string sRenameTemplate, int timeOffset,
                              string sSortingOption, bool bTestRun)
        {
            _sFileNames = sFileNames;
            _renamingType = renamingType;
            _sRenamingTemplate = sRenameTemplate;
            _timeOffset = timeOffset;
            _sSortingOption = sSortingOption;
            _bTestRun = bTestRun;
        } // end RenamerProcess constructor

        #endregion

        #region Methods

        #region RenameFiles

        public void RenameFiles()
        {
            PowerFileCollection powerFiles = new PowerFileCollection(_sFileNames);

            powerFiles.FileRenamed += new FileRenamedEventHandler(SendRenamedFileToLog);

            if (_sNamingOption == "UNDO")
            {
                powerFiles.Rename(ref _sOldFileNames, _bTestRun);
            } // end if
            else
            {
                powerFiles.Sort(_sSortingOption, "ASC");
                powerFiles.Rename(_sRenamingTemplate, _renamingType, _timeOffset, _bTestRun);
            } // end else

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
        private void SendRenamedFileToLog(object sender, AttributeChangedEventArgs e)
        {
            if (FileRenamed != null)
            {
                FileRenamed.Invoke(sender, e);
            }
        } // end SendRenamedFileToLog

        #endregion

        #endregion

        #endregion
    } // end RenamerProcess Class
} // end FileRenamer Namespace