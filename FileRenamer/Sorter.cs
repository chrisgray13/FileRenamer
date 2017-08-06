using System;

namespace FileRenamer
{
   /// <summary>
   /// Summary description for Sorter.
   /// </summary>
   public class Sorter
   {
      public static bool Sort(string[] sFileNames, string sOrder)
      {
         switch (sOrder)
         {
            case "DatePictureTaken":
               return (SortByDatePictureTaken(sFileNames));
            case "Filename":
               return (SortByFilename(sFileNames));
            case "Title":
               return (SortByTitle(sFileNames));
            default:
               return (false);
         } // end switch
      } // end Sort

      private static bool SortByTitle(string[] sFileNames)
      {
         return (false);
      } // end SortByTitle

      private static bool SortByDatePictureTaken(string[] sFileNames)
      {
         return (false);
      } // end SortByDatePictureTaken

      private static bool SortByFilename(string[] sFileNames)
      {
         return (false);
      } // end SortByFilename
   }
}
