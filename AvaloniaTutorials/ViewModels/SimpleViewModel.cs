using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AvaloniaTutorials.ViewModels;

public class SimpleViewModel : INotifyPropertyChanged
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
            //Only update UI if the name actually changed
            if (_name != value)
            {
                //update backing field
                _name = value;
                
                //call RaisePropertyChanged() to notify UI of changes
                //can omit property name here because [CallerMemberName] will provide it for us
                RaisePropertyChanged();
                
                //greeting also changed, so notify UI about it
                RaisePropertyChanged(nameof(Greeting));
            }
        }
    }

    //Greeting will change based on name
    public string Greeting
    {
        get
        {
            if (string.IsNullOrEmpty(Name))
            {
                //if no name is provided, use default greeting
                return "Hello World from Avalonia.Samples";
            }
            else
            {
                //else greet user with name
                return $"Hello {Name}";
            }
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged([CallerMemberName]string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}