using AutoMapper;
using FluentValidation;

using Erm.BusinessLayer.DTO;
using Erm.DataAccess.Repository;
using Erm.DataAccess.Models;


namespace Erm.BusinessLayer.Service;

public sealed class RiskProfileService(
    IMapper mapper,
    IValidator<RiskProfileCreateDTO> validator,
    IValidator<RiskProfileUpdateDTO> validatorUpdate,
    // RiskProfileRepositoryImMemoryProxy profileRepository,
    RiskProfileRepositoryRedisProxy profileRepository,
    NotificationRepository notificationRepository) : IRiskProfileService
{
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<RiskProfileCreateDTO> _validator = validator;
    private readonly IValidator<RiskProfileUpdateDTO> _validatorUpdate = validatorUpdate;
    private readonly NotificationRepository _notification = notificationRepository;
    // private readonly RiskProfileRepositoryImMemoryProxy _repository = profileRepository;
    private readonly RiskProfileRepositoryRedisProxy _repository = profileRepository;


    public async Task<RiskProfileDTO> CreateAsync(RiskProfileCreateDTO profileDTO, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(profileDTO, token);

        RiskProfile riskProfile = _mapper.Map<RiskProfile>(profileDTO);
        RiskProfile result = await _repository.CreateAsync(riskProfile, token);

        await _notification.AddRiskProfileNotificationAsync(result.Id, "Created", token);

        return _mapper.Map<RiskProfileDTO>(result);
    }

    public async Task<IEnumerable<RiskProfileDTO>> GetAllAsync(CancellationToken token = default) 
        => _mapper.Map<IEnumerable<RiskProfileDTO>>(await _repository.GetAllAsync(token));

    public async Task<RiskProfileDTO> GetAsync(int id, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        return _mapper.Map<RiskProfileDTO>(await _repository.GetAsync(id, token));
    }

    public async Task<RiskProfileDTO> UpdateAsync(int id, RiskProfileCreateDTO riskProfileDTO, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        await _validator.ValidateAndThrowAsync(riskProfileDTO, token);

        RiskProfile riskProfile = _mapper.Map<RiskProfile>(riskProfileDTO);
        RiskProfile result = await _repository.UpdateAsync(id, riskProfile);

        await _notification.AddRiskProfileNotificationAsync(result.Id, "Updated", token);

        return _mapper.Map<RiskProfileDTO>(result);
    }

    public async Task<RiskProfileDTO> UpdatePatchAsync(int id, RiskProfileUpdateDTO riskProfileUpdateDTO, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        await _validatorUpdate.ValidateAndThrowAsync(riskProfileUpdateDTO, token);

        RiskProfile riskProfile = _mapper.Map<RiskProfile>(riskProfileUpdateDTO);
        RiskProfile result = await _repository.UpdateAsync(id, riskProfile);

        await _notification.AddRiskProfileNotificationAsync(result.Id, "Updated", token);

        return _mapper.Map<RiskProfileDTO>(result);
    }

    public async Task DeleteAsync(int id, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        await _repository.DeleteAsync(id, token);
    }

    public async Task<IEnumerable<RiskProfileDTO>> QueryAsync(string query, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(query);
        return _mapper.Map<IEnumerable<RiskProfileDTO>>(await _repository.QueryAsync(query));
    }
}
