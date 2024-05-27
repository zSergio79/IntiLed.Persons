using Avalonia.Xaml.Interactivity;

using IntiLed.Persons.Core;
using IntiLed.Persons.Core.Interfaces;
using IntiLed.Persons.Core.PersonsProviders;
using ReactiveUI;

using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Persons.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Services
        private IPersonsProvider personsProvider;
        #endregion

        #region Bindable Properties
        private ObservableCollection<PersonEditViewModel> persons = new ObservableCollection<PersonEditViewModel>();
        public ObservableCollection<PersonEditViewModel> Persons
        {
            get => persons;
            set => this.RaiseAndSetIfChanged(ref persons, value);
        }
        private PersonEditViewModel? selected;
        public PersonEditViewModel? Selected
        {
            get => selected;
            set => this.RaiseAndSetIfChanged(ref selected, value);
        }
        #endregion

        #region .ctor
        public MainWindowViewModel()
        {
            EditCommand = ReactiveCommand.CreateFromTask<object?, Unit>(EditCommandExecuted);
            PersonEditDialog = new Interaction<PersonEditViewModel, PersonEditViewModel?>();

            personsProvider = new FilePersonsProvider() { Filename = "persons.json" };
            Persons = new ObservableCollection<PersonEditViewModel>(personsProvider.Load().Select(x=>new PersonEditViewModel(x)));
        }
        #endregion

        #region Commands
        public ReactiveCommand<object?, Unit> EditCommand { get; set; }
        private async Task<Unit> EditCommandExecuted(object? p)
        {
            var person = new PersonEditViewModel(selected);
            var result = await PersonEditDialog.Handle(person);
            if (result != null)
            {
                selected.Age = result.Age;
                selected.Name = result.Name;
                selected.Surname = result.Surname;
                selected.City = result.City;
            }
            return Unit.Default;
        }
        #endregion

        #region Dialogs
        public Interaction<PersonEditViewModel, PersonEditViewModel?> PersonEditDialog { get; }
        #endregion
    }
}
