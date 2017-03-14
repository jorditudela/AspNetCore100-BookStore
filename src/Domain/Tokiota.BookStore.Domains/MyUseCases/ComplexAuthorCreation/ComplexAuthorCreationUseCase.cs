
namespace Tokiota.BookStore.Domains.MyUseCases.ComplexAuthorCreation
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UseCases.Core;
    using XCutting;

    public class ComplexAuthorCreationUseCase : UseCaseCore<ComplexAuthorCreationRequest, ComplexAuthorCreationResponse>
    {
        public ComplexAuthorCreationUseCase(ILibraryUoW uow) : base(uow)
        {
        }
        public override async Task<ComplexAuthorCreationResponse> Handle(ComplexAuthorCreationRequest request)
        {
            var response = new ComplexAuthorCreationResponse();
            if (requestIsValid(request))
            {
                var author = MapToServer(request);
                await UoW.AuthorRepository.Create(author);
                await UoW.SaveChanges();
                response.Response = Enums.Responses.eResponseType.Ok;
            }
            else
            {
                response.Response = Enums.Responses.eResponseType.CreationError;
            }
            return response;
        }

        private Author MapToServer(ComplexAuthorCreationRequest request)
        {
            return new Author()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
        } 

        private bool requestIsValid(ComplexAuthorCreationRequest request)
        {
            return request != null && !string.IsNullOrWhiteSpace(request.Name);
        }
    }
}
