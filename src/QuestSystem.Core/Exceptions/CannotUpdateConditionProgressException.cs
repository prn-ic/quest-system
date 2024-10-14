using QuestSystem.Core.Common;
using QuestSystem.Core.Quests;

namespace QuestSystem.Core.Exceptions;

public class CannotUpdateConditionProgressException(List<QuestConditionProgress> progresses) : DomainException
{
    public List<QuestConditionProgress> Progresses { get; set; } = progresses;
    public override string Message =>
        "Невозможно завершить квест. Не выполнены условия (this.Progresses для получения условий)";
}
