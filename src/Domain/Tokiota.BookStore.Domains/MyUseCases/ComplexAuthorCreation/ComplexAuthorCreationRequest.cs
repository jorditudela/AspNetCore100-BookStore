
namespace Tokiota.BookStore.Domains.MyUseCases.ComplexAuthorCreation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UseCases.Core;
    public class ComplexAuthorCreationRequest : RequestUseCaseCore
    {
        public string Name { get; set; }
    }
}
