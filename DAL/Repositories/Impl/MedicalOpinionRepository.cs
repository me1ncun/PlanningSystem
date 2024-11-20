using DAL.Data;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Impl;

public class MedicalOpinionRepository : BaseRepository<MedicalOpinion>, IMedicalOpinionRepository
{
    public MedicalOpinionRepository(ApplicationDbContext context) : base(context)
    {
    }
}