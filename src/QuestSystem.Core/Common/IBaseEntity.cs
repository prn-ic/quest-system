using MediatR;

namespace QuestSystem.Core.Common;

public interface IBaseEntity
{
    IReadOnlyCollection<INotification> DomainEvents { get; }
    void AddEvent(INotification notification);
    void ClearEvents();
}
