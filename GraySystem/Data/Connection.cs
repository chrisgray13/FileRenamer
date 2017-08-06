#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;

#endregion


namespace GraySystem.Data
{
   public class Connection : IDisposable
   {
      #region Fields

      OleDbConnection _connection;
      ArrayList _transactions;
      OleDbCommand _command;
      OleDbDataAdapter _adapter;

      #endregion

      #region Properties

      #region IsConnectionValid

      public bool IsConnectionValid
      {
         get
         {
            if (IsOpen)
            {
               return (true);
            } // end if
            else
            {
               Open();

               if (IsOpen)
               {
                  Close();

                  return (true);
               } // end if
               else
               {
                  return (false);
               } // end else
            } // end else
         } // end get
      } // end IsConnectionValid property

      #endregion

      #region IsOpen

      public bool IsOpen
      {
         get { return (_connection.State == ConnectionState.Open); }
      } // end IsOpen property

      #endregion

      #region TransactionLevels

      public int TransactionLevels
      {
         get { return ((_transactions == null) ? 0 : _transactions.Count); }
      } // end TransactionLevels property

      #endregion

      #endregion

      #region Constructors

//      //public Connection(string sServer, string sDatabase, string sUserId, string sPassword)
//      //{
//      //   SqlConnectionStringBuilder sqlConnectionStrBldr = new SqlConnectionStringBuilder();

//      //   sqlConnectionStrBldr.DataSource = sServer;
//      //   sqlConnectionStrBldr.InitialCatalog = sDatabase;
//      //   sqlConnectionStrBldr.UserID = sUserId;
//      //   sqlConnectionStrBldr.Password = sPassword;

//      //   _connection = new SqlConnection(sqlConnectionStrBldr.ConnectionString);
//      //   _connection.Open();
//      //} // end Connection constructor

//      public Connection(string sDataSourceName, string sUserId, string sPassword)
//      {
         
//      } // end Connection constructor

//      //public Connection(string sDataSourceName)
//      //{
//      //   //OdbcConnectionStringBuilder odbcConnectionStrBldr = new OdbcConnectionStringBuilder();

//      //   //odbcConnectionStrBldr.Dsn = sDataSourceName;

//      //   //_connection = new OdbcConnection(odbcConnectionStrBldr.ConnectionString);
//      //   //_connection.Open();
//      //} // end Connection constructor

      public Connection(string sConnectionString)
      {
         _connection = new OleDbConnection(sConnectionString);

         //         _connection = new OdbcConnection(sConnectionString);

         _connection.Open();
      } // end Connection constructor

      #endregion

      #region Dispose

      public void Dispose()
      {
         if (_command != null)
         {
            _command.Dispose();
            _command = null;
         }

         RollbackAllTransactions();
         _transactions = null;

         if (_connection != null)
         {
            if (_connection.State != ConnectionState.Closed)
            {
               _connection.Close();
            }

            _connection.Dispose();
            _connection = null;
         }
      } // end Dispose

      #endregion

      #region Methods

      #region Connection Management

      #region Open

      public void Open()
      {
         if (!IsOpen)
         {
            _connection.Open();
         } // end if
      } // end Open

      #endregion

      #region Close

      public void Close()
      {
         if (IsOpen)
         {
            _connection.Close();
         } // end if
      } // end Close

      #endregion

      #endregion

      #region Sql Query Processing

      #region ExecuteQuery

      public DataTable ExecuteQuery(string sSql)
      {
         try
         {
            DataTable dataTable = new DataTable();

            using (_adapter = new System.Data.OleDb.OleDbDataAdapter(sSql, _connection))
            {
               if (TransactionLevels > 0)
               {
                  _adapter.SelectCommand.Transaction = (OleDbTransaction)_transactions[_transactions.Count - 1];
               } // end if

               _adapter.Fill(dataTable);

               return (dataTable);
            }
         }
         catch
         {
            RollbackTransaction();

            throw;
         }
      } // end ExecuteQuery

      #endregion

      #region ExecuteNonQuery

      public int ExecuteNonQuery(string sSql)
      {
         try
         {
            using (_command = _connection.CreateCommand())
            {
               _command.CommandText = sSql;
               if (TransactionLevels > 0)
               {
                  _command.Transaction = (OleDbTransaction)_transactions[_transactions.Count - 1];
               } // end if

               return (_command.ExecuteNonQuery());
            }
         }
         catch
         {
            RollbackTransaction();

            throw;
         }
      } // end ExecuteNonQuery

