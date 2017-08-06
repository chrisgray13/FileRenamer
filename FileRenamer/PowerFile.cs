#region Usings

using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

#endregion


namespace FileRenamer
{
   /// <summary>
   /// A "beefed-up" File class, which also contains functionality to retrieve metadata
   /// from metafiles.
   /// </summary>
   public class PowerFile
   {
      #region Fields

      private string _sPath;
      private string _sFileName;
      private string _sExtension;

      private string _sNewFileName;

      #endregion

      #region Properties

      #region FileName

      /// <summary>
      /// Gets the file name of the PowerFile.
      /// </summary>
      public string FileName
      {
         get { return (_sFileName); }
      } // end FileName property

      #endregion

      #region NewFileName

      /// <summary>
      /// Gets the new filename
      /// </summary>
      public string NewFileName
      {
         get { return (_sNewFileName); }
      } // end NewFileName property

      #endregion

      #region Path

      /// <summary>
      /// Gets the path of the PowerFile
      /// </summary>
      public string Path
      {
         get
         {
            int iPathEnd;

            if (_sPath.Length == 0)
            {
               iPathEnd = _sFileName.LastIndexOf('\\');
               _sPath = _sFileName.Substring(0, iPathEnd);
            } // end if

            return (_sPath);
         } // end get
      } // end Path property

      #endregion

      #region Extension

      /// <summary>
      /// Gets the extension of the PowerFile
      /// </summary>
      public string Extension
      {
         get
         {
            int iExtStart;

            if (_sExtension.Length == 0)
            {
               iExtStart = _sFileName.LastIndexOf('.');
               _sExtension = _sFileName.Substring(iExtStart, _sFileName.Length - iExtStart);
            } // end if

            return (_sExtension);
         } // end get
      } // end Extension property

      #endregion

      #region DatePictureTaken

      /// <summary>
      /// Gets the DateTime of the power file.  NOTE:  this is a metafile attribute.
      /// </summary>
      public string DatePictureTaken
      {
         get
         {
            try
            {
               byte[] bDateTimeProperty;

               using (Image image = Image.FromFile(_sFileName))
               {
                  bDateTimeProperty = image.GetPropertyItem(0x0132).Value;

                  return ((new ASCIIEncoding()).GetString(bDateTimeProperty,
                                                          0,
                                                          bDateTimeProperty.Length - 1));
               } // end using
            } // end try
            catch
            {
               return (null);
            } // end catch
         } // end get
      } // end DatePictureTaken property

      #endregion

      #region Title

      /// <summary>
      /// Gets the title of the power file.  NOTE:  this is a metafile attribute.
      /// </summary>
      public string Title
      {
         get
         {
            try
            {
               byte[] bTitleProperty;

               using (Image image = Image.FromFile(_sFileName))
               {
                  bTitleProperty = image.GetPropertyItem(0x0320).Value;

                  return ((new ASCIIEncoding()).GetString(bTitleProperty,
                                                          0,
                                                          bTitleProperty.Length - 1));
               } // end using
            } // end try
            catch
            {
               return (null);
            } // end catch
         } // end get
      } // end Title property

      #endregion

      #endregion

      #region Constructors

      /// <summary>
      /// Instantiates a new PowerFile object and initializes all of the fields including the
      /// FileName with the filename provided.
      /// </summary>
      /// <param name="sFileName">Filename for which the PowerFile object is created.</param>
      public PowerFile(string sFileName)
      {
         _sPath = "";
         _sFileName = sFileName;
         _sExtension = "";

         _sNewFileName = "";
      } // end PowerFile constructor

      #endregion

      #region Methods

      #region Rename

