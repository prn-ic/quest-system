namespace QuestSystem.Core.Exceptions;

public class CannotRemoveConditionByIdException(int id) : DomainException
{
    public override string Message => $"Невозможно удалить условие с идентификатором {id}";
}