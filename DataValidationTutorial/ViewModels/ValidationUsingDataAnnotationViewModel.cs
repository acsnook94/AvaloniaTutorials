using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace DataValidationTutorial.ViewModels;

public class ValidationUsingDataAnnotationViewModel : ReactiveObject
{
    private string? _email;

    [Required]
    [EmailAddress]
    public string? Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }
}