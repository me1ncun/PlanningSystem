using BLL.DTOs;
using DAL.Enums;

namespace BLL.Services.Interfaces;

public interface IRequestService
{
    Task<IEnumerable<RequestDto>> GetAllRequestsAsync();
    Task<RequestShortDto> GetRequestStatusAsync(int requestId);
    Task CreateRequestAsync(RequestDto requestDto);
    Task UpdateRequestAsync(RequestDto requestDto);
    Task ChangeRequestStatusAsync(int requestId, RequestStatus status);
    Task DeleteRequestAsync(int requestId);
}