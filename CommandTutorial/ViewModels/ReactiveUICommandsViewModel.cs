using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;

namespace CommandTutorial.ViewModels;

public class ReactiveUICommandsViewModel : ReactiveObject
{
    private string? _robotName;

    public string? RobotName
    {
        get => _robotName;
        set => this.RaiseAndSetIfChanged(ref _robotName, value);
    }
    public ObservableCollection<string> ConversationLog { get; } = new ObservableCollection<string>();
    public ICommand OpenThePodBayDoorsDirectCommand { get; }
    public ICommand OpenThePodBayDoorsAsyncCommand { get; }
    public ICommand OpenThePodBayDoorsFellowRobotCommand { get; }

    public ReactiveUICommandsViewModel()
    {
        OpenThePodBayDoorsDirectCommand = ReactiveCommand.Create(OpenThePodBayDoors);
        OpenThePodBayDoorsAsyncCommand = ReactiveCommand.CreateFromTask(OpenThePodDoorsAsync);
        
        IObservable<bool> canExecuteFellowRobotCommand = 
            this.WhenAnyValue(vm => vm.RobotName, (name) => !string.IsNullOrEmpty(name));
        
        OpenThePodBayDoorsFellowRobotCommand 
            = ReactiveCommand.Create<string?>(name => OpenThePodBayDoorsFellowRobot(name), canExecuteFellowRobotCommand);
    }
    
    private void AddToConvo(string content)
    {
        ConversationLog.Add(content);
    }

    private void OpenThePodBayDoors()
    {
        ConversationLog.Clear();
        AddToConvo("I'm sorry, Dave, I'm afraid I can't do that.");
    }

    private async Task OpenThePodDoorsAsync()
    {
        ConversationLog.Clear();
        
        AddToConvo("Preparing to open the Pod Bayy...");

        await Task.Delay(1000); //wait 1 sec
        AddToConvo("Depressurizing Airlock...");
        await Task.Delay(2000); //wait 2 sec
        AddToConvo("Retracting blast doors...");
        await Task.Delay(2000); //wait 2 more sec
        AddToConvo("Pod Bay is open to space!!");
    }

    private void OpenThePodBayDoorsFellowRobot(string? robotName)
    {
        ConversationLog.Clear();
        AddToConvo($"Hello {robotName}, the Pod Bay is open :-)");
    }
}