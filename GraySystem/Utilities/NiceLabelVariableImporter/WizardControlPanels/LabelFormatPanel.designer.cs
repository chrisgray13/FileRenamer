namespace ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels
{
   partial class LabelFormatPanel : ICS.GUI_Library.Controls.WizardControlPanels.WizardControlPanel
   {
      protected System.Windows.Forms.Label _lblToMessage;
      protected System.Windows.Forms.Label _lblToFilePath;
      protected System.Windows.Forms.TextBox _txtToFilePath;
      protected System.Windows.Forms.Button _btnToBrowse;

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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelFormatPanel));
         this._lblToMessage = new System.Windows.Forms.Label();
         this._lblToFilePath = new System.Windows.Forms.Label();
         this._txtToFilePath = new System.Windows.Forms.TextBox();
         this._btnToBrowse = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // _lblToMessage
         // 
         resources.ApplyResources(this._lblToMessage, "_lblToMessage");
         this._lblToMessage.Name = "_lblToMessage";
         // 
         // _lblToFilePath
         // 
         resources.ApplyResources(this._lblToFilePath, "_lblToFilePath");
         this._lblToFilePath.Name = "_lblToFilePath";
         // 
         // _txtToFilePath
         // 
         resources.ApplyResources(this._txtToFilePath, "_txtToFilePath");
         this._txtToFilePath.Name = "_txtToFilePath";
         // 
         // _btnToBrowse
         // 
         resources.ApplyResources(this._btnToBrowse, "_btnToBrowse");
         this._btnToBrowse.Name = "_btnToBrowse";
         this._btnToBrowse.Click += new System.EventHandler(this.GetFilePathFromFileDialog);
         // 
         // LabelFormatPanel
         // 
         this.Controls.Add(this._btnToBrowse);
         this.Controls.Add(this._txtToFilePath);
         this.Controls.Add(this._lblToFilePath);
         this.Controls.Add(this._lblToMessage);
         this.Name = "LabelFormatPanel";
         resources.ApplyResources(this, "$this");
         this.LeavePanel += new ICS.GUI_Library.Controls.WizardControlPanels.LeavePanelEventHandler(this.ValidateLabelFormatFilePath);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
   }
}
