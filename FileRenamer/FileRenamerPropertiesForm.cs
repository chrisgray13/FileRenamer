#region Usings

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using GraySystem;
using GraySystem.IO;

#endregion


namespace FileRenamer
{
    /// <summary>
    /// Property dialog used to specify how the files will be renamed, the prefix and
    /// suffix mask used, and how they will be sorted prior to being renamed to ensure
    /// the correct renaming sequence.
    /// </summary>
    public class FileRenamerPropertiesForm : System.Windows.Forms.Form
    {
        #region Fields

        #region Controls

        private System.Windows.Forms.Label _lblMessage;

        #region Naming Options Group

        private System.Windows.Forms.Label _lblItemToRename;
        private System.Windows.Forms.ComboBox _cmbItemToRename;
        private System.Windows.Forms.Label _lblFilenameTemplate;
        private System.Windows.Forms.TextBox _txtFilenameTemplate;
        private System.Windows.Forms.Button _btnMaskMenu;

        #endregion

        #region New Filename Example

        private System.Windows.Forms.Label _lblNewFilenameExampleLabel;
        private System.Windows.Forms.Label _lblNewFilenameExample;

        #endregion

        #region Sorting Options Group

        private System.Windows.Forms.Label _lblSortingOption;
        private System.Windows.Forms.ComboBox _cmbSortingOption;

        #endregion

        private System.Windows.Forms.CheckBox _chkbxTestRun;

        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Button _btnCancel;

        #region Context Menu

        private System.Windows.Forms.ContextMenu _contextMenuMasks;

        private System.Windows.Forms.MenuItem _menuItemYearFormat1;
        private System.Windows.Forms.MenuItem _menuItemDateFormat1;
        private System.Windows.Forms.MenuItem _menuItemDateFormat2;
        private System.Windows.Forms.MenuItem _menuItemTimeFormat1;
        private System.Windows.Forms.MenuItem _menuItemDateTimeFormat1;
        private System.Windows.Forms.MenuItem _menuItemContextSeparator1;
        private System.Windows.Forms.MenuItem _menuItemNumberSequence1;
        private System.Windows.Forms.MenuItem _menuItemNumberSequence2;
        private System.Windows.Forms.MenuItem _menuItemNumberSequence3;
        private System.Windows.Forms.MenuItem _menuItemNumberSequence4;
        private System.Windows.Forms.MenuItem _menuItemContextSeparator2;
        private System.Windows.Forms.MenuItem _menuItemContextDtTimeSeq;
        private System.Windows.Forms.MenuItem _menuItemContextSeparator3;
        private System.Windows.Forms.MenuItem _menuItemContextFilename;
        private System.Windows.Forms.MenuItem _menuItemContextTitle;
        private System.Windows.Forms.MenuItem _menuItemContextSeparator4;
        private System.Windows.Forms.MenuItem _menuItemContextReplace;
        private System.Windows.Forms.MenuItem _menuItemContextRemove;
        private Label _lblTimeOffset;
        private TextBox _txtTimeOffset;

        #endregion

        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container _components = null;

        #endregion

        #region Properties

        #region ItemToUpdate

        public string ItemToUpdate
        {
            get { return (_cmbItemToRename.Text); }
        }

        #endregion

        #region FilenameTemplate

        /// <summary>
        /// Gets the the template created for generating the filenames.
        /// </summary>
        public string FilenameTemplate
        {
            get { return (_txtFilenameTemplate.Text); }
        } // end FilenameTemplate property

        #endregion

        #region TimeOffset

        public int TimeOffset
        {
            get
            {
                if (_txtTimeOffset.Text.Trim().Length == 0)
                {
                    return 0;
                }
                else
                {
                    int offset;

                    if (Int32.TryParse(_txtTimeOffset.Text, out offset))
                    {
                        return offset;
                    }
                    else
                    {
                        throw new InvalidCastException("Time offset must be an integer");
                    }
                }
            }
        }

        #endregion

        #region SortingOption

        /// <summary>
        /// Gets the Sorting Option specified by the user from the form.
        /// </summary>
        public string SortingOption
        {
            get
            {
                switch (_cmbSortingOption.Text)
                {
                    case "Date Created":
                        return ("DatePictureTaken");
                        break;
                    case "Title":
                        return ("Title");
                        break;
                    case "Original Filename":
                    default:
                        return ("Filename");
                        break;
                } // end switch
            } // end get
        } // end SortingOption property

        #endregion

