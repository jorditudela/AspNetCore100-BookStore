
namespace Tokiota.BookStore.Web.Controllers.Api.Core
{
    using Domains.Business.Core;
    using Entities.Core;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public abstract class ApiCoreGuid<TEntity> : Controller where TEntity : EntityCore<Guid>
    {
        private readonly IBusinessCore<TEntity, Guid> Business;

        public ApiCoreGuid(IBusinessCore<TEntity, Guid> business) 
        {
            Business = business;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var entities = Business.Get();
            return Ok(entities);
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            if (!id.Equals(Guid.Empty))
            {
                var entity = Business.Get(id);
                if(entity != null)
                {
                    return Ok(entity);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post(TEntity entity)
        {
            if (entity != null)
            {
                Business.Add(entity);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TEntity entity)
        {
            if (id != Guid.Empty && entity != null)
            {
                Business.Update(id, entity);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                Business.Remove(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
