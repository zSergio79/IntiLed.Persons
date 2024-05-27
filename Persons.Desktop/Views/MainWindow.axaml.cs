using Avalonia.Controls;
using Avalonia.ReactiveUI;

using IntiLed.Persons.Core;

using Persons.Desktop.ViewModels;

using ReactiveUI;

using System;
using System.Threading.Tasks;

namespace Persons.Desktop.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(action => action(ViewModel!.PersonEditDialog.RegisterHandler(DoShowDialogAsync)));
        }
        private async Task DoShowDialogAsync(InteractionContext<PersonEditViewModel, PersonEditViewModel?> interaction)
        {
            var dialog = new PersonEditWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<PersonEditViewModel?>(this);
            interaction.SetOutput(result);
        }
    }
}