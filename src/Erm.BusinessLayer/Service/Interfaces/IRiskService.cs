using Erm.BusinessLayer.DTO;

namespace Erm.BusinessLayer.Service;

public interface IRiskService
{
    public Task<RiskDTO> CreateAsync(RiskCreateDTO riskDTO, CancellationToken token = default);
	public Task<IEnumerable<RiskDTO>> GetAllAsync(CancellationToken token = default);
    public Task<RiskDTO> GetAsync(int id, CancellationToken token = default);
    public Task<RiskDTO> UpdateAsync(int id, RiskCreateDTO riskDTO, CancellationToken token = default);
    public Task<RiskDTO> UpdatePatchAsync(int id, RiskUpdateDTO riskDTO, CancellationToken token = default);
    public Task DeleteAsync(int id, CancellationToken token = default);
}
