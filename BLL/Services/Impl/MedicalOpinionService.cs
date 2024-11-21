using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;

namespace BLL.Services.Impl;

public class MedicalOpinionService : IMedicalOpinionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<MedicalOpinion> _repository;
    private readonly IMapper _mapper;

    public MedicalOpinionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<MedicalOpinion>();
        _mapper = mapper;
    }

    public async Task EditMedicalOpinion(MedicalOpinionDto medicalOpinionDto)
    {
        var medicalOpinion = await _unitOfWork.MedicalOpinions.GetMedicalOpinionAsync(m =>
            m.EmployeeId == medicalOpinionDto.EmployeeId &&
            m.Conclusion == medicalOpinionDto.Conclusion);

        if (medicalOpinion is null)
        {
            throw new ApplicationException("MedicalOpinion not found");
        }

        medicalOpinion.Conclusion = medicalOpinionDto.Conclusion;
        medicalOpinion.EmployeeId = medicalOpinionDto.EmployeeId;
        medicalOpinion.Id = medicalOpinionDto.Id;
        medicalOpinion.IsHealthy = medicalOpinionDto.IsHealthy;

        _repository.Update(medicalOpinion);
        await _unitOfWork.SaveChanges();
    }

    public async Task CreateMedicalOpinion(MedicalOpinionDto medicalOpinionDto)
    {
        var medicalOpinion = await _unitOfWork.MedicalOpinions.GetMedicalOpinionAsync(m =>
            m.EmployeeId == medicalOpinionDto.EmployeeId &&
            m.Conclusion == medicalOpinionDto.Conclusion);

        if (medicalOpinion is not null)
        {
            throw new ApplicationException("MedicalOpinion already exists");
        }

        await _repository.Create(_mapper.Map<MedicalOpinion>(medicalOpinionDto));
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<MedicalOpinionDto>> GetMedicalOpinionsAsync()
    {
        var medicalOpinions = await _repository.GetAll();

        return _mapper.Map<IEnumerable<MedicalOpinionDto>>(medicalOpinions);
    }

    public async Task<MedicalOpinionDto> GetMedicalOpinionById(int id)
    {
        var medicalOpinion = await _repository.GetById(id);
        if (medicalOpinion is null)
        {
            throw new ApplicationException("MedicalOpinion not found");
        }

        return _mapper.Map<MedicalOpinionDto>(medicalOpinion);
    }
}