      #endregion

      #region ExecuteReader

      public OleDbDataReader ExecuteReader(string sSql)
      {
         try
         {
            using (_command = _connection.CreateCommand())
            {
               _command.CommandText = sSql;
               if (TransactionLevels > 0)
               {
                  _command.Transaction = (OleDbTransaction)_transactions[_transactions.Count - 1];
               } // end if

               return (_command.ExecuteReader());
            }
         }
         catch
         {
            RollbackTransaction();

            throw;
         }
      } // end ExecuteReader

      public OleDbDataReader ExecuteReader(string sSql, CommandBehavior behavior)
      {
         try
         {
            using (_command = _connection.CreateCommand())
            {
               _command.CommandText = sSql;
               if (TransactionLevels > 0)
               {
                  _command.Transaction = (OleDbTransaction)_transactions[_transactions.Count - 1];
               } // end if

               return (_command.ExecuteReader(behavior));
            }
         }
         catch
         {
            RollbackTransaction();

            throw;
         }
      } // end ExecuteReader

      #endregion

      #region ExecuteScalar

      public object ExecuteScalar(string sSql)
      {
         try
         {
            using (_command = _connection.CreateCommand())
            {
               _command.CommandText = sSql;
               if (TransactionLevels > 0)
               {
                  _command.Transaction = (OleDbTransaction)_transactions[_transactions.Count - 1];
               } // end if

               return (_command.ExecuteScalar());
            }
         }
         catch
         {
            RollbackTransaction();

            throw;
         }
      } // end ExecuteScalar

      #endregion

      #endregion

      #region Transaction Processing

      #region BeginTransaction

      public void BeginTransaction()
      {
         if (_transactions == null)
         {
            _transactions = new ArrayList();
            _transactions.Add(_connection.BeginTransaction());
         } // end if
         else if (_transactions.Count == 0)
         {
            _transactions.Add(_connection.BeginTransaction());
         } // end else if
         else
         {
            _transactions.Add(_transactions[_transactions.Count - 1]);
         } // end else
      } // end BeginTransaction

      public void BeginTransaction(IsolationLevel isolationLevel)
      {
         if (_transactions == null)
         {
            _transactions = new ArrayList();
            _transactions.Add(_connection.BeginTransaction(isolationLevel));
         } // end if
         else if (_transactions.Count == 0)
         {
            _transactions.Add(_connection.BeginTransaction(isolationLevel));
         } // end else if
         else
         {
            _transactions.Add(_transactions[_transactions.Count - 1]);
         } // end else
      } // end BeginTransaction

      #endregion

      #region CommitTransaction

      public void CommitTransaction()
      {
         if (_transactions != null && _transactions.Count != 0)
         {
            if (_transactions.Count == 1)
            {
               ((OleDbTransaction) _transactions[_transactions.Count - 1]).Commit();
            } // end if

            _transactions.RemoveAt(_transactions.Count - 1);
         } // end if
      } // end CommitTransaction

      #endregion

      #region CommitAllTransactions

      public void CommitAllTransactions()
      {
         if (_transactions != null && _transactions.Count != 0)
         {
            ((OleDbTransaction) _transactions[0]).Commit();
            _transactions.Clear();
         } // end if
      } // end CommitAllTransactions

      #endregion

      #region RollbackTransaction

      public void RollbackTransaction()
      {
         if (_transactions != null && _transactions.Count != 0)
         {
            if (_transactions.Count == 1)
            {
               ((OleDbTransaction) _transactions[_transactions.Count - 1]).Rollback();
            } // end if

            _transactions.RemoveAt(_transactions.Count - 1);
         } // end if
      } // end RollbackTransaction

      #endregion

      #region RollbackAllTransactions

      public void RollbackAllTransactions()
      {
         if (_transactions != null && _transactions.Count != 0)
         {
            ((OleDbTransaction) _transactions[0]).Rollback();
            _transactions.Clear();
         } // end if
      } // end RollbackAllTransactions

      #endregion

      #endregion

      #endregion
   }
} // end GraySystem.Data Namespace