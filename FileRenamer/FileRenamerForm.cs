#region Usings

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Data;

using GraySystem.IO;

#endregion


namespace FileRenamer
{
    /// <summary>
    /// Provides the user with the ability to select a group of files and rename them
    /// based on the properties and suffix mask specified.
    /// </summary>
    public class FileRenamerForm : System.Windows.Forms.Form
    {
        #region Fields

        #region Controls

        #region Main Menu

        private System.Windows.Forms.MainMenu _menuMain;
        private System.Windows.Forms.MenuItem _menuItemFile;
        private System.Windows.Forms.MenuItem _menuItemFile_Open;
        private System.Windows.Forms.MenuItem _menuItemFile_Separator;
        private System.Windows.Forms.MenuItem _menuItemFile_Exit;
        private System.Windows.Forms.MenuItem _menuItemEdit;
        private System.Windows.Forms.MenuItem _menuItemEdit_Undo;
        private System.Windows.Forms.MenuItem _menuItemEdit_Redo;
        private System.Windows.Forms.MenuItem _menuItemEdit_Separator;
        private System.Windows.Forms.MenuItem _menuItemEdit_ClearResults;
        private System.Windows.Forms.MenuItem _menuItemTools;
        private System.Windows.Forms.MenuItem _menuItemTools_RenameFiles;

        #endregion

        private System.Windows.Forms.TextBox _txtLog;
        private System.Windows.Forms.StatusBar _stsBar;
        private System.Windows.Forms.ProgressBar _prgRenaming;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container _components = null;

        private FileRenamerPropertiesForm _frmFileRenamerProps;

        #endregion

        private string[] _sFileNames;
        private string[] _sOldFileNames;
        private int _iFilesRenamed;
        private bool _bUndo;

        private string _sInitialDirectory;
        private int _iFilterIndex;

        private bool _bShowRenamingCompleteMessage;
        private bool _bRunFileRenaming;

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a FileRenamerForm object and intializes the components.
        /// </summary>
        public FileRenamerForm()
        {
            // Required for Windows Form Designer support
            InitializeComponent();

            _bShowRenamingCompleteMessage = true;
        } // end FileRenamerForm constructor

        #endregion

