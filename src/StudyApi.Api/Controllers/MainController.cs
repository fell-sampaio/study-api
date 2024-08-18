using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudyApi.Business.Interfaces;
using StudyApi.Business.Notifications;

namespace StudyApi.Api.Controllers;
[ApiController]
public abstract class MainController(INotifier notifier) : ControllerBase
{
    private readonly INotifier _notifier = notifier;

    protected bool ValidOperation()
    {
        return !_notifier.HasNotification();
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            susccess = false,
            errors = _notifier.GetNotifications().Select(n => n.Message)
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotificateInvalidModelError(modelState);

        return CustomResponse();
    }

    protected void NotificateInvalidModelError(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            NotificateError(errorMessage);
        }
    }

    protected void NotificateError(string message)
    {
        _notifier.Handle(new Notification(message));
    }
}
