namespace IceStormy.Template.Common.Result;

/// <summary>
/// Перечисление, представляющее типы ошибок операций.
/// </summary>
public enum OperationErrorType
{
    //TODO: Переписать summary на английский

    /// <summary>
    /// Неизвестная ошибка.
    /// </summary>
    None = 500,

    /// <summary>
    /// Ошибка валидации данных.
    /// </summary>
    Validation = 400,

    /// <summary>
    /// Ресурс не найден.
    /// </summary>
    NotFound = 404,

    /// <summary>
    /// Недостаточно прав.
    /// </summary>
    Forbidden = 403,

    /// <summary>
    /// Внутренняя ошибка.
    /// </summary>
    Internal = 500
}

/// <summary>
/// Класс-расширение для <see cref="OperationErrorType"/>, предоставляющий методы преобразования и описания типов ошибок.
/// </summary>
public static class OperationErrorTypeExtensions
{
    /// <summary>
    /// Возвращает описание типа ошибки на основе значения <see cref="OperationErrorType"/>.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <returns>Описание типа ошибки.</returns>
    public static string ToDescription(this OperationErrorType value) => value switch
    {
        OperationErrorType.Validation => "Ошибка валидации",
        OperationErrorType.NotFound => "Ресурс не найден",
        OperationErrorType.Forbidden => "Нет прав на выполнение операции",
        OperationErrorType.Internal => "Внутренняя ошибка",
        _ => "Не определено"
    };

    /// <summary>
    /// Возвращает ключ типа ошибки на основе значения <see cref="OperationErrorType"/>.
    /// </summary>
    /// <param name="value">Значениe.</param>
    /// <returns>Ключ типа ошибки.</returns>
    public static string ToType(this OperationErrorType value) => value switch
    {
        OperationErrorType.Validation => DefaultErrorKeys.ValidationError,
        OperationErrorType.NotFound => DefaultErrorKeys.NotFound,
        OperationErrorType.Forbidden => DefaultErrorKeys.Forbidden,
        OperationErrorType.Internal => DefaultErrorKeys.Internal,
        _ => string.Empty
    };
}