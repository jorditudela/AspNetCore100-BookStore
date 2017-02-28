
namespace Tokiota.BookStore.Web
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public static class Register
    {
        public static void Configure(IServiceCollection services, IConfigurationRoot configuration)
        {

            Domains.Register.Configure(services, configuration);
        }
    }
}
