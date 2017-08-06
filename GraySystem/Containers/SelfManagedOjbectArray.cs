using System;


namespace Data_Structures
{
   /// <summary> Summary description for SelfManagedArray. </summary>
   public class SelfManagedArray
   {
   //
   // Class Fields
   //
      #region Class Fields
      private object[] clsObjects;

      private int iNextIndex;
      private int iMaxSize;
      #endregion


      public SelfManagedArray(int iSize)
      {
         iMaxSize = iSize;

         clsObjects = new object[iMaxSize];
         clsObjects.Initialize();

         iNextIndex = 0;
      } // end SelfManagedArray constructor


   //
   // Private Class Methods
   //
      #region Private Class Methods

      /*******************************************************************
       * Name:  IncreaseArraySize
       *
       * Description:  This is used to increase the size of the array.
       * It is useful when trying to add item and the array is full.
       *******************************************************************/

      private void IncreaseArraySize()
      {
         object[] clsTempArray;

         // Creating and initializing a temporary array as a duplicate copy of
         // the clsObjects array
         clsTempArray = new object[iMaxSize];
         clsObjects.CopyTo(clsTempArray, 0);

         // Increasing the size of the clsObjects array by 10
         iMaxSize += 10;
         clsObjects = new object[iMaxSize];

         // Copying the contents of the temporary array back to the original array
         clsTempArray.CopyTo(clsObjects, 0);
      } //end IncreaseArraySize
      #endregion

   //
   // Public Class Methods
   //
      #region Public Class Methods

      /*****************************************************************
       * Name:  Find
       *
       * Description:  This is used to perform a brute-force search of
       * the array to find the value specified by the caller.
       *****************************************************************/

      public int Find(object clsValue)
      {
         int i;

         // Brute-Force Search through the array to find the value
         for(i = 0; i < iNextIndex; i++)
            if (clsObjects[i].Equals(clsValue))
               return(i);

         // Value not found
         return(-1);

      } // end Find


      /*************************************************************************
       * Name:  Add
       *
       * Description:  This is used to add an element to the array.  If the
       * array is full, then it is increased to accomodate the new element.
       *************************************************************************/

      public void Add(object clsValue)
      {
         // Checking to see if the array is full
         if (iNextIndex == iMaxSize)
         {
            // Increasing the size of the array to make room for new elements
            this.IncreaseArraySize();
         } // end if

         // Adding the new element
         clsObjects[iNextIndex++] = clsValue;
      } // end Add


      /*************************************************************************
       * Name:  Remove
       *
       * Description:  This is used to remove an element from the array based
       * on the key provided by the caller.
       *************************************************************************/

      public void Remove(int iKey)
      {
         object[] clsTempArray = new object[iMaxSize];
         int i;

         // Copying the first part of the array to a temporary array
         for(i = 0; i < iKey; i++)
            clsTempArray[i] = clsObjects[i];

         // Copying the remainder of the array minus the value to be removed
         // to a temporary array
         for(i++; i < iNextIndex; i++)
            clsTempArray[i - 1] = clsObjects[i];

         // Reinitializing the object array and copying the data back from the
         // temporary array
         clsObjects.Initialize();
         clsTempArray.CopyTo(clsObjects, 0);

         // Adjusting the iNextIndex value for the element that was removed
         iNextIndex--;
      } // end Remove


      /***********************************************************************
       * Name:  GetObjects
       *
       * Description:  This is used to return the elements within the array
       * to the caller by excluding any empty indexes.
       ***********************************************************************/

      public object[] GetObjects()
      {
         object[] clsTempArray = new object[iNextIndex];
         int i;

         for(i = 0; i < iNextIndex; i++)
            clsTempArray[i] = clsObjects[i];

         return(clsTempArray);

      } // end GetObjects
      #endregion

   } // end SelfManagedArray Class
} // end Data_Structures Namespace
