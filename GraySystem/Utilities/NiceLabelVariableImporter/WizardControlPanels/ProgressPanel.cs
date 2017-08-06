#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\ProgressPanel.cs-arc  $
 * $Revision:   1.1  $
 * $Author:   pmonaco  $
 * $Date:   Nov 08 2007 14:36:02  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\ProgressPanel.cs-arc  $
 * 
 *    Rev 1.1   Nov 08 2007 14:36:02   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.0   Feb 12 2007 15:06:26   cgray
 * Initial revision.
 * 
 *
 *
 */

#endregion

#region Usings

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using ICS.Utilities;
using ICS.Utilities.EventArguments;

#endregion


namespace ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels
{
   /// <summary>
   /// ProgressPanel Class is used to display the progress of the task to the user.
   /// </summary>
   public partial class ProgressPanel : ICS.GUI_Library.Controls.WizardControlPanels.ProgressPanel
   {
      #region Constructors

      /// <summary>
      /// Constructs a new ProgressPanel object.
      /// </summary>
      protected ProgressPanel() : base()
      {
         InitializeComponent();
      } // end ProgressPanel constructor

      /// <summary>
      /// Constructs a new ProgressPanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      public ProgressPanel(ICS.GUI_Library.Forms.Wizard wizParent) : base(wizParent)
      {
      } // end ProgressPanel constructor

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
   } // end ProgressPanel Class
} // end ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels Namespace