namespace IceStormy.Template.Common.Result;

/// <summary>
/// Класс, представляющий результат операции с данными типа T.
/// </summary>
/// <typeparam name="T">Тип данных, с которыми связан результат операции.</typeparam>
public sealed class Result<T> : Result
{
    /// <summary>
    /// Получает данные результата операции.
    /// </summary>
    public T Data { get; }

    /// <summary>
    /// Конструктор для создания результата успешной операции.
    /// </summary>
    /// <param name="value">Данные результата.</param>
    private Result(T value)
    {
        Data = value;
    }

    /// <summary>
    /// Конструктор для создания результата операции с переданными ошибками.
    /// </summary>
    /// <param name="errors">Ошибки.</param>
    private Result(ResultErrors errors) : base(errors)
    {
    }

    /// <summary>
    /// Создает экземпляр результата операции с указанными данными.
    /// </summary>
    /// <param name="value">Данные результата.</param>
    /// <returns>Экземпляр результата операции.</returns>
    public static Result<T> FromValue(T value) => new(value);

    /// <inheritdoc cref="Result.FromErrors"/>
    public new static Result<T> FromErrors(ResultErrors errors) => new(errors);

    /// <inheritdoc cref="Result.NotFound"/>
    public new static Result<T> NotFound(string errorMessage, string key = DefaultErrorKeys.NotFound) =>
        FromErrors(Result.NotFound(errorMessage, key).Errors);

    /// <inheritdoc cref="Result.Forbidden"/>
    public new static Result<T> Forbidden(string errorMessage, string key = DefaultErrorKeys.Forbidden) =>
        FromErrors(Result.Forbidden(errorMessage, key).Errors);

    /// <inheritdoc cref="Result.Internal"/>
    public new static Result<T> Internal(string errorMessage, string key = DefaultErrorKeys.Internal) =>
        FromErrors(Result.Internal(errorMessage, key).Errors);

    /// <summary>
    /// Создает экземпляр результата операции "Ошибка валидаций" с указанными ошибками.
    /// </summary>
    /// <param name="errors">Словарь ошибок.</param>
    /// <returns>Экземпляр результата операции.</returns>
    public new static Result<T> NotValid(Dictionary<string, string> errors = default) =>
        FromErrors(Result.NotValid(errors).Errors);

    /// <summary>
    /// Создает экземпляр результата операции "Ошибка валидаций" с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="errorMessage">Сообщение об ошибке.</param>
    /// <param name="key">Ключ ошибки.</param>
    /// <returns>Экземпляр результата операции.</returns>
    public new static Result<T> NotValid(string errorMessage, string key = DefaultErrorKeys.ValidationError) =>
        FromErrors(Result.NotValid(errorMessage, key).Errors);
}