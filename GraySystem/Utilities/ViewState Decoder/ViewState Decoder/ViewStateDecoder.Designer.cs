namespace ViewState_Decoder
{
   partial class ViewStateDecoder
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
         this._txtEncodedViewState = new System.Windows.Forms.TextBox();
         this._txtDecodedViewState = new System.Windows.Forms.TextBox();
         this._btnDecode = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // _txtEncodedViewState
         // 
         this._txtEncodedViewState.Location = new System.Drawing.Point(12, 12);
         this._txtEncodedViewState.MaxLength = 100000;
         this._txtEncodedViewState.Multiline = true;
         this._txtEncodedViewState.Name = "_txtEncodedViewState";
         this._txtEncodedViewState.Size = new System.Drawing.Size(768, 293);
         this._txtEncodedViewState.TabIndex = 0;
         // 
         // _txtDecodedViewState
         // 
         this._txtDecodedViewState.Location = new System.Drawing.Point(12, 367);
         this._txtDecodedViewState.Multiline = true;
         this._txtDecodedViewState.Name = "_txtDecodedViewState";
         this._txtDecodedViewState.Size = new System.Drawing.Size(768, 353);
         this._txtDecodedViewState.TabIndex = 1;
         // 
         // _btnDecode
         // 
         this._btnDecode.Location = new System.Drawing.Point(355, 322);
         this._btnDecode.Name = "_btnDecode";
         this._btnDecode.Size = new System.Drawing.Size(75, 23);
         this._btnDecode.TabIndex = 2;
         this._btnDecode.Text = "Decode";
         this._btnDecode.UseVisualStyleBackColor = true;
         this._btnDecode.Click += new System.EventHandler(this._btnDecode_Click);
         // 
         // ViewStateDecoder
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(792, 732);
         this.Controls.Add(this._btnDecode);
         this.Controls.Add(this._txtDecodedViewState);
         this.Controls.Add(this._txtEncodedViewState);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Name = "ViewStateDecoder";
         this.Text = "View State Decoder";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox _txtEncodedViewState;
      private System.Windows.Forms.TextBox _txtDecodedViewState;
      private System.Windows.Forms.Button _btnDecode;
   }
}

