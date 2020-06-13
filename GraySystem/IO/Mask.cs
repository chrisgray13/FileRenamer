#region Usings

using System;

using GraySystem;
using GraySystem.Containers;

#endregion


namespace GraySystem.IO
{
    /// <summary>
    /// Provides functionality for parsing a mask and substituting actual characters in place
    /// of the mask characters '#'.
    /// </summary>
    public class Mask
    {
        #region Fields

        private string _sMask;
        private int _iMaxMaskChars;
        private string _sMaskPrefix;
        private string _sMaskSuffix;
        private SelfManagedArray _arrMask;

        #endregion

        #region Properties

        #region IsEmpty

        public bool IsEmpty => String.IsNullOrEmpty(_sMask);

        #endregion

        #region MaxMaskChars

        /// <summary>
        /// Gets the mask substring consisting of the most consecutive mask characters (#).
        /// </summary>
        public int MaxMaskChars
        {
            get
            {
                int iMaskCharCounter = 0;

                if (_iMaxMaskChars == 0)
                {
                    foreach (char c in _sMask)
                    {
                        if (c == '#')
                        {
                            iMaskCharCounter++;
                        } // end if
                        else if (iMaskCharCounter > _iMaxMaskChars)
                        {
                            _iMaxMaskChars = iMaskCharCounter;
                            iMaskCharCounter = 0;
                        } // end else if
                    } // end foreach

                    if (_iMaxMaskChars == 0)  // Attempt to set the Max Mask Chars if it was not already set,
                    {                         // which may be because the last char was a mask char.
                        _iMaxMaskChars = iMaskCharCounter;
                    } // end if
                } // end if

                return (_iMaxMaskChars);
            } // end get
        } // end MaxMaskChars property

        #endregion

        #region MaskPrefix

        /// <summary>
        /// Gets the Mask's prefix, which is any characters prior to the mask.
        /// </summary>
        public string MaskPrefix
        {
            get
            {
                int iMaskStart;
                string sTemp = "";

                if (_sMaskPrefix.Length == 0)
                {
                    iMaskStart = _sMask.IndexOf(sTemp.PadLeft(MaxMaskChars, '#'));
                    _sMaskPrefix = _sMask.Substring(0, iMaskStart);
                } // end if

                return (_sMaskPrefix);
            } // end get
        } // end MaskPrefix property

        #endregion

        #region MaskSuffix

        /// <summary>
        /// Gets the Mask's suffix, which is any characters following the mask.
        /// </summary>
        public string MaskSuffix
        {
            get
            {
                int iMaskEnd;
                string sTemp = "";

                if (_sMaskSuffix.Length == 0)
                {
                    iMaskEnd = _sMask.LastIndexOf(sTemp.PadLeft(MaxMaskChars, '#')) + MaxMaskChars;
                    _sMaskSuffix = _sMask.Substring(iMaskEnd, _sMask.Length - iMaskEnd);
                } // end if

                return (_sMaskSuffix);
            } // end get
        } // end MaskPrefix property

        #endregion

        #region DateTimeExists

        public bool DateTimeExists
        {
            get
            {
                for (int i = 0; i < _arrMask.GetLength(0); i++)
                {
                    if (_arrMask[i, 1] == "*")
                    {
                        if ((_arrMask[i, 0][0] == 'y') || (_arrMask[i, 0][0] == 'M') || (_arrMask[i, 0][0] == 'd') ||
                            (_arrMask[i, 0][0] == 'h') || (_arrMask[i, 0][0] == 'm') || (_arrMask[i, 0][0] == 's'))
                        {
                            return (true);
                        } // end if
                    } // end if
                } // end for

                return (false);
            } // end get
        } // end DoesContainADateTime

        #endregion

        #region DoesContainASequence

