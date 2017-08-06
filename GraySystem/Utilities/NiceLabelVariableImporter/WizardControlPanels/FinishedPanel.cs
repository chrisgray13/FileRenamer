#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\FinishedPanel.cs-arc  $
 * $Revision:   1.1  $
 * $Author:   pmonaco  $
 * $Date:   Nov 08 2007 14:36:04  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\FinishedPanel.cs-arc  $
 * 
 *
 *
 */

#endregion

#region Usings

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion


namespace ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels
{
   /// <summary>
   /// FinishedPanel Class displays to the user the results of the wizard's process.
   /// </summary>
   public partial class FinishedPanel : ICS.GUI_Library.Controls.WizardControlPanels.FinishedPanel
   {
      #region Constructors

      /// <summary>
      /// Constructs a new FinishedPanel object.
      /// </summary>
      protected FinishedPanel() : base()
      {
         InitializeComponent();
      } // end FinishedPanel constructor

      /// <summary>
      /// Constructs a new FinishedPanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      public FinishedPanel(ICS.GUI_Library.Forms.Wizard wizParent) : base(wizParent)
      {
      } // end FinishedPanel constructor

      /// <summary>
      /// Constructs a new FinishedPanel object and sets the heading used for the process's
      /// result.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      /// <param name="sResultHeading">Sets the heading used for the Result.  This should
      /// be something identifying the task being executed.  Fill in the following:
      /// "___________ was Successful!"  If nothing is specified, the default will be used,
      /// which is "The task".</param>
      public FinishedPanel(ICS.GUI_Library.Forms.Wizard wizParent, string sResultHeading) : base(wizParent, sResultHeading)
      {
      } // end FinishedPanel constructor

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
   }
}
