using Avalonia;
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

using Microsoft.Extensions.DependencyInjection;

namespace Persons.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IClosingRequest
    {
        #region Services
        private IPersonsProvider? personsProvider;
        #endregion

        #region Bindable Properties
        private ObservableCollection<PersonEditViewModel> persons = new ObservableCollection<PersonEditViewModel>();
        public ObservableCollection<PersonEditViewModel> Persons
        {
            get => persons;
            set => this.SetAndRaiseIfChanged(ref persons, value);
        }
        private PersonEditViewModel? selected;
        public PersonEditViewModel? Selected
        {
            get => selected;
            set => this.SetAndRaiseIfChanged(ref selected, value);
        }
        #endregion

        #region .ctor
        public MainWindowViewModel()
        {
            var isPersonSelected = this.WhenAnyValue<MainWindowViewModel, bool, PersonEditViewModel?>(
                x => x.Selected,
                x => x != null);

            EditCommand = ReactiveCommand.CreateFromTask<object?, Unit>(EditCommandExecuted, isPersonSelected);
            DeleteCommand = ReactiveCommand.CreateFromTask<object?, Unit>(DeleteCommandExecuted, isPersonSelected);
            AddCommand = ReactiveCommand.CreateFromTask<object?, Unit>(AddCommandExecuted);

            PersonEditDialog = new Interaction<PersonEditViewModel, PersonEditViewModel?>();

            personsProvider = ((App)App.Current).ServiceProvider?.GetService<IPersonsProvider>();// new FilePersonsProvider() { Filename = "persons.json" };
            var p = personsProvider?.Load();
            Persons = p != null ?
                new ObservableCollection<PersonEditViewModel>(p.Select(x => new PersonEditViewModel(x))) :
                Persons = new ObservableCollection<PersonEditViewModel>();
        }
        #endregion

        #region Commands
        public ReactiveCommand<object?, Unit> EditCommand { get; set; }
        public ReactiveCommand<object?, Unit> DeleteCommand { get; set; }
        public ReactiveCommand<object?, Unit> AddCommand { get; set; }

        private async Task<Unit> DeleteCommandExecuted(object? p)
        {
            Persons.Remove(Selected);
            return Unit.Default;
        }

        private async Task<Unit> AddCommandExecuted(object? p)
        {
            var person = new PersonEditViewModel(new Person());
            var result = await PersonEditDialog.Handle(person);
            if (result != null)
            {
                Persons.Add(person);
            }
            return Unit.Default;
        }
        private async Task<Unit> EditCommandExecuted(object? p)
        {
            var person = new PersonEditViewModel(new Person() { Age = Selected.Age ?? 0 , City = Selected.City, Name = Selected.Name, Surname = Selected.Surname});
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

        public bool Closing()
        {
            personsProvider?.Save(Persons.Select(x => x.Person));
            return true;
        }
        #endregion

        #region Dialogs
        public Interaction<PersonEditViewModel, PersonEditViewModel?> PersonEditDialog { get; }
        #endregion
    }
}
