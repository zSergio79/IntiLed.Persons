using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntiLed.Persons.Core.Interfaces
{
    public interface IPersonsProvider
    {
        IEnumerable<Person>? Load();
        Task<IEnumerable<Person>?> LoadAsync();
        void Save(IEnumerable<Person> persons);
        Task SaveAsync(IEnumerable<Person> persons);
    }
}
