using AutoMapper;
using FluentValidation;

using Erm.BusinessLayer.DTO;
using Erm.DataAccess.Repository;
using Erm.DataAccess.Models;

namespace Erm.BusinessLayer.Service;

public sealed class BusinessProcessService(
    IMapper mapper,
    IValidator<BusinessProcessCreateDTO> validator,
    IValidator<BusinessProcessUpdateDTO> validatorUpdate,
    BusinessProcessRepositoryInMemoryProxy businessProcessRepository,
    // BusinessProcessRepositoryRedisProxy businessProcessRepository,
    NotificationRepository notificationRepository) : IBusinessProcessService
{
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<BusinessProcessCreateDTO> _validator = validator;
    private readonly IValidator<BusinessProcessUpdateDTO> _validatorUpdate = validatorUpdate;
    private readonly NotificationRepository _notification = notificationRepository;
    private readonly BusinessProcessRepositoryInMemoryProxy _repository = businessProcessRepository;

    // private readonly BusinessProcessRepositoryRedisProxy _repository = businessProcessRepository;


    public async Task<BusinessProcessDTO> CreateAsync(BusinessProcessCreateDTO businessProcessDTO, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(businessProcessDTO, token);

        BusinessProcess businessProcess = _mapper.Map<BusinessProcess>(businessProcessDTO);
        BusinessProcess result = await _repository.CreateAsync(businessProcess, token);

        await _notification.AddBusinessProcessNotificationAsync(result.Id, "Created", token);
        return _mapper.Map<BusinessProcessDTO>(result);
    }

    public async Task<BusinessProcessDTO> UpdateAsync(int id, BusinessProcessCreateDTO businessProcessDTO, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfZero(id);
        await _validator.ValidateAndThrowAsync(businessProcessDTO, token);

        BusinessProcess businessProcess = _mapper.Map<BusinessProcess>(businessProcessDTO);
        BusinessProcess result = await _repository.UpdateAsync(id, businessProcess, token);

        await _notification.AddBusinessProcessNotificationAsync(result.Id, "Updated", token);
        return _mapper.Map<BusinessProcessDTO>(result);
    }

    public async Task<BusinessProcessDTO> UpdatePatchAsync(int id, BusinessProcessUpdateDTO businessProcessUpdateDTO, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        await _validatorUpdate.ValidateAndThrowAsync(businessProcessUpdateDTO, token);

        BusinessProcess businessProcess = _mapper.Map<BusinessProcess>(businessProcessUpdateDTO);
        BusinessProcess result = await _repository.UpdateAsync(id, businessProcess, token);

        await _notification.AddBusinessProcessNotificationAsync(result.Id, "Updated", token);
        return _mapper.Map<BusinessProcessDTO>(result);
    }

    public async Task<IEnumerable<BusinessProcessDTO>> GetAllAsync(CancellationToken token = default) 
        => _mapper.Map<IEnumerable<BusinessProcessDTO>>(await _repository.GetAllAsync(token));


    public async Task<BusinessProcessDTO> GetAsync(int id, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        return _mapper.Map<BusinessProcessDTO>(await _repository.GetAsync(id, token));
    }

    public async Task DeleteAsync(int id, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        await _repository.DeleteAsync(id, token);
    }




}
