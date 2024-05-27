using IntiLed.Persons.Core;
using IntiLed.Persons.Core.Interfaces;
using IntiLed.Persons.Core.PersonsProviders;

using Microsoft.Extensions.DependencyInjection;

namespace ConsolePersons
{
    internal class Program
    {
        private const string filename = "persons.json";
        private static IEnumerable<Person> persons;
        //=
        //        [
        //        new Person() {Name = "1", Surname ="1", Age = 1, City = "1"},
        //        new Person() {Name = "2", Surname ="2", Age = 2, City = "2"},
        //        new Person() {Name = "3", Surname ="3", Age = 3, City = "3"},
        //        new Person() {Name = "4", Surname ="4", Age = 4, City = "4"},
        //        ];
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddFilePersonsProvider(filename);

            IServiceProvider servicesProvider = services.BuildServiceProvider();

            var personService = servicesProvider.GetRequiredService<IPersonsProvider>();

            Console.WriteLine("Load....");
            persons = personService.LoadAsync().GetAwaiter().GetResult();
            //persons = personService.Load();
            if (persons == null)
            {
                Console.WriteLine("No persons data...");
            }
            else
            {
                foreach (var p in persons)
                {
                    Console.WriteLine(p);
                }
            }

            if (persons != null)
            {
                Console.WriteLine("Save...");
                personService.SaveAsync(persons).GetAwaiter().GetResult();
            }
            Console.WriteLine("any key to exit...");
            Console.ReadKey();
        }
    }
}
