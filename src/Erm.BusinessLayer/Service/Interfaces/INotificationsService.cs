using Erm.DataAccess.Models;

namespace Erm.BusinessLayer.Service;

public interface INotificationService
{
	public Task<IEnumerable<Notification>> GetAllAsync(CancellationToken token = default);
    public Task<IEnumerable<Notification>> GetBusinessProcessAsync(int id, CancellationToken token = default);
    public Task<IEnumerable<Notification>> GetRiskAsync(int id, CancellationToken token = default);
    public Task<IEnumerable<Notification>> GetRiskProfileAsync(int id, CancellationToken token = default);
    public Task<Notification> MarkAsReadAsync(int notificationId, CancellationToken token = default);
}