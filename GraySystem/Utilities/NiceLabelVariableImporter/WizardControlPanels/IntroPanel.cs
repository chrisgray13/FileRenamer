#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\IntroPanel.cs-arc  $
 * $Revision:   1.1  $
 * $Author:   pmonaco  $
 * $Date:   Nov 08 2007 14:36:00  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\IntroPanel.cs-arc  $
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

using System;
using System.Windows.Forms;

#endregion


namespace ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels
{
   /// <summary>
   /// Intro Panel class is used to Display some sort of message to the user about the application.
   /// It may also include a menu selection of some sort to allow the user to specify a specific
   /// task they might like to complete.
   /// </summary>
   public partial class IntroPanel : ICS.GUI_Library.Controls.WizardControlPanels.IntroPanel
   {
      #region Constructors

      /// <summary>
      /// Constructs a new IntroPanel object.
      /// </summary>
      protected IntroPanel() : base()
      {
         InitializeComponent();
      } // end IntroPanel constructor

      /// <summary>
      /// Constructs a new IntroPanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      public IntroPanel(ICS.GUI_Library.Forms.Wizard wizParent) : base(wizParent)
      {
      } // end IntroPanel constructor

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

      #endregion
   } // end IntroPanel Class
} // end ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels Namespace