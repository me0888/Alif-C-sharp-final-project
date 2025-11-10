using AutoMapper;
using FluentValidation;
using Erm.DataAccess.Models;
using Erm.BusinessLayer.DTO;
using Erm.DataAccess.Repository;

namespace Erm.BusinessLayer.Service;

public sealed partial class RiskService(
    IMapper mapper,
    IValidator<RiskCreateDTO> validator,
    IValidator<RiskUpdateDTO> validatorUpdate,
    // RiskRepositoryImMemoryProxy repository,
    RiskRepositoryRedisProxy repository,
    NotificationRepository notificationRepository) : IRiskService
{
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<RiskCreateDTO> _validator = validator;
    private readonly IValidator<RiskUpdateDTO> _validatorUpdate = validatorUpdate;
    private readonly NotificationRepository _notification = notificationRepository;
    // private readonly RiskRepositoryImMemoryProxy _repository = repository;
    private readonly RiskRepositoryRedisProxy _repository = repository;

    public async Task<RiskDTO> CreateAsync(RiskCreateDTO riskDTO, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(riskDTO, token);

        Risk risk = _mapper.Map<Risk>(riskDTO);
        Risk result = await _repository.CreateAsync(risk, token);

        await _notification.AddRiskNotificationAsync(result.Id, "Created", token);

        return _mapper.Map<RiskDTO>(result);
    }

    public async Task<IEnumerable<RiskDTO>> GetAllAsync(CancellationToken token = default)
		=>_mapper.Map<IEnumerable<RiskDTO>>(await _repository.GetAllAsync());
    

    public async Task<RiskDTO> GetAsync(int id, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

        Risk risk = await _repository.GetAsync(id);
        return _mapper.Map<RiskDTO>(risk);
    }

    public async Task<RiskDTO> UpdateAsync(int id, RiskCreateDTO riskDTO, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        await _validator.ValidateAndThrowAsync(riskDTO, token);

        Risk risk = _mapper.Map<Risk>(riskDTO);
        Risk result = await _repository.UpdateAsync(id, risk, token);

        await _notification.AddRiskNotificationAsync(result.Id, "Updated", token);
        return _mapper.Map<RiskDTO>(result);
    }

    public async Task<RiskDTO> UpdatePatchAsync(int id, RiskUpdateDTO riskUpdateDTO, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        await _validatorUpdate.ValidateAndThrowAsync(riskUpdateDTO, token);

        Risk risk = _mapper.Map<Risk>(riskUpdateDTO);
        Risk result = await _repository.UpdateAsync(id, risk, token);

        await _notification.AddRiskNotificationAsync(result.Id, "Updated", token);
        return _mapper.Map<RiskDTO>(result);
    }
	
	public async Task DeleteAsync(int id, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfZero(id);
        await _repository.DeleteAsync(id, token);
    }
}
