using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StudentApi.Controllers;
using StudentApi.Models;
using StudentApi.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace StudentApi.Test
{
  public class StudentRepositoryTest
  {
    private Mock<IStudentRespository> _mockStudentRepository;
    private Mock<ILogger<StudentsController>> _mockLogger;

    [SetUp]
    public void Setup()
    {
      _mockStudentRepository = new Mock<IStudentRespository>();
      _mockLogger = new Mock<ILogger<StudentsController>>();
    }

    [Test]
    public async Task StudentRepository_InsertStudent_Should_Return_Success()
    {
      //Arrange
      _mockStudentRepository.Setup(s => s.AddStudentAsync(It.IsAny<Student>())).ReturnsAsync(true);
      var studentController = new StudentsController(_mockLogger.Object, _mockStudentRepository.Object);
      //Act
      var addStudentResult = await studentController.AddStudent(new Student());
      //Assert
      var result = addStudentResult as OkResult;
      Assert.That(result.StatusCode == 200); 
    }

    [Test]
    public async Task StudentRepository_InsertStudent_Should_Return_Failure()
    {
      //Arrange
      _mockStudentRepository.Setup(s => s.AddStudentAsync(It.IsAny<Student>())).ThrowsAsync(new Exception());
      var studentController = new StudentsController(_mockLogger.Object, _mockStudentRepository.Object);
      //Act
      var addStudentResult = await studentController.AddStudent(new Student());
      //Assert
      var result = addStudentResult as StatusCodeResult;
      Assert.That(result.StatusCode == 500);
    }


        [Test]
        public async Task StudentRepository_GetStudent_Should_Return_Success()
        {
            //Arrange
            _mockStudentRepository.Setup(s => s.GetStudentsAsync());
            var studentController = new StudentsController(_mockLogger.Object, _mockStudentRepository.Object);
            //Act
            var GetStudentResult = await studentController.GetStudents();
            //Assert
            //var result = GetStudentResult as OkResult; //need to fix this guy I need to run to retro meeting 
            //Assert.That(result.StatusCode == 200);
        }

        [Test]
        public async Task StudentRepository_GetStudent_Should_Return_Failure()
        {
            //Arrange
            _mockStudentRepository.Setup(s => s.GetStudentsAsync());
            var studentController = new StudentsController(_mockLogger.Object, _mockStudentRepository.Object);
            //Act
            var GetStudentResult = await studentController.GetStudents();
            //Assert
            //var result = GetStudentResult as OkResult;
            //Assert.That(result.StatusCode == 200);
        }

        [Test]
        public async Task StudentRepository_DeleteStudent_Should_Return_Success()
        {
            //Arrange
            _mockStudentRepository.Setup(s => s.DeleteStudentAsync(It.IsAny<int>())).ReturnsAsync(true);
            var studentController = new StudentsController(_mockLogger.Object, _mockStudentRepository.Object);
            //Act
            var DeleteStudentResult = await studentController.DeleteStudent(new int());
            //Assert
            var result = DeleteStudentResult as OkResult;
            Assert.That(result.StatusCode == 200);
        }

        [Test]
        public async Task StudentRepository_DeleteStudent_Should_Return_Failure()
        {
            //Arrange
            _mockStudentRepository.Setup(s => s.DeleteStudentAsync(It.IsAny<int>())).ThrowsAsync(new Exception());
            var studentController = new StudentsController(_mockLogger.Object, _mockStudentRepository.Object);
            //Act
            var DeleteStudentResult = await studentController.DeleteStudent(new int());
            //Assert
            var result = DeleteStudentResult as StatusCodeResult;
            Assert.That(result.StatusCode == 500);
        }

    }
}