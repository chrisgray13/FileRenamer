#region PVCS Comments

/*
 * Copyright ICS, Inc. 2005
 * All rights are reserved. Reproduction or transmission in whole or in part,
 * in any form or by any means, electronic, mechanical or otherwise, is
 * prohibited without the prior written consent of the copyright owner.
 *
 * $Archive:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardInstaller.cs-arc  $
 * $Revision:   1.0  $
 * $Author:   cgray  $
 * $Date:   Jul 21 2006 17:37:10  $
 *
 * $Log:   \\filer\rfsow\pvcsproj\rfsmart3\archives\Base\RTTC\ICS\Utilities\UpgradeTool\WizardInstaller.cs-arc  $
 * 
 *
 *
 */

#endregion

#region Usings

using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

#endregion


namespace ICS.Utilities.UpgradeTool
{
   /// <summary>
   /// Summary description for WizardInstaller.
   /// </summary>
   [RunInstaller(true)]
   public class WizardInstaller : System.Configuration.Install.Installer
   {
      #region Fields

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructs a WizardInstaller object and initializes the component
      /// </summary>
      public WizardInstaller()
      {
         // This call is required by the Designer.
         InitializeComponent();
      } // end WizardInstaller constructor

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
            if (components != null)
            {
               components.Dispose();
            } // end if
         } // end if

         base.Dispose(disposing);
      } // end Dispose

      #endregion

      #region Methods

      #region Install

      /// <summary>
      /// 
      /// </summary>
      /// <param name="stateSaver"></param>
      public override void Install(IDictionary stateSaver)
      {
         try
         {
            // This is supposed to be done first based on the API when you override this method
            base.Install (stateSaver);

            DirectoryInfo dirInfo = new DirectoryInfo((new Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(dirInfo.Parent.FullName + "\\DataExporter-RFS.exe.config");

            XmlNodeList xmlNodeList = xmlDoc.DocumentElement.GetElementsByTagName("add");

            // Correctly applying the application path to the config file
            xmlNodeList[0].Attributes["value"].Value = dirInfo.Parent.Parent.FullName;
            xmlDoc.Save(dirInfo.Parent.FullName + "\\DataExporter-RFS.exe.config");
         } // end try
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         } // end catch
      } // end Install

      #endregion

      #region InitializeComponent

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         components = new System.ComponentModel.Container();
      } // end InitializeComponent

      #endregion

      #endregion
   } // end WizardInstaller Class
} // end ICS.Utilities.UpgradeTool Namespace