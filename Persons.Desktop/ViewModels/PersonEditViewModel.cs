using IntiLed.Persons.Core;

using ReactiveUI;

using ReactiveValidation;
using ReactiveValidation.Extensions;

using System.Reactive;


namespace Persons.Desktop.ViewModels
{
    public class PersonEditViewModel : ViewModelBase
    {
        #region Commands
        public ReactiveCommand<Unit, PersonEditViewModel> OkCommand { get; set; }
        public ReactiveCommand<Unit, PersonEditViewModel?> CancelCommand { get; set; }
        #endregion

        #region Model
        private Person person;
        public Person Person { get { return person; } }
        #endregion

        #region Validation
        private IObjectValidator GetValidator()
        {
            var builder = new ValidationBuilder<PersonEditViewModel>();

            builder.RuleFor(vm => vm.Name).Must(x => !x.Contains(" ")).WithMessage("Имя не может содержать пробелы.")
                .NotEmpty().WithMessage("Имя не может быть пустым.");

            builder.RuleFor(vm => vm.Surname).Must(x => !x.Contains(" ")).WithMessage("Фамилия не может содержать пробелы.")
                .NotEmpty().WithMessage("Фамилия не может быть пустой.");

            builder.RuleFor(vm => vm.Age).GreaterThan(0).LessThan(101).WithMessage("Возраст должен быть в диапазоне от 0 до 100.");

            return builder.Build(this);
        }
        #endregion

        #region Bindable Properties
        private string name = string.Empty;
        private string surname = string.Empty;
        private int age;
        private string city = string.Empty;
        public string Name
        {
            get => name;
            set => this.SetAndRaiseIfChanged(ref this.name, value);
        }
        public string Surname
        {
            get => surname;
            set => this.SetAndRaiseIfChanged(ref this.surname, value);
        }
        public int Age
        {
            get => age;
            set => this.SetAndRaiseIfChanged(ref this.age, value);
        }
        public string City
        {
            get => city;
            set => this.SetAndRaiseIfChanged(ref this.city, value);
        }
        #endregion

        #region .ctor
        public PersonEditViewModel(Person person)
        {

            this.Name = person.Name;
            this.Age = person.Age;
            this.City = person.City;
            this.Surname = person.Surname;
            this.person = person;

            Validator = GetValidator();         

            //this.person = person;

            OkCommand = ReactiveCommand.Create<Unit, PersonEditViewModel>((_) => this);
            CancelCommand = ReactiveCommand.Create<Unit, PersonEditViewModel?>((_) => null);
        }
        #endregion
    }
}