      /// <summary>
      /// Renames the file based on the new filename provided.
      /// </summary>
      /// <param name="sNewNamePrefix">New filename prefix, which is the data pulled from
      /// an attribute or custom label defined by the user.</param>
      /// <param name="sNewNameSuffix">New filename suffix, which is the suffix created using
      /// the suffix mask defined by the user.</param>
      /// <param name="bTestRun">Determines if the renaming should actually be performed or
      /// just simulated.</param>
      /// <returns>Returns a FileRenamedEventArgs object, which stores information relating
      /// to the rename operation, which includes the original and new filenames, the result
      /// of the rename operation, and any message generated upon failure.</returns>
      public FileRenamedEventArgs Rename(string sNewNameBody, string sNewNamePrefix,
                                         string sNewNameSuffix, bool bTestRun)
      {
         string sNewName;

         try
         {
            if (sNewNameBody == null)
            {
               // Saving the old file name as the new to retain the correct list of file names
               _sNewFileName = _sFileName;

               return (new FileRenamedEventArgs(_sFileName, "", false,
                                                "File does not contain new name property specified." +
                                                Environment.NewLine));
            } // end if
            else
            {
               sNewName = FormatNewName(sNewNamePrefix + sNewNameBody + sNewNameSuffix + Extension);

               if (bTestRun)
               {
                  _sNewFileName = _sFileName;
               } // end if
               else
               {
                  File.Move(_sFileName, sNewName);
                  _sNewFileName = sNewName;
               } // end else

               return (new FileRenamedEventArgs(_sFileName, sNewName, true, ""));
            } // end else
         } // end try
         catch (Exception ex)
         {
            // Saving the old file name as the new to retain the correct list of file names
            _sNewFileName = _sFileName;

            sNewName = FormatNewName(sNewNamePrefix + sNewNameBody + sNewNameSuffix + Extension);

            return (new FileRenamedEventArgs(_sFileName, sNewName, false, ex.Message));
         } // end catch
      } // end Rename

      #endregion

      #region FormatNewName

      private string FormatNewName(string sNewName)
      {
         return (sNewName.Replace(':', '.'));  // Removing colons to ensure the name is valid
      } // end FormatNewName

      #endregion

      #endregion

      #region Bitmap Property Ids

