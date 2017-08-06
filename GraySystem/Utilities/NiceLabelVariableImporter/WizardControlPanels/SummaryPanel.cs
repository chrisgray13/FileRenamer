#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\SummaryPanel.cs-arc  $
 * $Revision:   1.1  $
 * $Author:   pmonaco  $
 * $Date:   Nov 08 2007 14:36:06  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\GUI_Library\Controls\WizardControlPanels\SummaryPanel.cs-arc  $
 * 
 *    Rev 1.1   Nov 08 2007 14:36:06   pmonaco
 * BE022677 - Added fixes to handle globalization checks in FxCop
 * 
 *    Rev 1.0   Feb 12 2007 15:06:50   cgray
 * Initial revision.
 * 
 *    Rev 1.1   Jul 25 2006 00:02:00   cgray
 * BE016512 - Changed comparisions using the empty string to check that the length is zero for enhanced performance.
 * 
 *    Rev 1.0   Jul 21 2006 17:37:44   cgray
 * Initial revision.
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
   /// SummaryPanel Class is used to display a Summary of the user's selections in
   /// order that they may validate their selections before proceeding.
   /// </summary>
   public partial class SummaryPanel : ICS.GUI_Library.Controls.WizardControlPanels.SummaryPanel
   {
      #region Constructors

      /// <summary>
      /// Constructs a SummaryPanel object.
      /// </summary>
      protected SummaryPanel() : base()
      {
         InitializeComponent();
      } // end SummaryPanel constructor

      /// <summary>
      /// Constructs a SummaryPanel object.
      /// </summary>
      /// <param name="wizParent">Parent wizard of the control panel.</param>
      public SummaryPanel(ICS.GUI_Library.Forms.Wizard wizParent) : base(wizParent)
      {
      } // end SummaryPanel constructor

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
   } // end SummaryPanel Class
} // end ICS.Utilities.NiceLabelVariableImporter.WizardControlPanels Namespace