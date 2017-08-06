#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\DBConnectionCtrl.cs-arc  $
 * $Revision:   1.3  $
 * $Author:   pmonaco  $
 * $Date:   Nov 08 2007 14:35:56  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\DBConnectionCtrl.cs-arc  $
 * 
 *    Rev 1.3   Nov 08 2007 14:35:56   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.2   Feb 20 2007 15:28:38   pmonaco
 * BD019026 - Update to remove Sql-DMO and replace with SMO
 * 
 *    Rev 1.1   Jul 24 2006 23:55:38   cgray
 * BE016512 - Changed comparisions using the empty string to check that the length is zero for enhanced performance.
 * 
 *    Rev 1.0   Jul 21 2006 16:49:36   cgray
 * Initial revision.
 *
 *
 */

#endregion

#region Usings

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using System.Resources;
using SMO = Microsoft.SqlServer.Management.Smo;

using GraySystem.Data;

#endregion


namespace GraySystem.UI.Controls
{
   /// <summary>
   /// DBConnectionCtrl Class is a control used to allow a user to specify a database connection
   /// by collecting a Server, Database, and login credentials.
   /// </summary>
   public class DBConnectionCtrl : System.Windows.Forms.UserControl
   {
      #region Fields

      #region Form Controls

      /// <summary>
      /// Panel for hold the controls
      /// </summary>
      protected System.Windows.Forms.Panel _pnlDB;

      /// <summary>
      /// Label to identify the Database Server combox
      /// </summary>
      protected System.Windows.Forms.Label _lblDBServer;

      /// <summary>
      /// Combobox to store available SQL Servers or allow the user
      /// to specify one
      /// </summary>
      protected System.Windows.Forms.ComboBox _cmbDBServer;

      /// <summary>
      /// Checkbox to allow the user to specify whether or not they would like to use
      /// a trusted connection or not to log into the database
      /// </summary>
      protected System.Windows.Forms.CheckBox _chkDBTrustConn;

      /// <summary>
      /// Label to identify the Database UserID textbox
      /// </summary>
      protected System.Windows.Forms.Label _lblDBUser;

      /// <summary>
      /// Textbox used to allow the user to specify the UserID with which to login into
      /// the Database
      /// </summary>
      protected System.Windows.Forms.TextBox _txtDBUser;

      /// <summary>
      /// Label to identify the Database UserID's Password textbox
      /// </summary>
      protected System.Windows.Forms.Label _lblDBPassword;

      /// <summary>
      /// Textbox used to allow the user to specify the Password of the UserID in order
      /// to log into the Database
      /// </summary>
      protected System.Windows.Forms.TextBox _txtDBPassword;

      /// <summary>
      /// Label to identify the Database Combobox
      /// </summary>
      protected System.Windows.Forms.Label _lblDBDB;

      /// <summary>
      /// Combobox used store available Databases based on the Server
      /// </summary>
      protected System.Windows.Forms.ComboBox _cmbDBDB;

      /// <summary>
      /// Button used to allow the user to check their Database connection
      /// </summary>
      protected System.Windows.Forms.Button _btnDBTestConnect;

      #endregion

      /// <summary>
      /// Container of the ComponentModel
      /// </summary>
      protected System.ComponentModel.Container components = null;

      /// <summary>
      /// Connection used to store a valid Database connection
      /// </summary>
      protected Connection _conDataAccessLayer;

      /// <summary>
      /// Name used to identify the data access layer for logging purposes
      /// </summary>
      protected string _sName;

      #endregion

      #region Properties

      #region Connection

      /// <summary>
      /// Gets the Data Access Layer.
      /// </summary>
      public Connection DataAccessLayer
      {
         get { return (_conDataAccessLayer); }
      } // end DataAccessLayer property

      #endregion

      #region Server

      /// <summary>
      /// Gets the selected (typed) server from the DB Server combobox.
      /// </summary>
      public string Server
      {
         get { return (_cmbDBServer.Text); }
      } // end Server property

