using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GraySystem.UI
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class MessageBox : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Label _lblMessage;
      private System.Windows.Forms.CheckBox _chkbxAlwaysShow;
      private System.Windows.Forms.Button _btnOK;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      #region Properties

      protected bool AlwaysShow
      {
         get { return (_chkbxAlwaysShow.Checked); }
      } // end AlwaysShow property

      #endregion

      #region Constructors

      protected MessageBox()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      } // end MessageBox constructor

      protected MessageBox(string sMessage)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         _lblMessage.Text = sMessage;
      } // end MessageBox constructor

      protected MessageBox(string sMessage, string sTitle)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         _lblMessage.Text = sMessage;
         Text = sTitle;
      } // end MessageBox constructor

      #endregion

      #region Dispose

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose( bool disposing )
      {
         if( disposing )
         {
            if( components != null )
            {
               components.Dispose();
            } // end if
         } // end if

         base.Dispose( disposing );
      } // end Dispose

      #endregion

      #region Methods

      #region Component Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this._lblMessage = new System.Windows.Forms.Label();
         this._chkbxAlwaysShow = new System.Windows.Forms.CheckBox();
         this._btnOK = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // _lblMessage
         // 
         this._lblMessage.Location = new System.Drawing.Point(8, 8);
         this._lblMessage.Name = "_lblMessage";
         this._lblMessage.Size = new System.Drawing.Size(280, 96);
         this._lblMessage.TabIndex = 3;
         // 
         // _chkbxAlwaysShow
         // 
         this._chkbxAlwaysShow.Checked = true;
         this._chkbxAlwaysShow.CheckState = System.Windows.Forms.CheckState.Checked;
         this._chkbxAlwaysShow.Location = new System.Drawing.Point(8, 136);
         this._chkbxAlwaysShow.Name = "_chkbxAlwaysShow";
         this._chkbxAlwaysShow.Size = new System.Drawing.Size(160, 24);
         this._chkbxAlwaysShow.TabIndex = 1;
         this._chkbxAlwaysShow.Text = "Always Show this Message";
         // 
         // _btnOK
         // 
         this._btnOK.Location = new System.Drawing.Point(104, 112);
         this._btnOK.Name = "_btnOK";
         this._btnOK.TabIndex = 2;
         this._btnOK.Text = "OK";
         this._btnOK.Click += new System.EventHandler(this.HandleOKClick);
         // 
         // MessageBox
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(292, 168);
         this.Controls.Add(this._btnOK);
         this.Controls.Add(this._chkbxAlwaysShow);
         this.Controls.Add(this._lblMessage);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "MessageBox";
         this.ShowInTaskbar = false;
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.TopMost = true;
         this.ResumeLayout(false);

      }

		#endregion

      public static DialogResult Show(IWin32Window owner, string sMessage, string sTitle, out bool bAlwaysShow)
      {
         GraySystem.UI.MessageBox msgBox = new GraySystem.UI.MessageBox(sMessage, sTitle);

         msgBox.ShowDialog(owner);

         bAlwaysShow = msgBox.AlwaysShow;

         return (msgBox.DialogResult);
      } // end Show
      #endregion

      private void HandleOKClick(object sender, System.EventArgs e)
      {
         this.DialogResult = System.Windows.Forms.DialogResult.OK;
         Close();
      }
   } // end MessageBox
} // end GraySystem.UI Namespace