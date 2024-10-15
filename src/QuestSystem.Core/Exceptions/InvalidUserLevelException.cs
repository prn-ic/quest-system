namespace QuestSystem.Core.Exceptions;

public class InvalidUserLevelException : DomainException
{
    public override string Message => "Уровень игрока не может быть отрицательным";
}
