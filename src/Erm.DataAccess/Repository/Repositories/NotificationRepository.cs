using Microsoft.EntityFrameworkCore;

using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;


public sealed class NotificationRepository(
    ErmDbContext dbContext) : INotificationRepository
{
    private readonly ErmDbContext _db = dbContext;
	
	public async Task<IEnumerable<Notification>> GetAllAsync(CancellationToken token = default)
        => await _db.Notifications
            .AsNoTracking()
            .OrderBy(n => n.Id)
            .ToArrayAsync(token);

    public async Task<IEnumerable<Notification>> GetBusinessProcessAsync(int id, CancellationToken token = default)
        => await _db.Notifications
            .AsNoTracking()
            .Where(x => x.BusinessProcessId == id && x.IsRead == false)
            .ToArrayAsync(token);

    public async Task<IEnumerable<Notification>> GetRiskAsync(int id, CancellationToken token = default)
        => await _db.Notifications
            .AsNoTracking()
            .Where(x => x.RiskId == id && x.IsRead == false)
            .ToArrayAsync(token);

    public async Task<IEnumerable<Notification>> GetRiskProfileAsync(int id, CancellationToken token = default)
        => await _db.Notifications
            .AsNoTracking()
            .Where(x => x.RiskProfileId == id && x.IsRead == false)
            .ToArrayAsync(token);

    public async Task AddRiskNotificationAsync(int id, string message, CancellationToken token = default)
    {

        await _db.Notifications.AddAsync(new Notification()
        {
            RiskId = id,
            Message = message,
            IsRead = false,
            CreatedAt = DateTime.Now
        }, token);

        await _db.SaveChangesAsync(token);
    }

    public async Task AddRiskProfileNotificationAsync(int id, string message, CancellationToken token = default)
    {
        Notification notification = new()
        {
            RiskProfileId = id,
            Message = message,
            IsRead = false,
            CreatedAt = DateTime.Now
        };

        await _db.Notifications.AddAsync(notification, token);
        await _db.SaveChangesAsync(token);
    }

    public async Task AddBusinessProcessNotificationAsync(int id, string message, CancellationToken token = default)
    {
        Notification notification = new()
        {
            BusinessProcessId = id,
            Message = message,
            IsRead = false,
            CreatedAt = DateTime.Now
        };

        await _db.Notifications.AddAsync(notification, token);
        await _db.SaveChangesAsync(token);
    }

    public async Task<Notification> MarkAsReadAsync(int notificationId, CancellationToken token = default)
    {

        Notification notification = await _db.Notifications.SingleAsync(x => x.Id == notificationId);
        notification.IsRead = true;
        await _db.SaveChangesAsync(token);
        return notification;
    }

}
