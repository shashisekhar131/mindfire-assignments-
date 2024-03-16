using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BookAnAppointment.DAL;
using BookAnAppointment.Models;
using BookAnAppointment.Controllers;

namespace BookAnAppointment.MSUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        //private DataAccess dataAccess;

       

        //[TestInitialize]
        //public void Setup()
        //{
        //     dataAccess = new DataAccess();
        //}

        [TestMethod]
        public void TestForDoctorLogin()
        {
            // Arrange
            var controller = new DoctorLoginController();

            // Act
            var doctorId = controller.CheckIfDoctorExists("xyz", "123");

            // Assert
            Assert.IsTrue(Convert.ToInt32(doctorId.Data) > 0); 
        }      
    }
}