        public bool DoesContainASequence
        {
            get
            {
                for (int i = 0; i < _arrMask.GetLength(0); i++)
                {
                    if (_arrMask[i, 1] == "*")
                    {
                        if (_arrMask[i, 0][0] == '#')
                        {
                            return (true);
                        } // end if
                    } // end if
                } // end for

                return (false);
            } // end get
        } // end DoesContainASequence

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a Mask object and sets the mask.
        /// </summary>
        public Mask()
        {
            _sMaskPrefix = _sMaskSuffix = "";
            _iMaxMaskChars = 0;
        } // end Mask constructor

        /// <summary>
        /// Instantiates a Mask object and sets the mask.
        /// </summary>
        /// <param name="sMask">Mask</param>
        public Mask(string sMask)
        {
            _sMask = sMask;
            _sMaskPrefix = _sMaskSuffix = "";
            _iMaxMaskChars = 0;
            Parse();
        } // end Mask constructor

        #endregion

        #region Methods

        #region IsMaskValid

        /// <summary>
        /// Determines if the suffix mask is valid by ensure that it contains at least on
        /// mask character (#).
        /// </summary>
        /// <returns>Returns true if there is at least on mask character (#); otherwise, false.</returns>
        public bool IsMaskValid()
        {
            return (MaxMaskChars > 0);
        } // end IsMaskValid

        /// <summary>
        /// Determines if the suffix mask is valid by ensure that it contains at least on
        /// mask character (#).
        /// </summary>
        /// <returns>Returns true if there is at least on mask character (#); otherwise, false.</returns>
        public bool IsMaskValid(string sMask)
        {
            return (true);
        } // end IsMaskValid

        #endregion

        #region Parse

        private void Parse()
        {
            int iStart = -1;
            int iCount = 0;
            bool bMask = false;

            _arrMask = new SelfManagedArray(5, 2);

            for (int i = 0; i < _sMask.Length; i++)
            {
                if (_sMask[i] == '{')
                {
                    if (iCount > 0)
                    {
                        _arrMask.Add(_sMask.Substring(iStart, iCount));
                    } // end if

                    iStart = i + 1;
                    iCount = 0;
                    bMask = false;
                    for (i++; i < _sMask.Length && _sMask[i] != '}'; i++)
                    {
                        if (_sMask[i] == 'y' || _sMask[i] == 'M' || _sMask[i] == 'd' ||
                            _sMask[i] == 'h' || _sMask[i] == 'm' || _sMask[i] == 's' ||
                            _sMask[i] == '#')
                        {
                            if (!bMask)
                            {
                                bMask = true;
                                if (iCount > 0)
                                {
                                    _arrMask.Add(_sMask.Substring(iStart, iCount));
                                    iCount = 0;
                                    iStart = i;
                                } // end if
                            }

                            iCount++;
                        } // end if
                        else if (((_sMask[i] == 't') || (_sMask[i] == 'T')) && (String.Compare(_sMask.Substring(i, 5), "title", true) == 0))
                        {
                            if (iCount > 0)
                            {
                                _arrMask.Add(_sMask.Substring(iStart, iCount));
                            } // end if

                            iCount = 0;
                            i += 4;
                            iStart = i;

                            _arrMask.Add(new string[] { "title", "*" });
                        }
                        else if (((_sMask[i] == 'f') || (_sMask[i] == 'F')) && (String.Compare(_sMask.Substring(i, 8), "filename", true) == 0))
                        {
                            if (iCount > 0)
                            {
                                _arrMask.Add(_sMask.Substring(iStart, iCount));
                            } // end if

                            iCount = 0;
                            i += 7;
                            iStart = i;

                            _arrMask.Add(new string[] { "filename", "*" });
                        }
                        else
                        {
                            if (bMask)
                            {
                                bMask = false;
                                if (iCount > 0)
                                {
                                    _arrMask.Add(new string[] { _sMask.Substring(iStart, iCount), "*" });
                                    iCount = 0;
                                    iStart = i;
                                } // end if
                            } // end if

                            iCount++;
                        } // end else
                    } // end for

                    if (iCount > 0)
                    {
                        _arrMask.Add(new string[] { _sMask.Substring(iStart, iCount), bMask ? "*" : "" });
                        iCount = 0;
                        iStart = -1;
                    } // end if
                } // end if - { }
                else
                {
                    if (iStart == -1)
                    {
                        iStart = i;
                    } // end if

                    iCount++;
                } // end else
            } // end for

            if (iCount > 0)
            {
                _arrMask.Add(_sMask.Substring(iStart, iCount));
            } // end if
        } // end Parse

