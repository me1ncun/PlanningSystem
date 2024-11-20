using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<MedicalOpinion> MedicalOpinions { get; set; }
}