using IntiLed.Persons.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntiLed.Persons.Core.PersonsProviders
{
    public class FilePersonsProvider : IPersonsProvider
    {
        private const string exceptionMessage = "Не указано имя файла.";
        #region Filename
        public string Filename { get; set; } = string.Empty;
        #endregion
        public IEnumerable<Person>? Load()
        {
            if (string.IsNullOrEmpty(Filename))
                throw new Exception(exceptionMessage);
            try
            {
                if (File.Exists(Filename))
                    using (FileStream fs = File.OpenRead(Filename))
                    {
                        IEnumerable<Person>? persons = JsonSerializer.Deserialize<IEnumerable<Person>>(fs);
                        //await Task.Delay(5000);
                        return persons;
                    }
                return null;
            }
            catch
            {
                throw;
            }
        }
            

        public async Task<IEnumerable<Person>?> LoadAsync()
        {
            if (string.IsNullOrEmpty(Filename))
                throw new Exception(exceptionMessage);
            try
            {
                if (File.Exists(Filename))
                    using (FileStream fs = File.OpenRead(Filename))
                    {
                        IEnumerable<Person>? persons = await JsonSerializer.DeserializeAsync<IEnumerable<Person>>(fs);
                        //await Task.Delay(5000);
                        return persons;
                    }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public void Save(IEnumerable<Person> persons)
            => SaveAsync(persons).GetAwaiter().GetResult();

        public async Task SaveAsync(IEnumerable<Person> persons)
        {
            if (string.IsNullOrEmpty(Filename))
                throw new Exception(exceptionMessage);
            try
            {
                using (FileStream fs = File.Open(Filename, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fs, persons);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
