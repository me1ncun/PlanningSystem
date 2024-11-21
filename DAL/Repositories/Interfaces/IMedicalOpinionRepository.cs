using System.Linq.Expressions;
using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface IMedicalOpinionRepository : IRepository<MedicalOpinion>
{
    Task<MedicalOpinion?> GetMedicalOpinionAsync(Expression<Func<MedicalOpinion, bool>> predicate);
}