        #region TestRun

        /// <summary>
        /// Gets the value of the Test Run checkbox to determine whether or not the operation
        /// should actually rename the files or just simulate (test).
        /// </summary>
        public bool TestRun
        {
            get { return (_chkbxTestRun.Checked); }
        } // end TestRun

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a FileRenamerPropertiesForm object and initializes the components.
        /// </summary>
        public FileRenamerPropertiesForm()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
        } // end FileRenamerPropertiesForm constructor

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileRenamerPropertiesForm));
            this._lblMessage = new System.Windows.Forms.Label();
            this._lblItemToRename = new System.Windows.Forms.Label();
            this._cmbItemToRename = new System.Windows.Forms.ComboBox();
            this._lblFilenameTemplate = new System.Windows.Forms.Label();
            this._txtFilenameTemplate = new System.Windows.Forms.TextBox();
            this._btnMaskMenu = new System.Windows.Forms.Button();
            this._lblNewFilenameExampleLabel = new System.Windows.Forms.Label();
            this._lblNewFilenameExample = new System.Windows.Forms.Label();
            this._lblSortingOption = new System.Windows.Forms.Label();
            this._cmbSortingOption = new System.Windows.Forms.ComboBox();
            this._chkbxTestRun = new System.Windows.Forms.CheckBox();
            this._btnOK = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._contextMenuMasks = new System.Windows.Forms.ContextMenu();
            this._menuItemYearFormat1 = new System.Windows.Forms.MenuItem();
            this._menuItemDateFormat1 = new System.Windows.Forms.MenuItem();
            this._menuItemDateFormat2 = new System.Windows.Forms.MenuItem();
            this._menuItemTimeFormat1 = new System.Windows.Forms.MenuItem();
            this._menuItemDateTimeFormat1 = new System.Windows.Forms.MenuItem();
            this._menuItemContextSeparator1 = new System.Windows.Forms.MenuItem();
            this._menuItemNumberSequence1 = new System.Windows.Forms.MenuItem();
            this._menuItemNumberSequence2 = new System.Windows.Forms.MenuItem();
            this._menuItemNumberSequence3 = new System.Windows.Forms.MenuItem();
            this._menuItemNumberSequence4 = new System.Windows.Forms.MenuItem();
            this._menuItemContextSeparator2 = new System.Windows.Forms.MenuItem();
            this._menuItemContextDtTimeSeq = new System.Windows.Forms.MenuItem();
            this._menuItemContextSeparator3 = new System.Windows.Forms.MenuItem();
            this._menuItemContextFilename = new System.Windows.Forms.MenuItem();
            this._menuItemContextTitle = new System.Windows.Forms.MenuItem();
            this._menuItemContextSeparator4 = new System.Windows.Forms.MenuItem();
            this._menuItemContextReplace = new System.Windows.Forms.MenuItem();
            this._menuItemContextRemove = new System.Windows.Forms.MenuItem();
            this._lblTimeOffset = new System.Windows.Forms.Label();
            this._txtTimeOffset = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _lblMessage
            // 
            this._lblMessage.Location = new System.Drawing.Point(8, 8);
            this._lblMessage.Name = "_lblMessage";
            this._lblMessage.Size = new System.Drawing.Size(272, 32);
            this._lblMessage.TabIndex = 0;
            this._lblMessage.Text = "Select the options you would like to use to rename the selected files.";
            // 
            // _lblItemToRename
            // 
            this._lblItemToRename.Location = new System.Drawing.Point(8, 56);
            this._lblItemToRename.Name = "_lblItemToRename";
            this._lblItemToRename.Size = new System.Drawing.Size(100, 16);
            this._lblItemToRename.TabIndex = 1;
            this._lblItemToRename.Text = "Item to Rename:";
            // 
            // _cmbItemToRename
            // 
            this._cmbItemToRename.Items.AddRange(new object[] {
            "Filename",
            "Title"});
            this._cmbItemToRename.Location = new System.Drawing.Point(112, 51);
            this._cmbItemToRename.Name = "_cmbItemToRename";
            this._cmbItemToRename.Size = new System.Drawing.Size(176, 21);
            this._cmbItemToRename.TabIndex = 2;
            // 
            // _lblFilenameTemplate
            // 
            this._lblFilenameTemplate.Location = new System.Drawing.Point(8, 90);
            this._lblFilenameTemplate.Name = "_lblFilenameTemplate";
            this._lblFilenameTemplate.Size = new System.Drawing.Size(104, 16);
            this._lblFilenameTemplate.TabIndex = 3;
            this._lblFilenameTemplate.Text = "Filename Template:";
            // 
            // _txtFilenameTemplate
            // 
            this._txtFilenameTemplate.Location = new System.Drawing.Point(111, 88);
            this._txtFilenameTemplate.Name = "_txtFilenameTemplate";
            this._txtFilenameTemplate.Size = new System.Drawing.Size(160, 20);
            this._txtFilenameTemplate.TabIndex = 4;
            this._txtFilenameTemplate.TextChanged += new System.EventHandler(this.UpdateFilenameExample);
            this._txtFilenameTemplate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AcceptPropertiesOnEnter);
            // 
            // _btnMaskMenu
            // 
            this._btnMaskMenu.Image = ((System.Drawing.Image)(resources.GetObject("_btnMaskMenu.Image")));
            this._btnMaskMenu.Location = new System.Drawing.Point(273, 90);
            this._btnMaskMenu.Name = "_btnMaskMenu";
            this._btnMaskMenu.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._btnMaskMenu.Size = new System.Drawing.Size(16, 16);
            this._btnMaskMenu.TabIndex = 5;
            this._btnMaskMenu.Click += new System.EventHandler(this.ShowMaskMenu);
            // 
            // _lblNewFilenameExampleLabel
            // 
            this._lblNewFilenameExampleLabel.Location = new System.Drawing.Point(8, 147);
            this._lblNewFilenameExampleLabel.Name = "_lblNewFilenameExampleLabel";
            this._lblNewFilenameExampleLabel.Size = new System.Drawing.Size(128, 16);
            this._lblNewFilenameExampleLabel.TabIndex = 6;
            this._lblNewFilenameExampleLabel.Text = "New Filename Example:";
            // 
            // _lblNewFilenameExample
            // 
            this._lblNewFilenameExample.Location = new System.Drawing.Point(24, 171);
            this._lblNewFilenameExample.Name = "_lblNewFilenameExample";
            this._lblNewFilenameExample.Size = new System.Drawing.Size(256, 16);
            this._lblNewFilenameExample.TabIndex = 7;
            // 
            // _lblSortingOption
            // 
            this._lblSortingOption.Location = new System.Drawing.Point(8, 201);
            this._lblSortingOption.Name = "_lblSortingOption";
            this._lblSortingOption.Size = new System.Drawing.Size(80, 16);
            this._lblSortingOption.TabIndex = 8;
            this._lblSortingOption.Text = "Sorting Option:";
            // 
            // _cmbSortingOption
            // 
            this._cmbSortingOption.Items.AddRange(new object[] {
            "Date Created",
            "Original Filename",
            "Title"});
            this._cmbSortingOption.Location = new System.Drawing.Point(112, 199);
            this._cmbSortingOption.Name = "_cmbSortingOption";
            this._cmbSortingOption.Size = new System.Drawing.Size(176, 21);
            this._cmbSortingOption.TabIndex = 9;
            // 
            // _chkbxTestRun
            // 
            this._chkbxTestRun.Location = new System.Drawing.Point(27, 237);
            this._chkbxTestRun.Name = "_chkbxTestRun";
            this._chkbxTestRun.Size = new System.Drawing.Size(104, 16);
            this._chkbxTestRun.TabIndex = 9;
            this._chkbxTestRun.Text = "Test Run";
            // 
            // _btnOK
            // 
            this._btnOK.Location = new System.Drawing.Point(67, 261);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(72, 32);
            this._btnOK.TabIndex = 10;
            this._btnOK.Text = "OK";
            this._btnOK.Click += new System.EventHandler(this.AcceptProperties);
            // 
            // _btnCancel
            // 
            this._btnCancel.Location = new System.Drawing.Point(171, 261);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(72, 32);
            this._btnCancel.TabIndex = 11;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.Click += new System.EventHandler(this.ClosePropertiesForm);
            // 
            // _contextMenuMasks
            // 
            this._contextMenuMasks.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._menuItemYearFormat1,
            this._menuItemDateFormat1,
            this._menuItemDateFormat2,
            this._menuItemTimeFormat1,
            this._menuItemDateTimeFormat1,
            this._menuItemContextSeparator1,
            this._menuItemNumberSequence1,
            this._menuItemNumberSequence2,
            this._menuItemNumberSequence3,
            this._menuItemNumberSequence4,
            this._menuItemContextSeparator2,
            this._menuItemContextDtTimeSeq,
            this._menuItemContextSeparator3,
            this._menuItemContextFilename,
            this._menuItemContextTitle,
            this._menuItemContextSeparator4,
            this._menuItemContextReplace,
            this._menuItemContextRemove});
            // 
            // _menuItemYearFormat1
            // 
            this._menuItemYearFormat1.Index = 0;
            this._menuItemYearFormat1.Text = "yyyy";
            this._menuItemYearFormat1.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemDateFormat1
            // 
            this._menuItemDateFormat1.Index = 1;
            this._menuItemDateFormat1.Text = "yyyy.MM.dd";
            this._menuItemDateFormat1.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemDateFormat2
            // 
            this._menuItemDateFormat2.Index = 2;
            this._menuItemDateFormat2.Text = "yyyy-MM-dd";
            this._menuItemDateFormat2.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemTimeFormat1
            // 
            this._menuItemTimeFormat1.Index = 3;
            this._menuItemTimeFormat1.Text = "hh.mm.ss";
            this._menuItemTimeFormat1.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemDateTimeFormat1
            // 
            this._menuItemDateTimeFormat1.Index = 4;
            this._menuItemDateTimeFormat1.Text = "yyyy.MM.dd hh.mm.ss";
            this._menuItemDateTimeFormat1.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemContextSeparator1
            // 
            this._menuItemContextSeparator1.Index = 5;
            this._menuItemContextSeparator1.Text = "-";
            // 
            // _menuItemNumberSequence1
            // 
            this._menuItemNumberSequence1.Index = 6;
            this._menuItemNumberSequence1.Text = "#";
            this._menuItemNumberSequence1.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemNumberSequence2
            // 
            this._menuItemNumberSequence2.Index = 7;
            this._menuItemNumberSequence2.Text = "##";
            this._menuItemNumberSequence2.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemNumberSequence3
            // 
            this._menuItemNumberSequence3.Index = 8;
            this._menuItemNumberSequence3.Text = "###";
            this._menuItemNumberSequence3.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemNumberSequence4
            // 
            this._menuItemNumberSequence4.Index = 9;
            this._menuItemNumberSequence4.Text = "####";
            this._menuItemNumberSequence4.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemContextSeparator2
            // 
            this._menuItemContextSeparator2.Index = 10;
            this._menuItemContextSeparator2.Text = "-";
            // 
            // _menuItemContextDtTimeSeq
            // 
            this._menuItemContextDtTimeSeq.Index = 11;
            this._menuItemContextDtTimeSeq.Text = "yyyy.MM.dd hh.mm.ss - ##";
            this._menuItemContextDtTimeSeq.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemContextSeparator3
            // 
            this._menuItemContextSeparator3.Index = 12;
            this._menuItemContextSeparator3.Text = "-";
            // 
            // _menuItemContextFilename
            // 
            this._menuItemContextFilename.Index = 13;
            this._menuItemContextFilename.Text = "Filename";
            this._menuItemContextFilename.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemContextTitle
            // 
            this._menuItemContextTitle.Index = 14;
            this._menuItemContextTitle.Text = "Title";
            this._menuItemContextTitle.Click += new System.EventHandler(this.InsertFormatIntoFilenameTemplate);
            // 
            // _menuItemContextSeparator4
            // 
            this._menuItemContextSeparator4.Index = 15;
            this._menuItemContextSeparator4.Text = "-";
            // 
            // _menuItemContextReplace
            // 
            this._menuItemContextReplace.Index = 16;
            this._menuItemContextReplace.Text = "Replace(old, new)";
            this._menuItemContextReplace.Click += new System.EventHandler(this.AddReplaceFunction);
            // 
            // _menuItemContextRemove
            // 
            this._menuItemContextRemove.Index = 17;
            this._menuItemContextRemove.Text = "Remove(start, number)";
            this._menuItemContextRemove.Click += new System.EventHandler(this.AddRemoveFunction);
            // 
            // _lblTimeOffset
            // 
            this._lblTimeOffset.AutoSize = true;
            this._lblTimeOffset.Location = new System.Drawing.Point(8, 122);
            this._lblTimeOffset.Name = "_lblTimeOffset";
            this._lblTimeOffset.Size = new System.Drawing.Size(64, 13);
            this._lblTimeOffset.TabIndex = 12;
            this._lblTimeOffset.Text = "Time Offset:";
            // 
            // _txtTimeOffset
            // 
            this._txtTimeOffset.Location = new System.Drawing.Point(111, 119);
            this._txtTimeOffset.Name = "_txtTimeOffset";
            this._txtTimeOffset.Size = new System.Drawing.Size(177, 20);
            this._txtTimeOffset.TabIndex = 13;
            // 
            // FileRenamerPropertiesForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 307);
            this.Controls.Add(this._txtTimeOffset);
            this.Controls.Add(this._lblTimeOffset);
            this.Controls.Add(this._lblMessage);
            this.Controls.Add(this._lblItemToRename);
            this.Controls.Add(this._cmbItemToRename);
            this.Controls.Add(this._lblFilenameTemplate);
            this.Controls.Add(this._txtFilenameTemplate);
            this.Controls.Add(this._btnMaskMenu);
            this.Controls.Add(this._lblNewFilenameExampleLabel);
            this.Controls.Add(this._lblNewFilenameExample);
            this.Controls.Add(this._lblSortingOption);
            this.Controls.Add(this._cmbSortingOption);
            this.Controls.Add(this._chkbxTestRun);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileRenamerPropertiesForm";
            this.ShowInTaskbar = false;
            this.Text = "Renaming Properties";
            this.ResumeLayout(false);
            this.PerformLayout();

        } // end InitializeComponent

        #endregion

        #region Event Handlers

        #region ShowMaskMenu

        private void ShowMaskMenu(object sender, System.EventArgs e)
        {
            _contextMenuMasks.Show(_btnMaskMenu, new Point(_btnMaskMenu.Width, 0));
        }

        #endregion

        #region InsertFormatIntoFilenameTemplate

        private void InsertFormatIntoFilenameTemplate(object sender, EventArgs e)
        {
            int iSelectionStart = _txtFilenameTemplate.SelectionStart;
            string sFilenameTemplate = _txtFilenameTemplate.Text;

            if (_txtFilenameTemplate.SelectionLength > 0)
            {
                sFilenameTemplate = sFilenameTemplate.Remove(iSelectionStart,
                                                             _txtFilenameTemplate.SelectionLength);
            } // end if

            _txtFilenameTemplate.Text = sFilenameTemplate.Insert(iSelectionStart,
                                                                 "{" + ((MenuItem)sender).Text + "}");
            _txtFilenameTemplate.SelectionLength = 0;
            _txtFilenameTemplate.SelectionStart = iSelectionStart + ((MenuItem)sender).Text.Length + 2;
            _txtFilenameTemplate.Focus();
        }

        #endregion

        #region UpdateFilenameExample

        private void UpdateFilenameExample(object sender, EventArgs e)
        {
            Mask mask = new Mask(_txtFilenameTemplate.Text);

            _lblNewFilenameExample.Text = mask.Format(DateTimeExtended.Now, 1, "Title", "Filename");
        }

        #endregion

        #region AcceptProperties

        /// <summary>
        /// Validates that the user entered a valid suffix mask.  If so, the form is closed, after
        /// setting the DialogResult to OK.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptProperties(object sender, System.EventArgs e)
        {
            if (_txtFilenameTemplate.Text.Trim().Length == 0)
            {
                MessageBox.Show(this,
                                "The filename template may not be blank.  Please construct a filename\n" +
                                "template to rename the selected files.  The menu of masks accessed via\n" +
                                "the button to the right of the Filename Template textbox may be used\n" +
                                "to insert various formats for dates, times, and sequencing.  It is\n" +
                                "advised to always use a sequencing of some sort to ensure the filenames\n" +
                                "are unique.",
                                "Filename Template Error");
            } // end if
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            } // end else
        } // end AcceptProperties

        #endregion

        #region AcceptPropertiesOnEnter

        /// <summary>
        /// Accepts the properties when the user presses the enter key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptPropertiesOnEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                AcceptProperties(sender, (EventArgs)e);
            } // end if
        } // end AcceptPropertiesOnEnter

        #endregion

        #region ClosePropertiesForm

        /// <summary>
        /// Closes the form after setting the DialogResult to Cancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosePropertiesForm(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        } // end ClosePropertiesForm

        #endregion

        private void AddReplaceFunction(object sender, System.EventArgs e)
        {

        }

        #endregion

        private void AddRemoveFunction(object sender, System.EventArgs e)
        {

        }

        #endregion

    } // end FileRenamerPropertiesForm Class
} // end FileRenamer Namespace