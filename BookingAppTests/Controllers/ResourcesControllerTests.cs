﻿using BookingApp.Controllers;
using BookingApp.Data.Models;
using BookingApp.DTOs;
using BookingApp.DTOs.Resource;
using BookingApp.Exceptions;
using BookingApp.Services;
using BookingApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingUtilities;
using Xunit;

namespace BookingAppTests.Controllers
{
    public class ResourcesControllerTests
    {
        #region List() tests
        [Fact]
        public async void List_ReturnsSomeResources()
        {
            // Arrange 
            bool isAdmin = It.IsAny<bool>();

            var resServiceMock = new Mock<IResourcesService>();
            resServiceMock.Setup(service => service.GetList()).ReturnsAsync(new List<Resource>());
            resServiceMock.Setup(service => service.ListActive()).ReturnsAsync(new List<Resource>());

            var bookServiceMock = new Mock<IBookingsService>();
            var mappServiceMock = new Mock<IMapperService>();

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(isAdmin);

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.List();

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var dtos = Assert.IsAssignableFrom<IEnumerable<ResourceBriefDto>>(okResult.Value);
            resServiceMock.Verify(mock => mock.GetList(), Times.AtMostOnce());
            resServiceMock.Verify(mock => mock.ListActive(), Times.AtMostOnce());
        }

        [Fact]
        public async void List_ReturnsAllResources_ForAdmin()
        {
            // Arrange
            bool isAdmin = true;

            var resServiceMock = new Mock<IResourcesService>();
            resServiceMock.Setup(service => service.GetList()).ReturnsAsync(new List<Resource>());

            var bookServiceMock = new Mock<IBookingsService>();

            var mappServiceMock = new Mock<IMapperService>();
            mappServiceMock.Setup(service => service.Map<IEnumerable<ResourceBriefDto>>(It.IsAny<object>())).Returns(new List<ResourceBriefDto>());

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(isAdmin);

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.List();

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var dtos = Assert.IsAssignableFrom<IEnumerable<ResourceBriefDto>>(okResult.Value);
            resServiceMock.Verify(mock => mock.GetList(), Times.Once());
        }

        [Fact]
        public async void List_ReturnsActiveResources()
        {
            // Arrange
            bool isAdmin = It.IsAny<bool>();
            var resServiceMock = new Mock<IResourcesService>();
            resServiceMock.Setup(service => service.ListActive()).ReturnsAsync(new List<Resource>());

            var bookServiceMock = new Mock<IBookingsService>();

            var mappServiceMock = new Mock<IMapperService>();
            mappServiceMock.Setup(service => service.Map<IEnumerable<ResourceBriefDto>>(It.IsAny<object>())).Returns(new List<ResourceBriefDto>());

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(isAdmin);

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.List();

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var dtos = Assert.IsAssignableFrom<IEnumerable<ResourceBriefDto>>(okResult.Value);
            resServiceMock.Verify(mock => mock.ListActive(), Times.Once());
        }
        #endregion

        #region Single() tests
        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async void Single_ReturnsAllowedResource(bool isActive, bool isAdmin)
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            resServiceMock.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync(new Resource());
            resServiceMock.Setup(service => service.IsActive(It.IsAny<int>())).ReturnsAsync(isActive);

            var bookServiceMock = new Mock<IBookingsService>();

            var mappServiceMock = new Mock<IMapperService>();
            mappServiceMock.Setup(service => service.Map<ResourceMaxDto>(It.IsAny<object>())).Returns(new ResourceMaxDto());

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(isAdmin);

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.Single(It.IsAny<int>());

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var dto = Assert.IsAssignableFrom<ResourceMaxDto>(okResult.Value);
            resServiceMock.Verify(mock => mock.Get(It.IsAny<int>()), Times.Once());
        }
        #endregion

