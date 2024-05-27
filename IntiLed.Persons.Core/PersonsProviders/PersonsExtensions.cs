using IntiLed.Persons.Core.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntiLed.Persons.Core.PersonsProviders
{
    public static class PersonsExtensions
    {
        public static void AddFilePersonsProvider(this IServiceCollection services, string filename)
        {
            services.AddSingleton<IPersonsProvider>(x => new FilePersonsProvider() { Filename = filename });
        }
    }
}
