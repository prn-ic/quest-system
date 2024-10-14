namespace QuestSystem.Core.Exceptions;

public static class GuardException
{
    public static void ValidateStringValue(string input, int minLength = 0, int maxLength = 0)
    {
        if (
            string.IsNullOrEmpty(input)
            || input.Length < minLength
            || (maxLength != 0 && input.Length > maxLength)
        )
            throw new InvalidNameException(minLength, maxLength);
    }

    public static void ValidateIntValueOnNegative(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException("Значение не может быть отрицательным");
    }

    public static void ValidateUserLevel(int level)
    {
        if (level < 0)
            throw new InvalidUserLevelException();
    }

    public static void ValidateQuestRewardExperience(int experience)
    {
        if (experience < 0)
            throw new InvalidQuestRewardExperienceException();
    }

    public static void ValidateQuestRewardCurrency(int currency)
    {
        if (currency < 0)
            throw new InvalidQuestRewardCurrencyException();
    }
}