      #endregion

      #region Database

      /// <summary>
      /// Gets the selected (typed) database from the Database combobox.
      /// </summary>
      public string Database
      {
         get { return (_cmbDBDB.Text); }
      } // end Database property

      #endregion

      #region UserID

      /// <summary>
      /// Gets the UserID
      /// </summary>
      public string UserID
      {
         get { return ((_chkDBTrustConn.Checked) ? String.Empty : _txtDBUser.Text); }
      } // end UserID property

      #endregion

      #region Password

      /// <summary>
      /// Gets the
      /// </summary>
      public string Password
      {
         get { return ((_chkDBTrustConn.Checked) ? String.Empty : _txtDBPassword.Text); }
      } // end Password property

      #endregion

      #region UsingTrustedConnection

      /// <summary>
      /// Gets the whether or not the user has decided to use a trusted connection or not.
      /// </summary>
      public bool UsingTrustedConnection
      {
         get { return (_chkDBTrustConn.Checked); }
      } // end UsingTrustedConnection property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Constructor used to create and initialize a new DBConnectionCtrl object.
      /// </summary>
      public DBConnectionCtrl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         _sName = Name;
      } // end DBConnectionCtrl

      /// <summary>
      /// Constructor used to create and initialize a new DBConnectionCtrl object.
      /// </summary>
      public DBConnectionCtrl(string sConnectionName)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         _sName = sConnectionName;
      } // end DBConnectionCtrl

      #endregion

      #region Dispose

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">Determines whether or not to release both managed and unmanaged
      /// resources or only unmanaged resources.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (components != null)
            {
               components.Dispose();
            } // end if
         } // end if

         if (_conDataAccessLayer != null)
         {
            _conDataAccessLayer.Dispose();
         } // end if

         base.Dispose(disposing);
      } // end Dispose

      #endregion

      #region Methods

      #region InitializeComponent

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBConnectionCtrl));
         this._pnlDB = new System.Windows.Forms.Panel();
         this._cmbDBDB = new System.Windows.Forms.ComboBox();
         this._lblDBDB = new System.Windows.Forms.Label();
         this._lblDBPassword = new System.Windows.Forms.Label();
         this._txtDBPassword = new System.Windows.Forms.TextBox();
         this._txtDBUser = new System.Windows.Forms.TextBox();
         this._lblDBUser = new System.Windows.Forms.Label();
         this._chkDBTrustConn = new System.Windows.Forms.CheckBox();
         this._cmbDBServer = new System.Windows.Forms.ComboBox();
         this._lblDBServer = new System.Windows.Forms.Label();
         this._btnDBTestConnect = new System.Windows.Forms.Button();
         this._pnlDB.SuspendLayout();
         this.SuspendLayout();
         // 
         // _pnlDB
         // 
         this._pnlDB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this._pnlDB.Controls.Add(this._cmbDBDB);
         this._pnlDB.Controls.Add(this._lblDBDB);
         this._pnlDB.Controls.Add(this._lblDBPassword);
         this._pnlDB.Controls.Add(this._txtDBPassword);
         this._pnlDB.Controls.Add(this._txtDBUser);
         this._pnlDB.Controls.Add(this._lblDBUser);
         this._pnlDB.Controls.Add(this._chkDBTrustConn);
         this._pnlDB.Controls.Add(this._cmbDBServer);
         this._pnlDB.Controls.Add(this._lblDBServer);
         this._pnlDB.Controls.Add(this._btnDBTestConnect);
         resources.ApplyResources(this._pnlDB, "_pnlDB");
         this._pnlDB.Name = "_pnlDB";
         // 
         // _cmbDBDB
         // 
         resources.ApplyResources(this._cmbDBDB, "_cmbDBDB");
         this._cmbDBDB.Name = "_cmbDBDB";
         this._cmbDBDB.DropDown += new System.EventHandler(this.LoadDatabases);
         // 
         // _lblDBDB
         // 
         this._lblDBDB.FlatStyle = System.Windows.Forms.FlatStyle.System;
         resources.ApplyResources(this._lblDBDB, "_lblDBDB");
         this._lblDBDB.Name = "_lblDBDB";
         // 
         // _lblDBPassword
         // 
         this._lblDBPassword.FlatStyle = System.Windows.Forms.FlatStyle.System;
         resources.ApplyResources(this._lblDBPassword, "_lblDBPassword");
         this._lblDBPassword.Name = "_lblDBPassword";
         // 
         // _txtDBPassword
         // 
         resources.ApplyResources(this._txtDBPassword, "_txtDBPassword");
         this._txtDBPassword.Name = "_txtDBPassword";
         // 
         // _txtDBUser
         // 
         resources.ApplyResources(this._txtDBUser, "_txtDBUser");
         this._txtDBUser.Name = "_txtDBUser";
         // 
         // _lblDBUser
         // 
         this._lblDBUser.FlatStyle = System.Windows.Forms.FlatStyle.System;
         resources.ApplyResources(this._lblDBUser, "_lblDBUser");
         this._lblDBUser.Name = "_lblDBUser";
         // 
         // _chkDBTrustConn
         // 
         this._chkDBTrustConn.Checked = true;
         this._chkDBTrustConn.CheckState = System.Windows.Forms.CheckState.Checked;
         resources.ApplyResources(this._chkDBTrustConn, "_chkDBTrustConn");
         this._chkDBTrustConn.Name = "_chkDBTrustConn";
         this._chkDBTrustConn.CheckedChanged += new System.EventHandler(this.ChangeTrustConnSectionView);
         // 
         // _cmbDBServer
         // 
         resources.ApplyResources(this._cmbDBServer, "_cmbDBServer");
         this._cmbDBServer.Name = "_cmbDBServer";
         this._cmbDBServer.SelectedIndexChanged += new System.EventHandler(this.LoadDBsForNewServerSelection);
         this._cmbDBServer.TextChanged += new System.EventHandler(this.UnloadDatabases);
         this._cmbDBServer.DropDown += new System.EventHandler(this.LoadServers);
         // 
         // _lblDBServer
         // 
         resources.ApplyResources(this._lblDBServer, "_lblDBServer");
         this._lblDBServer.Name = "_lblDBServer";
         // 
         // _btnDBTestConnect
         // 
         resources.ApplyResources(this._btnDBTestConnect, "_btnDBTestConnect");
         this._btnDBTestConnect.Name = "_btnDBTestConnect";
         this._btnDBTestConnect.Click += new System.EventHandler(this.TestDBConnection);
         // 
         // DBConnectionCtrl
         // 
         this.Controls.Add(this._pnlDB);
         this.Name = "DBConnectionCtrl";
         resources.ApplyResources(this, "$this");
         this._pnlDB.ResumeLayout(false);
         this._pnlDB.PerformLayout();
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #region Setting the Panel's View

      #region SetView

      /// <summary>
      /// Sets the view of the panel by enabling and disabling controls based on a users
      /// selection.
      /// </summary>
      /// <param name="bEnabled">Determines if the view is to be set for the user to set
      /// a Database Connection.</param>
      public void SetView(bool bEnabled)
      {
         _cmbDBServer.Enabled = bEnabled;

         SetTrustConnSectionView(bEnabled);

         _cmbDBDB.Enabled = bEnabled;
         _btnDBTestConnect.Enabled = bEnabled;
      } // end SetView

      #endregion

      #region SetTrustConnSectionView

      /// <summary>
      /// Sets the view of the Trusted Connection section of the DB Connection
      /// section.
      /// </summary>
      /// <param name="bEnabled">Determines if the view is to be set for the user to use a
      /// trusted connection or not.</param>
      private void SetTrustConnSectionView(bool bEnabled)
      {
         _chkDBTrustConn.Enabled = bEnabled;
         _txtDBUser.Enabled = (!_chkDBTrustConn.Checked) & bEnabled;
         _txtDBPassword.Enabled = (!_chkDBTrustConn.Checked) & bEnabled;
      } // end SetTrustConnSectionView

      #endregion

      #endregion

      #region Loading Control Data

      #region LoadCmbDBServer

      /// <summary>
      /// Loads the available SQL Server servers into the DB Server combobox.
      /// </summary>
      private void LoadCmbDBServer()
      {
         try
         {
            // Clear the DB Server combobox and fill it with SQL Server servers
            _cmbDBServer.Items.Clear();
            DataTable dt = SMO.SmoApplication.EnumAvailableSqlServers(false);
            if (dt.Rows.Count > 0)
            {
               foreach (DataRow dr in dt.Rows)
               {
                  this._cmbDBServer.Items.Add(dr["Name"]);
               }
            }
         } // end try
         catch (Exception ex)
         {
            RtlMessageBox.Show(GUILibStrings.msgSqlFetchError+"\n\n" +
                            ex.Message, GUILibStrings.errorSqlFetch);
         } // end catch
      } // end LoadCmbDBServer

      #endregion

      #region LoadCmbDB

      /// <summary>
      /// Loads associated Databases for the specified SQL Server server into the DB combobox.
      /// </summary>
      private void LoadCmbDB()
      {
         try
         {
            SMO.Server SqlServerObject;
            if (_chkDBTrustConn.Checked)
            {
               SqlServerObject = new SMO.Server();
               SqlServerObject.ConnectionContext.LoginSecure = _chkDBTrustConn.Checked;
               SqlServerObject.ConnectionContext.ServerInstance = _cmbDBServer.Text;
            }
            else
            {
               ServerConnection svrConn = new ServerConnection(_cmbDBServer.Text, _txtDBUser.Text, _txtDBPassword.Text);
               SqlServerObject = new SMO.Server(svrConn);
            }

            SqlServerObject.ConnectionContext.Connect();

            // Clear the DB combobox and fill it with the associated non-System databases for the Server
            _cmbDBDB.Items.Clear();
            foreach (SMO.Database database in SqlServerObject.Databases)
            {
               if (!database.IsSystemObject)
               {
                  _cmbDBDB.Items.Add(database.Name);
               } // end if
            } // end foreach
         } // end try
         catch (Exception ex)
         {
            RtlMessageBox.Show(ex.Message, GUILibStrings.captException);
         }// end catch
      } // end LoadCmbDB

      #endregion

      #endregion

      #region IsDBConnCriteriaComplete

      /// <summary>
      /// Checks to see that the user has specified a server, database, and if trusted connection
      /// has not been selected, a userid.  The user will receive a MessageBox for any missing
      /// value.
      /// </summary>
      /// <returns>Returns true if everything has been completed; otherwise, false.</returns>
      public bool IsDBConnCriteriaComplete()
      {
         bool bRetVal = false;

         if (_cmbDBServer.Text.Length == 0)
         {
            MessageBox.Show(GUILibStrings.msgSelectServer, GUILibStrings.errorServer);
         } // end if
         else if (!_chkDBTrustConn.Checked && (_txtDBUser.Text.Length == 0))
         {
            MessageBox.Show(GUILibStrings.errorTrusted, GUILibStrings.errorUserID);
         } // end else if
         else if (_cmbDBDB.Text.Length == 0)
         {
            MessageBox.Show(GUILibStrings.msgSelectServer, GUILibStrings.errorDB);
         } // end else if
         else
         {
            bRetVal = true;
         } // end else

         return (bRetVal);
      } // end IsDBConnCriteriaComplete

      #endregion

      #region TestDBConn

      /// <summary>
      /// Tests the Database Connection based on the data provided by the user.
      /// </summary>
      /// <returns>Returns true if the connection is successful; otherwise, false.</returns>
      public bool TestDBConn()
      {
         string sConnectionString;

         try
         {
            sConnectionString = "Server=" + _cmbDBServer.Text + ";" +
                                "Database=" + _cmbDBDB.Text + ";" +
                                ((_chkDBTrustConn.Checked) ? "Integrated security=SSPI;" :
                                                             "User ID=" + _txtDBUser.Text + ";" +
                                                             "Password=" + _txtDBPassword.Text + ";");

            if (_conDataAccessLayer == null)
            {
            } // end if
            else
            {
               _conDataAccessLayer = new Connection(sConnectionString);
            } // end else

            return (_conDataAccessLayer.IsConnectionValid);
         } // end try
         catch
         {
            return (false);
         } // end catch
      } // end TestDBConn

      #endregion

      #region Event Handlers

      #region LoadServers

      /// <summary>
      /// DropDown event for the CmbDBServer combobox, which is loads the DB Server combobox.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void LoadServers(object sender, EventArgs e)
      {
         Cursor = System.Windows.Forms.Cursors.WaitCursor;

         if (_cmbDBServer.Items.Count == 0)
         {
            LoadCmbDBServer();
         } // end if

         Cursor = System.Windows.Forms.Cursors.Default;
      } // end LoadServers

      #endregion

      #region LoadDBsForNewServerSelection

      /// <summary>
      /// Selected Index Change event for the _cmbDBServer combobox, which is used to load
      /// the DB combobox.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void LoadDBsForNewServerSelection(object sender, System.EventArgs e)
      {
         LoadCmbDB();
      } // end LoadDBsForNewServerSelection

      #endregion

      #region UnloadDatabases

      /// <summary>
      /// Text Changed event for the Database Server combobox, which resets the flag indicating that
      /// the Database combobox is loaded with correct data.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void UnloadDatabases(object sender, EventArgs e)
      {
         _cmbDBDB.Items.Clear();
      } // end UnloadDatabases

      #endregion

      #region ChangeTrustConnSectionView

      /// <summary>
      /// Checked changed event for the _chkDBTrustConn checkbox field, which sets the
      /// Trusted Connection View.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ChangeTrustConnSectionView(object sender, System.EventArgs e)
      {
         SetTrustConnSectionView(true);
      } // end ChangeTrustConnSectionView

      #endregion

      #region LoadDatabases

      /// <summary>
      /// DropDown event for the DB combobox, which is responsible for loading the database for the
      /// specified server if they have not already been loaded.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void LoadDatabases(object sender, EventArgs e)
      {
         Cursor = System.Windows.Forms.Cursors.WaitCursor;

         // If the Databases for the chosen Server have not been loaded, load them.
         if (_cmbDBDB.Items.Count == 0)
         {
            LoadCmbDB();
         } // end if

         Cursor = System.Windows.Forms.Cursors.Default;
      } // end LoadDatabases

      #endregion

      #region TestDBConnection

      /// <summary>
      /// Click event for the DB Test Connect button, which is used to check to see that the user
      /// has properly filled in the appropriate criteria for a Database connection, and that the
      /// data entered will properly connect the user to a database.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void TestDBConnection(object sender, System.EventArgs e)
      {
         // Checking to see if the Database Connection criteria has been fully entered
         if (IsDBConnCriteriaComplete())
         {
            // Checking the Database Connection based on the criteria specified
            if (TestDBConn())
            {
               RtlMessageBox.Show(GUILibStrings.msgConnected, GUILibStrings.msgConnectionStatus);
            } // end if
            else
            {
               RtlMessageBox.Show(GUILibStrings.errorFailedToConnect, GUILibStrings.msgConnectionStatus);
            } // end else
         } // end if
      } // end TestDBConnection

      #endregion

      #endregion

      #endregion
   } // end DBConnectionCtrl Class
} // end GraySystem.UI.Controls Namespace