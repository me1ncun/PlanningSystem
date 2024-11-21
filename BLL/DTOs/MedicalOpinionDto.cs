namespace BLL.DTOs;

public class MedicalOpinionDto
{
    public int Id { get; set; }
    public string Conclusion { get; set; }
    public bool IsHealthy { get; set; }
    public int EmployeeId { get; set; }
}