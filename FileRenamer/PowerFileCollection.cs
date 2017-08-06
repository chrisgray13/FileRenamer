#region Usings

using System;
using System.Collections;

#endregion


namespace FileRenamer
{
   public delegate void FileRenamedEventHandler(object sender, FileRenamedEventArgs e);
   public delegate string RenameFile(PowerFile powerFile, string sOtherText);

   /// <summary>
   /// Summary description for PowerFiles.
   /// </summary>
   public class PowerFileCollection : CollectionBase
   {
      #region Fields

      public event FileRenamedEventHandler FileRenamed;

      #endregion

      #region Properties

      #region NewFileNames

      public string[] NewFileNames
      {
         get
         {
            string[] sNewFileNames = new string[List.Count];
            int i = 0;

            foreach (PowerFile powerFile in List)
            {
               sNewFileNames[i++] = powerFile.NewFileName;
            } // end foreach

            return (sNewFileNames);
         } // end get
      } // end NewFileNames property

      #endregion

      #endregion

      #region Constructors

      public PowerFileCollection(PowerFile[] powerFiles)
      {
         AddRange(powerFiles);
      } // end PowerFileCollection constructor

      public PowerFileCollection(string[] sPowerFileNames)
      {
         AddRange(sPowerFileNames);
      } // end PowerFileCollection constructor

      public PowerFileCollection()
      {
      } // end PowerFileCollection constructor

      #endregion

      #region Methods

      #region Create Collection

      #region Add

      /// <summary>
      /// Adds a new PowerFile to the collection.
      /// </summary>
      /// <param name="sPowerFileName">PowerFileName used to create a PowerFile object
      /// to add to the collection.</param>
      /// <returns>Returns the index to which the element was added.</returns>
      public int Add(string sPowerFileName)
      {
         return (List.Add(new PowerFile(sPowerFileName)));
      } // end Add

      /// <summary>
      /// Adds a new PowerFile to the collection.
      /// </summary>
      /// <param name="powerFile">PowerFile to be added to the collection.</param>
      /// <returns>Returns the index to which the element was added.</returns>
      public int Add(PowerFile powerFile)
      {
         return (List.Add(powerFile));
      } // end Add

      #endregion

      #region AddRange

      /// <summary>
      /// Adds a group of PowerFile objects to the collection based on the group
      /// of PowerFileNames provided.
      /// </summary>
      /// <param name="sPowerFileNames">An array of PowerFileNames used to create an
      /// array of PowerFile objects to add to the collection.</param>
      public void AddRange(string[] sPowerFileNames)
      {
         foreach (string powerFileName in sPowerFileNames)
         {
            Add(powerFileName);
         } // end foreach
      } // end Add

      /// <summary>
      /// Adds a group of PowerFile objects to the collection based on the group
      /// of PowerFile objects provided.
      /// </summary>
      /// <param name="powerFiles">An array of PowerFile objects added to the collection.</param>
      public void AddRange(PowerFile[] powerFiles)
      {
         foreach (PowerFile powerFile in powerFiles)
         {
            Add(powerFile);
         } // end foreach
      } // end Add

      #endregion

      #region Remove

      public void Remove(string sPowerFileName)
      {
         List.Remove(new PowerFile(sPowerFileName));
      } // end Remove

      public void Remove(PowerFile powerFile)
      {
         List.Remove(powerFile);
      } // end Remove

      #endregion

      #endregion

      #region Manipulate Collection

      #region Sorting

      #region Sort

      public bool Sort(string sSortByProperty, string sDirection)
      {
         switch (sSortByProperty)
         {
            case "DatePictureTaken":
               return (SortByDatePictureTaken());
            case "Filename":
               return (SortByFilename());
            case "Title":
               return (SortByTitle());
            default:
               return (false);
         } // end switch
      } // end Sort

      #endregion

      #region SortByTitle

      private bool SortByTitle()
      {
         return (false);
      } // end SortByTitle

      #endregion

      #region SortByDatePictureTaken

      private bool SortByDatePictureTaken()
      {
         return (false);
      } // end SortByDatePictureTaken

      #endregion

      #region SortByFilename

      private bool SortByFilename()
      {
         return (false);
      } // end SortByFilename

      #endregion

      #endregion

      #region Renaming

      #region Rename

      public bool Rename(string sRenameProperty, string sPrefix, string sMask, bool bTestRun)
      {
         RenameFile RenameFileMethod = ChooseRenamingMethod(sRenameProperty);
         Mask mask = new Mask(sMask);
         string sPrevious = "";
         string sNewName;
         int iCounter = 0;

         foreach (PowerFile powerFile in List)
         {
            sNewName = RenameFileMethod(powerFile, sRenameProperty);
            if (sNewName == sPrevious)
            {
               iCounter++;
            } // end if
            else
            {
               sPrevious = sNewName;
               iCounter = 1;
            } // end else

            FileRenamed(this, powerFile.Rename(sNewName, sPrefix, mask.FormatSuffix(iCounter), bTestRun));
         } // end foreach

         return (true);
      } // end Rename

      #endregion

      #region ChooseRenamingMethod

      private RenameFile ChooseRenamingMethod(string sRenamingOption)
      {
         switch (sRenamingOption)
         {
            case "DatePictureTaken":
               return (new RenameFile(GetDatePictureTaken));
            case "Title":
               return (new RenameFile(GetTitle));
            default:
               return (new RenameFile(GetOther));
         } // end switch
      } // end ChooseRenamingMethod

      #endregion

      #region GetDatePictureTaken

      public string GetDatePictureTaken(PowerFile powerFile, string sOtherText)
      {
         return (powerFile.DatePictureTaken);
      } // end GetDatePictureTaken

      #endregion

      #region GetTitle

      public string GetTitle(PowerFile powerFile, string sOtherText)
      {
         return (powerFile.Title);
      } // end GetTitle

      #endregion

      #region GetOther

      public string GetOther(PowerFile powerFile, string sOtherText)
      {
         return (sOtherText);
      } // end GetOther

      #endregion

      #endregion

      #endregion

      #endregion
   }
}
