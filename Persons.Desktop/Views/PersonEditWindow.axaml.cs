using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using Persons.Desktop.ViewModels;
using System;
using ReactiveUI;

namespace Persons.Desktop;

public partial class PersonEditWindow : ReactiveWindow<PersonEditViewModel>
{
    public PersonEditWindow()
    {
        InitializeComponent();

        if (Design.IsDesignMode) return;

        this.WhenActivated(action =>
        {
            action(ViewModel!.OkCommand.Subscribe(Close));
            action(ViewModel!.CancelCommand.Subscribe(Close));
        });
    }
}