      /**********************************************************
       * ID      | Property tag
       *-------------------------------------------------------
       * 0x0000  | PropertyTagGpsVer 
       * 0x0001  | PropertyTagGpsLatitudeRef 
       * 0x0002  | PropertyTagGpsLatitude 
       * 0x0003  | PropertyTagGpsLongitudeRef 
       * 0x0004  | PropertyTagGpsLongitude 
       * 0x0005  | PropertyTagGpsAltitudeRef 
       * 0x0006  | PropertyTagGpsAltitude 
       * 0x0007  | PropertyTagGpsGpsTime 
       * 0x0008  | PropertyTagGpsGpsSatellites 
       * 0x0009  | PropertyTagGpsGpsStatus 
       * 0x000A  | PropertyTagGpsGpsMeasureMode 
       * 0x000B  | PropertyTagGpsGpsDop 
       * 0x000C  | PropertyTagGpsSpeedRef 
       * 0x000D  | PropertyTagGpsSpeed 
       * 0x000E  | PropertyTagGpsTrackRef 
       * 0x000F  | PropertyTagGpsTrack 
       * 0x0010  | PropertyTagGpsImgDirRef 
       * 0x0011  | PropertyTagGpsImgDir 
       * 0x0012  | PropertyTagGpsMapDatum 
       * 0x0013  | PropertyTagGpsDestLatRef 
       * 0x0014  | PropertyTagGpsDestLat 
       * 0x0015  | PropertyTagGpsDestLongRef 
       * 0x0016  | PropertyTagGpsDestLong 
       * 0x0017  | PropertyTagGpsDestBearRef 
       * 0x0018  | PropertyTagGpsDestBear 
       * 0x0019  | PropertyTagGpsDestDistRef 
       * 0x001A  | PropertyTagGpsDestDist 
       * 0x00FE  | PropertyTagNewSubfileType 
       * 0x00FF  | PropertyTagSubfileType 
       * 0x0100  | PropertyTagImageWidth 
       * 0x0101  | PropertyTagImageHeight 
       * 0x0102  | PropertyTagBitsPerSample 
       * 0x0103  | PropertyTagCompression 
       * 0x0106  | PropertyTagPhotometricInterp 
       * 0x0107  | PropertyTagThreshHolding 
       * 0x0108  | PropertyTagCellWidth 
       * 0x0109  | PropertyTagCellHeight 
       * 0x010A  | PropertyTagFillOrder 
       * 0x010D  | PropertyTagDocumentName 
       * 0x010E  | PropertyTagImageDescription 
       * 0x010F  | PropertyTagEquipMake 
       * 0x0110  | PropertyTagEquipModel 
       * 0x0111  | PropertyTagStripOffsets 
       * 0x0112  | PropertyTagOrientation 
       * 0x0115  | PropertyTagSamplesPerPixel 
       * 0x0116  | PropertyTagRowsPerStrip 
       * 0x0117  | PropertyTagStripBytesCount 
       * 0x0118  | PropertyTagMinSampleValue 
       * 0x0119  | PropertyTagMaxSampleValue 
       * 0x011A  | PropertyTagXResolution 
       * 0x011B  | PropertyTagYResolution 
       * 0x011C  | PropertyTagPlanarConfig 
       * 0x011D  | PropertyTagPageName 
       * 0x011E  | PropertyTagXPosition 
       * 0x011F  | PropertyTagYPosition 
       * 0x0120  | PropertyTagFreeOffset 
       * 0x0121  | PropertyTagFreeByteCounts 
       * 0x0122  | PropertyTagGrayResponseUnit 
       * 0x0123  | PropertyTagGrayResponseCurve 
       * 0x0124  | PropertyTagT4Option 
       * 0x0125  | PropertyTagT6Option 
       * 0x0128  | PropertyTagResolutionUnit 
       * 0x0129  | PropertyTagPageNumber 
       * 0x012D  | PropertyTagTransferFunction 
       * 0x0131  | PropertyTagSoftwareUsed 
       * 0x0132  | PropertyTagDateTime 
       * 0x013B  | PropertyTagArtist 
       * 0x013C  | PropertyTagHostComputer 
       * 0x013D  | PropertyTagPredictor 
       * 0x013E  | PropertyTagWhitePoint 
       * 0x013F  | PropertyTagPrimaryChromaticities 
       * 0x0140  | PropertyTagColorMap 
       * 0x0141  | PropertyTagHalftoneHints 
       * 0x0142  | PropertyTagTileWidth 
       * 0x0143  | PropertyTagTileLength 
       * 0x0144  | PropertyTagTileOffset 
       * 0x0145  | PropertyTagTileByteCounts 
       * 0x014C  | PropertyTagInkSet 
       * 0x014D  | PropertyTagInkNames 
       * 0x014E  | PropertyTagNumberOfInks 
       * 0x0150  | PropertyTagDotRange 
       * 0x0151  | PropertyTagTargetPrinter 
       * 0x0152  | PropertyTagExtraSamples 
       * 0x0153  | PropertyTagSampleFormat 
       * 0x0154  | PropertyTagSMinSampleValue 
       * 0x0155  | PropertyTagSMaxSampleValue 
       * 0x0156  | PropertyTagTransferRange 
       * 0x0200  | PropertyTagJPEGProc 
       * 0x0201  | PropertyTagJPEGInterFormat 
       * 0x0202  | PropertyTagJPEGInterLength 
       * 0x0203  | PropertyTagJPEGRestartInterval 
       * 0x0205  | PropertyTagJPEGLosslessPredictors 
       * 0x0206  | PropertyTagJPEGPointTransforms 
       * 0x0207  | PropertyTagJPEGQTables 
       * 0x0208  | PropertyTagJPEGDCTables 
       * 0x0209  | PropertyTagJPEGACTables 
       * 0x0211  | PropertyTagYCbCrCoefficients 
       * 0x0212  | PropertyTagYCbCrSubsampling 
       * 0x0213  | PropertyTagYCbCrPositioning 
       * 0x0214  | PropertyTagREFBlackWhite 
       * 0x0301  | PropertyTagGamma 
       * 0x0302  | PropertyTagICCProfileDescriptor 
       * 0x0303  | PropertyTagSRGBRenderingIntent 
       * 0x0320  | PropertyTagImageTitle 
       * 0x5001  | PropertyTagResolutionXUnit 
       * 0x5002  | PropertyTagResolutionYUnit 
       * 0x5003  | PropertyTagResolutionXLengthUnit 
       * 0x5004  | PropertyTagResolutionYLengthUnit 
       * 0x5005  | PropertyTagPrintFlags 
       * 0x5006  | PropertyTagPrintFlagsVersion 
       * 0x5007  | PropertyTagPrintFlagsCrop 
       * 0x5008  | PropertyTagPrintFlagsBleedWidth 
       * 0x5009  | PropertyTagPrintFlagsBleedWidthScale 
       * 0x500A  | PropertyTagHalftoneLPI 
       * 0x500B  | PropertyTagHalftoneLPIUnit 
       * 0x500C  | PropertyTagHalftoneDegree 
       * 0x500D  | PropertyTagHalftoneShape 
       * 0x500E  | PropertyTagHalftoneMisc 
       * 0x500F  | PropertyTagHalftoneScreen 
       * 0x5010  | PropertyTagJPEGQuality 
       * 0x5011  | PropertyTagGridSize 
       * 0x5012  | PropertyTagThumbnailFormat 
       * 0x5013  | PropertyTagThumbnailWidth 
       * 0x5014  | PropertyTagThumbnailHeight 
       * 0x5015  | PropertyTagThumbnailColorDepth 
       * 0x5016  | PropertyTagThumbnailPlanes 
       * 0x5017  | PropertyTagThumbnailRawBytes 
       * 0x5018  | PropertyTagThumbnailSize 
       * 0x5019  | PropertyTagThumbnailCompressedSize 
       * 0x501A  | PropertyTagColorTransferFunction 
       * 0x501B  | PropertyTagThumbnailData 
       * 0x5020  | PropertyTagThumbnailImageWidth 
       * 0x5021  | PropertyTagThumbnailImageHeight 
       * 0x5022  | PropertyTagThumbnailBitsPerSample 
       * 0x5023  | PropertyTagThumbnailCompression 
       * 0x5024  | PropertyTagThumbnailPhotometricInterp 
       * 0x5025  | PropertyTagThumbnailImageDescription 
       * 0x5026  | PropertyTagThumbnailEquipMake 
       * 0x5027  | PropertyTagThumbnailEquipModel 
       * 0x5028  | PropertyTagThumbnailStripOffsets 
       * 0x5029  | PropertyTagThumbnailOrientation 
       * 0x502A  | PropertyTagThumbnailSamplesPerPixel 
       * 0x502B  | PropertyTagThumbnailRowsPerStrip 
       * 0x502C  | PropertyTagThumbnailStripBytesCount 
       * 0x502D  | PropertyTagThumbnailResolutionX 
       * 0x502E  | PropertyTagThumbnailResolutionY 
       * 0x502F  | PropertyTagThumbnailPlanarConfig 
       * 0x5030  | PropertyTagThumbnailResolutionUnit 
       * 0x5031  | PropertyTagThumbnailTransferFunction 
       * 0x5032  | PropertyTagThumbnailSoftwareUsed 
       * 0x5033  | PropertyTagThumbnailDateTime 
       * 0x5034  | PropertyTagThumbnailArtist 
       * 0x5035  | PropertyTagThumbnailWhitePoint 
       * 0x5036  | PropertyTagThumbnailPrimaryChromaticities 
       * 0x5037  | PropertyTagThumbnailYCbCrCoefficients 
       * 0x5038  | PropertyTagThumbnailYCbCrSubsampling 
       * 0x5039  | PropertyTagThumbnailYCbCrPositioning 
       * 0x503A  | PropertyTagThumbnailRefBlackWhite 
       * 0x503B  | PropertyTagThumbnailCopyRight 
       * 0x5090  | PropertyTagLuminanceTable 
       * 0x5091  | PropertyTagChrominanceTable 
       * 0x5100  | PropertyTagFrameDelay 
       * 0x5101  | PropertyTagLoopCount 
       * 0x5102  | PropertyTagGlobalPalette 
       * 0x5103  | PropertyTagIndexBackground 
       * 0x5104  | PropertyTagIndexTransparent 
       * 0x5110  | PropertyTagPixelUnit 
       * 0x5111  | PropertyTagPixelPerUnitX 
       * 0x5112  | PropertyTagPixelPerUnitY 
       * 0x5113  | PropertyTagPaletteHistogram 
       * 0x8298  | PropertyTagCopyright 
       * 0x829A  | PropertyTagExifExposureTime 
       * 0x829D  | PropertyTagExifFNumber 
       * 0x8769  | PropertyTagExifIFD 
       * 0x8773  | PropertyTagICCProfile 
       * 0x8822  | PropertyTagExifExposureProg 
       * 0x8824  | PropertyTagExifSpectralSense 
       * 0x8825  | PropertyTagGpsIFD 
       * 0x8827  | PropertyTagExifISOSpeed 
       * 0x8828  | PropertyTagExifOECF 
       * 0x9000  | PropertyTagExifVer 
       * 0x9003  | PropertyTagExifDTOrig 
       * 0x9004  | PropertyTagExifDTDigitized 
       * 0x9101  | PropertyTagExifCompConfig 
       * 0x9102  | PropertyTagExifCompBPP 
       * 0x9201  | PropertyTagExifShutterSpeed 
       * 0x9202  | PropertyTagExifAperture 
       * 0x9203  | PropertyTagExifBrightness 
       * 0x9204  | PropertyTagExifExposureBias 
       * 0x9205  | PropertyTagExifMaxAperture 
       * 0x9206  | PropertyTagExifSubjectDist 
       * 0x9207  | PropertyTagExifMeteringMode 
       * 0x9208  | PropertyTagExifLightSource 
       * 0x9209  | PropertyTagExifFlash 
       * 0x920A  | PropertyTagExifFocalLength 
       * 0x927C  | PropertyTagExifMakerNote 
       * 0x9286  | PropertyTagExifUserComment 
       * 0x9290  | PropertyTagExifDTSubsec 
       * 0x9291  | PropertyTagExifDTOrigSS 
       * 0x9292  | PropertyTagExifDTDigSS 
       * 0xA000  | PropertyTagExifFPXVer 
       * 0xA001  | PropertyTagExifColorSpace 
       * 0xA002  | PropertyTagExifPixXDim 
       * 0xA003  | PropertyTagExifPixYDim 
       * 0xA004  | PropertyTagExifRelatedWav 
       * 0xA005  | PropertyTagExifInterop 
       * 0xA20B  | PropertyTagExifFlashEnergy 
       * 0xA20C  | PropertyTagExifSpatialFR 
       * 0xA20E  | PropertyTagExifFocalXRes 
       * 0xA20F  | PropertyTagExifFocalYRes 
       * 0xA210  | PropertyTagExifFocalResUnit 
       * 0xA214  | PropertyTagExifSubjectLoc 
       * 0xA215  | PropertyTagExifExposureIndex 
       * 0xA217  | PropertyTagExifSensingMethod 
       * 0xA300  | PropertyTagExifFileSource 
       * 0xA301  | PropertyTagExifSceneType 
       * 0xA302  | PropertyTagExifCfaPattern 
       **********************************************************/

      #endregion
   } // end PowerFile Class
} // end FileRenamer Namespace