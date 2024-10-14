using QuestSystem.Core.Common;

namespace QuestSystem.Core.Exceptions;

public class InvalidQuestRewardCurrencyException : DomainException
{
    public override string Message => "Получаемая валюта не может быть отрицательной";
}