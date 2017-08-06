#region Usings

using System;

#endregion


namespace FileRenamer
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

      #endregion

      #region Properties

      #region Mask

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
               _sMaskPrefix = _sMask.Substring(iMaskEnd, _sMask.Length - iMaskEnd);
            } // end if

            return (_sMaskPrefix);
         } // end get
      } // end MaskPrefix property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Instantiates a Mask object and sets the mask.
      /// </summary>
      /// <param name="sMask">Suffix Mask</param>
      public Mask(string sMask)
      {
         _sMask = sMask;
         _sMaskPrefix = _sMaskSuffix = "";
         _iMaxMaskChars = 0;
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

      #endregion

      #region FormatSuffix

      /// <summary>
      /// Formats the Suffix based on the sequence provided.
      /// </summary>
      /// <param name="iSequence">Sequence used to replace the mask</param>
      /// <returns>Returns the formatted mask.</returns>
      public string FormatSuffix(int iSequence)
      {
         return (MaskPrefix + iSequence.ToString().PadLeft(MaxMaskChars, '0') + MaskSuffix);
      } // end FormatSuffix

      #endregion

      #endregion
   } // end Mask Class
} // end FileRenamer Namespace