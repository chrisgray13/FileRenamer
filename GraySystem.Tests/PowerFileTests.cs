using System;
using System.Text;
using GraySystem.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GraySystem.Tests
{
    [TestClass]
    public class PowerFileTests
    {
        [TestMethod]
        public void CanSaveDateTaken()
        {
            var file = new PowerFile(@"C:\Users\cgray\Downloads\controlourwip.jpg");
            var newDate = file.DatePictureTaken.AddHours(-12);
            var result = file.UpdateDateTaken(newDate, false);

            Assert.IsTrue(result.Result, result.Message);

            file = new PowerFile(@"C:\Users\cgray\Downloads\controlourwip.jpg");

            Assert.AreEqual(newDate.ToString(), file.DatePictureTaken.ToString(), "Dates do not match");
        }
    }
}
