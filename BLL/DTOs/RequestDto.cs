using DAL.Enums;

namespace BLL.DTOs;

public class RequestDto
{
    public int Id { get; set; }
    public RequestType Type { get; set; }
    public RequestStatus Status { get; set; }
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public string Comment { get; set; }
    public int EmployeeId { get; set; }
    public int OpinionId { get; set; }
}