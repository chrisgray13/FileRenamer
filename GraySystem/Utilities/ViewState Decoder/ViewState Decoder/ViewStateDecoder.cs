using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ViewState_Decoder
{
   public partial class ViewStateDecoder : Form
   {
      public ViewStateDecoder()
      {
         InitializeComponent();
      }

      private void _btnDecode_Click(object sender, EventArgs e)
      {
         _txtDecodedViewState.Text = Encoding.UTF8.GetString(Convert.FromBase64String(this._txtEncodedViewState.Text));

         System.Web.UI.LosFormatter input = new System.Web.UI.LosFormatter();
         object temp = input.Deserialize(_txtDecodedViewState.Text);

      }
   }
}