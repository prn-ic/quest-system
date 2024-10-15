namespace QuestSystem.Core.Exceptions;

public class NotFoundConditionInUserQuestException(int id) : DomainException
{
    public override string Message => $"Не найдено условие с идентификатором: {id}";
}