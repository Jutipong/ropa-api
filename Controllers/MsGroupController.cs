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
        private readonly IMsGroupService _groupService;

        public MsGroupController(IMsGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("Inquiry")]
        public ResponseModels<MsGroup> Inquiry(GroupReqModel req)
        {
            var result = new ResponseModels<MsGroup>();
            try
            {
                result = _groupService.Inquiry(req);
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
                result = _groupService.Create(msGroup);
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
        public ResponseModel Delete(MsGroup msGroups)
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
