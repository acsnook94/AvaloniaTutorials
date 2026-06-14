using System;
using ReactiveUI;
// using CommunityToolkit.Mvvm;

namespace BasicMvvmSample.ViewModels;

public class ReactiveViewModel : ReactiveObject
{
    private string? _name;

    public string? Name
    {
        get
        {
            return _name;
        }
        set
        {
            //we can use "RaiseAndSetIfChanged" to check if the value changed and automatically notify the UI
            this.RaiseAndSetIfChanged(ref _name, value);
        }
    }

    public string Greeting
    {
        get
        {
            if (string.IsNullOrEmpty(Name))
            {
                //if no name provided, use default greeting
                return "Hello World from Avalonia.Samples";
            }
            else
            {
                //else greet user by name
                return $"Hello {Name}!";
            }

            // if (!string.IsNullOrEmpty(Name))
            // {
            //     //else greet user by name
            //     return $"Hello {Name}!";
            // }
            // return "Hello World from Avalonia.Samples";
            
        }
    }

    public ReactiveViewModel()
    {
        //we can listen to any property changes with "WhenAnyValue" and do whatever we want in "Subscribe"
        this.WhenAnyValue(o => o.Name)
            .Subscribe(o => this.RaisePropertyChanged(nameof(Greeting)));
    }
}