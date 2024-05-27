using IntiLed.Persons.Core;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using ReactiveUI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

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

        #region Bindable Properties
        private string name = string.Empty;
        private string surname = string.Empty;
        private int age;
        private string city = string.Empty;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref  this.name, value);
        }
        public string Surname
        {
            get => surname;
            set => this.RaiseAndSetIfChanged(ref this.surname, value);
        }
        public int Age
        {
            get => age;
            set => this.RaiseAndSetIfChanged(ref this.age, value);
        }
        public string City
        {
            get => city;
            set => this.RaiseAndSetIfChanged(ref this.city, value);
        }
        #endregion

        #region .ctor
        public PersonEditViewModel(PersonEditViewModel person)
        {

            this.Name = person.Name;
            this.Age = person.Age;
            this.City = person.City;
            this.Surname = person.Surname;

            //this.person = person;

            OkCommand = ReactiveCommand.Create<Unit, PersonEditViewModel>((_) => 
            {
                return this; 
            });
            CancelCommand = ReactiveCommand.Create<Unit, PersonEditViewModel?>((_) => null);
        }
        public PersonEditViewModel(Person person)
        {

            this.Name = person.Name;
            this.Age = person.Age;
            this.City = person.City;
            this.Surname = person.Surname;

            //this.person = person;

            OkCommand = ReactiveCommand.Create<Unit, PersonEditViewModel>((_) =>
            {
                return this;
            });
            CancelCommand = ReactiveCommand.Create<Unit, PersonEditViewModel?>((_) => null);
        }
        #endregion
    }
}
