using System.ComponentModel.DataAnnotations.Schema;
using DAL.Enums;

namespace DAL.Entities;

[Table("requests")]
public class Request
{
    [Column("id")]
    public int Id { get; set; }
    [Column("type")]
    public RequestType Type { get; set; }
    public RequestStatus Status { get; set; }
    [Column("start_date")]
    public DateOnly DateStart { get; set; }
    [Column("end_date")]
    public DateOnly DateEnd { get; set; }
    [Column("comment")]
    public string Comment { get; set; } = string.Empty;
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    [ForeignKey("Opinion")]
    public int OpinionId { get; set; }
    public MedicalOpinion? Opinion { get; set; }
}