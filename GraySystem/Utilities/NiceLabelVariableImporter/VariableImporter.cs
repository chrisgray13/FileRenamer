#region PVCS Comments

/*
 *  $Archive:     $
 *  $Revision:     $
 *  $Author:     $
 *  $Date:     $
 * 
 *  $Log:     $
 * 
 * 
 * 
 */

#endregion

#region Usings

using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

//using NiceEngine5WR;
using NiceLabel5WR;

#endregion


namespace ICS.Utilities.NiceLabelVariableImporter
{
   class VariableImporter : ICS.Utilities.Task
   {
      #region Fields

      private string _sDataDropFilePath;
      private string _sLabelFormatPath;

      #endregion

      #region Constructors

      public VariableImporter(string sDataDropFilePath, string sLabelFormatPath)
      {
         _sDataDropFilePath = sDataDropFilePath;
         _sLabelFormatPath = sLabelFormatPath;

         _sResults = new StringBuilder();

         _log = new ICS.Utilities.Logging(LogType.System, "RFS-NiceLabelVariableImporter");
      } // end VariableImporter constructor

      #endregion

      #region Methods

      #region Start

      public override void Start()
      {
         try
         {
            _bResult = ImportVariables();
         } // end try
         catch (Exception ex)
         {
            _log.LogException(ex);
            _bResult = false;
         } // end catch
         finally
         {
            RaiseUpdateProgressEvent(String.Format("Finished Importing the variables{0}...",
                                                   (_bResult) ? "" : " with errors"),
                                     100);
            RaiseTaskCompleteEvent(_bResult);
         } // end finally
      } // end Start

      #endregion

      #region ImportVariables

      private bool ImportVariables()
      {
         DataTable tblDataDrop;
         NiceApp niceLabelApp;
         NiceLabel5WR.NiceLabel niceLabel;
         WRVar variable;
         bool bReturn = true;
         int iProgressStep;

         if (System.IO.File.Exists(_sDataDropFilePath))
         {
            if (System.IO.File.Exists(_sLabelFormatPath))
            {
               tblDataDrop = LoadDropData();
               niceLabelApp = new NiceApp();
               niceLabel = niceLabelApp.LabelOpenEx(_sLabelFormatPath);
               iProgressStep = 100 / (tblDataDrop.Columns.Count + 2);
               for (int i = 0; i < tblDataDrop.Columns.Count; i++)
               {
                  RaiseUpdateProgressEvent("Attempting to add variable " +
                                              tblDataDrop.Columns[i].ColumnName + "...",
                                           iProgressStep * (i + 1));
                  if (niceLabel.Variables.FindByName(tblDataDrop.Columns[i].ColumnName) == null)
                  {
                     variable = niceLabel.Variables.Create(tblDataDrop.Columns[i].ColumnName);
                  } // end if
               } // end for

               RaiseUpdateProgressEvent("Saving the label format...", tblDataDrop.Columns.Count + 1);
               bReturn = niceLabel.Save();
               niceLabel.Free();

               niceLabelApp.Quit();
               niceLabelApp.Free();

               return (bReturn);
            } // end if
            else
            {
               throw (new System.IO.FileNotFoundException(null, _sLabelFormatPath));
            } // end else
         } // end if
         else
         {
            throw (new System.IO.FileNotFoundException(null, _sDataDropFilePath));
         } // end else
      } // end ImportVariables

      #endregion

      #region LoadDropData

      private DataTable LoadDropData()
      {
         System.IO.StreamReader streamReader;
         DataTable tblDropData;

         RaiseUpdateProgressEvent("Loading the drop data file...", 0);

         streamReader = new StreamReader(_sDataDropFilePath);
         tblDropData = ICS.LabelPrinting.PrintData.ToDataTable(streamReader.ReadToEnd());

         streamReader.Close();

         return (tblDropData);
      } // end LoadDropData

      #endregion

      #endregion
   } // end VariableImporter Class
} // end ICS.Utilities.NiceLabelVariableImporter Namespace