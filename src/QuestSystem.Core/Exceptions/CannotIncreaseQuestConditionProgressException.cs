namespace QuestSystem.Core.Exceptions;

public class CannotIncreaseQuestConditionProgressException : DomainException
{
    private int _progress;
    private int _nextProgress;
    private int _needed;

    public CannotIncreaseQuestConditionProgressException(int progress, int nextProgress, int needed)
    {
        _progress = progress;
        _nextProgress = nextProgress;
        _needed = needed;
    }

    public override string Message =>
        $@"Невозможно изменить прогресс. Текущий прогресс : {_progress}, Тот, который вы хотите получить: {_nextProgress},Нужный результат: {_needed}";
}
