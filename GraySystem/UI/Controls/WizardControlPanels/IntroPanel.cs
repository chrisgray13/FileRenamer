#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\IntroPanel.cs-arc  $
 * $Revision:   1.2  $
 * $Author:   cgray  $
 * $Date:   May 19 2008 21:31:38  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\IntroPanel.cs-arc  $
 * 
 *    Rev 1.2   May 19 2008 21:31:38   cgray
 * BE25492 - Added a new constructor that takes a Wizard object and sets it as the wizard (parent) of the control panel.
 * 
 * Added a call to the InitializeComponent method within the base constructor to ensure the Design-time view works correctly.
 * 
 * Removed the constructor that takes the Message parameter as the message is no longer set via a class field but a resource.
 * 
 *    Rev 1.1   Nov 08 2007 14:36:00   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.0   Feb 12 2007 15:00:50   cgray
 * Initial revision.
 * 
 *
 *
 */

#endregion

#region Usings

using System.Windows.Forms;

#endregion


namespace GraySystem.UI.Controls.WizardControlPanels
{
   /// <summary>
   /// Intro Panel class is used to Display some sort of message to the user about the application.
   /// It may also include a menu selection of some sort to allow the user to specify a specific
   /// task they might like to complete.
   /// </summary>
   public class IntroPanel : WizardControlPanel
   {
      #region Fields

      #region Controls

      /// <summary>
      /// Displays a message to the user indicating the function of the wizard.
      /// </summary>
      protected Label _lblMessage;

      #endregion

      private System.ComponentModel.IContainer _components = null;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a new IntroPanel object and sets the heading used for the panel's summary.
      /// </summary>
      protected IntroPanel() : base()
      {
         InitializeComponent();
      } // end IntroPanel constructor

      /// <summary>
      /// Constructs a new IntroPanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      public IntroPanel(GraySystem.UI.Forms.Wizard wizParent) : base(wizParent)
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntroPanel));
         this._lblMessage = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // _lblMessage
         // 
         resources.ApplyResources(this._lblMessage, "_lblMessage");
         this._lblMessage.Name = "_lblMessage";
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
} // end GraySystem.UI.Controls.WizardControlPanels Namespace