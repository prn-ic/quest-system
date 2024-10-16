namespace QuestSystem.Core.Exceptions;

public class QuestAlreadyTakenToUserException : DomainException
{
    public override string Message => "Данный квест уже был выполнен пользователем";
}