﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraySystem.IO
{
    public class TitleChangedEventArgs : AttributeChangedEventArgs
    {
        #region Constructors

        /// <summary>
        /// Instantiates a FileRenamedEventArgs object and sets the OriginalFileName, NewFileName,
        /// Result, and Message.
        /// </summary>
        /// <param name="sFilename">Original File Name prior an attempt to rename</param>
        /// <param name="sNewTitle">File Name to which the file will attempt to be changed</param>
        /// <param name="bResult">Result of the rename operation</param>
        /// <param name="sMessage">Any message associated with the rename operation</param>
        public TitleChangedEventArgs(string sFilename, string sNewTitle, bool bResult, string sMessage, bool logOnly) :
            base (sFilename, sNewTitle, bResult, sMessage, logOnly)
        {
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
                return ("Successfully updated the title of " + _sOriginalAttributeValue + " to " + _sNewAttributeValue);
            } // end if
            else
            {
                return ("Failed to update the title of " + _sOriginalAttributeValue + " to " + _sNewAttributeValue +
                        " because of the following reason:" + Environment.NewLine +
                        "\t" + _sMessage);
            } // end else
        } // end ToString

        #endregion

        #endregion

    }
}
