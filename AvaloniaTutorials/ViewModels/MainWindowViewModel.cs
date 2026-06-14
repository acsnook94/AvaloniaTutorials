using ReactiveUI;

namespace AvaloniaTutorials.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private decimal? _number1 = 2;
    private decimal? _number2 = 3;
    private char _operator = '+';
    
    public string Greeting { get; } = "Welcome to Avalonia!";
    public SimpleViewModel SimpleViewModel { get; } = new SimpleViewModel();
    public ReactiveViewModel ReactiveViewModel { get; } = new ReactiveViewModel();

    public decimal? Number1
    {
        // get => _number1;
        // set => _number1 = value;
        get { return _number1; }
        set { this.RaiseAndSetIfChanged(ref _number1, value); }
    }

    public decimal? Number2
    {
        get { return _number2; }
        set { this.RaiseAndSetIfChanged(ref _number2, value); }
    }

    public char Operator
    {
        get { return _operator; }
        set { this.RaiseAndSetIfChanged(ref _operator, value); }
    }

    public char[] AvailableMathOperators { get; } = new char[] { '+', '-', '*', '/' };
}