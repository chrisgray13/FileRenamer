#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\ExportFilePathPanel.cs-arc  $
 * $Revision:   1.0  $
 * $Author:   cgray  $
 * $Date:   Jul 21 2006 17:37:40  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\ExportFilePathPanel.cs-arc  $
 * 
 *
 *
 */

#endregion

#region Usings

using System;
using System.Data;
using System.IO;
using System.Text;

using ICS.Database;
using ICS.Utilities;

#endregion


namespace ICS.Utilities.DataExporter
{
   /// <summary>
   /// Summary description for DataTableExporter.
   /// </summary>
   public class DataTableExporter : IDisposable
   {
      #region Fields

      private DAL _dataAccessLayer;

      private string _sExportPath;

      private string _sDBDataType;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a DataTableExporter object, which initializes the DataAccessLayer, Export Path, and
      /// DB Data Type.
      /// </summary>
      /// <param name="dataAccessLayer">Data Access Layer for the RFSmart Version 3 Database</param>
      /// <param name="sExportPath">Path used to deposit the exported data.</param>
      /// <param name="sDBDataType">Database data type (System or Business)</param>
      public DataTableExporter(DAL dataAccessLayer, string sExportPath, string sDBDataType)
      {
         _dataAccessLayer = dataAccessLayer;
         _sExportPath = sExportPath;
         _sDBDataType = sDBDataType;
      } // end DataTableExporter constructor

      #endregion

      #region Dispose

      /// <summary>
      /// 
      /// </summary>
      public void Dispose()
      {
         if (_dataAccessLayer != null)
         {
            _dataAccessLayer.Dispose();
         } // end if
      } // end Dispose

      #endregion

      #region Methods

      #region Export using DataSet

      #region ExportDataTableUsingDataSet

      /// <summary>
      /// Exports data from a table for the columns specified.  The environment is included as part of the
      /// criteria for the query if the EnvironmentId exists in the table, which will most likely be a System
      /// table.
      /// </summary>
      /// <param name="sTableName">Name of the table to be exported</param>
      /// <param name="sColumnsToSelect">Columns needing to be exported</param>
      /// <param name="sEnvironment">Environment for which the data should be exported</param>
      /// <returns>Returns true if the data is successfully exported; otherwise, false.</returns>
      public bool ExportDataTableUsingDataSet(string sTableName, string sColumnsToSelect, string sEnvironment)
      {
         DataSet ds = null;
         DataTable dt = null;
         bool bRetVal = false;

         try
         {
            ds = new DataSet();

            dt = FetchDataTable(sTableName, sColumnsToSelect, sEnvironment);

            ds.Tables.Add(dt);
            ds.WriteXml(String.Format("{0}{1}.xml", _sExportPath, sTableName), XmlWriteMode.WriteSchema);

            bRetVal = (dt.Columns.Count > 0);
         } // end try
         catch (Exception ex)
         {
            _dataAccessLayer.System.Log.LogException(ex);
         } // end catch
         finally
         {
            if (ds != null)
            {
               ds.Dispose();
            } // end if

            if (dt != null)
            {
               dt.Dispose();
            } // end if
         } // end finally

         return (bRetVal);
      } // end ExportDataTableUsingDataSet

      #endregion

      #region FetchDataTable

