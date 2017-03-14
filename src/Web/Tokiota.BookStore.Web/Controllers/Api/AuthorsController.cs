namespace Tokiota.BookStore.Web.Controllers.Api
{
    using Core;
    using Domains.Business.Core;
    using Domains.UOW;
    using Entities;
    using Mapster;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Net;

    [Route("api/[controller]")]
    public class AuthorsController : ApiCoreGuid<Author>
    {
        public AuthorsController(IBusinessCore<Author, Guid> business) : base (business)
        {

        }

    }
}
