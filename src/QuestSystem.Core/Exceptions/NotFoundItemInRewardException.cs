using QuestSystem.Core.Common;

namespace QuestSystem.Core.Exceptions;

public class NotFoundItemInRewardException(string name) : DomainException
{
    public override string Message => $"Не найден предмет с именем {name}";
}