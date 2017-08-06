#region Usings

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;

#endregion


namespace GraySystem.Data
{
   class Query
   {
      #region Methods

      public static int Select(Connection connection, string sQuery)
      {
         //System.Data.OleDb.OleDbCommand oleCommand = connection.C
         //System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sQuery, connection);
         //System.Data.SqlClient.SqlDataReader sqlDataReader;

         //System.Data.SqlClient.SqlCommand sqlCommand = new SqlCommand(sQuery, connection);
         //sqlCommand.ExecuteReader();

         return (-1);
      } // end Select

      public static int Insert(Connection connection, string sQuery)
      {
         // If transaction, do not close connection or whatever we plan on doing

         return (-1);
      } // end Insert

      public static int Update(Connection connection, string sQuery)
      {
         // If transaction, do not close connection or whatever we plan on doing

         return (-1);
      } // end Update

      public static int Delete(Connection connection, string sQuery)
      {
         // If transaction, do not close connection or whatever we plan on doing

         
         return (-1);
      } // end Delete

      #endregion
   } // end Query Class
} // end GraySystem.Data Namespace