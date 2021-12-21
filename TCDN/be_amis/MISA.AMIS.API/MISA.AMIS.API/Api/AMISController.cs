using AMIS.Core.Entities;
using AMIS.Core.Interfaces.Repository;
using AMIS.Core.Interfaces.Services;
using AMIS.Core.Sevices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AMIS.Api.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AMISControlller<TEntity> : ControllerBase
    {
        IBaseRepository<TEntity> _baseRepository;
        IBaseService<TEntity> _baseService;
        public AMISControlller(IBaseService<TEntity> baseService, IBaseRepository<TEntity> baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
        }
        [HttpGet]
        public virtual IActionResult Get()
        {
            try
            {
                var entities = _baseRepository.GetAll();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return HandleException(ex.InnerException, "001");
            }
        }

        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            try
            {
                var entity = _baseRepository.GetById(entityId);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpPost]
        public IActionResult Post(TEntity entity)
        {
            try
            {
                var res = _baseService.Insert(entity);
                if (res.Success == true)
                {
                    return StatusCode(201, res.data);
                }
                else
                {
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                var res = _baseRepository.Delete(entityId);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpPut("{entityId}")]
        public IActionResult Put(Guid entityId, [FromBody] TEntity entity)
        {
            try
            {
                var res = _baseService.Update(entity, entityId);
                if (res.Success == true)
                {
                    return StatusCode(201, res.data);
                }
                else
                {
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        private ObjectResult HandleException(Exception ex, string erroCode = null)
        {
            var serviceResult = new ServiceResult();
            serviceResult.Success = false;
            serviceResult.devMsg = ex.Message;
            serviceResult.userMsg = Core.Resource.ErrorContactMisa;
            serviceResult.errorCode = MISACode.ErrorCodeException;

            return StatusCode(500, serviceResult);
        }
    }
}
