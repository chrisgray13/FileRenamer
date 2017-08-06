using System;


namespace GraySystem.Containers
{
   /// <summary> Summary description for SelfManagedArray. </summary>
   public class SelfManagedArray
   {
      #region Fields

      private string[,] _sValues;

      private int _iNextIndex;
      private int _iMaxRowSize;
      private int _iMaxColumnSize;

      #endregion

      #region Properties

      #region this[]

      /// <summary>
      /// 
      /// </summary>
      public string this[int iIndex]
      {
         get
         {
            return (this[iIndex, 0]);
         } // end get

         set
         {
            this[iIndex, 0] = value;
         } // end set
      } // end this[] property

      public string this[int iRowIndex, int iColumnIndex]
      {
         get
         {
            if ((iRowIndex >= _iNextIndex) || (iColumnIndex >= _iMaxColumnSize))
            {
               throw (new System.IndexOutOfRangeException());
            } // end if
            else
            {
               return (_sValues[iRowIndex, iColumnIndex]);
            } // end else
         } // end get

         set
         {
            if ((iRowIndex >= _iNextIndex) || (iColumnIndex >= _iMaxColumnSize))
            {
               throw (new System.IndexOutOfRangeException());
            } // end if
            else
            {
               _sValues[iRowIndex, iColumnIndex] = value;
            } // end else
         } // end set
      } // end this[] property

      #endregion

      #region Length

      public int Length
      {
         get { return (_iNextIndex * _iMaxColumnSize); }
      } // end Length

      #endregion

      #endregion

      #region Constructors

      public SelfManagedArray() : this(5, 1)
      {
      } // end SelfManagedArray constructor

      public SelfManagedArray(int iSize) : this(iSize, 1)
      {
      } // end SelfManagedArray constructor

      public SelfManagedArray(int iRowSize, int iColumnSize)
      {
         _iMaxRowSize = iRowSize;
         _iMaxColumnSize = iColumnSize;

         _sValues = new string[_iMaxRowSize, _iMaxColumnSize];
         _sValues.Initialize();
      } // end SelfManagedArray constructor

      #endregion

      #region Methods

      #region IncreaseArraySize

      /// <summary>
      /// Increases the size of the array.  It is useful when trying to add item and the array is
      /// full.
      /// </summary>
      private void IncreaseArraySize()
      {
         string[,] sTempArray;

         // Creating and initializing a temporary array as a duplicate copy of
         // the _sValues array
         sTempArray = new string[_iMaxRowSize, _iMaxColumnSize];
         for (int i = 0; i < sTempArray.GetLength(0); i++)
         {
            for (int j = 0; j < _iMaxColumnSize; j++)
            {
               sTempArray[i, j] = _sValues[i, j];
            } // end for
         } // end for

         // Increasing the size of the _sValues array by 10
         _iMaxRowSize += 10;
         _sValues = new string[_iMaxRowSize, _iMaxColumnSize];

         // Copying the contents of the temporary array back to the original array
         for (int i = 0; i < sTempArray.GetLength(0); i++)
         {
            for (int j = 0; j < _iMaxColumnSize; j++)
            {
               _sValues[i, j] = sTempArray[i, j];
            } // end for
         } // end for
      } //end IncreaseArraySize

      #endregion

      #region Find

      /// <summary>
      /// Performs a brute-force search of the array to find the value specified by the caller.
      /// </summary>
      /// <param name="sValue"></param>
      /// <returns></returns>
      public int Find(string sValue)
      {
         int i;

         // Brute-Force Search through the array to find the value
         for (i = 0; i < _iNextIndex; i++)
         {
            if (_sValues[i, 0].Equals(sValue))
            {
               return (i);
            } // end if
         } // end for

         return (-1);  // Value not found
      } // end Find

      #endregion

      #region Add

      /// <summary>
      /// Add an element to the array.  If the array is full, then it is increased to accomodate
      /// the new element.
      /// </summary>
      /// <param name="sValue"></param>
      public void Add(string sValue)
      {
         // Checking to see if the array is full
         if (_iNextIndex == _iMaxRowSize)
         {
            // Increasing the size of the array to make room for new elements
            IncreaseArraySize();
         } // end if

         // Adding the new element
         _sValues[_iNextIndex++, 0] = sValue;
      } // end Add

      /// <summary>
      /// Add an element to the array.  If the array is full, then it is increased to accomodate
      /// the new element.
      /// </summary>
      /// <param name="sValues"></param>
      public void Add(string[] sValues)
      {
         if (sValues.Length > _iMaxColumnSize)
         {
            throw (new System.IndexOutOfRangeException());
         } // end if
         else
         {
            // Checking to see if the array is full
            if (_iNextIndex == _iMaxRowSize)
            {
               // Increasing the size of the array to make room for new elements
               IncreaseArraySize();
            } // end if

            // Adding the new elements
            for (int i = 0; i < _iMaxColumnSize; i++)
            {
               _sValues[_iNextIndex, i] = sValues[i];
            } // end for

            _iNextIndex++;
         } // end else
      } // end Add

      #endregion

      #region Remove

      /// <summary>
      /// Removes an element from the array based on the key provided by the caller.
      /// </summary>
      /// <param name="iRow"></param>
      public void Remove(int iRow)
      {
         if (iRow >= _iMaxRowSize)
         {
            throw (new System.IndexOutOfRangeException());
         } // end if
         else
         {
            // Reinitializing the object array and copying the data back from the
            // temporary array
            for(int i = iRow + 1; i < _iNextIndex; i++)
            {
               for (int j = 0; j < _iMaxColumnSize; j++)
               {
                  _sValues[i - 1, j] = _sValues[i, j];
               } // end for
            } // end for

            // Adjusting the _iNextIndex value for the element that was removed
            _iNextIndex--;
         } // end else
      } // end Remove

      #endregion

      #region GetValues

      /// <summary>
      /// Returns the elements within the array to the caller by excluding any empty indexes.
      /// </summary>
      /// <returns></returns>
      public string[] GetValues()
      {
         return (GetValues(0));
      } // end GetValues

      /// <summary>
      /// Returns the elements within the array to the caller by excluding any empty indexes.
      /// </summary>
      /// <returns></returns>
      public string[] GetValues(int iColumn)
      {
         string[] sTempArray;
         int i;

         if (iColumn >= _iMaxColumnSize)
         {
            throw (new System.IndexOutOfRangeException());
         } // end if
         else
         {
            sTempArray = new string[_iNextIndex];

            for(i = 0; i < _iNextIndex; i++)
            {
               sTempArray[i] = _sValues[i, iColumn];
            } // end for

            return(sTempArray);
         } // end else
      } // end GetValues

      /// <summary>
      /// Returns the elements within the array to the caller by excluding any empty indexes.
      /// </summary>
      /// <returns></returns>
      public string[,] GetAllValues()
      {
         string[,] sTempArray = new string[_iNextIndex, _iMaxColumnSize];
         int i;

         for(i = 0; i < _iNextIndex; i++)
         {
            for (int j = 0; j < _iMaxColumnSize; j++)
            {
               sTempArray[i, j] = _sValues[i, j];
            } // end for
         } // end for

         return(sTempArray);
      } // end GetAllValues

      #endregion

      #region GetLength

      public int GetLength(int iDimension)
      {
         return (_iNextIndex);
      }

      #endregion

      #endregion
   } // end SelfManagedArray Class
} // end GraySystem.Containers Namespace