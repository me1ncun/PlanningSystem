using BLL.DTOs;

namespace BLL.Services.Interfaces;

public interface IMedicalOpinionService
{
    Task EditMedicalOpinion(MedicalOpinionDto medicalOpinionDto);
    Task CreateMedicalOpinion(MedicalOpinionDto medicalOpinionDto);
    Task<IEnumerable<MedicalOpinionDto>> GetMedicalOpinionsAsync();
    Task<MedicalOpinionDto> GetMedicalOpinionById(int id);
    Task DeleteMedicalOpinion(int id);
}