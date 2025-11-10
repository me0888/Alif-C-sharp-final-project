using Erm.DataAccess.Models;
using Erm.BusinessLayer.DTO;

namespace Erm.BusinessLayer.Service;

public interface IRiskProfileService
{
    public Task<RiskProfileDTO> CreateAsync(RiskProfileCreateDTO profileDTO, CancellationToken token = default);
	public Task<IEnumerable<RiskProfileDTO>> GetAllAsync(CancellationToken token = default);
    public Task<RiskProfileDTO> GetAsync(int id, CancellationToken token = default);
    public Task<RiskProfileDTO> UpdateAsync(int id, RiskProfileCreateDTO riskProfileDTO, CancellationToken token = default);
    public Task<RiskProfileDTO> UpdatePatchAsync(int id, RiskProfileUpdateDTO riskProfileUpdateDTO, CancellationToken token = default);
    public Task DeleteAsync(int id, CancellationToken token = default);
    public Task<IEnumerable<RiskProfileDTO>> QueryAsync(string query, CancellationToken token = default);
}