        #endregion

        #region Format

        /// <summary>
        /// Formats the Suffix based on the sequence provided.
        /// </summary>
        /// <param name="iSequence">Sequence used to replace the mask</param>
        /// <returns>Returns the formatted mask.</returns>
        public string Format(string sMask, string sFileName, string sTitle)
        {
            if ((sMask != null) && (sMask.Length != 0))
            {
                sMask = FormatNumberSequence(sMask);

                sMask = FormatDateTime(sMask);
            } // end if

            return (sMask);
        } // end Format

        /// <summary>
        /// Formats the Suffix based on the sequence provided.
        /// </summary>
        /// <param name="iSequence">Sequence used to replace the mask</param>
        /// <returns>Returns the formatted mask.</returns>
        public string Format(int iSequence)
        {
            return (MaskPrefix + iSequence.ToString().PadLeft(MaxMaskChars, '0') + MaskSuffix);
        } // end Format

        /// <summary>
        /// Formats the Suffix based on the sequence provided.
        /// </summary>
        /// <param name="iSequence">Sequence used to replace the mask</param>
        /// <returns>Returns the formatted mask.</returns>
        public string Format(DateTimeExtended dtDateTime, int iSequence, string sTitle, string sFilename)
        {
            string sFormattedMask = "";

            for (int i = 0; i < _arrMask.GetLength(0); i++)
            {
                if (_arrMask[i, 1] != null && _arrMask[i, 1] == "*")
                {
                    if ((_arrMask[i, 0][0] == 'y') || (_arrMask[i, 0][0] == 'M') || (_arrMask[i, 0][0] == 'd') ||
                        (_arrMask[i, 0][0] == 'h') || (_arrMask[i, 0][0] == 'm') || (_arrMask[i, 0][0] == 's'))
                    {
                        sFormattedMask += dtDateTime.ToString(_arrMask[i, 0]);
                    }
                    else if (_arrMask[i, 0] == "title")
                    {
                        sFormattedMask += sTitle;
                    }
                    else if (_arrMask[i, 0] == "filename")
                    {
                        sFormattedMask += sFilename;
                    }
                    else
                    {
                        sFormattedMask += iSequence.ToString().PadLeft(_arrMask[i, 0].Length, '0');
                    }
                } // end if
                else
                {
                    sFormattedMask += _arrMask[i, 0];
                } // end else
            } // end for

            return (sFormattedMask);
        } // end Format

        #endregion

        #region FormatNumberSequence

        private string FormatNumberSequence(string sMask)
        {
            //         return (MaskPrefix + iSequence.ToString().PadLeft(MaxMaskChars, '0') + MaskSuffix);
            return (FormatNumberSequence(sMask, 1));
        } // end FormatNumberSequence

        private string FormatNumberSequence(string sMask, int iNumber)
        {

            return (sMask);
        } // end FormatNumberSequence

        #endregion

        #region FormatDateTime

        private string FormatDateTime(string sMask)
        {
            return (FormatDateTime(sMask, DateTime.Now));
        } // end FormatDateTime

        private string FormatDateTime(string sMask, DateTime dtDateTime)
        {
            return (sMask);
        } // end FormatDateTime

        #endregion

        #endregion
    } // end Mask Class
} // end GraySystem.IO Namespace
