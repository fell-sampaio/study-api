using FluentValidation;
using FluentValidation.Results;
using StudyApi.Business.Interfaces;
using StudyApi.Business.Models;
using StudyApi.Business.Notifications;

namespace StudyApi.Business.Services;
public abstract class BaseService(INotifier notifier)
{
    private readonly INotifier _notifier = notifier;

    protected void Notify(List<ValidationFailure> validationResult)
    {
        foreach (var error in validationResult)
        {
            Notify(error.ErrorMessage);
        }
    }

    protected void Notify(string message)
    {
        _notifier.Handle(new Notification(message));
    }

    protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator.Errors);

        return false;
    }
}
