using FluentValidation.Results;

namespace ProposalService.Application.Extensions;

public static class FluentValidationExtensions
{
    public static List<string> GetMessages(this ValidationResult validationResult)
    {
        List<string> result = [];

        if (validationResult is null) return result;

        result = validationResult
            .Errors
            .Select(e => e.ErrorMessage)
            .ToList();

        return result;
    }
}
