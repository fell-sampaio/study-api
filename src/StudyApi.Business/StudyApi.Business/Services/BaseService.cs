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

    protected void Notify(string mensagem)
    {
        _notifier.Handle(new Notification(mensagem));
    }

    protected bool ExecuteValidation<TV, TE>(TV validacao, TE entity) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = validacao.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator.Errors);

        return false;
    }
}
