namespace QuestSystem.Core.Exceptions;

public class CannotAddQuestToUserException : DomainException
{
    public override string Message => "Невозможно создать квест для пользователя, не подходит по условиям";
}