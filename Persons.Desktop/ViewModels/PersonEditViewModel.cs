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
        public string Name
        {
            get => Person?.Name;
            set
            {
                if (Person != null)
                {
                    if (Person.Name != value)
                    {
                        Person.Name = value;
                        OnPropertyChanged();
                    }
                }

            }
                //this.SetAndRaiseIfChanged(ref this.name, value);
        }
        public string Surname
        {
            get => Person?.Surname;
            set
            {
                if (Person != null)
                {
                    if (Person.Surname != value)
                    {
                        Person.Surname = value;
                        OnPropertyChanged();
                    }
                }
            }

        }
        public int? Age
        {
            get => Person.Age;
            set 
            {
                if (Person != null)
                {
                    if(Person.Age != value)
                    {
                        Person.Age = value ?? 0;
                        OnPropertyChanged();
                    }
                }
            }
        }
        public string City
        {
            get => Person?.City;
            set 
            {
                if (Person != null)
                {
                    if(Person.City != value)
                    {
                        Person.City = value;
                        OnPropertyChanged();
                    }
                }
            }
        }
        #endregion

        #region .ctor
        public PersonEditViewModel(Person person)
        {
            this.person = person;

            Validator = GetValidator();

            var isOkEnabled = this.WhenAnyValue<PersonEditViewModel, bool>(
                x => x.Validator.IsValid);

            //this.person = person;

            OkCommand = ReactiveCommand.Create<Unit, PersonEditViewModel>((_) => this, isOkEnabled);
            CancelCommand = ReactiveCommand.Create<Unit, PersonEditViewModel?>((_) => null);
            
        }
        #endregion
    }
}
