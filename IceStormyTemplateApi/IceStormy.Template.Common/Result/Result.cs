namespace IceStormy.Template.Common.Result;

/// <summary>
/// Класс, представляющий результат операции с возможными ошибками.
/// </summary>
public class Result
{
    /// <summary>
    /// Ошибки, связанные с результатом операции.
    /// </summary>
    public ResultErrors Errors { get; }

    /// <summary>
    /// Возвращает значение, указывающее, успешна ли операция.
    /// </summary>
    public bool IsSuccess => Errors == null || Errors.Values.Count == 0;

    /// <summary>
    /// Конструктор для создания результата операции с указанным типом ошибки.
    /// </summary>
    /// <param name="errorType">Тип ошибки операции.</param>
    /// <param name="errors">Словарь ошибок.</param>
    protected Result(OperationErrorType errorType, IDictionary<string, string> errors)
    {
        Errors =
            new ResultErrors(errorType)
            {
                Values = errors
            };
    }

    /// <summary>
    /// Конструктор для создания результата операции с переданными ошибками.
    /// </summary>
    /// <param name="errors">Ошибки.</param>
    protected Result(ResultErrors errors)
    {
        Errors = errors;
    }

    /// <summary>
    /// Конструктор для создания успешного результата операции без ошибок.
    /// </summary>
    protected Result()
    {
    }

    /// <summary>
    /// Возвращает успешный результат операции.
    /// </summary>
    public static Result Success => new();

    /// <summary>
    /// Создает экземпляр результата операции на основе переданных ошибок.
    /// </summary>
    /// <param name="errors">Объект ошибок.</param>
    /// <returns>Экземпляр результата операции.</returns>
    public static Result FromErrors(ResultErrors errors) => new(errors);

    /// <summary>
    /// Создает экземпляр результата операции с ошибкой "Ресурс не найден" <see cref="OperationErrorType.NotFound"/>.
    /// </summary>
    /// <param name="errorMessage">Сообщение об ошибке.</param>
    /// <param name="key">Ключ ошибки.</param>
    /// <returns>Экземпляр результата операции.</returns>
    public static Result NotFound(string errorMessage, string key = DefaultErrorKeys.NotFound) =>
        new(OperationErrorType.NotFound, new Dictionary<string, string>
        {
            { key, errorMessage }
        });

    /// <summary>
    /// Создает экземпляр результата операции с внутренней ошибкой <see cref="OperationErrorType.Internal"/>.
    /// </summary>
    /// <param name="errorMessage">Сообщение об ошибке.</param>
    /// <param name="key">Ключ ошибки.</param>
    /// <returns>Экземпляр результата операции.</returns>
    public static Result Internal(string errorMessage, string key = DefaultErrorKeys.Internal) =>
        new(OperationErrorType.Internal, new Dictionary<string, string>
        {
            {key, errorMessage}
        });

    /// <summary>
    /// Создает экземпляр результата операции с ошибкой валидации <see cref="OperationErrorType.Validation"/>.
    /// </summary>
    /// <param name="errors">Словарь ошибок валидации.</param>
    /// <returns>Экземпляр результата операции.</returns>
    public static Result NotValid(Dictionary<string, string> errors = default) =>
        new(OperationErrorType.Validation, errors);

    /// <summary>
    /// Создает экземпляр результата операции с ошибкой валидации <see cref="OperationErrorType.Validation"/>.
    /// </summary>
    /// <param name="errorMessage">Сообщение об ошибке.</param>
    /// <param name="key">Ключ ошибки.</param>
    /// <returns>Экземпляр результата операции.</returns>
    public static Result NotValid(string errorMessage, string key = DefaultErrorKeys.ValidationError) =>
        new(OperationErrorType.Validation, new Dictionary<string, string>
        {
            {key, errorMessage}
        });

    /// <summary>
    /// Создает экземпляр результата операции с ошибкой "Недостаточно прав" <see cref="OperationErrorType.Forbidden"/>.
    /// </summary>
    /// <param name="errorMessage">Сообщение об ошибке.</param>
    /// <param name="key">Ключ ошибки.</param>
    /// <returns>Экземпляр результата операции.</returns>
    public static Result Forbidden(string errorMessage, string key = DefaultErrorKeys.Forbidden) =>
        new(OperationErrorType.Forbidden, new Dictionary<string, string>
        {
            {key, errorMessage}
        });
}
