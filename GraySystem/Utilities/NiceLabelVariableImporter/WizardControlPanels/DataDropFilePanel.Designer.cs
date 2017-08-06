namespace ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels
{
   partial class DataDropFilePanel : ICS.GUI_Library.Controls.WizardControlPanels.WizardControlPanel
   {
      protected System.Windows.Forms.Label _lblFromMessage;
      protected System.Windows.Forms.Label _lblFromFilePath;
      protected System.Windows.Forms.TextBox _txtFromFilePath;
      protected System.Windows.Forms.Button _btnFromBrowse;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataDropFilePanel));
         this._lblFromMessage = new System.Windows.Forms.Label();
         this._lblFromFilePath = new System.Windows.Forms.Label();
         this._txtFromFilePath = new System.Windows.Forms.TextBox();
         this._btnFromBrowse = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // _lblFromMessage
         // 
         resources.ApplyResources(this._lblFromMessage, "_lblFromMessage");
         this._lblFromMessage.Name = "_lblFromMessage";
         // 
         // _lblFromFilePath
         // 
         resources.ApplyResources(this._lblFromFilePath, "_lblFromFilePath");
         this._lblFromFilePath.Name = "_lblFromFilePath";
         // 
         // _txtFromFilePath
         // 
         resources.ApplyResources(this._txtFromFilePath, "_txtFromFilePath");
         this._txtFromFilePath.Name = "_txtFromFilePath";
         // 
         // _btnFromBrowse
         // 
         resources.ApplyResources(this._btnFromBrowse, "_btnFromBrowse");
         this._btnFromBrowse.Name = "_btnFromBrowse";
         this._btnFromBrowse.Click += new System.EventHandler(this.GetFilePathFromFileDialog);
         // 
         // DataDropFilePanel
         // 
         this.Controls.Add(this._btnFromBrowse);
         this.Controls.Add(this._txtFromFilePath);
         this.Controls.Add(this._lblFromFilePath);
         this.Controls.Add(this._lblFromMessage);
         this.Name = "DataDropFilePanel";
         resources.ApplyResources(this, "$this");
         this.LeavePanel += new ICS.GUI_Library.Controls.WizardControlPanels.LeavePanelEventHandler(this.ValidateDataDropFilePath);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
   }
}
