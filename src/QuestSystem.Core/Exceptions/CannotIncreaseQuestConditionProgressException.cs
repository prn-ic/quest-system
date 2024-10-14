using QuestSystem.Core.Common;

namespace QuestSystem.Core.Exceptions;

public class CannotIncreaseQuestConditionProgressException : DomainException
{
    public override string Message =>
        "Невозможно изменить прогресс. Нарушено условие. Учитывайте, прогресс не может уменьшаться и не превышать условия квеста";
}
