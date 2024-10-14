using QuestSystem.Core.Common;

namespace QuestSystem.Core.Exceptions;

public class InvalidQuestRewardExperienceException : DomainException
{
    public override string Message => "Получаемый опыт не может быть отрицательным";
}