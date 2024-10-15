using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace QuestSystem.Core.Common;

public abstract class BaseEntity<TId> : IBaseEntity
    where TId : struct
{
    private List<INotification> _domainEvents = new();
    public TId Id { get; set; } = default;

    [NotMapped]
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents;

    public void AddEvent(INotification notification) => _domainEvents.Add(notification);

    public void ClearEvents() => _domainEvents.Clear();
}
