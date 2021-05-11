using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Entities.Models;
using WebApi.Models;
using WebApi.Services.Group;
using static WebApi.Models.GroupsModel;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MsGroupController : ControllerBase
    {
        private readonly IMsGroupService _msGroupService;
        public MsGroupController(IMsGroupService groupService)
        {
            _msGroupService = groupService;
        }

        [HttpPost("Inquiry")]
        public ResponseModels<MsGroup> Inquiry(GroupReqModel req)
        {
            var result = new ResponseModels<MsGroup>();
            try
            {
                result = _msGroupService.Inquiry(req);
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
        public ResponseModel<MsGroup> Create(MsGroupDto msGroup)
        {
            var result = new ResponseModel<MsGroup>();
            try
            {
                result = _msGroupService.Create(msGroup);
                return result;
            }
            catch
            {
                return new ResponseModel<MsGroup>
                {
                    Message = result.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost("Update")]
        public ResponseModel Update(MsGroup msGroups)
        {
            var result = new ResponseModel();
            try
            {
                result = _msGroupService.Update(msGroups);
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
        public ResponseModel Delete(MsGroup msGroups)
        {
            var result = new ResponseModel();
            try
            {
                result = _msGroupService.Delete(msGroups);
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

        [HttpPost("GetAll")]
        public ResponseModel GetAll(MsQuestion msQuestion)
        {
            var result = new ResponseModel();
            try
            {
                result = _msGroupService.GetAll();
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
