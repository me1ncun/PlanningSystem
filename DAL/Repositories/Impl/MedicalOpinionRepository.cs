using System.Linq.Expressions;
using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Impl;

public class MedicalOpinionRepository : BaseRepository<MedicalOpinion>, IMedicalOpinionRepository
{
    private readonly ApplicationDbContext _context;

    public MedicalOpinionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<MedicalOpinion> GetMedicalOpinionAsync(Expression<Func<MedicalOpinion, bool>> predicate)
    {
        return await _context.MedicalOpinions.FirstOrDefaultAsync(predicate);
    }
}