      /// <summary>
      /// Fetches the data based on the database data type (System or Business).  The If it is a system
      /// database, an attempt to filter by EnvironmentId is made.  If the table does not contain
      /// the EnvironmentId, or it is a Business table, the data is fetched
      /// </summary>
      /// <param name="sTableName"></param>
      /// <param name="sColumnsToSelect"></param>
      /// <param name="sEnvironment"></param>
      /// <returns></returns>
      private DataTable FetchDataTable(string sTableName, string sColumnsToSelect, string sEnvironment)
      {
         DataTable dt = new DataTable(sTableName);

         try
         {
            if (_sDBDataType == "System")
            {
               if ((sColumnsToSelect == "*") || (sColumnsToSelect.IndexOf("Environment") > -1))
               {
                  _dataAccessLayer.System.FetchSchemaAndData(String.Format("SELECT {0} FROM {1} WHERE EnvironmentID = '{2}'",
                                                                           sColumnsToSelect, sTableName, sEnvironment),
                                                             ref dt);
               } // end if

               if (dt.Columns.Count == 0)
               {
                  _dataAccessLayer.System.FetchSchemaAndData(String.Format("SELECT {0} FROM {1}",
                                                                           sColumnsToSelect, sTableName),
                                                             ref dt);
               } // end if
            } // end if
            else
            {
               _dataAccessLayer.Data.FetchSchemaAndData(String.Format("SELECT {0} FROM {1}",
                                                                      sColumnsToSelect,
                                                                      GetQualifiedTable(sTableName, sEnvironment)),
                                                        ref dt);
            } // end else
         } // end try
         catch (Exception ex)
         {
            _dataAccessLayer.System.Log.LogException(ex);
         } // end catch

         return (dt);
      } // end FetchDataTable

      #endregion

      #endregion

      #region Export using DataReader

      #region ExportDataTableUsingDataReader

      /// <summary>
      /// Exports data from a table using a DataReader.
      /// </summary>
      /// <param name="sTableName"></param>
      /// <param name="sColumnsToSelect"></param>
      /// <param name="sEnvironment"></param>
      /// <returns></returns>
      public bool ExportDataTableUsingDataReader(string sTableName, string sColumnsToSelect, string sEnvironment)
      {
         try
         {
            if (_sDBDataType == "System")
            {
               if ((sColumnsToSelect == "*") || (sColumnsToSelect.IndexOf("Environment") > -1))
               {
                  FetchSystemDataForEnvironment(sTableName, sColumnsToSelect, sEnvironment);
               }
               else
               {
                  FetchSystemData(sTableName, sColumnsToSelect);
               } // end else

               return (WriteDataToFile(_dataAccessLayer.System, sTableName));
            } // end if
            else
            {
               FetchBusinessData(sTableName, sColumnsToSelect, sEnvironment);
               return (WriteDataToFile(_dataAccessLayer.Data, sTableName));
            } // end else
         } // end try
         catch (Exception ex)
         {
            _dataAccessLayer.System.Log.LogException(ex);

            return (false);
         } // end catch
      } // end ExportDataTableUsingDataReader

      #endregion

      #region FetchSystemDataForEnvironment

      private void FetchSystemDataForEnvironment(string sTableName, string sColumnsToSelect, string sEnvironment)
      {
         try
         {
            _dataAccessLayer.System.Log.WriteLine(2, String.Format("Fetch Statement: SELECT {0} FROM {1} WHERE EnvironmentID = '{2}'",
                                                                   sColumnsToSelect, sTableName, sEnvironment));
            _dataAccessLayer.System.Query = String.Format("SELECT {0} FROM {1} WHERE EnvironmentID = '{2}'",
                                                          sColumnsToSelect, sTableName, sEnvironment);
            if (_dataAccessLayer.System.DataReader == null)
            {
               FetchSystemData(sTableName, sColumnsToSelect);
            } // end if
         } // end try
         catch
         {
            FetchSystemData(sTableName, sColumnsToSelect);
         } // end catch
      } // end FetchSystemDataForEnvironment

      #endregion

      #region FetchSystemData

      private void FetchSystemData(string sTableName, string sColumnsToSelect)
      {
         _dataAccessLayer.System.Log.WriteLine(2, String.Format("Fetch Statement: SELECT {0} FROM {1}",
                                                                sColumnsToSelect, sTableName));
         _dataAccessLayer.System.Query = String.Format("SELECT {0} FROM {1}", sColumnsToSelect, sTableName);
      } // end FetchSystemData

      #endregion

      #region FetchBusinessData

