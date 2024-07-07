namespace IceStormy.Template.Common.Result;

/// <summary>
/// Результаты ошибок
/// </summary>
public class ResultErrors
{
    /// <inheritdoc cref="OperationErrorType"/>
    public OperationErrorType Type { get; }

    /// <summary>
    /// Словарь ошибок
    /// </summary>
    public IDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// Конструктор для создания <see cref="ResultErrors"/>
    /// </summary>
    /// <param name="type">Тип ошибки</param>
    public ResultErrors(OperationErrorType type)
    {
        Type = type;
    }
}