        #region Create() tests
        [Fact]
        public async void Create_ReturnsCreatedResponse()
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            var bookServiceMock = new Mock<IBookingsService>();
            var mappServiceMock = new Mock<IMapperService>();
            mappServiceMock.Setup(service => service.Map<Resource>(It.IsAny<object>())).Returns(new Resource());

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.UserId).Returns(It.IsAny<string>());
            resControllerMock.SetupGet(mock => mock.BaseApiUrl).Returns(It.IsAny<string>());

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.Create(new ResourceDetailedDto());

            //Assert 
            var createdResult = Assert.IsType<CreatedResult>(actionResult);
            Assert.IsAssignableFrom<DateTime>(createdResult.Value.GetType().GetProperty("CreatedTime")?.GetValue(createdResult.Value, null));
            Assert.IsAssignableFrom<int>(createdResult.Value.GetType().GetProperty("ResourceId")?.GetValue(createdResult.Value, null));
            resServiceMock.Verify(mock => mock.Create(It.IsAny<Resource>()), Times.Once());
        }

        [Fact]
        public async void Create_ReturnsBadRequest_OnInvalidModel()
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            var bookServiceMock = new Mock<IBookingsService>();
            var mappServiceMock = new Mock<IMapperService>();

            var subjectResController = new ResourcesController(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object);
            subjectResController.ModelState.AddModelError("blah", "blah");

            // Act
            var actionResult = await subjectResController.Create(It.IsAny<ResourceDetailedDto>());

            //Assert 
            Assert.IsType<BadRequestObjectResult>(actionResult);
            resServiceMock.Verify(mock => mock.Create(It.IsAny<Resource>()), Times.Never());
        }
        #endregion

        #region Update() tests
        [Fact]
        public async void Update_ReturnsCreatedResponse()
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            var bookServiceMock = new Mock<IBookingsService>();
            var mappServiceMock = new Mock<IMapperService>();
            mappServiceMock.Setup(service => service.Map<Resource>(It.IsAny<object>())).Returns(new Resource());

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.UserId).Returns(It.IsAny<string>());
            resControllerMock.SetupGet(mock => mock.BaseApiUrl).Returns(It.IsAny<string>());

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.Update(It.IsAny<int>(), new ResourceDetailedDto());

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsAssignableFrom<DateTime>(okResult.Value.GetType().GetProperty("UpdatedTime")?.GetValue(okResult.Value, null));
            resServiceMock.Verify(mock => mock.Update(It.IsAny<Resource>()), Times.Once());
        }

        [Fact]
        public async void Update_ReturnsBadRequest_OnInvalidModel()
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            var bookServiceMock = new Mock<IBookingsService>();
            var mappServiceMock = new Mock<IMapperService>();
            var subjectResController = new ResourcesController(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object);
            subjectResController.ModelState.AddModelError("blah", "blah");

            // Act
            var actionResult = await subjectResController.Update(It.IsAny<int>(), It.IsAny<ResourceDetailedDto>());

            //Assert 
            Assert.IsType<BadRequestObjectResult>(actionResult);
            resServiceMock.Verify(mock => mock.Update(It.IsAny<Resource>()), Times.Never());
        }
        #endregion

        #region Delete() tests
        [Fact]
        public async void Delete_ReturnsOkDeletedResponse()
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            var bookServiceMock = new Mock<IBookingsService>();

            var mappServiceMock = new Mock<IMapperService>();
            var subjectResController = new ResourcesController(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object);

            // Act
            var actionResult = await subjectResController.Delete(It.IsAny<int>());

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsAssignableFrom<DateTime>(okResult.Value.GetType().GetProperty("DeletedTime")?.GetValue(okResult.Value, null));
            resServiceMock.Verify(mock => mock.Delete(It.IsAny<int>()), Times.Once());
        }
        #endregion

        #region ListOccupancy() tests
        [Fact]
        public async void ListOccupancy_ReturnsAllOccupancies_ForAdmin()
        {
            // Arrange
            bool isAdmin = true;

            var resServiceMock = new Mock<IResourcesService>();

            var bookServiceMock = new Mock<IBookingsService>();
            resServiceMock.Setup(service => service.GetOccupancies()).ReturnsAsync(new Dictionary<int, double?>());

            var mappServiceMock = new Mock<IMapperService>();

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(isAdmin);

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.ListOccupancy();

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var map = Assert.IsAssignableFrom<Dictionary<int, double?>>(okResult.Value);
            int expectedCount = ResourceUtils.TestSet.Count();
            resServiceMock.Verify(mock => mock.GetOccupancies(), Times.Once());
        }

        [Fact]
        public async void ListOccupancy_ReturnsActiveOccupancies()
        {
            // Arrange
            bool isAdmin = It.IsAny<bool>();

            var resServiceMock = new Mock<IResourcesService>();

            var bookServiceMock = new Mock<IBookingsService>();
            resServiceMock.Setup(service => service.GetActiveOccupancies()).ReturnsAsync(new Dictionary<int, double?>());

            var mappServiceMock = new Mock<IMapperService>();

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(isAdmin);

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.ListOccupancy();

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var map = Assert.IsAssignableFrom<Dictionary<int, double?>>(okResult.Value);
            resServiceMock.Verify(mock => mock.GetActiveOccupancies(), Times.Once());
        }
        #endregion

        #region SingleOccupancy() tests
        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async void SingleOccupancy_ReturnsAllowedOccupancy(bool isActive, bool isAdmin)
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            resServiceMock.Setup(service => service.IsActive(It.IsAny<int>())).ReturnsAsync(isActive);

            var bookServiceMock = new Mock<IBookingsService>();
            resServiceMock.Setup(service => service.OccupancyByResource(It.IsAny<int>())).ReturnsAsync(It.IsAny<double>());

            var mappServiceMock = new Mock<IMapperService>();

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(isAdmin);

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.SingleOccupancy(It.IsAny<int>());

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var occupancyValue = Assert.IsAssignableFrom<double>(okResult.Value);
            resServiceMock.Verify(mock => mock.OccupancyByResource(It.IsAny<int>()), Times.Once());
        }
        #endregion

        #region ListRelatedBookings() tests
        // ReturnsSomeDTOs_OnAllowedResource
        [Theory]
        [InlineData(false, true, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, false)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        [InlineData(true, true, true)]
        public async void ListRelatedBookings_ReturnsSomeDTOs_OnAllowedResource(bool isActive, bool isAdmin, bool isUser)
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            resServiceMock.Setup(service => service.IsActive(It.IsAny<int>())).ReturnsAsync(isActive);

            var bookServiceMock = new Mock<IBookingsService>();
            bookServiceMock.Setup(service => service.ListBookingOfResource(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(BookingUtils.TestSet);

            var mappServiceMock = new Mock<IMapperService>();

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(isAdmin);
            resControllerMock.SetupGet(mock => mock.IsUser).Returns(isUser);
            resControllerMock.SetupGet(mock => mock.UserId).Returns(It.IsAny<string>());

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.ListRelatedBookings(It.IsAny<int>(), null, null);

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var dtos = Assert.IsAssignableFrom<IEnumerable<BookingMinimalDTO>>(okResult.Value);
            bookServiceMock.Verify(mock => mock.ListBookingOfResource(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once());
        }

        [Theory]
        [InlineData(false, true, false)]
        [InlineData(false, true, true)]
        [InlineData(true, true, false)]
        [InlineData(true, true, true)]
        public async void ListRelatedBookings_ReturnAdminDTOs(bool isActive, bool isAdmin, bool isUser)
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            resServiceMock.Setup(service => service.IsActive(It.IsAny<int>())).ReturnsAsync(isActive);

            var bookServiceMock = new Mock<IBookingsService>();
            bookServiceMock.Setup(service => service.ListBookingOfResource(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(BookingUtils.TestSet);

            var mappServiceMock = new Mock<IMapperService>();
            mappServiceMock.Setup(service => service.Map<IEnumerable<BookingAdminDTO>>(It.IsAny<object>())).Returns(new List<BookingAdminDTO>());
            mappServiceMock.Setup(service => service.Map<BookingOwnerDTO>(It.IsAny<object>())).Returns(new BookingOwnerDTO());
            mappServiceMock.Setup(service => service.Map<BookingMinimalDTO>(It.IsAny<object>())).Returns(new BookingMinimalDTO());
            mappServiceMock.Setup(service => service.Map<IEnumerable<BookingMinimalDTO>>(It.IsAny<object>())).Returns(new List<BookingMinimalDTO>());

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(isAdmin);
            resControllerMock.SetupGet(mock => mock.IsUser).Returns(isUser);
            resControllerMock.SetupGet(mock => mock.UserId).Returns(It.IsAny<string>());

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.ListRelatedBookings(It.IsAny<int>(), null, null);

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var dtos = Assert.IsAssignableFrom<IEnumerable<BookingAdminDTO>>(okResult.Value);
            bookServiceMock.Verify(mock => mock.ListBookingOfResource(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once());
        }

        [Fact]
        public async void ListRelatedBookings_ReturnOwnerDTOs_OnOwnedBookings()
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            resServiceMock.Setup(service => service.IsActive(It.IsAny<int>())).ReturnsAsync(true);

            var bookServiceMock = new Mock<IBookingsService>();
            bookServiceMock.Setup(service => service.ListBookingOfResource(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(BookingUtils.TestSet);

            var mappServiceMock = new Mock<IMapperService>();

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(false);
            resControllerMock.SetupGet(mock => mock.IsUser).Returns(true);
            resControllerMock.SetupGet(mock => mock.UserId).Returns(BookingUtils.SoleBookingCreator);

            var fakeResController = resControllerMock.Object;

            // Act
            var actionResult = await fakeResController.ListRelatedBookings(It.IsAny<int>(), null, null);

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var minimalDtos = Assert.IsAssignableFrom<IEnumerable<BookingMinimalDTO>>(okResult.Value);
            var dtos = Assert.IsAssignableFrom<IEnumerable<BookingOwnerDTO>>(minimalDtos.Cast<BookingOwnerDTO>());
            Assert.NotEmpty(dtos);
            bookServiceMock.Verify(mock => mock.ListBookingOfResource(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once());
        }
        #endregion

        #region AuthorizeForSingleResource() [helper] test
        [Theory]
        [InlineData(false, false)]
        public async void AuthorizeForSingleResourceHelper_ThrowsNotFound(bool isActive, bool isAdmin)
        {
            // Arrange
            var resServiceMock = new Mock<IResourcesService>();
            resServiceMock.Setup(service => service.IsActive(It.IsAny<int>())).ReturnsAsync(isActive);

            var bookServiceMock = new Mock<IBookingsService>();
            var mappServiceMock = new Mock<IMapperService>();

            var resControllerMock = new Mock<ResourcesController>(resServiceMock.Object, bookServiceMock.Object, mappServiceMock.Object) { CallBase = true };
            resControllerMock.SetupGet(mock => mock.IsAdmin).Returns(isAdmin);

            var fakeResController = resControllerMock.Object;

            //Assert-Act
            await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => fakeResController.AuthorizeForSingleResource(It.IsAny<int>()));

            //Assert
            resServiceMock.Verify(mock => mock.IsActive(It.IsAny<int>()), Times.Once());
        }
        #endregion
    }
}