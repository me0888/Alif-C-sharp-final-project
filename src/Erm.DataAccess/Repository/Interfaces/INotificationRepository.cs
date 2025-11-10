using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public interface INotificationRepository
{
	public Task<IEnumerable<Notification>> GetAllAsync(CancellationToken token = default);
    public Task AddRiskNotificationAsync(int id, string message, CancellationToken token = default);
    public Task AddRiskProfileNotificationAsync(int id, string message, CancellationToken token = default);
    public Task AddBusinessProcessNotificationAsync(int id, string message, CancellationToken token = default);
    public Task<IEnumerable<Notification>> GetRiskAsync(int id, CancellationToken token = default);
    public Task<IEnumerable<Notification>> GetRiskProfileAsync(int id, CancellationToken token = default);
    public Task<Notification> MarkAsReadAsync(int notificationId, CancellationToken token = default);
}


