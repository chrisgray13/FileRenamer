namespace ICS.Utilities.NiceLabelVariableImporter
{
   partial class NiceLabelVariableImportWizard : ICS.GUI_Library.Forms.Wizard
   {
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NiceLabelVariableImportWizard));
         ((System.ComponentModel.ISupportInitialize) (this._picBanner)).BeginInit();
         this._pnlButtons.SuspendLayout();
         this.SuspendLayout();
         // 
         // _picBanner
         // 
         this._picBanner.Image = ((System.Drawing.Image) (resources.GetObject("_picBanner.Image")));
         // 
         // NiceLabelVariableImportWizard
         // 
         this.ClientSize = new System.Drawing.Size(464, 382);
         this.Name = "NiceLabelVariableImportWizard";
         this.Text = "NiceLabel Variable Importer";
         ((System.ComponentModel.ISupportInitialize) (this._picBanner)).EndInit();
         this._pnlButtons.ResumeLayout(false);
         this.ResumeLayout(false);
         this.Load += new System.EventHandler(LoadWizard);

      }

      #endregion
   }
}

