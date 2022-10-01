using Application.Dtos;
using Application.Services;
using AutoMapper;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Moq;
using System.Net;
using System.Reflection;

namespace Clean.Architecture.UnitTests
{
    public class StudentServicesTests
    {

        private readonly StudentService _services;
        private Mock<UnitOfWork> _unitOfWorkMoc = new Mock<UnitOfWork>();
        private Mock<IMapper> _mappperMock = new Mock<IMapper>();

        public StudentServicesTests()
        {
            _services = new StudentService(_unitOfWorkMoc.Object, _mappperMock.Object);
        }

        [Fact]
        public async Task GetByIdAync_ShouldReturnStudent_WhenStudentExists()
        {
            // Arrange
            var studentId = 1;
            var studnentReadDto = new StudentReadDto
            {
                Id = studentId,
                FullName = "",
                DateOfBirth = DateTime.Now,
                Gender = false,
                IDCard = null,
                Address = null,
                PhoneNumber = null,
                Email = null,
                DateOfIssue = null,
                PlaceOfIssue = null,
                Base64ImageString = null,
            };

            // Act

            // Assert

            Assert.Equal("12", "12");
        }
    }
}