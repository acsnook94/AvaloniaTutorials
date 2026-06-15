using System;
using System.ComponentModel;
using ReactiveUI;

namespace DataValidationTutorial.ViewModels;

public class ValidationUsingExceptionInsideSetterViewModel : ViewModelBase, IReactiveObject
{
    //fields
    private string? _email;
    
    //properties
    public string? Email
    {
        get { return _email; }
        set
        {
            //email should not be empty
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(Email), "Email cannot be null or empty.");
            }
            //check if email string contains an '@' sign
            else if (!value.Contains("@"))
            {
                throw new ArgumentException(nameof(Email), "Email must contain an '@' character.");
            }
            //checks were successful
            else
            {
                this.RaiseAndSetIfChanged(ref _email, value);
            }
        }
    }
    
    //constructor
    
    //IReactiveObject method implementations (IGNORE, NOT NEEDED)
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        // throw new System.NotImplementedException();
    }

    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        // throw new System.NotImplementedException();
    }
}