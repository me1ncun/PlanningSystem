using AutoMapper;
using BLL.DTOs;
using BLL.Services.Impl;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Enums;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Moq;

namespace BLL.Tests;

public class RequestServiceTests
{
    [Fact]
    public async void GetAllRequests_ReturnsCorrectResult()
    {
        var requestServiceMock = new Mock<IRequestService>();
        
        // Arrange
        var requests = new List<RequestDto>
        {
            new RequestDto()
            {
                Id = 1,
                Comment = "Comment",
                DateStart = new DateOnly(2020, 01, 01),
                DateEnd = new DateOnly(2020, 01, 01),
                Type = RequestType.Vacation,
                EmployeeId = 1,
                OpinionId = 1
            }
        };
        
        requestServiceMock.Setup(x => x.GetAllRequestsAsync()).ReturnsAsync(requests);
        
        // Act
        var requestsResulted = await requestServiceMock.Object.GetAllRequestsAsync();

        // Assert
        Assert.Equal(requests, requestsResulted);
        Assert.Equivalent(1, requestsResulted.Count());
    }

    [Fact]
    public async void GetRequestStatus_ReturnsCorrectResult()
    {
        var requestServiceMock = new Mock<IRequestService>();
        
        // Arrange
        var requestId = 1;
        var requestExpected = new RequestShortDto()
        {
            Id = 1,
            Status = "Accepted",
            Type = "Vacation"
        };
        
        requestServiceMock.Setup(x => x.GetRequestStatusAsync(requestId)).ReturnsAsync(requestExpected);
        
        // Act
        var requestStatusResulted = await requestServiceMock.Object.GetRequestStatusAsync(requestId);
        
        // Assert
        Assert.Equal(requestStatusResulted, requestExpected);
        Assert.Equal(requestId, requestExpected.Id);
    }
    
    [Fact]
    public async void GetRequestStatus_ReturnsErrorResponse()
    {
        var requestRepositoryMock = new Mock<IRequestRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var mapperMock = new Mock<IMapper>();
        
        // Arrange
        var requestId = 1;
        
        requestRepositoryMock.Setup(x => x.GetById(requestId)).ReturnsAsync((Request)null);
        
        unitOfWorkMock.Setup(uow => uow.GetRepository<Request>(false))
            .Returns(requestRepositoryMock.Object);

        var service = new RequestService(unitOfWorkMock.Object, mapperMock.Object);
        
        // Act
        var exception = await Assert.ThrowsAsync<ApplicationException>(
                            () => service.GetRequestStatusAsync(requestId));
        
        // Assert
        Assert.Equal("Request not found", exception.Message);
    }
}