#region Usings

using System;

#endregion


namespace GraySystem.IO
{
    /// <summary>
    /// Provides information pertaining to the results of a File renaming operation.
    /// </summary>
    public class AttributeChangedEventArgs : EventArgs
    {
        #region Fields

        protected string _sOriginalAttributeValue;
        protected string _sNewAttributeValue;
        protected bool _bResult;
        protected string _sMessage;

        #endregion

        #region Properties

        #region OriginalFileName

        /// <summary>
        /// Gets the File Name prior to a rename operation
        /// </summary>
        public string OriginalFileName
        {
            get { return (_sOriginalAttributeValue); }
        } // end OriginalFileName property

        #endregion

        #region NewFileName

        /// <summary>
        /// Gets the File Name to which the file is to be renamed
        /// </summary>
        public string NewFileName
        {
            get { return (_sNewAttributeValue); }
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

        public bool LogOnly { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a FileRenamedEventArgs object and sets the OriginalFileName, NewFileName,
        /// Result, and Message.
        /// </summary>
        /// <param name="sOriginalAttributeValue">Original File Name prior an attempt to rename</param>
        /// <param name="sNewAttributeValue">File Name to which the file will attempt to be changed</param>
        /// <param name="bResult">Result of the rename operation</param>
        /// <param name="sMessage">Any message associated with the rename operation</param>
        public AttributeChangedEventArgs(string sOriginalAttributeValue, string sNewAttributeValue,
                                    bool bResult, string sMessage, bool logOnly)
            : base()
        {
            _sOriginalAttributeValue = sOriginalAttributeValue;
            _sNewAttributeValue = sNewAttributeValue;
            _bResult = bResult;
            _sMessage = sMessage;
            LogOnly = logOnly;
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
            if (LogOnly)
            {
                return ("Just so you know for " + _sOriginalAttributeValue + ":" + Environment.NewLine + "\t" + _sMessage);
            }
            else if (_bResult)
            {
                return ("Successfully changed " + _sOriginalAttributeValue + " to " + _sNewAttributeValue);
            } // end if
            else
            {
                return ("Failed to change " + _sOriginalAttributeValue + " to " + _sNewAttributeValue +
                        " because of the following reason:" + Environment.NewLine +
                        "\t" + _sMessage);
            } // end else
        } // end ToString

        #endregion

        #endregion
    } // end FileRenamedEventArgs Class
} // end GraySystem.IO Namespace
