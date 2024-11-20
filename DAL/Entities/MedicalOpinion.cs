using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

[Table("medical_opinions")]
public class MedicalOpinion
{
    [Column("id")]
    public int Id { get; set; }
    [Column("conclusion")]
    public string? Conclusion { get; set; }
    [Column("is_healthy")]
    public bool IsHealthy { get; set; } 
    [ForeignKey("Emloyee")]
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}