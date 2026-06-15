using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ReactiveUI;

namespace DataValidationTutorial.ViewModels;

public class ValidationUsingINotifyDataErrorInfoViewModel : ViewModelBase, INotifyDataErrorInfo, IReactiveObject
{
    //fields
    private string? _email;
    private Dictionary<string, List<ValidationResult>> _errors 
        =  new Dictionary<string, List<ValidationResult>>();
    
    //properties
    public string? Email
    {
        get => _email;
        set=> this.RaiseAndSetIfChanged(ref _email, value);
    }
    
    //constructor
    public ValidationUsingINotifyDataErrorInfoViewModel()
    {
        //listen to changes of "ValidationUsingINotifyDataErrorInfo" and re-evaluate validation
        this.WhenAnyValue(x => x.Email)
            .Subscribe(_ => ValidateEmail());
        
        //run INotifyDataErrorInfo validation on start-up
        ValidateEmail();
    }
    
    //methods
    public IEnumerable GetErrors(string? propName)
    {
        // throw new NotImplementedException(); 
        
        //get entity-level errors when prop is null or empty
        if (string.IsNullOrEmpty(propName))
        {
            return _errors.Values.SelectMany(static _errors => _errors);
        }
        
        //get prop-level errors
        if (this._errors.TryGetValue(propName!, out List<ValidationResult>? results))
        {
            return results;
        }
        
        //return empty array if there are no errors
        return Enumerable.Empty<ValidationResult>();
    }

    // public bool HasErrors { get; }
    public bool HasErrors => _errors.Count > 0;  
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    protected void ClearErrors(string? propName = null)
    {
        //clear entity-levek errors when the prop is null or empty
        if (string.IsNullOrEmpty(propName))
        {
            _errors.Clear();
        }
        else
        {
            _errors.Remove(propName);
        }
        
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propName));
        this.OnPropertyChanged(nameof(HasErrors));
    }

    protected void AddError(string propName, string errorMsg)
    {
        if (!_errors.TryGetValue(propName, out List<ValidationResult>? propErrors))
        {
            propErrors = new List<ValidationResult>();
            _errors.Add(propName, propErrors);
        }
        
        propErrors.Add(new ValidationResult(errorMsg));
        
        //notify that errors ahave changed
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propName));
        this.OnPropertyChanged(nameof(HasErrors));
    }

    private void ValidateEmail()
    {
        //clear all previous errors
        ClearErrors(nameof(Email));
        
        //ensure email is not empty
        if (string.IsNullOrEmpty(this.Email))
        {
            AddError(nameof(this.Email), "Email field is required.");
        }
        
        //ensure that the @-sign is present
        if (Email is null || !Email.Contains('@'))
        {
            AddError(nameof(this.Email), "Email field requires an '@'-sign");
        }
    }

    //IReactiveObject method implementations (IGNORE, NOT NEEDED)
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        // throw new NotImplementedException();
    }

    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        // throw new NotImplementedException();
    }
}