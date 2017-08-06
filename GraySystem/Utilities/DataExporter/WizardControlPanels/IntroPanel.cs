#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\IntroPanel.cs-arc  $
 * $Revision:   1.1  $
 * $Author:   cgray  $
 * $Date:   Jul 25 2006 00:00:16  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardControlPanels\IntroPanel.cs-arc  $
 * 
 *
 *
 */

#endregion

#region Usings

using System.Windows.Forms;

#endregion


namespace ICS.Utilities.DataExporter.WizardControlPanels
{
   /// <summary>
   /// Intro Panel class is used to Display some sort of message to the user about the application.
   /// It may also include a menu selection of some sort to allow the user to specify a specific
   /// task they might like to complete.
   /// </summary>
   public class IntroPanel : ICS.GUI_Library.Controls.WizardControlPanel
   {
      #region Fields

      private Label _lblMessage;

      private System.ComponentModel.IContainer _components = null;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new IntroPanel object.
      /// </summary>
      public IntroPanel() : base()
      {
      } // end IntroPanel constructor

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
            if (_components != null)
            {
               _components.Dispose();
            } // end if
         } // end if

         base.Dispose(disposing);
      } // end if

      #endregion

      #region Methods

      #region Initialize

      /// <summary>
      /// Initializes the panel by adding the controls and event handlers.
      /// </summary>
      protected override void Initialize()
      {
         // Checking to see if the panel has been initialized yet, which is denoted by the Name
         // being set as initially it is blank.
         if (Name.Length == 0)
         {
            base.Initialize();

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
         } // end if
      } // end Initialize

      #endregion

      #region InitializeComponent

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this._lblMessage = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // _lblMessage
         // 
         this._lblMessage.Location = new System.Drawing.Point(8, 8);
         this._lblMessage.Name = "_lblMessage";
         this._lblMessage.Size = new System.Drawing.Size(448, 104);
         this._lblMessage.TabIndex = 0;
         this._lblMessage.Text = "Welcome to the ICS, Inc. RFSmart Data Exporter.  " +
                                 "This tool is to be used to export data from RFSmart " +
                                 "System tables and ERP Business tables.";
         // 
         // IntroPanel
         // 
         this.Controls.Add(this._lblMessage);
         this.Name = "IntroPanel";
         this.ResumeLayout(false);

      } // end InitializeComponent

      #endregion

      #endregion
   } // end IntroPanel Class
} // end ICS.Utilities.DataExporter.WizardControlPanels Namespace