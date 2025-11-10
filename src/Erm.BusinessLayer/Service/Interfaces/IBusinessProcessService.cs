using Erm.BusinessLayer.DTO;

namespace Erm.BusinessLayer.Service;

public interface IBusinessProcessService
{
    public Task<BusinessProcessDTO> CreateAsync(BusinessProcessCreateDTO businessProcessDTO, CancellationToken token = default);
    public Task<BusinessProcessDTO> GetAsync(int id, CancellationToken token = default);
    public Task<IEnumerable<BusinessProcessDTO>> GetAllAsync(CancellationToken token = default);
    public Task<BusinessProcessDTO> UpdateAsync(int id, BusinessProcessCreateDTO businessProcessDTO, CancellationToken token = default);
    public Task<BusinessProcessDTO> UpdatePatchAsync(int id, BusinessProcessUpdateDTO businessProcessUpdateDTO, CancellationToken token = default);
    public Task DeleteAsync(int id, CancellationToken token = default);
}