        #region Dispose

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_components != null)
                {
                    _components.Dispose();
                } // end if
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FileRenamerForm));
            this._menuMain = new System.Windows.Forms.MainMenu();
            this._menuItemFile = new System.Windows.Forms.MenuItem();
            this._menuItemFile_Open = new System.Windows.Forms.MenuItem();
            this._menuItemFile_Separator = new System.Windows.Forms.MenuItem();
            this._menuItemFile_Exit = new System.Windows.Forms.MenuItem();
            this._menuItemEdit = new System.Windows.Forms.MenuItem();
            this._menuItemEdit_Undo = new System.Windows.Forms.MenuItem();
            this._menuItemEdit_Separator = new System.Windows.Forms.MenuItem();
            this._menuItemEdit_ClearResults = new System.Windows.Forms.MenuItem();
            this._menuItemTools = new System.Windows.Forms.MenuItem();
            this._menuItemTools_RenameFiles = new System.Windows.Forms.MenuItem();
            this._txtLog = new System.Windows.Forms.TextBox();
            this._stsBar = new System.Windows.Forms.StatusBar();
            this._prgRenaming = new System.Windows.Forms.ProgressBar();
            this._menuItemEdit_Redo = new System.Windows.Forms.MenuItem();
            this._stsBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuMain
            // 
            this._menuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                  this._menuItemFile,
                                                                                  this._menuItemEdit,
                                                                                  this._menuItemTools});
            // 
            // _menuItemFile
            // 
            this._menuItemFile.Index = 0;
            this._menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this._menuItemFile_Open,
                                                                                      this._menuItemFile_Separator,
                                                                                      this._menuItemFile_Exit});
            this._menuItemFile.Text = "&File";
            // 
            // _menuItemFile_Open
            // 
            this._menuItemFile_Open.Index = 0;
            this._menuItemFile_Open.Text = "&Open";
            this._menuItemFile_Open.Click += new System.EventHandler(this.SelectFiles);
            // 
            // _menuItemFile_Separator
            // 
            this._menuItemFile_Separator.Index = 1;
            this._menuItemFile_Separator.Text = "-";
            // 
            // _menuItemFile_Exit
            // 
            this._menuItemFile_Exit.Index = 2;
            this._menuItemFile_Exit.Text = "E&xit";
            this._menuItemFile_Exit.Click += new System.EventHandler(this.ExitApplication);
            // 
            // _menuItemEdit
            // 
            this._menuItemEdit.Index = 1;
            this._menuItemEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this._menuItemEdit_Undo,
                                                                                      this._menuItemEdit_Redo,
                                                                                      this._menuItemEdit_Separator,
                                                                                      this._menuItemEdit_ClearResults});
            this._menuItemEdit.Text = "&Edit";
            // 
            // _menuItemEdit_Undo
            // 
            this._menuItemEdit_Undo.Enabled = false;
            this._menuItemEdit_Undo.Index = 0;
            this._menuItemEdit_Undo.Text = "&Undo";
            this._menuItemEdit_Undo.Click += new System.EventHandler(this.UndoPreviousRenaming);
            // 
            // _menuItemEdit_Redo
            // 
            this._menuItemEdit_Redo.Enabled = false;
            this._menuItemEdit_Redo.Index = 1;
            this._menuItemEdit_Redo.Text = "&Redo";
            this._menuItemEdit_Redo.Click += new EventHandler(this.RedoPreviousRenaming);
            // 
            // _menuItemEdit_Separator
            // 
            this._menuItemEdit_Separator.Index = 2;
            this._menuItemEdit_Separator.Text = "-";
            // 
            // _menuItemEdit_ClearResults
            // 
            this._menuItemEdit_ClearResults.Index = 3;
            this._menuItemEdit_ClearResults.Text = "Clear Results";
            this._menuItemEdit_ClearResults.Click += new System.EventHandler(this.ClearLog);
            // 
            // _menuItemTools
            // 
            this._menuItemTools.Index = 2;
            this._menuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                       this._menuItemTools_RenameFiles});
            this._menuItemTools.Text = "&Tools";
            // 
            // _menuItemTools_RenameFiles
            // 
            this._menuItemTools_RenameFiles.Index = 0;
            this._menuItemTools_RenameFiles.Text = "&Rename Files";
            this._menuItemTools_RenameFiles.Click += new System.EventHandler(this.ShowRenamingPropertiesForm);
            // 
            // _txtLog
            // 
            this._txtLog.Location = new System.Drawing.Point(0, 0);
            this._txtLog.Multiline = true;
            this._txtLog.Name = "_txtLog";
            this._txtLog.ReadOnly = true;
            this._txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtLog.Size = new System.Drawing.Size(448, 400);
            this._txtLog.TabIndex = 0;
            this._txtLog.Text = "";
            this._txtLog.WordWrap = false;
            // 
            // _stsBar
            // 
            this._stsBar.Controls.Add(this._prgRenaming);
            this._stsBar.Location = new System.Drawing.Point(0, 401);
            this._stsBar.Name = "_stsBar";
            this._stsBar.ShowPanels = true;
            this._stsBar.Size = new System.Drawing.Size(448, 24);
            this._stsBar.TabIndex = 1;
            // 
            // _prgRenaming
            // 
            this._prgRenaming.Location = new System.Drawing.Point(0, 2);
            this._prgRenaming.Name = "_prgRenaming";
            this._prgRenaming.Size = new System.Drawing.Size(432, 23);
            this._prgRenaming.Step = 1;
            this._prgRenaming.TabIndex = 2;
            // 
            // FileRenamerForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(448, 425);
            this.Controls.Add(this._stsBar);
            this.Controls.Add(this._txtLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this._menuMain;
            this.Name = "FileRenamerForm";
            this.Text = "File Renamer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.AdjustControlsForResize);
            this._stsBar.ResumeLayout(false);
            this.ResumeLayout(false);

        } // end InitializeComponent

        #endregion

        #region GetRenamingPropertiesAndRename

        /// <summary>
        /// Displays the Renaming Properties Form to the user, and if the user presses the "OK" button,
        /// then the properties selected are sent to the renamer process.  At that point, a thread will be started
        /// to process the renamer process's RenameFiles method to rename the files.
        /// </summary>
        private void GetRenamingPropertiesAndRename()
        {
            // If the file renamer property dialog has not been instantiated, do so.
            if (_frmFileRenamerProps == null)
            {
                _frmFileRenamerProps = new FileRenamerPropertiesForm();
            } // end if

            if (_frmFileRenamerProps.ShowDialog(this) == DialogResult.OK)
            {
                RenamerProcess renamerProcess = new RenamerProcess(_sFileNames,
                                                                   (RenamingTypes)Enum.Parse(typeof(RenamingTypes), _frmFileRenamerProps.ItemToUpdate),
                                                                   _frmFileRenamerProps.FilenameTemplate,
                                                                   _frmFileRenamerProps.TimeOffset,
                                                                   _frmFileRenamerProps.SortingOption,
                                                                   _frmFileRenamerProps.TestRun);

                if (!_frmFileRenamerProps.TestRun)
                {
                    _bRunFileRenaming = false;
                } // end if
                else
                {
                    _bUndo = false;
                } // end else

                Rename(renamerProcess);
            } // end if
        } // end GetRenamingPropertiesAndRename

        #endregion

        #region Rename

        private void Rename(RenamerProcess renamerProcess)
        {
            Thread thread;

            renamerProcess.FileRenamed += new FileRenamedEventHandler(AddRenamedFileToLog);
            renamerProcess.Finished += new RenamingFinished(HandleRenamingComplete);
            Cursor = Cursors.WaitCursor;

            _iFilesRenamed = 0;
            _prgRenaming.Value = 0;

            thread = new Thread(new ThreadStart(renamerProcess.RenameFiles));

            thread.Start();
        } // end Rename

        #endregion

        #region Event Handlers

        #region SelectFiles

        /// <summary>
        /// Opens the OpenFileDialog for the user to select the file(s) to be renamed according
        /// to the renaming properties specified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFiles(object sender, System.EventArgs e)
        {
            OpenFileDialog dlgFile = new OpenFileDialog();

            dlgFile.CheckFileExists = true;
            dlgFile.CheckPathExists = true;
            dlgFile.Filter = "JPEG (*.jpg; *jpeg) | *.jpg; *.jpeg; | " +
                             "Bitmap (*.bmp) | *.bmp; | " +
                             "Windows Media Audio (*.wma) | *.wma; | " +
                             "MP3 (*.mp3) | *.mp3; | " +
                             "All Files (*.*) | *.*";
            dlgFile.Multiselect = true;

            if (_sInitialDirectory != null)
            {
                dlgFile.InitialDirectory = _sInitialDirectory;
                dlgFile.FilterIndex = _iFilterIndex;
            } // end if

            if (dlgFile.ShowDialog(this) == DialogResult.OK)
            {
                _sFileNames = dlgFile.FileNames;

                _sInitialDirectory = dlgFile.FileName.Substring(0, dlgFile.FileName.LastIndexOf("\\"));
                _iFilterIndex = dlgFile.FilterIndex;

                _menuItemEdit_Undo.Enabled = false;
                _menuItemEdit_Redo.Enabled = false;

                _prgRenaming.Value = 0;

                ShowRenamingPropertiesForm(sender, e);
            } // end if
        } // end SelectFiles

        #endregion

        #region ExitApplication

        /// <summary>
        /// Closes the form in order to exit the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitApplication(object sender, System.EventArgs e)
        {
            Close();
        } // end ExitApplication

        #endregion

        #region ClearLog

        /// <summary>
        /// Clears the results posted to the log.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearLog(object sender, System.EventArgs e)
        {
            _txtLog.Clear();
            _prgRenaming.Value = 0;
        } // end ClearLog

        #endregion

        #region ShowRenamingPropertiesForm

        /// <summary>
        /// Checks to make sure that the user has specified a list of files and calls a method to
        /// display the renaming properties and start the renaming process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowRenamingPropertiesForm(object sender, System.EventArgs e)
        {
            if ((_sFileNames == null) || (_sFileNames.Length == 0))
            {
                MessageBox.Show(this,
                                "You must select at least one file before you may display the renaming\n" +
                                "properties form.",
                                "No Files Selected Error");
            } // end if
            else
            {
                GetRenamingPropertiesAndRename();
            } // end else
        } // end ShowRenamingPropertiesForm

        #endregion

        #region AddRenamedFileToLog

        /// <summary>
        /// Adds the renamed file to the log textbox for informative purposes.
        /// </summary>
        /// <param name="sender">Object throwing the event</param>
        /// <param name="e">Event arguements relating to a the File renaming operation</param>
        private void AddRenamedFileToLog(object sender, AttributeChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new FileRenamedEventHandler(AddRenamedFileToLog), sender, e);
            }
            else
            {
                _txtLog.AppendText(e.ToString() + Environment.NewLine);
                _txtLog.SelectionLength = 0;
                _txtLog.ScrollToCaret();

                if (!e.LogOnly)
                {
                    _prgRenaming.Value = (++_iFilesRenamed * 100) / _sFileNames.Length;
                }
            }
        } // end AddRenamedFileToLog

        #endregion

        #region HandleRenamingComplete

        /// <summary>
        /// Resets the cursor and filenames after the renaming is finished.  It also displays a message
        /// to the user indicating the files are renamed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="sFileNames"></param>
        private void HandleRenamingComplete(object sender, string[] sFileNames)
        {
            if (InvokeRequired)
            {
                Invoke(new RenamingFinished(HandleRenamingComplete), sender, sFileNames);
            }
            else
            {
                Cursor = Cursors.Default;

                if (_frmFileRenamerProps.TestRun && !_bRunFileRenaming)
                {
                    if (MessageBox.Show("Would you like to run the renaming process?", "Run Renaming Process", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _bRunFileRenaming = true;

                        Rename(new RenamerProcess(_sFileNames,
                                                  (RenamingTypes)Enum.Parse(typeof(RenamingTypes), _frmFileRenamerProps.ItemToUpdate),
                                                  _frmFileRenamerProps.FilenameTemplate,
                                                  _frmFileRenamerProps.TimeOffset,
                                                  _frmFileRenamerProps.SortingOption,
                                                  false));
                    } // end if
                } // end if
                else
                {
                    if (_bShowRenamingCompleteMessage)
                    {
                        GraySystem.UI.MessageBox.Show(this, "Renaming Complete!", "Renaming", out _bShowRenamingCompleteMessage);
                    } // end if

                    _sOldFileNames = _sFileNames; // Setting the old filenames for undo
                    _sFileNames = sFileNames;

                    _menuItemEdit_Undo.Enabled = !_bUndo;
                    _menuItemEdit_Redo.Enabled = _bUndo;
                } // end else
            }
        } // end HandleRenamingComplete

        #endregion

        #region UndoPreviousRenaming

        private void UndoPreviousRenaming(object sender, EventArgs e)
        {
            _menuItemEdit_Undo.Enabled = false;

            _bUndo = true;

            Rename(new RenamerProcess(_sFileNames, _sOldFileNames));
        } // end UndoPreviousRenaming

        #endregion

        #region RedoPreviousRenaming

        private void RedoPreviousRenaming(object sender, EventArgs e)
        {
            _menuItemEdit_Redo.Enabled = false;

            _bUndo = false;

            Rename(new RenamerProcess(_sFileNames, _sOldFileNames));
        } // end RedoPreviousRenaming

        #endregion

        #region AdjustControlsForResize

        /// <summary>
        /// Adjusts the controls on the form to same position and ratio as the form changes size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdjustControlsForResize(object sender, EventArgs e)
        {
            _txtLog.Height = Height - 81;
            _txtLog.Width = Width - 8;
            _stsBar.Width = Width - 8;
            _prgRenaming.Width = Width - 25;
        } // end AdjustControlsForResize

        #endregion

        #endregion

        #endregion

        #region Main

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new FileRenamerForm());
        } // end Main

        #endregion
    } // end FileRenamerForm Class
} // end FileRenamer Namespace