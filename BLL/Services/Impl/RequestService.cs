using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Enums;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;

namespace BLL.Services.Impl;

public class RequestService : IRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Request> _repository;
    private readonly IMapper _mapper;

    public RequestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        ArgumentNullException.ThrowIfNull(unitOfWork);
        _repository = unitOfWork.GetRepository<Request>();
        _mapper = mapper;
    }

    public async Task<IEnumerable<RequestDto>> GetAllRequestsAsync()
    {
        var requests = await _repository.GetAll();

        return _mapper.Map<IEnumerable<RequestDto>>(requests);
    }

    public async Task<RequestShortDto> GetRequestStatusAsync(int requestId)
    {
        var request = await _repository.GetById(requestId);
        if (request is null)
        {
            throw new ApplicationException("Request not found");
        }

        return _mapper.Map<RequestShortDto>(request);
    }

    public async Task CreateRequestAsync(RequestDto requestDto)
    {
        var request = await _unitOfWork.Requests.GetRequestAsync(r => r.Id == requestDto.Id &&
                                                                      r.Comment == requestDto.Comment &&
                                                                      r.Status == requestDto.Status &&
                                                                      r.DateEnd == requestDto.DateEnd
                                                                      && r.DateStart == requestDto.DateStart &&
                                                                      r.EmployeeId == requestDto.EmployeeId &&
                                                                      r.OpinionId == requestDto.OpinionId);

        if (request is not null)
        {
            throw new ApplicationException("Request already exists");
        }

        await _repository.Create(_mapper.Map<Request>(requestDto));
        await _unitOfWork.SaveChanges();
    }

    public async Task UpdateRequestAsync(RequestDto requestDto)
    {
        var request = await _unitOfWork.Requests.GetRequestAsync(r => r.Status == requestDto.Status &&
                                                                      r.DateEnd == requestDto.DateEnd
                                                                      && r.DateStart == requestDto.DateStart &&
                                                                      r.EmployeeId == requestDto.EmployeeId &&
                                                                      r.OpinionId == requestDto.OpinionId);

        if (request is null)
        {
            throw new ApplicationException("Request not found");
        }

        request.Comment = requestDto.Comment;
        request.DateEnd = requestDto.DateEnd;
        request.DateStart = requestDto.DateStart;
        request.EmployeeId = requestDto.EmployeeId;
        request.OpinionId = requestDto.OpinionId;
        request.Id = requestDto.Id;
        request.Status = requestDto.Status;
        request.Type = requestDto.Type;

        _repository.Update(request);
        await _unitOfWork.SaveChanges();
    }

    public async Task ChangeRequestStatusAsync(int requestId, RequestStatus status)
    {
        var request = await _repository.GetById(requestId);
        if (request is null)
        {
            throw new ApplicationException("Request not found");
        }

        request.Status = status;

        _repository.Update(request);
        await _unitOfWork.SaveChanges();
    }
}