using QuestSystem.Core.Common;

namespace QuestSystem.Core.Exceptions;

public class InvalidNameException : DomainException
{
    private int _minLength;
    private int _maxLength;
    public override string Message =>
        _maxLength == 0
            ? $"Недопустимое имя! Не может быть пустым. Минимальный диапазон: {_minLength}"
            : $"Недопустимое имя! Не может быть пустым. Минимальный диапазон: {_minLength}. Максимальный диапазон: {_maxLength}";

    public InvalidNameException() { }

    public InvalidNameException(int minLength)
    {
        _minLength = minLength;
    }

    public InvalidNameException(int maxLength, int minLength = 0)
    {
        _maxLength = maxLength;
        _minLength = minLength;
    }
}
