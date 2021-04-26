using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Entities.Models;
using WebApi.Models;
using WebApi.Services.Group;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MsGroupController : ControllerBase
    {
        private readonly IMsGroupService _groupService;

        public MsGroupController(IMsGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("Inquiry")]
        public ResponseModels<MsGroup> Inquiry(List<MsGroup> msGroups)
        {
            var result = new ResponseModels<MsGroup>();
            try
            {
                result = _groupService.Inquiry(msGroups);
                return result;
            }
            catch
            {
                return new ResponseModels<MsGroup>
                {
                    Message = result.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost("Create")]
        public ResponseModels<MsGroup> Create(List<MsGroup> msGroups)
        {
            var result = new ResponseModels<MsGroup>();
            try
            {
                result = _groupService.Create(msGroups);
                return result;
            }
            catch
            {
                return new ResponseModels<MsGroup>
                {
                    Message = result.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost("Update")]
        public ResponseModel Update(List<MsGroup> msGroups)
        {
            var result = new ResponseModel();
            try
            {
                result = _groupService.Update(msGroups);
                return result;
            }
            catch
            {
                return new ResponseModel
                {
                    Message = result.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost("Delete")]
        public ResponseModel Delete(List<MsGroup> msGroups)
        {
            var result = new ResponseModel();
            try
            {
                result = _groupService.Delete(msGroups);
                return result;
            }
            catch
            {
                return new ResponseModel
                {
                    Message = result.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
