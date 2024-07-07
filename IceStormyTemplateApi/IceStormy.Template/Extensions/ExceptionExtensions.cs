using IceStormy.Template.Common.Constants;
using IceStormy.Template.Common.Extensions;
using IceStormy.Template.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace IceStormy.Template.Extensions;

internal static class ExceptionExtensions
{
    public static ProblemDetails ToProblemDetails(this Exception exception, IHostEnvironment env)
    {
        var responseMessage = AbstractConstants.InternalError;

        if (!env.IsProduction())
            responseMessage = exception?.ToString();

        return exception.ToResult(responseMessage).ToProblemDetails();
    }

    public static Result ToResult(this Exception exception, string responseMessage)
    {
        return exception switch
        {
            _ => Result.Internal(responseMessage)
        };
    }
}