      private void FetchBusinessData(string sTableName, string sColumnsToSelect, string sEnvironment)
      {
         _dataAccessLayer.System.Log.WriteLine(2, String.Format("Fetch Statement: SELECT {0} FROM {1}",
                                                                sColumnsToSelect,
                                                                GetQualifiedTable(sTableName, sEnvironment)));
         _dataAccessLayer.Data.Query = String.Format("SELECT {0} FROM {1}",
                                                     sColumnsToSelect,
                                                     GetQualifiedTable(sTableName, sEnvironment));
      } // end FetchBusinessData

      #endregion

      #region WriteDataToFile

      private bool WriteDataToFile(DbConnectBase dbConnection, string sTableName)
      {
         StreamWriter streamWriter = new StreamWriter(String.Format("{0}{1}.txt", _sExportPath, sTableName),
                                                      false, Encoding.Default);
         object[] oValues = new object[dbConnection.DataReader.FieldCount];

         while (dbConnection.DataReader.Read())
         {
            dbConnection.DataReader.GetValues(oValues);
            streamWriter.Write(String.Format("{0}|", GetFormattedDataRecord(oValues)));
         } // end while

         dbConnection.DataReader.Close();
         streamWriter.Close();

         return (true);
      } // end WriteDataToFile

      #endregion

      #region GetFormattedDataRecord

      private string GetFormattedDataRecord(object[] oValues)
      {
         StringBuilder strDataRecord = new StringBuilder();

         foreach (object o in oValues)
         {
            if (strDataRecord.Length > 0)
            {
               strDataRecord.Append("~~~");
            } // end if

            strDataRecord.AppendFormat("{0}", o.ToString());
         } // end foreach

         return (strDataRecord.ToString());
      } // end GetFormattedDataRecord

      #endregion

      #endregion

      #region GetQualifiedTable

      /// <summary>
      /// Gets the qualified table, which is the table owner, owner separator, and table name
      /// concatenated together.  The table owner is retrieved from the RFS_TableOwnerOverride
      /// table.  If a record does not exist, the owner is retrieved from the Environment
      /// configuration.
      /// </summary>
      /// <param name="sTableName">Table Name for which the owner is needed.</param>
      /// <param name="sEnvironment">Environment for which the owner should be retrieved.</param>
      /// <returns>Returns the table owner, owner separator, and table name concatenated together.</returns>
      private string GetQualifiedTable(string sTableName, string sEnvironment)
      {
         string sQuery = String.Format("SELECT OwnerName FROM RFS_TableOwnerOverride WHERE TableName = '{0}' AND EnvironmentID = '{1}'",
                                       sTableName, sEnvironment);
         string sTableOwner;

         try
         {
            // Check for override in dataset
            _dataAccessLayer.System.Log.WriteLine(2, String.Format("Fetch Statement: {0}", sQuery));
            _dataAccessLayer.System.Query = sQuery;

            if (_dataAccessLayer.System.DataReader.Read())
            {
               _dataAccessLayer.System.Log.WriteLine(2, "Dataset Loaded.  Row Count: 1");
               sTableOwner = _dataAccessLayer.System.DataReader.GetString(0);
            } // end if
            else
            {
               _dataAccessLayer.System.Log.WriteLine(2, "Dataset Loaded.  Row Count: 0");
               sTableOwner = _dataAccessLayer.Data.TableOwner;
            } // end else
         } // end try
         catch (Exception ex)
         {
            _dataAccessLayer.System.Log.LogException(ex);

            _dataAccessLayer.System.Log.WriteLine(2, "Dataset Loaded.  Row Count: 0");
            sTableOwner = "";
         } // end catch
         finally
         {
            _dataAccessLayer.System.DataReader.Close();
         } // end finally

         return (sTableOwner + _dataAccessLayer.Data.OwnerSeparator + sTableName);
      } // end GetQualifiedTable

      #endregion

      #endregion
   } // end DataTableExport Class
} // end ICS.Utilities.DataExporter Namespace